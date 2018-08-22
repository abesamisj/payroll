using Payroll.Data;
using Payroll.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Payroll.Code
{
    public class AssingedEmployeeIncomeBL
    {
        public AssignedEmployeeIncome GetAssignedEmployeeIncome(int userPersonalInformationId, int incomeId)
        {
            using (var db = new PayrollEntities())
            {
                var query = from a in db.AssignedEmployeeIncomes
                            where (a.UserPersonalInformationID == userPersonalInformationId)
                            &&  (a.IncomeId == incomeId)
                            select a;

                return query.FirstOrDefault();
            }
        }
        public List<AssignedEmployeeIncome> GetAssignedEmployeeIncomes(int userPersonalInformationId)
        {
            using (var db = new PayrollEntities())
            {
                var query = from a in db.AssignedEmployeeIncomes
                            join b in db.Incomes on a.IncomeId equals b.IncomeId
                            where a.UserPersonalInformationID == userPersonalInformationId
                            orderby b.Order ascending
                            select a;

                return query.ToList();
            }
        }

        //public decimal GetAssignedEmployeeIncomesByIncomeId(int userPersonalInformationId, int incomeId)
        //{
        //    decimal returnValue = 0.0M;
        //    using (var db = new PayrollEntities())
        //    {
        //        var query = from a in db.AssignedEmployeeIncomes
        //                    join b in db.Incomes on a.IncomeId equals b.IncomeId
        //                    where (a.UserPersonalInformationID == userPersonalInformationId)
        //                    && (a.IncomeId == incomeId)
        //                    select a.CustomAmount;

        //        if (query != null)
        //        {
        //            returnValue = Convert.ToDecimal(query.FirstOrDefault().Value);
        //        };

        //        return returnValue;
        //    }
        //}

        public void DeleteIncome(int userPersonalInformationId, int incomeId)
        {
           
            using (var db = new PayrollEntities())
            {
               
                var itemToRemove = db.AssignedEmployeeIncomes.SingleOrDefault(x => x.UserPersonalInformationID == userPersonalInformationId && x.IncomeId == incomeId); //returns a single item.

                if (itemToRemove != null)
                {
                    db.AssignedEmployeeIncomes.Remove(itemToRemove);
                    db.SaveChanges();
                }
            }
        }

        public bool IsIncomeAssignedToEmployee(int userPersonalInformationId, int incomeId)
        {
            using (var db = new PayrollEntities())
            {
                var query = from a in db.AssignedEmployeeIncomes
                            where (a.UserPersonalInformationID == userPersonalInformationId)
                            && (a.IncomeId == incomeId)
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


        public void AddEmployeeIncome(AssignedEmployeeIncome toDB)
        {
            using (var db = new PayrollEntities())
            {
                db.AssignedEmployeeIncomes.Add(toDB);
                db.SaveChanges();
            }
        }
        public void UpdateEmployeeIncome(AssignedEmployeeIncome toDB)
        {
            using (var db = new PayrollEntities())
            {
                var result = db.AssignedEmployeeIncomes.SingleOrDefault(b => b.UserPersonalInformationID == toDB.UserPersonalInformationID
                && b.IncomeId == toDB.IncomeId);
                if (result != null)
                {
                    //result.CustomAmount = toDB.CustomAmount;
                    db.SaveChanges();
                }
            }
        }
    }
}