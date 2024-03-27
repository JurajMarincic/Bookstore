using Bookstore.DataAccess.Data;
using Bookstore.DataAccess.Repository.IRepository;
using Bookstore.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Bookstore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bookstore.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Product> productList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
            return View(productList);
        }

        public IActionResult Upsert(int? id)
        {
            IEnumerable<SelectListItem> categoryList = _unitOfWork.Category.GetAll().Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });

            //ViewBag.CategoryList = categoryList;
            //ViewData["Category List"] = categoryList;

            ProductViewModel productViewModel = new ProductViewModel()
            {
                CategoryList = categoryList,
                Product = new Product()
            };

            if(id == null || id == 0)
            {
                //Create
                return View(productViewModel);
            }
            else
            {
                //Update
                productViewModel.Product = _unitOfWork.Product.Get(p => p.Id == id);
                return View(productViewModel);
            }
        }

        [HttpPost]
        public IActionResult Upsert(ProductViewModel productViewModel, IFormFile? file)
        {

            if (productViewModel.Product.Title.Length > 20)
            {
                ModelState.AddModelError("Product", "The name must not be longer than 10 characters.");
            }

            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;

                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\product");

                    if(!string.IsNullOrEmpty(productViewModel.Product.ImageUrl))
                    {
                        var oldImagepath = Path.Combine(wwwRootPath, productViewModel.Product.ImageUrl.Trim('\\'));

                        if(System.IO.File.Exists(oldImagepath))
                        {
                            System.IO.File.Delete(oldImagepath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    productViewModel.Product.ImageUrl = @"\images\product\" + fileName;
                }

                if(productViewModel.Product.Id == 0)
                {
                    _unitOfWork.Product.Add(productViewModel.Product);
                }
                else
                {
                    _unitOfWork.Product.Update(productViewModel.Product);
                }

                _unitOfWork.Save();
                //TempData["success"] = "New product added successfully";
                return RedirectToAction("Index", "Product");
            }
            else
            {
                productViewModel.CategoryList = _unitOfWork.Category.GetAll().Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                });
            }

            return View();
        }

        //public IActionResult Delete(int? productId)
        //{
        //    if (productId == null && productId == 0)
        //    {
        //        return NotFound();
        //    }
        //    Product? product = _unitOfWork.Product.Get(c => c.Id == productId);

        //    if (product == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(product);
        //}

        //[HttpPost, ActionName("Delete")]
        //public IActionResult DeletePOST(int? productId)
        //{
        //    Product? product = _unitOfWork.Product.Get(c => c.Id == productId);

        //    if (product == null)
        //    {
        //        return NotFound();
        //    }
        //    _unitOfWork.Product.Remove(product);
        //    _unitOfWork.Save();
        //    TempData["success"] = "Product deleted successfully";

        //    return RedirectToAction("Index", "Product");
        //}

        #region API Calls
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Product> productList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
            return Json(new { data = productList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var product = _unitOfWork.Product.Get(p => p.Id == id);
            if (product == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            var oldImagepath = Path.Combine(_webHostEnvironment.WebRootPath, product.ImageUrl.Trim('\\'));

            if (System.IO.File.Exists(oldImagepath))
            {
                System.IO.File.Delete(oldImagepath);
            }

            _unitOfWork.Product.Remove(product);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete successful"});


        }
        #endregion
    }
}
