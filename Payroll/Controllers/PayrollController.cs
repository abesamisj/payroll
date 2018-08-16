using Payroll.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Payroll.Controllers
{
    public class PayrollController : Controller
    {
        // GET: Payroll
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            List<PayrollViewModels> list = new List<PayrollViewModels>();
            return View(list);
        }

        // GET: Payroll/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Payroll/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Payroll/Create
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

        // GET: Payroll/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Payroll/Edit/5
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

        // GET: Payroll/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Payroll/Delete/5
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
