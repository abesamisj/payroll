using Payroll.Code;
using Payroll.Data;
using Payroll.Models;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Payroll.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {

        public AccountController()
        {
        }
        
        UserBL userBL = new UserBL();
        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            User userLogin = new User() { UserName = user.UserName, Password = user.Password };

            user = userBL.GetByUsernameAndPassword(user);

            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(user.UserName, false);

                var authTicket = new FormsAuthenticationTicket(1, user.UserName, DateTime.Now, DateTime.Now.AddMinutes(20), false, "Admin");
                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                HttpContext.Response.Cookies.Add(authCookie);
                return RedirectToAction("Index", "Home");
            }

            else
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(user);
            }
        }
        //
        // POST: /Account/LogOff
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            TempData["Message"] = null;
            if (ModelState.IsValid)
            {
                try
                {
                    using (var db = new PayrollEntities())
                    {
                        var query = from b in db.Users
                                    where b.UserName == model.UserName
                                    select b;

                        if (query.Count() > 0)
                        {
                            TempData["Message"] = "Username already exists";
                            return RedirectToAction("Register", "Account");
                        }
                        else
                        {
                            string password = Helper.Encrypt(model.Password);
                            var user = new User { UserName = model.UserName, Password = password, UserRoleId = 3 };
                            db.Users.Add(user);
                            db.SaveChanges();
                            
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return RedirectToAction("Index", "Home");
        }

        ////
        //// GET: /Account/ForgotPassword
        //[AllowAnonymous]
        //public ActionResult ForgotPassword()
        //{
        //    return View();
        //}

        ////
        //// POST: /Account/ForgotPassword
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        //var user = await UserManager.FindByNameAsync(model.Email);
        //        //if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
        //        //{
        //        //    // Don't reveal that the user does not exist or is not confirmed
        //        //    return View("ForgotPasswordConfirmation");
        //        //}

        //        // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
        //        // Send an email with this link
        //        // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
        //        // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
        //        // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
        //        // return RedirectToAction("ForgotPasswordConfirmation", "Account");
        //    }

        //    // If we got this far, something failed, redisplay form
        //    return View(model);
        //}

        ////
        //// GET: /Account/ForgotPasswordConfirmation
        //[AllowAnonymous]
        //public ActionResult ForgotPasswordConfirmation()
        //{
        //    return View();
        //}

        ////
        //// GET: /Account/ResetPassword
        //[AllowAnonymous]
        //public ActionResult ResetPassword(string code)
        //{
        //    return code == null ? View("Error") : View();
        //}

        ////
        //// POST: /Account/ResetPassword
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        //{
        //    //if (!ModelState.IsValid)
        //    //{
        //    //    return View(model);
        //    //}
        //    //var user = await UserManager.FindByNameAsync(model.Email);
        //    //if (user == null)
        //    //{
        //    //    // Don't reveal that the user does not exist
        //    //    return RedirectToAction("ResetPasswordConfirmation", "Account");
        //    //}
        //    //var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
        //    //if (result.Succeeded)
        //    //{
        //    //    return RedirectToAction("ResetPasswordConfirmation", "Account");
        //    //}
        //    //AddErrors(result);
        //    //return View();
        //}

        ////
        //// GET: /Account/ResetPasswordConfirmation
        //[AllowAnonymous]
        //public ActionResult ResetPasswordConfirmation()
        //{
        //    return View();
        //}


    }
}