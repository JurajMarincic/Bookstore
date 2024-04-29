using Bookstore.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Bookstore.DataAccess.Data;
using Bookstore.Models.Models;
using Bookstore.Models.ViewModels;
using Bookstore.Utilities;
using Microsoft.AspNetCore.Authorization;

namespace Bookstore.Areas.Admin.Controllers;

[Area("Admin")]

public class CompanyController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public CompanyController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public IActionResult Index()
    {
        List<Company> companyList = _unitOfWork.Company.GetAll().ToList();
        return View(companyList);
    }

    public IActionResult Upsert(int? id)
    {
        Company? company = new Company();

        if (id == null || id == 0)
        {
            // Create
            return View(company);
        }
        else
        {
            // Update
            company = _unitOfWork.Company.Get(p => p.Id == id);
            return View(company);
        }
    }

    [HttpPost]
    public IActionResult Upsert(Company company)
    {
        if (ModelState.IsValid)
        {
            if (company.Id == 0)
            {
                _unitOfWork.Company.Add(company);
            }
            else
            {
                _unitOfWork.Company.Update(company);
            }

            _unitOfWork.Save();
            return RedirectToAction("Index", "Company");
        }

        return View(company);
    }

    #region API Calls
    [HttpGet]
    public IActionResult GetAll()
    {
        List<Company> companyList = _unitOfWork.Company.GetAll().ToList();
        return Json(new { data = companyList });
    }

    [HttpDelete]
    public IActionResult Delete(int? id)
    {
        var company = _unitOfWork.Company.Get(p => p.Id == id);

        if (company == null)
        {
            return Json(new { success = false, message = "Errow while deleting" });
        }

        _unitOfWork.Company.Remove(company);
        _unitOfWork.Save();

        return Json(new { success = true, message = "Delete successful" });
    }
    #endregion
}