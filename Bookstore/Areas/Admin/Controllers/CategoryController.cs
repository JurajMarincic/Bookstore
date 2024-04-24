using Bookstore.DataAccess.Data;
using Bookstore.DataAccess.Repository;
using Bookstore.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Bookstore.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Bookstore.Utilities;

namespace Bookstore.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = Role.RoleAdmin)]
public class CategoryController : Controller
{
    //private readonly ApplicationDbContext _context;
    //private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    //public CategoryController(ApplicationDbContext context, ICategoryRepository categoryRepository)
    //{
    //    _context = context;
    //    _categoryRepository = categoryRepository;
    //}

    public CategoryController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [AllowAnonymous]
    public IActionResult Index()
    {
        List<Category> categoryList = _unitOfWork.Category.GetAll().ToList();
        return View(categoryList);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]

    public IActionResult Create(Category category)
    {
        if (category.Name == category.DisplayOrder.ToString())
        {
            ModelState.AddModelError("Name", "The name must not be longer than 10 characters.");
        }

        if (category.Name.Length > 10)
        {
            ModelState.AddModelError("Name", "The name must not be longer than 10 characters.");
        }

        if (ModelState.IsValid)
        {
            _unitOfWork.Category.Add(category);
            _unitOfWork.Save();
            TempData["success"] = "Category created successfully";
            return RedirectToAction("Index", "Category");
        }

        return View();
    }


    public IActionResult Edit(int? categoryId)
    {
        if (categoryId == null && categoryId == 0)
        {
            return NotFound();
        }
        Category? category = _unitOfWork.Category.Get(c => c.Id == categoryId);

        if (category == null)
        {
            return NotFound();
        }
        return View(category);
    }

    [HttpPost]
    public IActionResult Edit(Category category)
    {

        if (ModelState.IsValid)
        {

            _unitOfWork.Category.Update(category);
            _unitOfWork.Save();  //potrebno da je se spremi na bazu
            TempData["success"] = "Category edited successfully";
            return RedirectToAction("Index", "Category");
        }

        return View();
    }
    public IActionResult Delete(int? categoryId)
    {
        if (categoryId == null && categoryId == 0)
        {
            return NotFound();
        }
        Category? category = _unitOfWork.Category.Get(c => c.Id == categoryId);

        if (category == null)
        {
            return NotFound();
        }
        return View(category);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeletePOST(int? categoryId)
    {
        Category? category = _unitOfWork.Category.Get(c => c.Id == categoryId);

        if (category == null)
        {
            return NotFound();
        }
        _unitOfWork.Category.Remove(category);
        _unitOfWork.Save();
        TempData["success"] = "Category deleted successfully";

        return RedirectToAction("Index", "Category");
    }
}
