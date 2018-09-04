using Payroll.Data;
using Payroll.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Payroll.Code
{
    public class PayrollTransactionDeductionBL
    {
        public List<PayrollTransactionDeduction> GetAllPayrollTransactionDeductions(int userPersonalInformationId,
    int payrollPeriodId)
        {
            using (var db = new PayrollEntities())
            {
                var query = from b in db.PayrollTransactionDeductions
                            join c in db.Deductions on b.DeductionId equals c.DeductionId
                            where (b.UserPersonalInformationid == userPersonalInformationId)
                            && (b.PayrollPeriodId == payrollPeriodId)
                            orderby c.Order ascending
                            select b;

                return query.ToList();
            }
        }

        public PayrollTransactionDeduction GetPayrollTransactionDeduction(int userPersonalInformationId,
            int payrollPeriodId, int deductionId)
        {
            using (var db = new PayrollEntities())
            {
                var query = from b in db.PayrollTransactionDeductions
                            join c in db.Deductions on b.DeductionId equals c.DeductionId
                            where (b.UserPersonalInformationid == userPersonalInformationId)
                            && (b.PayrollPeriodId == payrollPeriodId)
                            && (b.DeductionId == deductionId)
                            orderby c.Order ascending
                            select b;

                return query.FirstOrDefault();
            }
        }


        public PayrollTransactionDeduction InsertPayrollTransactionDeductions(PayrollTransactionDeductionViewModels viewModels)
        {
            using (var db = new PayrollEntities())
            {
                PayrollTransactionDeduction payroll = new PayrollTransactionDeduction();
                payroll.CustomDeductionAmount = viewModels.CustomDeductionAmount;
                payroll.DeductionId = viewModels.DeductionId;
                payroll.PayrollPeriodId = viewModels.PayrollPeriodId;
                payroll.UserPersonalInformationid = viewModels.UserPersonalInformationid;

                db.PayrollTransactionDeductions.Add(payroll);
                db.SaveChanges();

                int id = payroll.PayrollTransactionDeductionId;

                return GetPayrollTransactionDeduction(id);
            }
        }

        public PayrollTransactionDeduction GetPayrollTransactionDeduction(int payrollTransactionDeductionId)
        {
            using (var db = new PayrollEntities())
            {
                var query = from b in db.PayrollTransactionDeductions
                            where (b.PayrollTransactionDeductionId == payrollTransactionDeductionId)
                            select b;

                return query.FirstOrDefault();
            }
        }
    }
}