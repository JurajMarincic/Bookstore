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

    public IProductRepository Product {  get; private set; }
    public ICategoryRepository Category { get; private set; }

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        Category = new CategoryRepository(_context);
        Product = new ProductRepository(_context);
    }
    public void Save()
    {
        _context.SaveChanges();
    }
}
