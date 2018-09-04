using Payroll.Code;
using Payroll.Data;
using Payroll.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Payroll.Controllers
{
    public class PayrollTransactionController : Controller
    {
  
        // GET: PayrollTransaction
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Index(int id)
        {

            PayrollPeriodBL payrollPeriodBL = new PayrollPeriodBL();
            List<PayrollPeriod> payrollPeriods = new List<PayrollPeriod>();
            payrollPeriods = payrollPeriodBL.GetPayrollPeriods();
            PayrollTransactionsPeriodViewModels view = new PayrollTransactionsPeriodViewModels();
            view.PayrollPeriods = new SelectList(payrollPeriods, "PayrollPeriodId", "PayPeriodFrom", 1);

            ViewBag.Id = id;    
            return View(view);
            //PayrollTransactionBL payrollTransactionBL = new PayrollTransactionBL();
            //PayrollPeriodBL payrollPeriodBL = new PayrollPeriodBL();
            //EmployeeBL employeeBL = new EmployeeBL();
            //AssingedEmployeeIncomeBL assingedEmployeeIncomeBL = new AssingedEmployeeIncomeBL();
            //PayrollTransactionIncomeBL payrollTransactionIncomeBL = new PayrollTransactionIncomeBL();
            //IncomeBL incomeBL = new IncomeBL();


            ////PayrollTransaction payrollTransaction = new PayrollTransaction();
            //List<PayrollPeriod> payrollPeriods = new List<PayrollPeriod>();
            //PayrollPeriod payrollPeriod = new PayrollPeriod();
            //List<PayrollTransactionIncome> payrollTransactionIncomes = new List<PayrollTransactionIncome>();
            //PayrollTransactionIncome payrollTransactionIncome = new PayrollTransactionIncome();
            //List<Income> incomes = new List<Income>();
            //Income income = new Income();

            //PayrollTransactionViewModels payrollTransactionsViewModels = new PayrollTransactionViewModels();
            //List<PayrollTransactionIncomeViewModels> payrollTransactionIncomeViewModels = new List<PayrollTransactionIncomeViewModels>();
            //PayrollTransactionIncomeViewModels payrollTransactionIncomeView = new PayrollTransactionIncomeViewModels();

            //List<AssignedEmployeeIncome> assignedEmployeeIncomes = new List<AssignedEmployeeIncome>();
            //AssignedEmployeeIncome assignedEmployeeIncome = new AssignedEmployeeIncome();

            //payrollPeriods = payrollPeriodBL.GetPayrollPeriods();


            //if (payrollPeriods != null)
            //{
            //    foreach (var item in payrollPeriods)
            //    {
            //        payrollPeriod = new PayrollPeriod();
            //        payrollPeriod.PayrollPeriodId = item.PayrollPeriodId;
            //        break;
            //    }

            //    payrollTransactionsViewModels.PayrollPeriods = new SelectList(payrollPeriods, "PayrollPeriodId", "PayPeriodFrom", 1);

            //    payrollTransactionsViewModels.UserPersonalInformationId = id;
            //    //payrollTransaction = payrollTransactionBL.GetPayrollTransactionsByUserIdPayrollPeriodId(userPersonalInformationId, payrollPeriod.PayrollPeriodId);
            //    int incomeId = 0;


            //    payrollTransactionIncomes = payrollTransactionIncomeBL.GetAllPayrollTransactionIncomes(id, payrollPeriod.PayrollPeriodId);
            //    if (payrollTransactionIncomes != null)
            //    {
            //        //i have an existing income
            //        foreach (var item in payrollTransactionIncomes)
            //        {
            //            incomeId = 0;


            //            payrollTransactionIncomeView = new PayrollTransactionIncomeViewModels();
            //            payrollTransactionIncomeView.CustomIncomeAmount = item.CustomIncomeAmount;

            //            incomeId = item.IncomeId > 0 ? Convert.ToInt32(item.IncomeId) : 0;

            //            income = incomeBL.GetIncomesById(incomeId);
            //            payrollTransactionIncomeView.DefaultIncomeAmount = income.IncomeValue;
            //            payrollTransactionIncomeView.IncomeId = incomeId;
            //            payrollTransactionIncomeView.PayrollPeriodId = item.PayrollPeriodId;
            //            payrollTransactionIncomeView.PayrollTransactionIncomeId = item.PayrollTransactionIncomeId;
            //            payrollTransactionIncomeView.UserPersonalInformationid = item.UserPersonalInformationid;
            //            payrollTransactionIncomeViewModels.Add(payrollTransactionIncomeView);


            //        }

            //    }
            //    else
            //    {
            //        // i have an assigned income
            //        assignedEmployeeIncomes = assingedEmployeeIncomeBL.GetAssignedEmployeeIncomes(id);

            //        foreach (var item in assignedEmployeeIncomes)
            //        {
            //            incomeId = 0;
            //            incomeId = item.IncomeId > 0 ? Convert.ToInt32(item.IncomeId) : 0;

            //            income = incomeBL.GetIncomesById(incomeId);
            //            payrollTransactionIncomeView = new PayrollTransactionIncomeViewModels();
            //            payrollTransactionIncomeView.DefaultIncomeAmount = income.IncomeValue;
            //            payrollTransactionIncomeView.IncomeId = incomeId;

            //            payrollTransactionIncomeView.CustomIncomeAmount = 0.0M;
            //            payrollTransactionIncomeView.IncomeId = item.IncomeId;
            //            payrollTransactionIncomeView.PayrollPeriodId = payrollPeriod.PayrollPeriodId;
            //            payrollTransactionIncomeView.UserPersonalInformationid = item.UserPersonalInformationID;
            //            payrollTransactionIncomeViewModels.Add(payrollTransactionIncomeView);

            //            //insert in payrolltransactionsincome
            //            payrollTransactionIncomeBL.InsertPayrollTransactionIncomes(payrollTransactionIncomeView);
            //        }

            //    }
            //    payrollTransactionsViewModels.Incomes = payrollTransactionIncomeViewModels;

            //}
            //else
            //{
            //    ModelState.AddModelError("", "Create payroll periods first.");
            //    return View(payrollTransactionsViewModels);
            //}


            //return View(payrollTransactionsViewModels);
        }

        //public ActionResult Incomes(int id)
        //{
        //    var partialViewModel = new PartialViewModel();
        //    // TODO: Populate the model (viewmodel) here using the id

        //    return PartialView("_MyPartial", partialViewModel);
        //}

        // GET: PayrollTransaction/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PayrollTransaction/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PayrollTransaction/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: PayrollTransaction/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PayrollTransaction/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: PayrollTransaction/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PayrollTransaction/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
