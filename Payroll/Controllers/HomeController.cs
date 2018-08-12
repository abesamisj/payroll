using Payroll.Code;
using Payroll.Data;
using Payroll.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Payroll.Controllers
{
    public class HomeController : Controller
    {
        EmployeeBL employeeBL = new EmployeeBL();

        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult CreateEmployee()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult CreateEmployee(EmployeeListModel viewModel)
        {

            if (ModelState.IsValid)
            {
                if (employeeBL.CheckIfEmployeeIdExists(viewModel.EmployeeId))
                {
                    ModelState.AddModelError("", "Employee ID already exists.");
                    return View(viewModel);
                }
                else
                {
                    UserPersonalInformation toDB = new UserPersonalInformation();
                    toDB.Active = 1;
                    toDB.Address = viewModel.Address;
                    toDB.EmployeeId = viewModel.EmployeeId;
                    toDB.FirstName = viewModel.FirstName;
                    toDB.LastName = viewModel.LastName;
                    toDB.Position = viewModel.Position;
                    toDB.BasicPay = viewModel.BasicPay;
                    employeeBL.InsertEmployee(toDB);
                }
            }
            else
            {
                    ModelState.AddModelError("", "Error.");
                    return View(viewModel);
            }
            return RedirectToAction("Employees", "Home");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Employees()
        {
            List<EmployeeListModel> employeeListModels = new List<EmployeeListModel>();
            EmployeeListModel employee = new EmployeeListModel();

            List<UserPersonalInformation> listFromDB = employeeBL.GetAllEmployees();

            foreach (UserPersonalInformation item in listFromDB)
            {
                employee = new EmployeeListModel();
                employee.Active = Convert.ToBoolean(item.Active);
                employee.Address = item.Address;
                employee.EmployeeId = item.EmployeeId;
                employee.FirstName = item.FirstName;
                employee.LastName = item.LastName;
                employee.UserPersonalInformationID = item.UserPersonalInformationId;
                employee.Position = item.Position;
                employee.BasicPay = item.BasicPay;
                employeeListModels.Add(employee);
            }

            return View(employeeListModels);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult EditEmployee(int id)
        {
            EmployeeListModel viewModel = new EmployeeListModel();
            if (ModelState.IsValid)
            { 
                
                UserPersonalInformation fromDB = employeeBL.GetEmployeesById(id);

                bool active = false;

                if (fromDB.Active > 0)
                {
                    active = true;
                }

                if (fromDB != null)
                {
                    viewModel = new EmployeeListModel();
                    viewModel.Active = active;
                    viewModel.Address = fromDB.Address;
                    viewModel.EmployeeId = fromDB.EmployeeId;
                    viewModel.FirstName = fromDB.FirstName;
                    viewModel.LastName = fromDB.LastName;
                    viewModel.Position = fromDB.Position;
                    viewModel.BasicPay = fromDB.BasicPay;
                    viewModel.UserPersonalInformationID = fromDB.UserPersonalInformationId;
                }
            }
            else
            {
                ModelState.AddModelError("", "Error.");
                return View(viewModel);
            }
            return View(viewModel);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ViewEmployee(int id)
        {
            EmployeeListModel viewModel = new EmployeeListModel();
            UserPersonalInformation fromDB = employeeBL.GetEmployeesById(id);

            bool active = false;

            if (fromDB.Active > 0)
            {
                active = true;
            }

            if (fromDB != null)
            {
                viewModel = new EmployeeListModel();
                viewModel.Active = active;
                viewModel.Address = fromDB.Address;
                viewModel.EmployeeId = fromDB.EmployeeId;
                viewModel.FirstName = fromDB.FirstName;
                viewModel.LastName = fromDB.LastName;
                viewModel.Position = fromDB.Position;
                viewModel.BasicPay = fromDB.BasicPay;
                viewModel.UserPersonalInformationID = fromDB.UserPersonalInformationId;
            }
            return View(viewModel);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditEmployee(EmployeeListModel viewModel)
        {
            if (ModelState.IsValid)
            {

                int active = 0;
                if (viewModel.Active)
                {
                    active = 1;
                }
                UserPersonalInformation toDB = new UserPersonalInformation();
                toDB.Active = active;
                toDB.Address = viewModel.Address;
                toDB.EmployeeId = viewModel.EmployeeId;
                toDB.FirstName = viewModel.FirstName;
                toDB.LastName = viewModel.LastName;
                toDB.Position = viewModel.Position;
                toDB.BasicPay = viewModel.BasicPay;
                toDB.UserPersonalInformationId = viewModel.UserPersonalInformationID;

                employeeBL.UpdateEmployee(toDB);

            }
            else
            {
                ModelState.AddModelError("", "Error.");
                return View(viewModel);
            }
            return RedirectToAction("Employees", "Home");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}