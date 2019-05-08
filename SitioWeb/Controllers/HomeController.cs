using SitioWeb.Methods;
using SitioWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SitioWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Contact person)
        {
            string EncodedResponse = Request.Form["g-Recaptcha-Response"];
            bool IsCaptchaValid = (ReCaptcha.Validate(EncodedResponse) == "True" ? true : false);

            if (IsCaptchaValid)
            {
                Methods.Email email = new Methods.Email();
                email.SendEmail(person, "Contacto", "Cicor");
            }
            return View();
        }
    }
}