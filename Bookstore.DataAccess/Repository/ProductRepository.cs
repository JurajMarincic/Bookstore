using Bookstore.DataAccess.Data;
using Bookstore.DataAccess.Repository.IRepository;
using Bookstore.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {

        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Product product)
        {
            // _context.Update(product);
            var productInDb = _context.Products.FirstOrDefault(p => p.Id == product.Id);
            if (productInDb != null)
            {
                productInDb.Title = product.Title;
                productInDb.Author = product.Author;
                productInDb.Description = product.Description;
                productInDb.CategoryId = product.CategoryId;
                productInDb.Price = product.Price;
                productInDb.ListPrice = product.ListPrice;
                productInDb.Price50 = product.Price50;
                productInDb.Price100 = product.Price100;

                if(productInDb.ImageUrl != null)
                {
                    productInDb.ImageUrl = product.ImageUrl;
                }
            }
        }
    }
}

