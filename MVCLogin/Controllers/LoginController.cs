using MVCLogin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCLogin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Autherize(MVCLogin.Models.Customer customer)
        {
            using (mMvcCrudEntities db = new mMvcCrudEntities())
            {
                var customerDetail = db.Customers.Where(x => x.Name == customer.Name && x.Description == customer.Description).FirstOrDefault();
                if (customerDetail == null)
                {
                    customer.LoginErrorMessage = "wrong Username or password.";
                    return View("Index", customer);
                }
                else
                {
                    Session["Id"] = customer.Id;
                    Session["Name"] = customer.Name;
                    Session["Description"] = customer.Description;
                    return RedirectToAction("Index", "Home");//redireccionamos a otra vista, primer parametro es el metodo home y el segundo es el nombre del controlador
                }
            }
        }

        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }

    }
}