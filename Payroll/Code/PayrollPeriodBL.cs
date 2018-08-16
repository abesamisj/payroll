using Payroll.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Payroll.Code
{
    public class PayrollPeriodBL
    {
        public List<PayrollPeriod> GetPayrollPeriods()
        {
            using (var db = new PayrollEntities())
            {
                var query = from b in db.PayrollPeriods
                            orderby b.Month, b.PayrollPeriodId, b.PayPeriodFrom descending
                            select b;

                return query.ToList();
            }
        }
        public PayrollPeriod GetPayrollPeriodById(int id)
        {
            using (var db = new PayrollEntities())
            {
                var query = from b in db.PayrollPeriods
                            where b.PayrollPeriodId == id
                            orderby b.Month, b.PayrollPeriodId, b.PayPeriodFrom descending
                            select b;

                return query.FirstOrDefault();
            }
        }
        public void InsertPayrollPeriod(PayrollPeriod payrollPeriod)
        {
            using (var db = new PayrollEntities())
            {
                db.PayrollPeriods.Add(payrollPeriod);
                db.SaveChanges();
            }
        }

        public void UpdatePayrollPeriod(PayrollPeriod payrollPeriod)
        {
            using (var db = new PayrollEntities())
            {
                var result = db.PayrollPeriods.SingleOrDefault(b => b.PayrollPeriodId == payrollPeriod.PayrollPeriodId);
                if (result != null)
                {
                    result.PayrollPeriodDescription = payrollPeriod.PayrollPeriodDescription;
                    result.Year = payrollPeriod.Year;
                    result.Month = payrollPeriod.Month;
                    result.PayPeriodFrom = payrollPeriod.PayPeriodFrom;
                    result.PayPeriodTo = payrollPeriod.PayPeriodTo;
                    result.WorkDays = payrollPeriod.WorkDays;
                    result.WorkHours = payrollPeriod.WorkHours;
                    db.SaveChanges();
                }
            }
        }
        //public bool IsPayPeriodExists(DateTime payFrom, DateTime payTo)
        //{
        //    using (var db = new PayrollEntities())
        //    {
        //        var query = from b in db.PayrollPeriods
        //                    where b.PayPeriodFrom >= payFrom && b.PayPeriodFrom <= payTo
        //                    orderby b.Month, b.PayrollPeriodId, b.PayPeriodFrom descending
        //                    select b;

        //        return query.FirstOrDefault();
        //    }
        //}
    }
}