using Payroll.Data;
using Payroll.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Payroll.Code
{
    public class PayrollTransactionIncomeBL
    {
        public List<PayrollTransactionIncome> GetAllPayrollTransactionIncomes(int userPersonalInformationId, 
            int payrollPeriodId)
        {
            using (var db = new PayrollEntities())
            {
                var query = from b in db.PayrollTransactionIncomes
                            join c in db.Incomes on b.IncomeId equals c.IncomeId
                            where (b.UserPersonalInformationid == userPersonalInformationId)
                            && (b.PayrollPeriodId == payrollPeriodId)
                            orderby c.Order ascending
                            select b;

                return query.ToList();
            }
        }

        public PayrollTransactionIncome GetPayrollTransactionIncome(int userPersonalInformationId,
            int payrollPeriodId, int incomeId)
        {
            using (var db = new PayrollEntities())
            {
                var query = from b in db.PayrollTransactionIncomes
                            join c in db.Incomes on b.IncomeId equals c.IncomeId
                            where (b.UserPersonalInformationid == userPersonalInformationId)
                            && (b.PayrollPeriodId == payrollPeriodId)
                            && (b.IncomeId == incomeId)
                            orderby c.Order ascending
                            select b;

                return query.FirstOrDefault();
            }
        }


        public PayrollTransactionIncome InsertPayrollTransactionIncomes(PayrollTransactionIncomeViewModels viewModels)
        {
            using (var db = new PayrollEntities())
            {
                PayrollTransactionIncome payroll = new PayrollTransactionIncome();
                payroll.CustomIncomeAmount = viewModels.CustomIncomeAmount;
                payroll.IncomeId = viewModels.IncomeId;
                payroll.PayrollPeriodId = viewModels.PayrollPeriodId;
                payroll.UserPersonalInformationid = viewModels.UserPersonalInformationid;

                db.PayrollTransactionIncomes.Add(payroll);
                db.SaveChanges();

                int id = payroll.PayrollTransactionIncomeId;

                return GetPayrollTransactionIncome(id);
            }
        }

        public PayrollTransactionIncome GetPayrollTransactionIncome(int payrollTransactionIncomeId)
        {
            using (var db = new PayrollEntities())
            {
                var query = from b in db.PayrollTransactionIncomes
                            where (b.PayrollTransactionIncomeId == payrollTransactionIncomeId)
                            select b;

                return query.FirstOrDefault();
            }
        }
    }
}