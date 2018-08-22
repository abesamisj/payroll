using Payroll.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Payroll.Code
{
    public class AssignedEmployeeDeductionBL
    {
        public AssignedEmployeeDeduction GetAssignedEmployeeDeduction(int userPersonalInformationId, int deductionId)
        {
            using (var db = new PayrollEntities())
            {
                var query = from a in db.AssignedEmployeeDeductions
                            where (a.UserPersonalInformationID == userPersonalInformationId)
                            && (a.DeductionId == deductionId)
                            select a;

                return query.FirstOrDefault();
            }
        }
        public List<AssignedEmployeeDeduction> GetAssignedEmployeeDeductions(int userPersonalInformationId)
        {
            using (var db = new PayrollEntities())
            {
                var query = from a in db.AssignedEmployeeDeductions
                            join b in db.Deductions on a.DeductionId equals b.DeductionId
                            where a.UserPersonalInformationID == userPersonalInformationId
                            select a;

                return query.ToList();
            }
        }

        //public decimal GetAssignedEmployeeDeductionsByDeductionId(int userPersonalInformationId, int deductionId)
        //{
        //    decimal returnValue = 0.0M;
        //    using (var db = new PayrollEntities())
        //    {
        //        var query = from a in db.AssignedEmployeeDeductions
        //                    join b in db.Deductions on a.DeductionId equals b.DeductionId
        //                    where (a.UserPersonalInformationID == userPersonalInformationId)
        //                    && (a.DeductionId == deductionId)
        //                    select a.CustomAmount;

        //        if (query.FirstOrDefault().HasValue)
        //        {
        //            returnValue = Convert.ToDecimal(query.FirstOrDefault().Value);
        //        };

        //        return returnValue;
        //    }
        //}

        public bool IsDeductionAssignedToEmployee(int userPersonalInformationId, int deductionId)
        {
            using (var db = new PayrollEntities())
            {
                var query = from a in db.AssignedEmployeeDeductions
                            where (a.UserPersonalInformationID == userPersonalInformationId)
                            && (a.DeductionId == deductionId)
                            select a;

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
        public void DeleteDeduction(int userPersonalInformationId, int deductionId)
        {

            using (var db = new PayrollEntities())
            {

                var itemToRemove = db.AssignedEmployeeDeductions.SingleOrDefault(x => x.UserPersonalInformationID == userPersonalInformationId && x.DeductionId == deductionId); //returns a single item.

                if (itemToRemove != null)
                {
                    db.AssignedEmployeeDeductions.Remove(itemToRemove);
                    db.SaveChanges();
                }
            }
        }

        public void AddEmployeeDeductions(AssignedEmployeeDeduction toDB)
        {
            using (var db = new PayrollEntities())
            {
                db.AssignedEmployeeDeductions.Add(toDB);
                db.SaveChanges();
            }
        }
        public void UpdateEmployeeDeduction(AssignedEmployeeDeduction toDB)
        {
            using (var db = new PayrollEntities())
            {
                var result = db.AssignedEmployeeDeductions.SingleOrDefault(b => b.UserPersonalInformationID == toDB.UserPersonalInformationID
                && b.DeductionId == toDB.DeductionId);
                if (result != null)
                {
                    //result.CustomAmount = toDB.CustomAmount;
                    db.SaveChanges();
                }
            }
        }
    }
}