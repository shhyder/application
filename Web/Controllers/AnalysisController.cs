using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class AnalysisController : Controller
    {
        //
        // GET: /Analysis/
        [Authorize]
        public ActionResult Index()
        {
            GADCAPI.Base report  = new GADCAPI.Base();

            report.GAEmailAddress = "shhyder@gmail.com";
            report.GAPassword = "has537167";
            report.GAProfileId = "2125192";// "UA-1205308-1";
            report.FromDate = DateTime.Now.AddDays(-25);
            report.ToDate = DateTime.Now.AddDays(-1);


            ViewData["VisitorsOverview"] = report.VisitorsOverview("VisitorsOverview") ;
            ViewData["TrafficSourcesOverview"] = report.TrafficSourcesOverview("TrafficSourcesOverview");
            ViewData["WorldMap"] = report.WorldMap("WorldMap");
            ViewData["ContentOverview"] = report.ContentOverview();
            return View();
        }

    }
}
