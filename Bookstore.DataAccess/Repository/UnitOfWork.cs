using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookstore.DataAccess.Data;
using Bookstore.DataAccess.Repository.IRepository;

namespace Bookstore.DataAccess.Repository;

public class UnitOfWork : IUnitOfWork
{
    private ApplicationDbContext _context;

    public ICompanyRepository Company { get; set; }
    public IProductRepository Product {  get; private set; }
    public ICategoryRepository Category { get; private set; }

    public IShoppingCartRepository ShoppingCart { get; private set; }

    public IApplicationUserRepository ApplicationUser { get; private set; }

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        Category = new CategoryRepository(_context);
        Product = new ProductRepository(_context);
        Company = new CompanyRepository(_context);
        ShoppingCart = new ShoppingCartRepository(_context);
        ApplicationUser = new ApplicationUserRepository(_context);
    }
    public void Save()
    {
        _context.SaveChanges();
    }
}
