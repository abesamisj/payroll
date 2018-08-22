using Payroll.Code;
using Payroll.Data;
using System.Web.Mvc;

namespace Payroll.Controllers
{
    public class PayrollTransactionController : Controller
    {
        PayrollTransactionBL payrollTransactionBL = new PayrollTransactionBL();
        // GET: PayrollTransaction
        [Authorize(Roles = "Admin")]
        public ActionResult Index(int id)
        {
            PayrollTransaction payrollTransaction = new PayrollTransaction();
            //payrollTransaction = payrollTransactionBL.GetPayrollTransactionsByPayrollPeriodId(id);
            if (payrollTransaction != null)
            {

            }

            return View();
        }



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
