using Payroll.Data;
using Payroll.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Payroll.Code
{
    public class PayrollTransactionBL
    {
        //public List<PayrollTransactionViewModels> GetAllPayrollTransactions()
        //{
        //    List<PayrollTransactionViewModels> list = new List<PayrollTransactionViewModels>();
        //    PayrollTransactionViewModels model = new PayrollTransactionViewModels();
        //    using (var db = new PayrollEntities())
        //    {
        //        var query = from b in db.PayrollTransactions
        //                    join c in db.PayrollPeriods on b.PayrollPeriodId equals c.PayrollPeriodId
        //                    orderby c.PayPeriodFrom descending
        //                    select b;

        //        foreach (var item in query)
        //        {
        //            model = new PayrollTransactionViewModels();
        //            model.PayrollTransactionId = item.PayrollTransactionId;
        //            model.PayrollPeriodId = item.PayrollPeriodId;
        //            model.UserPersonalInformationId = item.UserPersonalInformationId;
        //            model.IncomeId = item.IncomeId;
        //            model.CustomIncomeAmount = item.CustomIncomeAmount;

        //            model.DeductionId = item.DeductionId;
        //            model.CustomDeductionAmount = item.CustomDeductionAmount;
        //            model.TotalDeductionAmount = item.TotalDeductionAmount;
        //            model.TotalIncomeAmount = item.TotalIncomeAmount;

        //            list.Add(model);

        //        }


        //        return list.ToList();
        //    }
        //}

        //public PayrollTransaction GetPayrollTransactionsByUserIdPayrollPeriodId(int userId, int payrollPeriodId)
        //{
            
        //    using (var db = new PayrollEntities())
        //    {
        //        var query = from b in db.PayrollTransactions
        //                    join c in db.PayrollPeriods on b.PayrollPeriodId equals c.PayrollPeriodId
        //                    where (b.PayrollPeriodId == payrollPeriodId) && (b.UserPersonalInformationId == userId)
        //                    select b;




        //        return query.FirstOrDefault();
        //    }
        //}


        //public void InsertPayrollTransaction(PayrollTransactionIncomeViewModels viewModels)
        //{
        //    using (var db = new PayrollEntities())
        //    {
        //        PayrollTransactionIncome payroll = new PayrollTransactionIncome();
        //        payroll.CustomIncomeAmount = viewModels.CustomIncomeAmount;
        //        payroll.IncomeId = viewModels.IncomeId;
        //        payroll.PayrollPeriodId = viewModels.PayrollPeriodId;
        //        payroll.UserPersonalInformationid = viewModels.UserPersonalInformationid;

        //        db.PayrollTransactionIncomes.Add(payroll);
        //        db.SaveChanges();
        //    }
        //}
        //public PayrollTransaction GetPayrollTransactionsByPayrollPeriodId(int id)
        //{
        //    //using (var db = new PayrollEntities())
        //    //{

        //    //    var query = from b in db.PayrollTransactions
        //    //                left join c in db.PayrollPeriods on b.PayrollPeriodId equals c.PayrollPeriodId
        //    //                left join d in db.UserPersonalInformations on b.UserPersonalInformationId equals d.UserPersonalInformationId
        //    //                left join e in db.Departments on b.DepartmentId equals e.DepartmentId
        //    //                left join f in db.Incomes on b.IncomeId equals f.IncomeId
        //    //                left join g in db.Deductions on b.DeductionId equals g.DeductionId
        //    //                where b.PayrollPeriodId == id
        //    //                orderby d.LastName ascending
        //    //                select new
        //    //                {
        //    //                    b.PayrollPeriodId,
        //    //                    b.CustomDeductionAmount,
        //    //                    b.CustomIncomeAmount
        //    //                ,
        //    //                    b.DeductionId,
        //    //                    b.DepartmentId,
        //    //                    b.IncomeId,
        //    //                    b.PayrollTransactionId,
        //    //                    b.TotalDeductionAmount
        //    //                ,
        //    //                    b.TotalIncomeAmount,
        //    //                    b.UserPersonalInformationId,
        //    //                    c.Month,
        //    //                    c.PayPeriodFrom
        //    //                ,
        //    //                    c.WorkHours,
        //    //                    c.WorkDays,
        //    //                    d.EmployeeId,
        //    //                    d.FirstName,
        //    //                    d.LastName,
        //    //                    d.Position,
        //    //                    d.BasicPay
        //    //                ,
        //    //                    e.DepartmentName,
        //    //                    f.IncomeName,
        //    //                    f.IncomeValue,
        //    //                    g.DeductionName,
        //    //                    g.DeductionValue
        //    //                };

        //    //    foreach (var item in query)
        //    //    {
        //    //        model = new PayrollTransactionViewModels();
        //    //        model.PayrollTransactionId = item.PayrollTransactionId;
        //    //        model.PayrollPeriodId = item.PayrollPeriodId;
        //    //        model.UserPersonalInformationId = item.UserPersonalInformationId;
        //    //        model.DepartmentId = item.DepartmentId;
        //    //        model.IncomeId = item.IncomeId;
        //    //        model.CustomIncomeAmount = item.CustomIncomeAmount;

        //    //        model.DeductionId = item.DeductionId;
        //    //        model.CustomDeductionAmount = item.CustomDeductionAmount;
        //    //        model.TotalDeductionAmount = item.TotalDeductionAmount;
        //    //        model.TotalIncomeAmount = item.TotalIncomeAmount;

        //    //        //payroll period
        //    //        model.PayrollPeriodFrom = item.PayPeriodFrom;
        //    //        model.WorkDays = item.WorkDays;
        //    //        model.WorkHours = item.WorkHours;

        //    //        //employee information
        //    //        model.EmployeeId = item.EmployeeId;
        //    //        model.FirstName = item.FirstName;
        //    //        model.LastName = item.LastName;
        //    //        model.Position = item.Position;
        //    //        model.BasicPay = item.BasicPay;

        //    //        //department
        //    //        model.DepartmentName = item.DepartmentName;

        //    //        //income
        //    //        model.IncomeName = item.IncomeName;
        //    //        model.IncomeValue = item.IncomeValue;

        //    //        //deduction
        //    //        model.DeductionName = item.DeductionName;
        //    //        model.DeductionValue = item.DeductionValue;
        //    //        list.Add(model);

        //    //    }


        //    //    return list.ToList();

        //    //    var query = from b in db.PayrollTransactions
        //    //                where b.PayrollPeriodId == id
        //    //                select b;

        //    //    return query.FirstOrDefault();
        //    //}
        //}
    }
}