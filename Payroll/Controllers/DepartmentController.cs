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
    public class DepartmentController : Controller
    {
        DepartmentBL departmentBL = new DepartmentBL();


        // GET: Department
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            List<DepartmentViewModels> listModels = new List<DepartmentViewModels>();
            DepartmentViewModels model = new DepartmentViewModels();

            List<Department> fromDB = departmentBL.GetAllDepartments();

            foreach (Department item in fromDB)
            {
                model = new DepartmentViewModels();
                model.Active = Convert.ToBoolean(item.Active);
                model.DepartmentDescription = item.DepartmentDescription;
                model.DepartmentId = item.DepartmentId;
                model.DepartmentName = item.DepartmentName;
                listModels.Add(model);
            }

            return View(listModels);
        }

        // GET: Department/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int id)
        {
            Department department = departmentBL.GetDepartmentById(id);
            DepartmentViewModels model = new DepartmentViewModels();
            model = new DepartmentViewModels();
            model.Active = Convert.ToBoolean(department.Active);
            model.DepartmentDescription = department.DepartmentDescription;
            model.DepartmentId = department.DepartmentId;
            model.DepartmentName = department.DepartmentName;

            return View(model);
        }

        // GET: Department/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Department/Create
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(DepartmentViewModels collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Department toDB = new Department();
                    toDB.Active = 1;
                    toDB.DepartmentDescription = collection.DepartmentDescription;
                    toDB.DepartmentId = collection.DepartmentId;
                    toDB.DepartmentName = collection.DepartmentName;

                    departmentBL.InsertDepartment(toDB);

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

        // GET: Department/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            Department department = departmentBL.GetDepartmentById(id);
            DepartmentViewModels model = new DepartmentViewModels();
            model = new DepartmentViewModels();
            model.Active = Convert.ToBoolean(department.Active);
            model.DepartmentDescription = department.DepartmentDescription;
            model.DepartmentId = department.DepartmentId;
            model.DepartmentName = department.DepartmentName;

            return View(model);
        }

        // POST: Department/Edit/5
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DepartmentViewModels collection)
        {
            try
            {
                int active = 0;
                if (collection.Active)
                {
                    active = 1;
                }
                if (ModelState.IsValid)
                {
                    Department toDB = new Department();
                    toDB.Active = active;
                    toDB.DepartmentDescription = collection.DepartmentDescription;
                    toDB.DepartmentId = collection.DepartmentId;
                    toDB.DepartmentName = collection.DepartmentName;

                    departmentBL.UpdateDepartment(toDB);

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

        // GET: Department/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Department/Delete/5
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
