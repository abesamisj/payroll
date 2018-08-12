using Payroll.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Payroll.Code
{
    public class IncomeBL
    {
        PayrollEntities context = new PayrollEntities();

        public List<Income> GetActiveIncomes()
        {
            using (var db = new PayrollEntities())
            {
                var query = from b in db.Incomes
                            where b.Active == 1
                            orderby b.Active descending
                            select b;

                return query.ToList();
            }
        }

        public List<Income> GetAllIncomes()
        {
            using (var db = new PayrollEntities())
            {
                var query = from b in db.Incomes
                            orderby b.Active descending
                            select b;

                return query.ToList();
            }
        }

        public Income GetIncomesById(int incomeId)
        {
            using (var db = new PayrollEntities())
            {
                var query = from b in db.Incomes
                            where b.IncomeId == incomeId
                            select b;

                return query.FirstOrDefault();
            }
        }

        public void InsertIncome(Income income)
        {
            context.Incomes.Add(income);
            context.SaveChanges();
        }

        public void UpdateIncome(Income income)
        {
            using (var db = new PayrollEntities())
            {
                var result = db.Incomes.SingleOrDefault(b => b.IncomeId == income.IncomeId);
                if (result != null)
                {
                    result.Active = income.Active;
                    result.IncomeDescription = income.IncomeDescription;
                    result.IncomeName = income.IncomeName;
                    result.IncomeValue = income.IncomeValue;
                    db.SaveChanges();
                }
            }
        }

    }
}