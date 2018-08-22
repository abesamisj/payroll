using Payroll.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Payroll.Code
{
    public class IncomeBL
    {

        public List<Income> GetActiveIncomes()
        {
            using (var db = new PayrollEntities())
            {
                var query = from b in db.Incomes
                            where b.Active == 1
                            orderby b.Order, b.Active ascending
                            select b;

                return query.ToList();
            }
        }

        public List<int> GetActiveIncomeIds()
        {
            var list = new List<int>();
            using (var db = new PayrollEntities())
            {
                var query = from b in db.Incomes
                            where b.Active == 1
                            orderby b.Order, b.Active ascending
                            select b;

                foreach (var item in query)
                {
                    list.Add(item.IncomeId);
                }
                return list;
            }
        }

        public List<Income> GetAllIncomes()
        {
            using (var db = new PayrollEntities())
            {
                var query = from b in db.Incomes
                            orderby b.Order, b.Active ascending
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
            using (var db = new PayrollEntities())
            {
                db.Incomes.Add(income);
                db.SaveChanges();
            }
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
                    result.Order = income.Order;
                    db.SaveChanges();
                }
            }
        }

    }
}