using Payroll.Code;
using Payroll.Data;
using Payroll.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Payroll.Controllers
{
    public class DeductionController : Controller
    {
        DeductionBL deductionBL = new DeductionBL();
        
        // GET: Deduction
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            List<DeductionViewModels> listModels = new List<DeductionViewModels>();
            DeductionViewModels model = new DeductionViewModels();

            List<Deduction> fromDB = deductionBL.GetAllDeductions();

            foreach (Deduction item in fromDB)
            {
                model = new DeductionViewModels();
                model.Active = Convert.ToBoolean(item.Active);
                model.DeductionDescription = item.DeductionDescription;
                model.DeductionId = item.DeductionId;
                model.DeductionName = item.DeductionName;
                //model.DeductionValue = item.DeductionValue;
                model.Order = item.Order;
                listModels.Add(model);
            }

            return View(listModels);
        }

        // GET: Deduction/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int id)
        {
            Deduction deduction = deductionBL.GetDeductionById(id);
            DeductionViewModels model = new DeductionViewModels();
            model = new DeductionViewModels();
            model.Active = Convert.ToBoolean(deduction.Active);
            model.DeductionDescription = deduction.DeductionDescription;
            model.DeductionId = deduction.DeductionId;
            model.DeductionName = deduction.DeductionName;
            //model.DeductionValue = deduction.DeductionValue;
            model.Order = deduction.Order;
            return View(model);
        }

        // GET: Deduction/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Deduction/Create
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(DeductionViewModels collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Deduction toDB = new Deduction();
                    toDB.Active = 1;
                    toDB.DeductionDescription = collection.DeductionDescription;
                    toDB.DeductionName = collection.DeductionName;
                    //toDB.DeductionValue = collection.DeductionValue;
                    toDB.Order = collection.Order;
                    deductionBL.InsertDeduction(toDB);

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

        // GET: Deduction/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            Deduction deduction = deductionBL.GetDeductionById(id);
            DeductionViewModels model = new DeductionViewModels();
            model = new DeductionViewModels();
            model.Active = Convert.ToBoolean(deduction.Active);
            model.DeductionDescription = deduction.DeductionDescription;
            model.DeductionId = deduction.DeductionId;
            model.DeductionName = deduction.DeductionName;
            //model.DeductionValue = deduction.DeductionValue;
            model.Order = deduction.Order;
            return View(model);
        }

        // POST: Deduction/Edit/5
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DeductionViewModels viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int active = 0;
                    if (viewModel.Active)
                    {
                        active = 1;
                    }
                    Deduction toDB = new Deduction();
                    toDB.Active = active;
                    toDB.DeductionDescription = viewModel.DeductionDescription;
                    toDB.DeductionName = viewModel.DeductionName;
                    //toDB.DeductionValue = viewModel.DeductionValue;
                    toDB.DeductionId = viewModel.DeductionId;
                    toDB.Order = viewModel.Order;
                    deductionBL.UpdateDeduction(toDB);

                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Error.");
                    return View(viewModel);
                }
            }
            catch
            {
                ModelState.AddModelError("", "Error.");
                return View(viewModel);
            }
        }

        // GET: Deduction/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Deduction/Delete/5
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
