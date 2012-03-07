using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace Web.Controllers
{
    public class ImageController : Controller
    {
        //
        // GET: /Image/

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Upload()
        {
            var r = new System.Collections.Generic.List<ViewDataUploadFilesResult>();

            ViewDataUploadFilesResult vs = new ViewDataUploadFilesResult();


            foreach (string inputTagName in Request.Files)
            {
                HttpPostedFileBase file = Request.Files[inputTagName];
                if (file.ContentLength > 0)
                {
                    string filePath = Path.Combine(HttpContext.Server.MapPath("../Uploads")
                        , Path.GetFileName(file.FileName));
                    file.SaveAs(filePath);
                }
            }



            return this.Json(vs, JsonRequestBehavior.AllowGet);
        }


        public class ViewDataUploadFilesResult
        {
            public string Thumbnail_url { get; set; }
            public string Name { get; set; }
            public int Length { get; set; }
            public string Type { get; set; }
        }

    }
}
