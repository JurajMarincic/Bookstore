﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookstore.Models.Models;

namespace Bookstore.DataAccess.Repository.IRepository;

public interface IUnitOfWork
{
    ICategoryRepository Category { get; }
    ICompanyRepository Company { get; }
    IProductRepository Product { get; }
    IShoppingCartRepository ShoppingCart { get; }
    void Save();
}
