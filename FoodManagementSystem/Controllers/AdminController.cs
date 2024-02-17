using System;
using System.Web.Mvc;
using EmployeeManagementSystem.Models;
using AdminLibrary;
namespace EmployeeManagementSystem.Controllers
{
    public class AdminController : Controller
    {
        private readonly AdminDAL _adminDAL = new AdminDAL();
        public ActionResult AdminRegister()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AdminRegister(AdminModel adminModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    AdminMaster admin = new AdminMaster
                    {
                        AdminName = adminModel.AdminName,
                        Email = adminModel.Email,
                        Password = adminModel.Password
                    };

                    _adminDAL.AddAdmin(admin);
                    return RedirectToAction("Index", "FoodItem");
                }
                return View(adminModel);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMsg = ex.Message;
                return View(adminModel);
            }
        }
        public ActionResult AdminLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AdminLogin(AdminModel loginModel)
        {
            try
            {
                bool isValidLogin = _adminDAL.ValidateAdminLogin(loginModel.Email, loginModel.Password);

                if (isValidLogin)
                {
                    return RedirectToAction("Index", "FoodItem");
                }
                ViewBag.ErrorMsg = "Invalid email or password.";
                return View(loginModel);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMsg = ex.Message;
                return View(loginModel);
            }
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(AdminModel model )
        {
            try
            {
                bool isEmailValid = _adminDAL.ValidateAdminMail(model.Email);

                if (isEmailValid)
                {
                    bool passwordUpdated = _adminDAL.UpdateAdminPassword(model.Email, model.NewPassword);
                    if (passwordUpdated)
                    {
                        return RedirectToAction("UpdatePwdMessage");
                    }
                    else
                    {
                        ViewBag.ErrorMsg = "Failed to update the password. Please try again.";
                    }
                }
                else
                {
                    return RedirectToAction("AdminRegister");
                }

                return View();
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMsg = ex.Message;
                return View();
            }
        }
        public ActionResult UpdatePwdMessage()
        {
            return View();
        }
    }
}
