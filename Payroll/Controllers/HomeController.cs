using Payroll.Code;
using Payroll.CodeHelper;
using Payroll.Data;
using Payroll.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Payroll.Controllers
{
    public class HomeController : Controller
    {
        HomeHelper homeHelper = new HomeHelper();
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
                    homeHelper.CreateEmployee(viewModel);
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
            EmployeeListPayrollPeriodViewModel employeeListPayrollPeriod = new EmployeeListPayrollPeriodViewModel();

            List<EmployeeListModel> employeeListModels = new List<EmployeeListModel>();
            EmployeeListModel employee = new EmployeeListModel();
            DepartmentBL department = new DepartmentBL();
            List<UserPersonalInformation> listFromDB = employeeBL.GetAllEmployees();


            PayrollPeriodBL payrollPeriodBL = new PayrollPeriodBL();
            List<PayrollPeriod> payrollPeriods = new List<PayrollPeriod>();
            payrollPeriods = payrollPeriodBL.GetPayrollPeriods();
            
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
                employee.Name = item.FirstName + " " + employee.LastName;
                if (item.DepartmentId > 0)
                {
                    employee.Department = department.GetDepartmentById(item.DepartmentId).DepartmentName;
                }
                employeeListModels.Add(employee);
            }
            employeeListPayrollPeriod.EmployeeList = employeeListModels;
            employeeListPayrollPeriod.PayrollPeriods = new SelectList(payrollPeriods, "PayrollPeriodId", "PayPeriodFrom", 2);
            return View(employeeListPayrollPeriod);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Payroll(FormCollection collection)
        {

            var payPeriodId = collection.GetValue("PayrollPeriodId").AttemptedValue;
            if (ModelState.IsValid)
            {
                if (payPeriodId.ToString().Length > 0)
                {
                    List<EmployeeListModel> employeeListModels = new List<EmployeeListModel>();
                    EmployeeListModel employee = new EmployeeListModel();
                    DepartmentBL department = new DepartmentBL();
                    List<UserPersonalInformation> listFromDB = employeeBL.GetAllEmployees();
                    AssingedEmployeeIncomeBL assingedEmployeeIncomeBL = new AssingedEmployeeIncomeBL();
                    List<AssignedEmployeeIncomesViewModel> assignEmployeeIncomeViewModelList = new List<AssignedEmployeeIncomesViewModel>();
                    AssignedEmployeeIncomesViewModel assignEmployeeIncomeViewModel = new AssignedEmployeeIncomesViewModel();
                    List<Income> incomeDBList = new List<Income>();
                    Income incomeDB = new Income();
                    IncomeBL incomeBL = new IncomeBL();

                    PayrollPeriodBL payrollPeriodBL = new PayrollPeriodBL();
                    PayrollPeriod payrollPeriodDB = new PayrollPeriod();

                    List<Deduction> deductionDBList = new List<Deduction>();
                    Deduction deductionDB = new Deduction();
                    DeductionBL deductionBL = new DeductionBL();

                    List<PayrollTransactionIncome> payrollTransactionIncomes = new List<PayrollTransactionIncome>();
                    PayrollTransactionIncome payrollTransactionIncome = new PayrollTransactionIncome();
                    PayrollTransactionIncomeBL payrollTransactionIncomeBL = new PayrollTransactionIncomeBL();

                    PayrollTransactionDeduction payrollTransactionDeduction = new PayrollTransactionDeduction();
                    PayrollTransactionDeductionBL payrollTransactionDeductionBL = new PayrollTransactionDeductionBL();

                    List<PayrollTransactionDeductionViewModels> payrollTransactionDeductionViewModels = new List<PayrollTransactionDeductionViewModels>();
                    PayrollTransactionDeductionViewModels payrollTransactionDeductionViewModel = new PayrollTransactionDeductionViewModels();

                    List<PayrollTransactionIncomeViewModels> payrollTransactionIncomeViewModels = new List<PayrollTransactionIncomeViewModels>();
                    PayrollTransactionIncomeViewModels payrollTransactionIncomeViewModel = new PayrollTransactionIncomeViewModels();

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
                        employee.Name = item.FirstName + " " + employee.LastName;


                        #region incomes
                        incomeDBList = incomeBL.GetActiveIncomes();
                        if (incomeDBList != null)
                        {
                            foreach (var dbIncome in incomeDBList)
                            {
                                payrollTransactionIncome = new PayrollTransactionIncome();
                                payrollTransactionIncome = payrollTransactionIncomeBL.GetPayrollTransactionIncome(item.UserPersonalInformationId, Convert.ToInt32(payPeriodId), dbIncome.IncomeId);

                                if (payrollTransactionIncome != null)
                                {
                                    payrollTransactionIncomeViewModel = new PayrollTransactionIncomeViewModels();
                                    payrollTransactionIncomeViewModel.PayrollTransactionIncomeId = payrollTransactionIncome.PayrollTransactionIncomeId;
                                    payrollTransactionIncomeViewModel.UserPersonalInformationid = item.UserPersonalInformationId;
                                    payrollTransactionIncomeViewModel.PayrollPeriodId = Convert.ToInt32(payPeriodId);


                                    incomeDB = incomeBL.GetIncomesById(Convert.ToInt32(payrollTransactionIncome.IncomeId));
                                    payrollTransactionIncomeViewModel.IncomeId = Convert.ToInt32(payrollTransactionIncome.IncomeId);
                                    payrollTransactionIncomeViewModel.CustomIncomeAmount = payrollTransactionIncome.CustomIncomeAmount > 0 ? Convert.ToDecimal(payrollTransactionIncome.CustomIncomeAmount) : 0.0M;
                                    //payrollTransactionIncomeViewModel.DefaultIncomeAmount = dbIncome.IncomeAmount > 0 ? Convert.ToDecimal(dbIncome.IncomeAmount) : 0.0M;
                                    payrollTransactionIncomeViewModel.DefaultIncomeName = incomeDB.IncomeName;

                                    payrollTransactionIncomeViewModels.Add(payrollTransactionIncomeViewModel);
                                }
                                else
                                {
                                    PayrollTransactionIncomeViewModels insertView = new PayrollTransactionIncomeViewModels();
                                    insertView.CustomIncomeAmount = 0.0M;
                                    insertView.PayrollTransactionIncomeId = 0;
                                    insertView.UserPersonalInformationid = item.UserPersonalInformationId;

                                    incomeDB = incomeBL.GetIncomesById(Convert.ToInt32(dbIncome.IncomeId));
                                    insertView.IncomeId = Convert.ToInt32(dbIncome.IncomeId);
                                    insertView.CustomIncomeAmount = 0.0M;
                                    //insertView.DefaultIncomeAmount = dbIncome.IncomeAmount > 0 ? Convert.ToDecimal(dbIncome.IncomeAmount) : 0.0M;
                                    insertView.DefaultIncomeName = incomeDB.IncomeName;
                                    insertView.PayrollPeriodId = Convert.ToInt32(payPeriodId);

                                    payrollTransactionIncome = new PayrollTransactionIncome();
                                    payrollTransactionIncome = payrollTransactionIncomeBL.InsertPayrollTransactionIncomes(insertView);
                                    insertView = new PayrollTransactionIncomeViewModels();
                                    insertView.PayrollTransactionIncomeId = payrollTransactionIncome.PayrollTransactionIncomeId;
                                    insertView.UserPersonalInformationid = item.UserPersonalInformationId;
                                    insertView.PayrollPeriodId = Convert.ToInt32(payPeriodId);
                                    insertView.IncomeId = Convert.ToInt32(payrollTransactionIncome.IncomeId);
                                    insertView.CustomIncomeAmount = payrollTransactionIncome.CustomIncomeAmount > 0 ? Convert.ToDecimal(payrollTransactionIncome.CustomIncomeAmount) : 0.0M;
                                    //insertView.DefaultIncomeAmount = dbIncome.IncomeAmount > 0 ? Convert.ToDecimal(dbIncome.IncomeAmount) : 0.0M;
                                    insertView.DefaultIncomeName = incomeDB.IncomeName;
                                    payrollTransactionIncomeViewModels.Add(insertView);
                                }
                            }
                        }
                        #endregion

                        #region deductions
                        deductionDBList = deductionBL.GetActiveDeductions();
                        if (deductionDBList != null)
                        {
                            foreach (var dbDeduction in deductionDBList)
                            {
                                payrollTransactionDeduction = new PayrollTransactionDeduction();
                                payrollTransactionDeduction = payrollTransactionDeductionBL.GetPayrollTransactionDeduction(item.UserPersonalInformationId, Convert.ToInt32(payPeriodId), dbDeduction.DeductionId);

                                if (payrollTransactionDeduction != null)
                                {
                                    payrollTransactionDeductionViewModel = new PayrollTransactionDeductionViewModels();
                                    payrollTransactionDeductionViewModel.PayrollTransactionDeductionId = payrollTransactionDeduction.PayrollTransactionDeductionId;
                                    payrollTransactionDeductionViewModel.UserPersonalInformationid = item.UserPersonalInformationId;
                                    payrollTransactionDeductionViewModel.PayrollPeriodId = Convert.ToInt32(payPeriodId);


                                    deductionDB = deductionBL.GetDeductionById(Convert.ToInt32(payrollTransactionDeduction.DeductionId));
                                    payrollTransactionDeductionViewModel.DeductionId = Convert.ToInt32(payrollTransactionDeduction.DeductionId);
                                    payrollTransactionDeductionViewModel.CustomDeductionAmount = payrollTransactionDeduction.CustomDeductionAmount > 0 ? Convert.ToDecimal(payrollTransactionDeduction.CustomDeductionAmount) : 0.0M;
                                    //payrollTransactionIncomeViewModel.DefaultIncomeAmount = dbIncome.IncomeAmount > 0 ? Convert.ToDecimal(dbIncome.IncomeAmount) : 0.0M;
                                    payrollTransactionDeductionViewModel.DefaultDeductionName = deductionDB.DeductionName;

                                    payrollTransactionDeductionViewModels.Add(payrollTransactionDeductionViewModel);
                                }
                                else
                                {
                                    PayrollTransactionDeductionViewModels deductionView = new PayrollTransactionDeductionViewModels();
                                    deductionView.CustomDeductionAmount = 0.0M;
                                    deductionView.PayrollTransactionDeductionId = 0;
                                    deductionView.UserPersonalInformationid = item.UserPersonalInformationId;

                                    deductionDB = deductionBL.GetDeductionById(Convert.ToInt32(dbDeduction.DeductionId));
                                    deductionView.DeductionId = Convert.ToInt32(dbDeduction.DeductionId);
                                    deductionView.CustomDeductionAmount = 0.0M;
                                    //insertView.DefaultIncomeAmount = dbIncome.IncomeAmount > 0 ? Convert.ToDecimal(dbIncome.IncomeAmount) : 0.0M;
                                    deductionView.DefaultDeductionName = incomeDB.IncomeName;
                                    deductionView.PayrollPeriodId = Convert.ToInt32(payPeriodId);

                                    payrollTransactionDeduction = new PayrollTransactionDeduction();
                                    payrollTransactionDeduction = payrollTransactionDeductionBL.InsertPayrollTransactionDeductions(deductionView);
                                    deductionView = new PayrollTransactionDeductionViewModels();
                                    deductionView.PayrollTransactionDeductionId = payrollTransactionDeduction.PayrollTransactionDeductionId;
                                    deductionView.UserPersonalInformationid = item.UserPersonalInformationId;
                                    deductionView.PayrollPeriodId = Convert.ToInt32(payPeriodId);
                                    deductionView.DeductionId = Convert.ToInt32(payrollTransactionDeduction.DeductionId);
                                    deductionView.CustomDeductionAmount = payrollTransactionDeduction.CustomDeductionAmount > 0 ? Convert.ToDecimal(payrollTransactionDeduction.CustomDeductionAmount) : 0.0M;
                                    //insertView.DefaultIncomeAmount = dbIncome.IncomeAmount > 0 ? Convert.ToDecimal(dbIncome.IncomeAmount) : 0.0M;
                                    deductionView.DefaultDeductionName = incomeDB.IncomeName;
                                    payrollTransactionDeductionViewModels.Add(deductionView);
                                }
                            }
                        }
                        #endregion

                        employee.Incomes = payrollTransactionIncomeViewModels;
                        payrollTransactionIncomeViewModels = new List<PayrollTransactionIncomeViewModels>();
                        employee.Deductions = payrollTransactionDeductionViewModels;
                        payrollTransactionDeductionViewModels = new List<PayrollTransactionDeductionViewModels>();

                        if (item.DepartmentId > 0)
                        {
                            employee.Department = department.GetDepartmentById(item.DepartmentId).DepartmentName;
                        }


                        #region EmployeeDetailsPayrollViewModel
                        EmployeeDetailsPayrollViewModel employeeDetailsPayrollViewModel = new EmployeeDetailsPayrollViewModel();
                        employeeDetailsPayrollViewModel.BasicPay = employee.BasicPay > 0 ? Convert.ToDecimal(employee.BasicPay) : 0.0M;
                        employeeDetailsPayrollViewModel.Department = employee.Department;
                        employeeDetailsPayrollViewModel.EmployeeId = employee.EmployeeId;
                        employeeDetailsPayrollViewModel.Name = employee.Name;

                        #endregion

                        #region EmployeePayPeriodDetailsPayrollViewModel
                        payrollPeriodDB = payrollPeriodBL.GetPayrollPeriodById(Convert.ToInt32(payPeriodId));
                        EmployeePayPeriodDetailsPayrollViewModel employeePayPeriodDetailsPayrollViewModel = new EmployeePayPeriodDetailsPayrollViewModel();
                        employeePayPeriodDetailsPayrollViewModel.CurrentPeriodWorkDays = payrollPeriodDB.WorkDays > 0 ? Convert.ToDecimal(payrollPeriodDB.WorkDays) : 0.0M;
                        employeePayPeriodDetailsPayrollViewModel.CurrentPeriodWorkHours = payrollPeriodDB.WorkHours > 0 ? Convert.ToDecimal(payrollPeriodDB.WorkHours) : 0.0M; ;
                        employeePayPeriodDetailsPayrollViewModel.Month = payrollPeriodDB.Month;
                        employeePayPeriodDetailsPayrollViewModel.PayPeriodFrom = payrollPeriodDB.PayPeriodFrom;
                        employeePayPeriodDetailsPayrollViewModel.PayPeriodTo = payrollPeriodDB.PayPeriodTo;
                        employeePayPeriodDetailsPayrollViewModel.PayPeriodId = Convert.ToInt32(payPeriodId);
                        #endregion


                        employee.EmployeeDetailsPayrollViewModel = employeeDetailsPayrollViewModel;
                        employee.EmployeePayPeriodDetailsPayrollViewModel = employeePayPeriodDetailsPayrollViewModel;
                        employeeListModels.Add(employee);
                    }

                    return View(employeeListModels);
                }
                else
                {
                    ModelState.AddModelError("", "Choose a pay period.");
                    return RedirectToAction("Employees", "Home");
                }
            }
            else
            {
                ModelState.AddModelError("", "Choose a pay period.");
                return RedirectToAction("Employees", "Home");
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult EditEmployee(int id)
        {
            EmployeeListModel viewModel = new EmployeeListModel();
            if (ModelState.IsValid)
            {
                viewModel = homeHelper.GetEmployee(id);
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
            return View(homeHelper.GetEmployee(id));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditEmployee(EmployeeListModel viewModel)
        {
            if (ModelState.IsValid)
            {
                homeHelper.UpdateEmployee(viewModel);
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
                    ModelState.AddModelError("", "Error");
                    return View(assignDepartmentViewModel);
                }
                
            }
            else
            {
                ModelState.AddModelError("", "Error.");
                return View(assignDepartmentViewModel);
            }
            return RedirectToAction("Employees", "Home");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult AssignIncome(int id)
        {
            List<AssignedEmployeeIncomesViewModel> assignEmployeeIncomeViewModelList = new List<AssignedEmployeeIncomesViewModel>();
            string name = "";
            assignEmployeeIncomeViewModelList = homeHelper.AssignIncome(id, out name);
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
            //toModel.SelectedIncomeAmount = fromModel.SelectedIncomeAmount;

            income = bL.GetIncomesById(fromModel.IncomeId);
            if (income != null)
            {
                if (fromModel.SelectedIncomeAmount > 0)
                {
                    toModel.SelectedIncomeAmount = fromModel.SelectedIncomeAmount;
                }
                else
                {
                    toModel.SelectedIncomeAmount = 0.0M;
                }
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
                        ModelState.AddModelError("", "Please select another income.");
                        return View(toModel);
                    }
                    else
                    {
                        toDB.UserPersonalInformationID = toModel.UserPersonalInformationId;
                        //toDB.CustomAmount = toModel.SelectedCustomAmount;
                        toDB.IncomeAmount = toModel.SelectedIncomeAmount;
                        toDB.IncomeId = toModel.IncomeId;

                        assingedEmployeeIncomeBL.AddEmployeeIncome(toDB);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Please select another income.");
                    return View(toModel);
                }

            }
            else
            {
                ModelState.AddModelError("", "Please select an income.");
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

            viewModels.UserPersonalInformationId = id;
            income = bL.GetIncomesById(incomeId);
            //viewModels.SelectedCustomAmount = assingedEmployeeIncomeBL.GetAssignedEmployeeIncomesByIncomeId(id, incomeId);
            if (income != null)
            {
                //viewModels.SelectedIncomeAmount = income.IncomeValue;
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
            //toModel.SelectedCustomAmount = fromModel.SelectedCustomAmount;

            income = bL.GetIncomesById(fromModel.IncomeId);
            if (income != null)
            {
                //toModel.SelectedIncomeAmount = income.IncomeValue;
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
                    //toDB.CustomAmount = toModel.SelectedCustomAmount;
                    toDB.IncomeAmount = toModel.SelectedIncomeAmount;
                    toDB.IncomeId = toModel.IncomeId;

                    assingedEmployeeIncomeBL.UpdateEmployeeIncome(toDB);


                }
                else
                {
                    ModelState.AddModelError("", "Please select an income.");
                    return View(toModel);
                }

            }
            else
            {
                ModelState.AddModelError("", "Error.");
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
            List<AssignedEmployeeDeductionsViewModel> assignEmployeeDeductionsViewModelList = new List<AssignedEmployeeDeductionsViewModel>();
            string name = "";
            assignEmployeeDeductionsViewModelList = homeHelper.AssignDeduction(id, out name);
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
            //toModel.SelectedCustomAmount = fromModel.SelectedCustomAmount;

            deduction = bL.GetDeductionById(fromModel.DeductionId);
            if (deduction != null)
            {
                toModel.SelectedDeductionAmount = 0.0M;
                toModel.SelectedDeductionName = deduction.DeductionName;
            }
            else
            {
                //toModel.SelectedCustomAmount = fromModel.SelectedDeductionAmount;
                toModel.SelectedDeductionName = fromModel.SelectedDeductionName;
            }




            AssignedEmployeeDeduction toDB = new AssignedEmployeeDeduction();


            if (ModelState.IsValid)
            {
                if (fromModel.DeductionId > 0)
                {

                    if (assingedEmployeeBL.IsDeductionAssignedToEmployee(fromModel.UserPersonalInformationId, fromModel.DeductionId))
                    {
                        ModelState.AddModelError("", "Please select a different deduction.");
                        return View(toModel);
                    }
                    else
                    {
                        toDB.UserPersonalInformationID = toModel.UserPersonalInformationId;
                        //toDB.CustomAmount = toModel.SelectedCustomAmount;
                        toDB.DeductionAmount = toModel.SelectedDeductionAmount;
                        toDB.DeductionId = toModel.DeductionId;

                        assingedEmployeeBL.AddEmployeeDeductions(toDB);
                    }


                }
                else
                {
                    ModelState.AddModelError("", "Please select a deduction.");
                    return View(toModel);
                }

            }
            else
            {
                ModelState.AddModelError("", "Error.");
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

            viewModels.UserPersonalInformationId = id;
            deduction = bL.GetDeductionById(deductionId);
            //viewModels.SelectedCustomAmount = assingedEmployeeBL.GetAssignedEmployeeDeductionsByDeductionId(id, deductionId);
            if (deduction != null)
            {
                //viewModels.SelectedDeductionAmount = deduction.DeductionValue;
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
            //toModel.SelectedCustomAmount = fromModel.SelectedCustomAmount;

            deduction = bL.GetDeductionById(fromModel.DeductionId);
            if (deduction != null)
            {
                toModel.SelectedDeductionAmount = 0.0M;
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
                    //toDB.CustomAmount = toModel.SelectedCustomAmount;
                    toDB.DeductionAmount = toModel.SelectedDeductionAmount;
                    toDB.DeductionId = toModel.DeductionId;

                    assingedEmployeeBL.UpdateEmployeeDeduction(toDB);


                }
                else
                {
                    ModelState.AddModelError("", "Please select a Deduction.");
                    return View(toModel);
                }

            }
            else
            {
                ModelState.AddModelError("", "Error.");
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

        [Authorize(Roles = "Admin")]
        public ActionResult PayrollTransaction(int id)
        {
            return RedirectToAction("Index", "PayrollTransaction", new { id = id });
        }
    }
}