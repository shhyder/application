using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using BusinessLogic;
using System.Text;
using System.Globalization;

namespace Web.Controllers
{
    public class SearchDealerController : SearchController
    {
        //
        // GET: /SearchDealer/
        [ValidateInput(false)]
        public ActionResult Result(FormCollection collection)
        {
            collection = new FormCollection(ConvertRawUrlToQuerystring());
           

            ListPopulate(1,collection);
            return View();
        }


        public ActionResult List(int? id, FormCollection collection)
        {
            collection = new FormCollection(ConvertRawUrlToQuerystring());
            ListPopulate(id, collection);
            return View("List");
        }

    
        public ActionResult ListPopup(int? id, FormCollection collection)
        {
            //collection = new FormCollection(ConvertRawUrlToQuerystring());
            ListPopupPopulate(id, collection);
            return View("ListPopup");
        }

        [NonAction]
        void ListPopupPopulate(int? id, FormCollection collection)
        {
            
            int pageSize = 5;
            int pageCalled = 0;
            if (id == null)
                pageCalled = 1;
            else
                pageCalled = (int)id;

            DataSet.DSParameter ds = (DataSet.DSParameter)System.Web.HttpContext.Current.Cache["data"];

            string filter = " state = '" + collection["state"].ToString() + "'";
            DataRow[] rows = ds.Distributor.Select(filter, " distributor asc");

            int totalItems = rows.Length;

            Web.Model.Pagination pagination = new Web.Model.Pagination(true);

            pagination.BaseUrl = Url.Content("~/SearchDealer/ListPopup/");
            pagination.TrailingQueryString = "state="+collection["state"].ToString();
            pagination.Is_UserClickFunction = true;
            pagination.ClickFunctionName = "GetDealerPage";

            Session["QSCurDL"] = "state=" + collection["state"].ToString();
        
            pagination.TotalRows = totalItems;
            pagination.CurPage = pageCalled;
            pagination.PerPage = pageSize;

            pagination.PrevLink = "Prev";
            pagination.NextLink = "Next";
            pagination.UpdateTargetId = "divLoading";


            string pageLinks = pagination.GetPageLinks();
            int start = (pageCalled - 1) * pagination.PerPage;
            int offset = pagination.PerPage;
            ViewData["pageLinks"] = pageLinks;

            ViewData["Count"] = totalItems;

            //////ViewData["startLatitude"] = startLatitude.ToString();
            //////ViewData["startLongitude"] = startLongitude.ToString();



            IQueryable<DataRow> dt = rows.Cast<DataRow>().AsQueryable();
            ViewData[UIDealerSearch.listDealer.ToString()] = dt.Skip(start).Take(offset).ToList();// dt.Skip((pageCalled) * pageSize).Take(pageSize).ToList();


        }



        void ListPopulate(int? id, FormCollection collection)
        {
            int distance = 0;

            string postalCode = collection[UISearch.txtzipcode.ToString()].ToString();// CultureInfo.InvariantCulture string.Format("{D5}", "210");// "210".ToString("D5");

            if (base.IsNumeric(postalCode))
            {
                postalCode = String.Format("{0:d5}", Convert.ToInt32(postalCode));
                distance = Convert.ToInt32(collection[UISearch.cobDistance.ToString()].ToString());
            }


            string product_IDs = "";


            foreach (string col_id in collection)
            {
                if (col_id.Contains("pro-"))
                {
                    product_IDs += col_id.Replace("pro-", "") + ",";
                }
            }

            if (product_IDs.Length > 0)
            {
                product_IDs += product_IDs.Remove(product_IDs.LastIndexOf(","));
            }
           
            decimal startLatitude=0;
            decimal startLongitude=0;
            bool is_Valid_Postal_Code = true;

            BusinessLogic.Search.Search ser = new BusinessLogic.Search.Search();
            System.Data.DataSet ds = ser.GetProductDealerList(postalCode, distance, product_IDs, ref startLatitude, ref startLongitude,ref is_Valid_Postal_Code);




            int pageSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PageSize"].ToString());
            int pageCalled = 0;
            if (id == null)
                pageCalled = 1;
            else
                pageCalled = (int)id;



            base.Activity((int)Activities.viewList, "postalCode=" + postalCode + "  distance= " + distance.ToString() + " miles" + " product_IDs = " + product_IDs, Convert.ToInt32(id));


            //BusinessLogic.Search.Search obj = new BusinessLogic.Search.Search();
            //obj._ID = 1;
            //System.Data.DataSet ds = obj.GetSearchCritariaBy(1);
            DataRow[] rows = ds.Tables["List"].Select(""," distance asc");



            int totalItems = rows.Length;

            Web.Model.Pagination pagination = new Web.Model.Pagination(true);

            pagination.BaseUrl = Url.Content("~/SearchDealer/List/");
            pagination.TrailingQueryString = "?" +  GetRawUrl() ;

            Session["QSCurDL"] = "?" + HttpUtility.UrlDecode( GetRawUrl() );
            ViewData["product_IDs"] = product_IDs;

            pagination.TotalRows = totalItems;
            pagination.CurPage = pageCalled;
            pagination.PerPage = pageSize;

            pagination.PrevLink = "Prev";
            pagination.NextLink = "Next";
            pagination.UpdateTargetId = "mainSearchPanel";


            string pageLinks = pagination.GetPageLinks();
            int start = (pageCalled - 1) * pagination.PerPage;
            int offset = pagination.PerPage;
            ViewData["pageLinks"] = pageLinks;

            ViewData["Count"] = totalItems;

            ViewData["startLatitude"] = startLatitude.ToString();
            ViewData["startLongitude"] = startLongitude.ToString();



            IQueryable<DataRow> dt = ds.Tables["List"].Rows.Cast<DataRow>().AsQueryable();
            ViewData[ UIDealerSearch.listDealer.ToString()] = dt.Skip(start).Take(offset).ToList();// dt.Skip((pageCalled) * pageSize).Take(pageSize).ToList();
            

        }


        public ActionResult Route(int? id, FormCollection collection)
        {
            collection = new FormCollection(ConvertRawUrlToQuerystring());



            string product_IDs = "";


            foreach (string col_id in collection)
            {
                if (col_id.Contains("pro-"))
                {
                    product_IDs += col_id.Replace("pro-", "") + ",";
                }
            }

            if (product_IDs.Length > 0)
            {
                product_IDs += product_IDs.Remove(product_IDs.LastIndexOf(","));
            }


            ViewData["product_IDs"] = product_IDs;








            base.Activity((int)Activities.clickDealer, "Route", Convert.ToInt32(id));


            ViewData["startLatitude"] = collection["strLat"];
            ViewData["startLongitude"] = collection["strLog"];
            ViewData["endLatitude"] = collection["endLat"];
            ViewData["endLongitude"] = collection["endLog"];


            BusinessLogic.Distributor.Distributor obj = new BusinessLogic.Distributor.Distributor();
            obj._ID = Convert.ToInt32(collection["id"]);
            DataSet.DSParameter.DistributorRow row = ( DataSet.DSParameter.DistributorRow )  obj.Get().Distributor.Select( " Distributor_ID = " + obj._ID )[0];

            ViewData["Distributor"] = row.Distributor;
            ViewData["Address"] = row.Address ;

            string dist = collection["dist"].Replace("/Route", "").ToString();

            if (dist.LastIndexOf('.') >= 0)
            {
                dist = dist.Remove(dist.LastIndexOf('.'));
            }

            ViewData["Phone1"] = row["Phone1"];
            ViewData["Phone2"] = row["Phone2"];
            ViewData["Website"] = row["Website"];

            
            ViewData["Distance"] = dist;
            ViewData["Email"] = row.Email;
            ViewData["Distributor_ID"] = row["Distributor_ID"];

            ViewData["Querystring"] = Request.RawUrl.Substring(Request.RawUrl.LastIndexOf('?') + 1);
        
            //ListPopulate(id, collection);
            return View("RouteDirection");
        }



        public ActionResult Direction(int? id, FormCollection collection)
        {
            collection = new FormCollection(ConvertRawUrlToQuerystring());

            base.Activity((int)Activities.clickDealer, "Route", Convert.ToInt32(id));


            ViewData["startLatitude"] = collection["strLat"];
            ViewData["startLongitude"] = collection["strLog"];
            ViewData["endLatitude"] = collection["endLat"];
            ViewData["endLongitude"] = collection["endLog"];


            BusinessLogic.Distributor.Distributor obj = new BusinessLogic.Distributor.Distributor();
            obj._ID = Convert.ToInt32(collection["id"]);

            DataSet.DSParameter.DistributorRow row = (DataSet.DSParameter.DistributorRow)obj.Get().Distributor.Select(" Distributor_ID = " + obj._ID)[0];

            ViewData["Distributor"] = row.Distributor;
            ViewData["Address"] = row.Address;

            string dist = collection["dist"].Replace("/Route", "").ToString();

            if (dist.LastIndexOf('.') >= 0)
            {
                dist = dist.Remove(dist.LastIndexOf('.'));
            }
            ViewData["Distributor_ID"] = row["Distributor_ID"];
            ViewData["Phone1"] = row["Phone1"];
            ViewData["Phone2"] = row["Phone2"];
            ViewData["Website"] = row["Website"];


            ViewData["Distance"] = dist;
            ViewData["Email"] = row.Email;
            ViewData["Querystring"] = Request.RawUrl.Substring(Request.RawUrl.LastIndexOf('?') + 1);

            //ListPopulate(id, collection);
            return View("RouteDirection");
        }






    }
}
