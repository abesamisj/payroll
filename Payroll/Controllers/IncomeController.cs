using Payroll.Code;
using Payroll.Data;
using Payroll.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Payroll.Controllers
{
    public class IncomeController : Controller
    {
        IncomeBL incomeBL = new IncomeBL();
        // GET: Income
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            List<IncomeViewModels> listModels = new List<IncomeViewModels>();
            IncomeViewModels model = new IncomeViewModels();

            List<Income> fromDB = incomeBL.GetAllIncomes();

            foreach (Income item in fromDB)
            {
                model = new IncomeViewModels();
                model.Active = Convert.ToBoolean(item.Active);
                model.IncomeDescription = item.IncomeDescription;
                model.IncomeId = item.IncomeId;
                model.IncomeName = item.IncomeName;
                model.IncomeValue = item.IncomeValue;
                listModels.Add(model);
            }

            return View(listModels);
        }

        // GET: Income/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int id)
        {
            Income income = incomeBL.GetIncomesById(id);
            IncomeViewModels model = new IncomeViewModels();
            model = new IncomeViewModels();
            model.Active = Convert.ToBoolean(income.Active);
            model.IncomeDescription = income.IncomeDescription;
            model.IncomeId = income.IncomeId;
            model.IncomeName = income.IncomeName;
            model.IncomeValue = income.IncomeValue;

            return View(model);
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
        public ActionResult Create(IncomeViewModels collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Income toDB = new Income();
                    toDB.Active = 1;
                    toDB.IncomeDescription = collection.IncomeDescription;
                    toDB.IncomeName = collection.IncomeName;
                    toDB.IncomeValue = collection.IncomeValue;

                    incomeBL.InsertIncome(toDB);

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

        // GET: Income/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            IncomeViewModels viewModel = new IncomeViewModels();
            Income fromDB = incomeBL.GetIncomesById(id);

            bool active = false;

            if (fromDB.Active > 0)
            {
                active = true;
            }

            if (fromDB != null)
            {
                viewModel = new IncomeViewModels();
                viewModel.Active = active;
                viewModel.IncomeDescription = fromDB.IncomeDescription;
                viewModel.IncomeId = fromDB.IncomeId;
                viewModel.IncomeName = fromDB.IncomeName;
                viewModel.IncomeValue = fromDB.IncomeValue;
            }
            return View(viewModel);
        }

        // POST: Income/Edit/5
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(IncomeViewModels viewModel)
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
                    Income toDB = new Income();
                    toDB.Active = active;
                    toDB.IncomeDescription = viewModel.IncomeDescription;
                    toDB.IncomeName = viewModel.IncomeName;
                    toDB.IncomeValue = viewModel.IncomeValue;
                    toDB.IncomeId = viewModel.IncomeId;

                    incomeBL.UpdateIncome(toDB);

                    return RedirectToAction("Index", "Income");
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

        // GET: Income/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Income/Delete/5
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
