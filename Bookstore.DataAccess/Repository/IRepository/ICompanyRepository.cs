using Bookstore.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookstore.Models.Models;


namespace Bookstore.DataAccess.Repository.IRepository;

public interface ICompanyRepository : IRepository<Company>
{
    void Update(Company company);
}