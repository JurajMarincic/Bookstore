using Bookstore.Models.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Models.ViewModels
{
    public class ShoppingCartViewModel
    {
        [ValidateNever]
        public IEnumerable<ShoppingCart> ShoppingCartList { get; set; }
        public double OrderTotal { get; set; }

        public ShoppingCartViewModel(List<ShoppingCart> shoppingCarts)
        {
            double orderTotal = 0;
            foreach (var cart in shoppingCarts)
            {
                if (cart.Count > 100)
                {
                    orderTotal += cart.Product.Price100 * cart.Count;
                }
                else if (cart.Count > 50)
                {
                    orderTotal += cart.Product.Price50 * cart.Count;
                }
                else
                {
                    orderTotal += cart.Product.Price * cart.Count;
                }
            }
            OrderTotal = orderTotal;
            ShoppingCartList = shoppingCarts;
        }
    }
}
