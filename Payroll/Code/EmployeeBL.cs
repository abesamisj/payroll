using Payroll.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Payroll.Code
{
    public class EmployeeBL
    {

        public List<UserPersonalInformation> GetActiveEmployees()
        {
            using (var db = new PayrollEntities())
            {
                var query = from b in db.UserPersonalInformations
                            where b.Active == 1
                            orderby b.Active descending
                            select b;

                return query.ToList();
            }
        }

        public List<UserPersonalInformation> GetAllEmployees()
        {
            using (var db = new PayrollEntities())
            {
                var query = from b in db.UserPersonalInformations
                            orderby b.Active descending
                            select b;

                return query.ToList();
            }
        }
        public UserPersonalInformation GetEmployeesById(int id)
        {
            using (var db = new PayrollEntities())
            {
                var query = from b in db.UserPersonalInformations
                            where b.UserPersonalInformationId == id
                            select b;

                return query.FirstOrDefault();
            }
        }

        public string GetEmployeeById(int id)
        {
            string name = "";
            using (var db = new PayrollEntities())
            {
                var query = from b in db.UserPersonalInformations
                            where b.UserPersonalInformationId == id
                            select b;

                if (query != null)
                {
                    foreach (var item in query)
                    {
                        return item.FirstName + " " + item.LastName;
                    }
                }

                return name;
            }
        }

        public bool CheckIfEmployeeIdExists(string employeeId)
        {
            using (var db = new PayrollEntities())
            {
                var query = from b in db.UserPersonalInformations
                            where b.EmployeeId.Trim() == employeeId.Trim()
                            select b;

                if (query.Count() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public void InsertEmployee(UserPersonalInformation toDB)
        {
            using (var db = new PayrollEntities())
            {
                db.UserPersonalInformations.Add(toDB);
                db.SaveChanges();
            }
        }

        public void UpdateEmployee(UserPersonalInformation userPersonalInformation)
        {
            using (var db = new PayrollEntities())
            {
                var result = db.UserPersonalInformations.SingleOrDefault(b => b.UserPersonalInformationId == userPersonalInformation.UserPersonalInformationId);
                if (result != null)
                {

                    result.Active = userPersonalInformation.Active;
                    result.EmployeeId = userPersonalInformation.EmployeeId;
                    result.Address = userPersonalInformation.Address;
                    result.FirstName = userPersonalInformation.FirstName;
                    result.LastName = userPersonalInformation.LastName;
                    result.Position = userPersonalInformation.Position;
                    result.BasicPay = userPersonalInformation.BasicPay;
                    db.SaveChanges();
                }
            }
        }

        public void UpdateEmployeeDepartment(int? userPersonalInformationID, int departmentID)
        {
            using (var db = new PayrollEntities())
            {
                var result = db.UserPersonalInformations.SingleOrDefault(b => b.UserPersonalInformationId == userPersonalInformationID);
                if (result != null)
                {
                    result.DepartmentId = departmentID;
                    db.SaveChanges();
                }
            }
        }
    }
}