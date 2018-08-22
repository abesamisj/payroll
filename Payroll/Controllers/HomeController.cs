using Payroll.Code;
using Payroll.Data;
using Payroll.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public ActionResult AssignDepartment(int id)
        {
            DepartmentBL departmentBL = new DepartmentBL();
            AssignDepartmentViewModel assignDepartmentViewModel = new AssignDepartmentViewModel();
            assignDepartmentViewModel.Departments = new SelectList(departmentBL.GetActiveDepartments(), "DepartmentId", "DepartmentName", 1);
            assignDepartmentViewModel.UserPersonalInformationId = id;

            UserPersonalInformation fromDB = employeeBL.GetEmployeesById(id);
            if (fromDB != null)
            {
                if (fromDB.DepartmentId > 0)
                {
                    assignDepartmentViewModel.SelectedDepartment = departmentBL.GetDepartmentById(fromDB.DepartmentId).DepartmentName;
                }
                else
                {
                    assignDepartmentViewModel.SelectedDepartment = "None Selected";
                }
            }

            return View(assignDepartmentViewModel);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult AssignDepartment(AssignDepartmentViewModel viewModel)
        {
            DepartmentBL departmentBL = new DepartmentBL();
            AssignDepartmentViewModel assignDepartmentViewModel = new AssignDepartmentViewModel();
            assignDepartmentViewModel.Departments = new SelectList(departmentBL.GetActiveDepartments(), "DepartmentId", "DepartmentName", 1);
            assignDepartmentViewModel.UserPersonalInformationId = viewModel.UserPersonalInformationId;

            UserPersonalInformation fromDB = employeeBL.GetEmployeesById(viewModel.UserPersonalInformationId);

            if (ModelState.IsValid)
            {
                if (viewModel.DepartmentId > 0)
                {
                    employeeBL.UpdateEmployeeDepartment(viewModel.UserPersonalInformationId, viewModel.DepartmentId);
                }
                else
                {

                   
                    ModelState.AddModelError("", "Please Select a department.");
                    return View(assignDepartmentViewModel);
                }
                
            }
            else
            {
                ModelState.AddModelError("", "Please Select a department.");
                return View(assignDepartmentViewModel);
            }
            return RedirectToAction("Employees", "Home");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult AssignIncome(int id)
        {
            IncomeBL incomeBL = new IncomeBL();
            Income income = new Income();
            AssingedEmployeeIncomeBL assingedEmployeeIncomeBL = new AssingedEmployeeIncomeBL();


            List<AssignedEmployeeIncomesViewModel> assignEmployeeIncomeViewModelList = new List<AssignedEmployeeIncomesViewModel>();
            AssignedEmployeeIncomesViewModel assignEmployeeIncomeViewModel = new AssignedEmployeeIncomesViewModel();
         

            UserPersonalInformation employee = employeeBL.GetEmployeesById(id);
            string name = employee.FirstName + " " + employee.LastName;

            List<AssignedEmployeeIncome> fromDB = assingedEmployeeIncomeBL.GetAssignedEmployeeIncomes(id);

            if (fromDB != null)
            {
                foreach (var item in fromDB)
                {
                    assignEmployeeIncomeViewModel = new AssignedEmployeeIncomesViewModel();
                    assignEmployeeIncomeViewModel.Name = name;
                    assignEmployeeIncomeViewModel.AssignedEmployeeIncomeId = item.AssignedEmployeeIncomeID;
                    assignEmployeeIncomeViewModel.CustomIncomeAmount = item.CustomAmount;
                    assignEmployeeIncomeViewModel.IncomeAmount = item.IncomeAmount;
                    assignEmployeeIncomeViewModel.IncomeId = item.IncomeId;
                    income = incomeBL.GetIncomesById(item.IncomeId);
                    assignEmployeeIncomeViewModel.IncomeName = income.IncomeName;
                    assignEmployeeIncomeViewModel.UserPersonalInformationId = item.UserPersonalInformationID;

                    assignEmployeeIncomeViewModelList.Add(assignEmployeeIncomeViewModel);
                }

            }
            ViewBag.Name = name;
            ViewBag.Id = id;
            return View(assignEmployeeIncomeViewModelList);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult AddIncome(int id)
        {

            IncomeBL bL = new IncomeBL();
            AddIncomeViewModel viewModels = new AddIncomeViewModel();
            viewModels.Incomes = new SelectList(bL.GetActiveIncomes(), "IncomeId", "IncomeName", 1);
            viewModels.UserPersonalInformationId = id;


            return View(viewModels);
            
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult AddIncome(AddIncomeViewModel fromModel)
        {
            AssingedEmployeeIncomeBL assingedEmployeeIncomeBL = new AssingedEmployeeIncomeBL();
            IncomeBL bL = new IncomeBL();
            AddIncomeViewModel toModel = new AddIncomeViewModel();
            Income income = new Income();

            toModel.Incomes = new SelectList(bL.GetActiveIncomes(), "IncomeId", "IncomeName", 1);
            toModel.UserPersonalInformationId = fromModel.UserPersonalInformationId;
            toModel.IncomeId = fromModel.IncomeId;
            toModel.Name = fromModel.Name;
            toModel.SelectedCustomAmount = fromModel.SelectedCustomAmount;

            income = bL.GetIncomesById(fromModel.IncomeId);
            if (income != null)
            {
                toModel.SelectedIncomeAmount = income.IncomeValue;
                toModel.SelectedIncomeName = income.IncomeName;
            }
            else
            {
                toModel.SelectedIncomeAmount = fromModel.SelectedIncomeAmount;
                toModel.SelectedIncomeName = fromModel.SelectedIncomeName;
            }
            



            AssignedEmployeeIncome toDB = new AssignedEmployeeIncome();
            

            if (ModelState.IsValid)
            {
                if (fromModel.IncomeId > 0)
                {

                    if (assingedEmployeeIncomeBL.IsIncomeAssignedToEmployee(fromModel.UserPersonalInformationId, fromModel.IncomeId))
                    {
                        ModelState.AddModelError("", "Please Select another income.");
                        return View(toModel);
                    }
                    else
                    {
                        toDB.UserPersonalInformationID = toModel.UserPersonalInformationId;
                        toDB.CustomAmount = toModel.SelectedCustomAmount;
                        toDB.IncomeAmount = toModel.SelectedIncomeAmount;
                        toDB.IncomeId = toModel.IncomeId;

                        assingedEmployeeIncomeBL.AddEmployeeIncome(toDB);
                    }


                }
                else
                {
                    ModelState.AddModelError("", "Please Select an income.");
                    return View(toModel);
                }

            }
            else
            {
                ModelState.AddModelError("", "Please Select an income.");
                return View(toModel);
            }
            return RedirectToAction("AssignIncome", "Home", new { id = fromModel.UserPersonalInformationId });
        }


        [Authorize(Roles = "Admin")]
        public ActionResult EditIncome(int id, int incomeId)
        {
            AssingedEmployeeIncomeBL assingedEmployeeIncomeBL = new AssingedEmployeeIncomeBL();
            Income income = new Income();
            IncomeBL bL = new IncomeBL();
            AddIncomeViewModel viewModels = new AddIncomeViewModel();
            EmployeeBL employeeBL = new EmployeeBL();
            viewModels.UserPersonalInformationId = id;
            income = bL.GetIncomesById(incomeId);
            viewModels.SelectedCustomAmount = assingedEmployeeIncomeBL.GetAssignedEmployeeIncomesByIncomeId(id, incomeId);
            if (income != null)
            {
                viewModels.SelectedIncomeAmount = income.IncomeValue;
                viewModels.SelectedIncomeName = income.IncomeName;
            }

            ViewBag.Name = employeeBL.GetEmployeeById(id);
            return View(viewModels);
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult EditIncome(AddIncomeViewModel fromModel)
        {
            AssingedEmployeeIncomeBL assingedEmployeeIncomeBL = new AssingedEmployeeIncomeBL();
            IncomeBL bL = new IncomeBL();
            AddIncomeViewModel toModel = new AddIncomeViewModel();
            Income income = new Income();

            toModel.Incomes = new SelectList(bL.GetActiveIncomes(), "IncomeId", "IncomeName", 1);
            toModel.UserPersonalInformationId = fromModel.UserPersonalInformationId;
            toModel.IncomeId = fromModel.IncomeId;
            toModel.Name = fromModel.Name;
            toModel.SelectedCustomAmount = fromModel.SelectedCustomAmount;

            income = bL.GetIncomesById(fromModel.IncomeId);
            if (income != null)
            {
                toModel.SelectedIncomeAmount = income.IncomeValue;
                toModel.SelectedIncomeName = income.IncomeName;
            }
            else
            {
                toModel.SelectedIncomeAmount = fromModel.SelectedIncomeAmount;
                toModel.SelectedIncomeName = fromModel.SelectedIncomeName;
            }




            AssignedEmployeeIncome toDB = new AssignedEmployeeIncome();


            if (ModelState.IsValid)
            {
                if (fromModel.IncomeId > 0)
                {

                    toDB.UserPersonalInformationID = toModel.UserPersonalInformationId;
                    toDB.CustomAmount = toModel.SelectedCustomAmount;
                    toDB.IncomeAmount = toModel.SelectedIncomeAmount;
                    toDB.IncomeId = toModel.IncomeId;

                    assingedEmployeeIncomeBL.UpdateEmployeeIncome(toDB);


                }
                else
                {
                    ModelState.AddModelError("", "Please Select an income.");
                    return View(toModel);
                }

            }
            else
            {
                ModelState.AddModelError("", "Please Select an income.");
                return View(toModel);
            }
            return RedirectToAction("AssignIncome", "Home", new { id = fromModel.UserPersonalInformationId });
        }

        [Authorize(Roles = "Admin")]
        public ActionResult DeleteIncome(int id, int incomeId)
        {
            AssingedEmployeeIncomeBL bl = new AssingedEmployeeIncomeBL();
            bl.DeleteIncome(id, incomeId);
            return RedirectToAction("AssignIncome", "Home", new { id = id });
        }



        [Authorize(Roles = "Admin")]
        public ActionResult AssignDeduction(int id)
        {
            DeductionBL deductionBL = new DeductionBL();
            Deduction deduction = new Deduction();
            AssignedEmployeeDeductionBL assingedEmployeeDeductionBL = new AssignedEmployeeDeductionBL();


            List<AssignedEmployeeDeductionsViewModel> assignEmployeeDeductionsViewModelList = new List<AssignedEmployeeDeductionsViewModel>();
            AssignedEmployeeDeductionsViewModel assignEmployeeDeductionViewModel = new AssignedEmployeeDeductionsViewModel();


            UserPersonalInformation employee = employeeBL.GetEmployeesById(id);
            string name = employee.FirstName + " " + employee.LastName;

            List<AssignedEmployeeDeduction> fromDB = assingedEmployeeDeductionBL.GetAssignedEmployeeDeductions(id);

            if (fromDB != null)
            {
                foreach (var item in fromDB)
                {
                    assignEmployeeDeductionViewModel = new AssignedEmployeeDeductionsViewModel();
                    assignEmployeeDeductionViewModel.Name = name;
                    assignEmployeeDeductionViewModel.AssignedEmployeeDeductionsId = item.AssignedEmployeeDeductionID;
                    assignEmployeeDeductionViewModel.CustomIncomeAmount = item.CustomAmount;
                    assignEmployeeDeductionViewModel.DeductionAmount = item.DeductionAmount;
                    assignEmployeeDeductionViewModel.DeductionId = item.DeductionId;
                    deduction = deductionBL.GetDeductionById(item.DeductionId);
                    assignEmployeeDeductionViewModel.DeductionName = deduction.DeductionName;
                    assignEmployeeDeductionViewModel.UserPersonalInformationId = item.UserPersonalInformationID;

                    assignEmployeeDeductionsViewModelList.Add(assignEmployeeDeductionViewModel);
                }

            }
            ViewBag.Name = name;
            ViewBag.Id = id;
            return View(assignEmployeeDeductionsViewModelList);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult AddDeduction(int id)
        {

            DeductionBL bL = new DeductionBL();
            AddDeductionViewModel viewModels = new AddDeductionViewModel();
            viewModels.Deductions = new SelectList(bL.GetActiveDeductions(), "DeductionId", "DeductionName", 1);
            viewModels.UserPersonalInformationId = id;


            return View(viewModels);

        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult AddDeduction(AddDeductionViewModel fromModel)
        {
            AssignedEmployeeDeductionBL assingedEmployeeBL = new AssignedEmployeeDeductionBL();
            DeductionBL bL = new DeductionBL();
            AddDeductionViewModel toModel = new AddDeductionViewModel();
            Deduction deduction = new Deduction();

            toModel.Deductions = new SelectList(bL.GetActiveDeductions(), "DeductionId", "DeductionName", 1);
            toModel.UserPersonalInformationId = fromModel.UserPersonalInformationId;
            toModel.DeductionId = fromModel.DeductionId;
            toModel.Name = fromModel.Name;
            toModel.SelectedCustomAmount = fromModel.SelectedCustomAmount;

            deduction = bL.GetDeductionById(fromModel.DeductionId);
            if (deduction != null)
            {
                toModel.SelectedDeductionAmount = deduction.DeductionValue;
                toModel.SelectedDeductionName = deduction.DeductionName;
            }
            else
            {
                toModel.SelectedCustomAmount = fromModel.SelectedDeductionAmount;
                toModel.SelectedDeductionName = fromModel.SelectedDeductionName;
            }




            AssignedEmployeeDeduction toDB = new AssignedEmployeeDeduction();


            if (ModelState.IsValid)
            {
                if (fromModel.DeductionId > 0)
                {

                    if (assingedEmployeeBL.IsDeductionAssignedToEmployee(fromModel.UserPersonalInformationId, fromModel.DeductionId))
                    {
                        ModelState.AddModelError("", "Please Select another deduction.");
                        return View(toModel);
                    }
                    else
                    {
                        toDB.UserPersonalInformationID = toModel.UserPersonalInformationId;
                        toDB.CustomAmount = toModel.SelectedCustomAmount;
                        toDB.DeductionAmount = toModel.SelectedDeductionAmount;
                        toDB.DeductionId = toModel.DeductionId;

                        assingedEmployeeBL.AddEmployeeDeductions(toDB);
                    }


                }
                else
                {
                    ModelState.AddModelError("", "Please Select an deduction.");
                    return View(toModel);
                }

            }
            else
            {
                ModelState.AddModelError("", "Please Select an deduction.");
                return View(toModel);
            }
            return RedirectToAction("AssignDeduction", "Home", new { id = fromModel.UserPersonalInformationId });
        }


        [Authorize(Roles = "Admin")]
        public ActionResult EditDeduction(int id, int deductionId)
        {
            AssignedEmployeeDeductionBL assingedEmployeeBL = new AssignedEmployeeDeductionBL();
            Deduction deduction = new Deduction();
            DeductionBL bL = new DeductionBL();
            AddDeductionViewModel viewModels = new AddDeductionViewModel();
            EmployeeBL employeeBL = new EmployeeBL();
            viewModels.UserPersonalInformationId = id;
            deduction = bL.GetDeductionById(deductionId);
            viewModels.SelectedCustomAmount = assingedEmployeeBL.GetAssignedEmployeeDeductionsByDeductionId(id, deductionId);
            if (deduction != null)
            {
                viewModels.SelectedDeductionAmount = deduction.DeductionValue;
                viewModels.SelectedDeductionName = deduction.DeductionName;
            }

            ViewBag.Name = employeeBL.GetEmployeeById(id);
            return View(viewModels);
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult EditDeduction(AddDeductionViewModel fromModel)
        {
            AssignedEmployeeDeductionBL assingedEmployeeBL = new AssignedEmployeeDeductionBL();
            DeductionBL bL = new DeductionBL();
            AddDeductionViewModel toModel = new AddDeductionViewModel();
            Deduction deduction = new Deduction();

            toModel.Deductions = new SelectList(bL.GetActiveDeductions(), "DeductionId", "DeductionName", 1);
            toModel.UserPersonalInformationId = fromModel.UserPersonalInformationId;
            toModel.DeductionId = fromModel.DeductionId;
            toModel.Name = fromModel.Name;
            toModel.SelectedCustomAmount = fromModel.SelectedCustomAmount;

            deduction = bL.GetDeductionById(fromModel.DeductionId);
            if (deduction != null)
            {
                toModel.SelectedDeductionAmount = deduction.DeductionValue;
                toModel.SelectedDeductionName = deduction.DeductionName;
            }
            else
            {
                toModel.SelectedDeductionAmount = fromModel.SelectedDeductionAmount;
                toModel.SelectedDeductionName = fromModel.SelectedDeductionName;
            }




            AssignedEmployeeDeduction toDB = new AssignedEmployeeDeduction();


            if (ModelState.IsValid)
            {
                if (fromModel.DeductionId > 0)
                {

                    toDB.UserPersonalInformationID = toModel.UserPersonalInformationId;
                    toDB.CustomAmount = toModel.SelectedCustomAmount;
                    toDB.DeductionAmount = toModel.SelectedDeductionAmount;
                    toDB.DeductionId = toModel.DeductionId;

                    assingedEmployeeBL.UpdateEmployeeDeduction(toDB);


                }
                else
                {
                    ModelState.AddModelError("", "Please Select an Deduction.");
                    return View(toModel);
                }

            }
            else
            {
                ModelState.AddModelError("", "Please Select an Deduction.");
                return View(toModel);
            }
            return RedirectToAction("AssignDeduction", "Home", new { id = fromModel.UserPersonalInformationId });
        }
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteDeduction(int id, int deductionId)
        {
            AssignedEmployeeDeductionBL bl = new AssignedEmployeeDeductionBL();
            bl.DeleteDeduction(id, deductionId);
            return RedirectToAction("AssignDeduction", "Home", new { id = id });
        }
    }
}