using Payroll.Code;
using Payroll.Data;
using Payroll.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Payroll.Controllers
{
    public class PayrollPeriodController : Controller
    {
        PayrollPeriodBL payrollPeriodBL = new PayrollPeriodBL();
        
        // GET: PayrollPeriod
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            List<PayrollPeriodViewModels> listModels = new List<PayrollPeriodViewModels>();
            PayrollPeriodViewModels model = new PayrollPeriodViewModels();

            List<PayrollPeriod> fromDB = payrollPeriodBL.GetPayrollPeriods();
            //var months = Helper.GetAllMonths();
            foreach (PayrollPeriod item in fromDB)
            {
                model = new PayrollPeriodViewModels();
                model.PayrollPeriodId = item.PayrollPeriodId;
                model.PayrollPeriodDescription = item.PayrollPeriodDescription;
                model.Year = item.Year;
                model.Month = item.Month;
                model.PayPeriodFrom = item.PayPeriodFrom;
                model.PayPeriodTo = item.PayPeriodTo;
                model.WorkDays = item.WorkDays;
                model.WorkHours = item.WorkHours;
                listModels.Add(model);
            }
            return View(listModels);
        }


        // GET: PayrollPeriod/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Income/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
           
            return View();
        }

        // POST: Income/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Create(PayrollPeriodViewModels collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    PayrollPeriod toDB = new PayrollPeriod();
                    toDB.PayrollPeriodDescription = collection.PayrollPeriodDescription;
                    toDB.PayPeriodFrom = collection.PayPeriodFrom;
                    toDB.PayPeriodTo = collection.PayPeriodTo;
                    toDB.WorkDays = collection.WorkDays;
                    toDB.WorkHours = collection.WorkHours;
                    toDB.Year = collection.PayPeriodFrom.Year;
                    toDB.Month = Helper.GetMonthName(collection.PayPeriodFrom.Month);

                    payrollPeriodBL.InsertPayrollPeriod(toDB);

                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Error.");
                    return View(collection);
                }
            }
            catch
            {
                ModelState.AddModelError("", "Error.");
                return View(collection);
            }
        }

        // GET: PayrollPeriod/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            PayrollPeriodViewModels model = new PayrollPeriodViewModels();

            PayrollPeriod fromDB = payrollPeriodBL.GetPayrollPeriodById(id);
            model = new PayrollPeriodViewModels();
            model.PayrollPeriodId = fromDB.PayrollPeriodId;
            model.PayrollPeriodDescription = fromDB.PayrollPeriodDescription;
            model.Year = fromDB.Year;
            model.Month = fromDB.Month;
            model.PayPeriodFrom = fromDB.PayPeriodFrom;
            model.PayPeriodTo = fromDB.PayPeriodTo;
            model.WorkDays = fromDB.WorkDays;
            model.WorkHours = fromDB.WorkHours;

            return View(model);
        }


        // POST: PayrollPeriod/Edit/5
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Edit(PayrollPeriodViewModels collection)
        {
            try
            {
                PayrollPeriod toDB = new PayrollPeriod();
                if (ModelState.IsValid)
                {
                    
                    toDB = new PayrollPeriod();
                    toDB.PayrollPeriodId = collection.PayrollPeriodId;
                    toDB.PayrollPeriodDescription = collection.PayrollPeriodDescription;
                    toDB.Year = collection.Year;
                    toDB.Month = collection.Month;
                    toDB.PayPeriodFrom = collection.PayPeriodFrom;
                    toDB.PayPeriodTo = collection.PayPeriodTo;
                    toDB.WorkDays = collection.WorkDays;
                    toDB.WorkHours = collection.WorkHours;
                    payrollPeriodBL.UpdatePayrollPeriod(toDB);
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Error.");
                    return View(collection);
                }
            }
            catch
            {
                ModelState.AddModelError("", "Error.");
                return View(collection);
            }
        }

        // GET: PayrollPeriod/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PayrollPeriod/Delete/5
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

        [Authorize(Roles = "Admin")]
        public ActionResult Payroll(int id)
        {
            return RedirectToAction("Index", "PayrollTransaction", new { ID = id });
        }
    }
}
