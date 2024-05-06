using Bookstore.DataAccess.Repository;
using Bookstore.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Bookstore.Models.ViewModels;
using Bookstore.Models.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Bookstore.Areas.Customer.Controllers
{
    [Area("Customer")]
	public class ShoppingCartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ShoppingCartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<ShoppingCart> shoppingList = _unitOfWork.ShoppingCart.GetAll(includeProperties: "Product").ToList();
            ShoppingCartViewModel shoppingCartView = new ShoppingCartViewModel(shoppingList);
            return View(shoppingCartView);
        }

        public int GetShoppingCartItemCount()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            List<ShoppingCart> shoppingCarts = _unitOfWork.ShoppingCart.GetAll(includeProperties: "Product").ToList();
            int count = 0;
            foreach (var cart in shoppingCarts)
            {
                if (cart.ApplicationUserId == userId)
                {
                    count += cart.Count;
                }
            }
            return count;
        }



        public IActionResult Delete(int? shoppingCartId)
        {
            ShoppingCart? shoppingCart = _unitOfWork.ShoppingCart.Get(c => c.Id == shoppingCartId);

            if (shoppingCart == null)
            {
                return NotFound();
            }
            _unitOfWork.ShoppingCart.Remove(shoppingCart);
            _unitOfWork.Save();

            return RedirectToAction(nameof(Index));
        }
        public IActionResult AddOneItem(int? shoppingCartId)
        {
            ShoppingCart? shoppingCart = _unitOfWork.ShoppingCart.Get(c => c.Id == shoppingCartId);

            if (shoppingCart == null)
            {
                return NotFound();
            }
            shoppingCart.Count++;
            _unitOfWork.ShoppingCart.Update(shoppingCart);
            _unitOfWork.Save();

            return RedirectToAction(nameof(Index));
        }
        public IActionResult DeleteOneItem(int? shoppingCartId)
        {
            ShoppingCart? shoppingCart = _unitOfWork.ShoppingCart.Get(c => c.Id == shoppingCartId);

            if (shoppingCart == null)
            {
                return NotFound();
            }

            if (shoppingCart.Count == 1)
            {
                _unitOfWork.ShoppingCart.Remove(shoppingCart);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));

            }
            shoppingCart.Count--;
            _unitOfWork.ShoppingCart.Update(shoppingCart);
            _unitOfWork.Save();

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Summary()
        {
            List<ShoppingCart> shoppingList = _unitOfWork.ShoppingCart.GetAll(includeProperties: "Product").ToList();
            ShoppingCartViewModel shoppingCartView = new ShoppingCartViewModel(shoppingList);
            return View(shoppingCartView);
        }

    }
}
