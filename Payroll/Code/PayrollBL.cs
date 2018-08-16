using Payroll.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Payroll.Code
{
    public class PayrollBL
    {
        PayrollEntities context = new PayrollEntities();

        //public List<PayrollTransaction> GetAllPayrollTransactions()
        //{
        //    using (var db = new PayrollEntities())
        //    {
        //        var query = from b in db.PayrollTransactions
        //                    orderby b.Month descending
        //                    select b;

        //        return query.ToList();
        //    }
        //}
    }
}