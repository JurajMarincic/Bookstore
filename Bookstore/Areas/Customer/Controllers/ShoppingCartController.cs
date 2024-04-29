using Bookstore.DataAccess.Repository;
using Bookstore.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Bookstore.Models.ViewModels;
using System.Security.Claims;

namespace Bookstore.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class ShoppingCartController : Controller
    {
        public ShoppingCartViewModel ShoppingCartViewModel { get; set; }
        private readonly IUnitOfWork _unitOfWork;
        public ShoppingCartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            
            return View();
        }
    }
}
