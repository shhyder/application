using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    [HandleError]
    public class HomeController : _BaseController
    {
        public ActionResult Index()
        {
            ViewData["Message"] = "Welcome to ASP.NET MVC!";

            ViewData["txtErrorMessage"] = "";

            return RedirectToAction("Index", "Search");//

            //return View();
        }

        public ActionResult About()
        {
            return View();
        }


        public ActionResult Home(string txtZipcode)
        {
            
            base.Activity( ( int ) Activities.clickSearch, "Zipcode=" + txtZipcode, 0);
            if (base.IsNumeric(txtZipcode))
            {
                txtZipcode = String.Format("{0:d5}", Convert.ToInt32(txtZipcode));
            }
            else
            {
                ViewData["txtErrorMessage"] = "Enter valid US zipcode";
                return View("Index");
            }

            BusinessLogic.Search.Search ser = new BusinessLogic.Search.Search();
            if (!ser.ValidateZipcode(txtZipcode))
            {
                ViewData["txtErrorMessage"] = "Enter valid US zipcode";
                return View("Index");
            }

            return RedirectToAction("Index", "Search", new { id = txtZipcode });
        }
    }
}
