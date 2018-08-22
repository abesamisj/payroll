using Payroll.Data;
using System.Collections.Generic;
using System.Linq;

namespace Payroll.Code
{
    public class DeductionBL
    {
        public List<Deduction> GetActiveDeductions()
        {
            using (var db = new PayrollEntities())
            {
                var query = from b in db.Deductions
                            where b.Active == 1
                            orderby b.Order, b.Active ascending
                            select b;

                return query.ToList();
            }
        }

        public List<Deduction> GetAllDeductions()
        {
            using (var db = new PayrollEntities())
            {
                var query = from b in db.Deductions
                            orderby b.Order, b.Active ascending
                            select b;

                return query.ToList();
            }
        }

        public Deduction GetDeductionById(int deductionId)
        {
            using (var db = new PayrollEntities())
            {
                var query = from b in db.Deductions
                            where b.DeductionId == deductionId
                            select b;

                return query.FirstOrDefault();
            }
        }

        public void InsertDeduction(Deduction deduction)
        {
            using (var db = new PayrollEntities())
            {
                db.Deductions.Add(deduction);
                db.SaveChanges();
            }
        }
        public void UpdateDeduction(Deduction deduction)
        {
            using (var db = new PayrollEntities())
            {
                var result = db.Deductions.SingleOrDefault(b => b.DeductionId == deduction.DeductionId);
                if (result != null)
                {
                    result.Active = deduction.Active;
                    result.DeductionDescription = deduction.DeductionDescription;
                    result.DeductionName = deduction.DeductionName;
                    result.DeductionValue = deduction.DeductionValue;
                    result.Order = deduction.Order;
                    db.SaveChanges();
                }
            }
        }
    }
}