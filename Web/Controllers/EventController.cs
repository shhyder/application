using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using BusinessLogic;
using System.Text;
using System.Globalization;
using System.Net;
using System.Xml;

namespace Web.Controllers
{
    public class EventController : _BaseController
    {
        //
        // GET: /Event/

        public ActionResult Index(int? id)
        {
            ViewData["ZipCode"] = id.ToString();

            base.Activity((int)Activities.clickSearch, "Zipcode=" + ViewData["ZipCode"].ToString(), 0);
            BusinessLogic.Search.Search obj = new BusinessLogic.Search.Search();
            return View();
        }


        public ActionResult Event(int? id)
        {
            ViewData["Event_ID"] = id;
            ViewData["Is_Direct_Link"] = true;
            ViewData["product_IDs"] = "0,0";
            ViewData["Item_Link"] = "/Event/Event/" + id;
            return View();
        }



        [NonAction]
        ViewDataDictionary ListPopulateSearch(int? id, FormCollection collection)
        {
            int distance = 0;

            string postalCode = collection[UISearch.txtzipcode.ToString()].ToString();

            if (base.IsNumeric(postalCode))
            {
                postalCode = String.Format("{0:d5}", Convert.ToInt32(postalCode));
                distance = Convert.ToInt32(collection[UISearch.cobDistance.ToString()].ToString());
                ViewData["Error"] = "";
            }
            else
            {
                ViewData["Error"] = "Enter valid Zip code";
                return ViewData;
            }


            string start_Date = "";
            string end_Date = "";


            decimal startLatitude = 0;
            decimal startLongitude = 0;
            bool is_Valid_Postal_Code = true;


            switch (collection["cobPeriod"].ToString())
            {
                // upcomming Events
                case "1":
                    start_Date = DateTime.Now.ToShortDateString();
                    end_Date = null;
                    break;

                // All Events
                case "0":
                    start_Date = null;
                    end_Date = null;
                    break;

                // Past event
                case "-1":
                    start_Date = null;
                    end_Date = DateTime.Now.ToShortDateString();
                    break;

            }



            BusinessLogic.Search.Event ser = new BusinessLogic.Search.Event();
            System.Data.DataSet ds = ser.GetEventList(postalCode, distance, ref startLatitude, ref startLongitude, ref is_Valid_Postal_Code,
                start_Date, end_Date);




            int pageSize = 5;
            int pageCalled = 0;
            if (id == null)
                pageCalled = 1;
            else
                pageCalled = (int)id;


            if (collection["cobRows"] != null)
            {
                pageSize = Convert.ToInt32(collection["cobRows"]);
            }

            base.Activity((int)Activities.viewList, "postalCode=" + postalCode + "  distance= " + distance.ToString() + " miles" + " product_IDs = (0)", Convert.ToInt32(id));


            // no product find and postal code not valid
            if (!is_Valid_Postal_Code)
            {
                ViewData["Error"] = "Enter valid U.S Zip code";
                return ViewData;
            }


            string error = "No stores event found in your search. Your product may be available at one of our national, regional, or online distributors below. All products are available from Jatai.net.";


            if (ds.Tables.Count == 0)
            {
                ViewData["Error"] = error;
                return ViewData;
            }

            if (ds.Tables["List"].Rows.Count == 0)
            {
                ViewData["Error"] = error;
                return ViewData;
            }


            DataRow[] rows = ds.Tables["List"].Select("", " distance asc");

            int totalItems = rows.Length;




            Web.Model.Pagination pagination = new Web.Model.Pagination(true);
            pagination.BaseUrl = Url.Content("~/SearchDealer/ListSearch/");
            pagination.TrailingQueryString = ConvertCollectionToQuerystring(collection);

            pagination.TrailingQueryString = ConvertCollectionToQuerystring(collection);
            pagination.Is_UserClickFunction = true;
            pagination.ClickFunctionName = "javascript:GetEventList";
            pagination._class = " class=gradbutton ";

            Session["QSCurDL"] = ConvertCollectionToQuerystring(collection);
            ViewData["queryString"] = ConvertCollectionToQuerystring(collection);
            ViewData["product_IDs"] = "";

            pagination.TotalRows = totalItems;
            pagination.CurPage = pageCalled;
            pagination.PerPage = pageSize;

            pagination.PrevLink = "Prev";
            pagination.NextLink = "Next";
            pagination.UpdateTargetId = "dealerList";


            string pageLinks = pagination.GetPageLinks();
            int start = (pageCalled - 1) * pagination.PerPage;
            int offset = pagination.PerPage;
            ViewData["pageLinks"] = pageLinks;

            ViewData["Count"] = totalItems;
            ViewData["PageSize"] = pageSize;
            ViewData["startLatitude"] = startLatitude.ToString();
            ViewData["startLongitude"] = startLongitude.ToString();
            ViewData["start"] = (start + 1).ToString();

            IQueryable<DataRow> dt = ds.Tables["List"].Rows.Cast<DataRow>().AsQueryable();
            ViewData[UIDealerSearch.listDealer.ToString()] = dt.Skip(start).Take(offset).ToList();// dt.Skip((pageCalled) * pageSize).Take(pageSize).ToList();
            return ViewData;
        }



        public JsonResult GetEvent(int? id, FormCollection collection)
        {
            //FormCollection  collection = new FormCollection(ConvertRawUrlToQuerystring());

            Widget obj = new Widget();
            obj.Id = 123;
            obj.Name = "naamemem";
            obj.Price = 324;
            ViewDataDictionary vdd = ListPopulateSearch(id, collection);
            obj.DealerList = RenderViewToStringAsHTML(this, "ListSearch", vdd);
          

            if (ViewData["Error"].ToString().Length == 0)
            {
                obj.DealerListNavigator = RenderViewToStringAsHTML(this, "Navigator", vdd);
                obj.DealerListHeader = RenderViewToStringAsHTML(this, "ListSearchHeader", vdd);
            }
            else
            {
                obj.DealerListNavigator = "";
                obj.DealerListHeader = "";
            }



            obj.latitude = Convert.ToDecimal(vdd["startLatitude"]);
            obj.longitude = Convert.ToDecimal(vdd["startLongitude"]);


            StringBuilder strBld = new StringBuilder();


            int id2 = 1;
            string icon = Url.Content("~/icon/state.png");
            obj.Error = vdd["Error"].ToString();

            int sr_No = Convert.ToInt32(ViewData["start"]);

            string title = "";
            ViewDataDictionary vddMarker = new ViewDataDictionary();
            vddMarker.Add("distance", 0);
            vddMarker.Add("Distributor_ID", 0);
            vddMarker.Add("product_IDs", vdd["product_IDs"]);


            if (vdd[UIDealerSearch.listDealer.ToString()] != null)
            {
                obj.Markers = new Marker[(vdd[UIDealerSearch.listDealer.ToString()] as List<DataRow>).Count + 1];
                foreach (var dealer in (vdd[UIDealerSearch.listDealer.ToString()] as List<DataRow>))
                {
                    icon = Url.Content("~/MarkerIcon.ashx?label=" + sr_No.ToString());
                    //dealer["Distributor"].ToString().Trim()
                    vddMarker["distance"] = dealer["distance"];
                    vddMarker["Event_ID"] = dealer["Event_ID"].ToString();
                    vddMarker["Item_Link"] = "/Event/Event/" + dealer["Event_ID"].ToString();

                    title = dealer["Event"].ToString().Trim() + "     " + Convert.ToInt32(dealer["distance"]).ToString() + " miles";
                    obj.Markers[id2 - 1] = new Marker(id2, Convert.ToDecimal(dealer["Latitude"]),
                        Convert.ToDecimal(dealer["Longitude"]), title, icon, RenderViewToStringAsHTML(this, "Detail", vddMarker), 1,
                        RenderViewToStringAsHTML(this, "Facebook", vddMarker));
                    id2++;
                    sr_No++;
                }
            }
            else
            {
                obj.Markers = new Marker[1];
            }

            icon = Url.Content("~/icon/home.png");
            obj.Markers[id2 - 1] = new Marker(id2, obj.latitude,
                obj.longitude, "Central Location", icon, "", 1,"");

            //if (vdd[UIDealerSearch.listDealer.ToString()] != null)
            obj.totalMarker = Convert.ToInt32(ViewData["Count"]);

            return Json(obj, JsonRequestBehavior.AllowGet);
        }


        public class Widget
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public decimal Price { get; set; }
            public string DealerLocator { get; set; }
            public string DealerListHeader { get; set; }
            public string DealerListNavigator { get; set; }
           
            public string DealerList { get; set; }
            public decimal latitude { get; set; }
            public decimal longitude { get; set; }
            public string Error { get; set; }
            public int totalMarker { get; set; }
            public Web.Controllers.Marker[] Markers { get; set; }

        }


    }
}
