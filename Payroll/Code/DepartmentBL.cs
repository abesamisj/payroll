using Payroll.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Payroll.Code
{
    public class DepartmentBL
    {
        PayrollEntities context = new PayrollEntities();

        public List<Department> GetActiveDepartments()
        {
            using (var db = new PayrollEntities())
            {
                var query = from b in db.Departments
                            where b.Active == 1
                            orderby b.Active descending
                            select b;

                return query.ToList();
            }
        }

        public List<Department> GetAllDepartments()
        {
            using (var db = new PayrollEntities())
            {
                var query = from b in db.Departments
                            orderby b.Active descending
                            select b;

                return query.ToList();
            }
        }

        public Department GetDepartmentById(int id)
        {
            using (var db = new PayrollEntities())
            {
                var query = from b in db.Departments
                            where b.DepartmentId == id
                            select b;

                return query.FirstOrDefault();
            }
        }

        public void InsertDepartment(Department department)
        {
            context.Departments.Add(department);
            context.SaveChanges();
        }

        public void UpdateDepartment(Department department)
        {
            using (var db = new PayrollEntities())
            {
                var result = db.Departments.SingleOrDefault(b => b.DepartmentId == department.DepartmentId);
                if (result != null)
                {
                    result.Active = department.Active;
                    result.DepartmentDescription = department.DepartmentDescription;
                    result.DepartmentName = department.DepartmentName;
                    db.SaveChanges();
                }
            }
        }
    }
}