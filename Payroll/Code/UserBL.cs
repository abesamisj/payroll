using Payroll.Data;
using Payroll.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Payroll.Code
{
    public class UserBL
    {
        PayrollEntities context = new PayrollEntities();
        public User GetByUsernameAndPassword(User user)
        {
            string passwordFromDB = "";
            using (var db = new PayrollEntities())
            {
                var query = from b in db.Users
                            where b.UserName == user.UserName
                            select b;
                passwordFromDB = Helper.Decrypt(query.FirstOrDefault().Password);
            }
            
            return context.Users.Where(u => u.UserName == user.UserName & passwordFromDB == user.Password).FirstOrDefault();
        }
    }
}