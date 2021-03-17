using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MvcExercise.Models;
using MvcExercise.Models.Repositories;
using MvcExercise.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcExercise.Controllers 
{
    public class EmployeeController : Controller
    {
        private readonly ICompanyRepository<Employee> _icompanyrepository;
        public EmployeeController(ICompanyRepository<Employee> icompanyrepository)
        {
            _icompanyrepository = icompanyrepository;
        }
        [AllowAnonymous]
        public ViewResult Index()
        {
            //viewdata to pass data!!
            //ViewData["key1"]="imad el idrissi";
            //LIST VIEW  <==================================================================LIST VIEW

            IEnumerable<Employee> employees = _icompanyrepository.GetEntities();
            return View(employees);
        }
        public ViewResult Details(int id)
        {
            Employee employee = _icompanyrepository.Get(id);
            return View(employee);
        }
        public ViewResult more()
        {
            EmployeeMoreViewModel model = new EmployeeMoreViewModel()
            {
                Employee = _icompanyrepository.Get(1),
                title="hello to this page"

            };
            //ViewBag.title = "helloto this page";
            //Employee emp = _icompanyrepository.Get(2);

            return View(model);
        }
        
        [HttpGet]
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        public ActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {


                _icompanyrepository.Add(employee);
                return RedirectToAction("Details", new { id = employee.Id });
            } 
                return View();
            
            
        }
        [Authorize]
        public ActionResult Delete(int id)
        {
            _icompanyrepository.Delete(id);
            return RedirectToAction("Index");
        }
        [HttpGet]
        [Authorize]
        public ActionResult Update(int id)
        {
            Employee employee = _icompanyrepository.Get(id);
            return View(employee); 
        }
        [HttpPost]
        [Authorize]
        public ActionResult Update(Employee employee)
        {
            _icompanyrepository.Update(employee);
            return RedirectToAction("Details", new { id = employee.Id });
        }
    }
}
