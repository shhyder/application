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
    public class SearchController : _BaseController
    {
        //
        // GET: /Search/
        
        public ActionResult Index(int? id)
        {
            ViewData["ZipCode"] = id.ToString();

            base.Activity((int)Activities.clickSearch, "Zipcode=" + ViewData["ZipCode"].ToString(), 0);


            BusinessLogic.Search.Search obj = new BusinessLogic.Search.Search();
            obj._ID = 1;
            System.Data.DataSet ds = obj.GetSearchCritariaBy(1);
            DataRow[] rows = ds.Tables["List"].Select("");

            //getLocation(Request.ServerVariables["REMOTE_ADDR"].Trim());


            IQueryable<DataRow> dt = ds.Tables["List"].Rows.Cast<DataRow>().AsQueryable();
            ViewData[UIProductType.listProductType.ToString()] = dt.ToList();
            return SetViewPage("Index");
        }

        protected string getLocation(string strIPAddress)
        {
            strIPAddress = "110.37.10.155";

            WebRequest rssReq = WebRequest.Create("http://freegeoip.appspot.com/xml/" + strIPAddress);

            //Create a Proxy
            WebProxy px = new WebProxy("http://freegeoip.appspot.com/xml/" + strIPAddress, true);

            //Assign the proxy to the WebRequest
            rssReq.Proxy = px;

            //Set the timeout in Seconds for the WebRequest
            rssReq.Timeout = 2000;


            try
            {

                //Get the WebResponse

                WebResponse rep = rssReq.GetResponse();

                //Read the Response in a XMLTextReader

                XmlTextReader xtr = new XmlTextReader(rep.GetResponseStream());

                //Create a new DataSet

                System.Data.DataSet ds = new System.Data.DataSet();

                //Read the Response into the DataSet
                //<Status>true</Status>
                //<Ip>72.14.247.141</Ip>
                //<CountryCode>US</CountryCode>
                //<CountryName>United States</CountryName>
                //<RegionCode>CA</RegionCode>
                //<RegionName>California</RegionName>
                //<City>Mountain View</City>
                //<ZipCode>94043</ZipCode>
                //<Latitude>37.4192</Latitude>
                //<Longitude>-122.057</Longitude>

                ds.ReadXml(xtr);
                DataTable dt = ds.Tables[0];

                if (dt != null && dt.Rows.Count > 0)
                {

                    if (dt.Rows[0]["Status"].ToString().Equals("true", StringComparison.CurrentCultureIgnoreCase))
                    {
                        return dt.Rows[0]["CountryCode"].ToString();

                    }
                }
                return String.Empty;
            }
            catch
            {
                return String.Empty;
            }

        }

        public ActionResult PopUpSingle(int? id)
        {
            base.Activity((int)Activities.popUpSearch, "ProductId=" + id.ToString(), Convert.ToInt32(id));

            ViewData["productID"] = "pro-" + id.ToString();
            return View("PopUpSingle");
        }

        public ActionResult PopUpMultiple()
        {
            base.Activity((int)Activities.popUpSearch, "ProductId=" , 0);
            DataSet.DS _ds = (DataSet.DS)Session[UIProductSearch.svSelectedProduct.ToString()];

            string queryString = "";
            foreach (DataRow row in _ds.SelectedProduct.Rows)
            {
                queryString += "&pro-" + row["Product_ID"].ToString() + "=" + row["Product_ID"].ToString() ;
            }

            ViewData["QueryString"] = queryString;
            //ViewData["productID"] = "pro-" + id.ToString();
            return View("PopUpMultiple");
        }


        public ActionResult Store(int? id)
        {
            ViewData["Distributor_ID"] = id;
            ViewData["Is_Direct_Link"] = true;
            ViewData["product_IDs"] = "0,0";
            ViewData["Item_Link"] = "/Search/Store/" + id;
            return View();
        }




        public ActionResult ProductList(FormCollection collection)
        {
          
            int distance = 0;

            collection = new FormCollection(ConvertRawUrlToQuerystring());
            string postalCode = collection[UISearch.txtzipcode.ToString()].ToString();// CultureInfo.InvariantCulture string.Format("{D5}", "210");// "210".ToString("D5");

            if (base.IsNumeric(postalCode))
            {
                postalCode = String.Format("{0:d5}", Convert.ToInt32(postalCode));
                distance = Convert.ToInt32(collection[UISearch.cobDistance.ToString()].ToString());
            }
            else
            {
                return Content("");
            }

            base.Activity((int)Activities.viewList, "postalCode=" + postalCode + "  distance= " + distance.ToString() + " miles", 0);


            BusinessLogic.Search.Search ser = new BusinessLogic.Search.Search();
            if (!ser.ValidateZipcode(postalCode))
            {
                return Content("");
            }


            System.Data.DataSet ds = ser.GetProductList(postalCode, distance, "", "", "");

            

            ViewData["Querystring"] = Request.Form; ;

            ViewData["Querystring"] = Server.UrlEncode(ViewData["Querystring"].ToString());


            IQueryable<DataRow> dt = ds.Tables["List"].Rows.Cast<DataRow>().AsQueryable();
            ViewData[UIProductType.listProductType.ToString()] = dt.ToList();
            return View();
        }

        public ActionResult DealerList(FormCollection collection)
        {
           
           
            return View();
        }

        public ViewDataDictionary DealerLocator(FormCollection collection)
        {
            
            string postalCode = collection[UISearch.txtzipcode.ToString()].ToString();// CultureInfo.InvariantCulture string.Format("{D5}", "210");// "210".ToString("D5");
            int distance = 0;

            if( base.IsNumeric( postalCode ) )
            {
                 postalCode = String.Format("{0:d5}", Convert.ToInt32( postalCode ) );
                 distance = Convert.ToInt32(collection[UISearch.cobDistance.ToString()].ToString());
            }
            else
            {
                return InvalidPostalCode();
            }
            
            BusinessLogic.Search.Search ser = new BusinessLogic.Search.Search();
            if (!ser.ValidateZipcode(postalCode))
            {
                return InvalidPostalCode();
            }

            string product_IDs = "";

            int product_Count = 0;
            foreach( string id in collection)
            {
                if( id.Contains("pro-")  )
                {
                    product_IDs += id.Replace("pro-","") + ",";
                    product_Count++;
                }
            }

            if (product_IDs.Length > 0)
            {
                product_IDs = product_IDs.Remove(product_IDs.LastIndexOf(","));
            }

            base.Activity((int)Activities.viewList, "postalCode=" + postalCode + "  distance= " + distance.ToString() + " miles" + " product_IDs = " + product_IDs, 0);


            ViewData["Total_Dealers"] = ser.GetProductDealerCount(postalCode, distance, product_IDs);
            ViewData["Total_Products"] = product_Count.ToString();
            ViewData["product_IDs"] = product_IDs;

            //ViewData["Querystring"] = Request.Form.ToString(); ;

            ViewData["Querystring"] = "data=" + HttpUtility.UrlEncode( Request.Form.ToString() );//ViewData["Querystring"].ToString());

            //ViewData["Querystring"] = Request.Form;


            return ViewData;
        }

        ViewDataDictionary InvalidPostalCode()
        {
            ViewData["Total_Dealers"] = "0";
            ViewData["Total_Products"] = "0";
            ViewData["product_IDs"] = "";
            ViewData["Querystring"] = "data=";
            return ViewData;
        }


        public ActionResult DealerCount(FormCollection collection)
        {

            string postalCode = collection[UISearch.txtzipcode.ToString()].ToString();// CultureInfo.InvariantCulture string.Format("{D5}", "210");// "210".ToString("D5");
            int distance = 0;

            if (base.IsNumeric(postalCode))
            {
                postalCode = String.Format("{0:d5}", Convert.ToInt32(postalCode));
                distance = Convert.ToInt32(collection[UISearch.cobDistance.ToString()].ToString());
            }
            else
            {
                return Content("");
            }

            BusinessLogic.Search.Search ser = new BusinessLogic.Search.Search();
            if (!ser.ValidateZipcode(postalCode))
            {
                return Content("");
            }


            string product_IDs = "";

            int product_Count = 0;
            foreach (string id in collection)
            {
                if (id.Contains("pro-"))
                {
                    product_IDs += id.Replace("pro-", "") + ",";
                    product_Count++;
                }
            }

            if (product_IDs.Length > 0)
            {
                product_IDs = product_IDs.Remove(product_IDs.LastIndexOf(","));
            }

           

            return Content(  ser.GetProductDealerCount(postalCode, distance, product_IDs).ToString() );
        }

        public JsonResult GetDealer(int? id, FormCollection collection)
        {
            //FormCollection  collection = new FormCollection(ConvertRawUrlToQuerystring());
          
            Widget obj = new Widget();
            obj.Id = 123;
            obj.Name = "naamemem";
            obj.Price = 324;
            ViewDataDictionary vdd = ListPopulateSearch(id,collection);
            obj.DealerLocator = RenderViewToStringAsHTML(this, "DealerLocator", DealerLocator(collection));
            obj.DealerList = RenderViewToStringAsHTML(this, "ListSearch", vdd );
            obj.latitude = Convert.ToDecimal( vdd["startLatitude"] );
            obj.longitude = Convert.ToDecimal( vdd["startLongitude"] );


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
                    icon = Url.Content("~/MarkerIcon.ashx?label=" + sr_No.ToString() );
                    //dealer["Distributor"].ToString().Trim()
                    vddMarker["distance"] = dealer["distance"];
                    vddMarker["Distributor_ID"] = dealer["Distributor_ID"].ToString();
                    vddMarker["Item_Link"] = "/Search/Store/" + dealer["Distributor_ID"].ToString();
                    title = dealer["Distributor"].ToString().Trim() + "     " + Convert.ToInt32(dealer["distance"]).ToString() + " miles";
                    obj.Markers[id2 - 1] = new Marker(id2, Convert.ToDecimal(dealer["Latitude"]),
                        Convert.ToDecimal(dealer["Longitude"]), title, icon, RenderViewToStringAsHTML(this, "Detail", vddMarker),1,
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
                obj.longitude, "Central Location", icon,"",3,"");
            
            //if (vdd[UIDealerSearch.listDealer.ToString()] != null)
            obj.totalMarker = Convert.ToInt32(ViewData["Count"]);
            
            return Json(obj, JsonRequestBehavior.AllowGet);
        }



        public ActionResult GetDealerListPage(int? id, FormCollection collection)
        {
            //FormCollection  collection = new FormCollection(ConvertRawUrlToQuerystring());

            Widget obj = new Widget();
            obj.Id = 123;
            obj.Name = "naamemem";
            obj.Price = 324;
            ViewDataDictionary vdd = ListPopulateSearch(id, collection);
            obj.DealerLocator = RenderViewToStringAsHTML(this, "DealerLocator", DealerLocator(collection));
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



            string title = "";
            if (vdd[UIDealerSearch.listDealer.ToString()] != null)
            {
                obj.Markers = new Marker[(vdd[UIDealerSearch.listDealer.ToString()] as List<DataRow>).Count + 1];
                foreach (var dealer in (vdd[UIDealerSearch.listDealer.ToString()] as List<DataRow>))
                {
                    icon = Url.Content("~/icon/0" + id2.ToString() + ".png");
                    title = dealer["Distributor"].ToString().Trim() + "     " + Convert.ToInt32(dealer["distance"]).ToString() + " miles";
                    obj.Markers[id2 - 1] = new Marker(id2, Convert.ToDecimal(dealer["Latitude"]),
                        Convert.ToDecimal(dealer["Longitude"]), title, icon,"",1,"");
                    id2++;
                }
            }
            else
            {
                obj.Markers = new Marker[1];
            }

            icon = Url.Content("~/icon/home.png");
            obj.Markers[id2 - 1] = new Marker(id2, obj.latitude,
                obj.longitude, "Central Location", icon,"",3,"");

            //if (vdd[UIDealerSearch.listDealer.ToString()] != null)
            obj.totalMarker = Convert.ToInt32(ViewData["Count"]);

            return View("GetDealerListPage");
        }

        public ActionResult Detail(int? id)
        {
            ViewData["Distributor_ID"] = id;
            return View("Detail");
        }


        public ActionResult ListSearch(int? id, FormCollection collection)
        {
            ListPopulateSearch(id, collection);
            return View("ListSearch");
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

            decimal startLatitude = 0;
            decimal startLongitude = 0;
            bool is_Valid_Postal_Code = true;

            BusinessLogic.Search.Search ser = new BusinessLogic.Search.Search();
            System.Data.DataSet ds = ser.GetProductDealerList(postalCode, distance, product_IDs, ref startLatitude, ref startLongitude, ref is_Valid_Postal_Code);




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
           

            base.Activity((int)Activities.viewList, "postalCode=" + postalCode + "  distance= " + distance.ToString() + " miles" + " product_IDs = " + product_IDs, Convert.ToInt32(id));


            // no product find and postal code not valid
            if ( !is_Valid_Postal_Code)
            {
                ViewData["Error"] = "Enter valid U.S Zip code";
                return ViewData;
            }


            string error = "No stores were found in your search. Your product may be available at one of our national, regional, or online distributors below. All products are available from Jatai.net.";


            if( ds.Tables.Count == 0 )
            {
                ViewData["Error"] = error;
                return ViewData;
            }

            if (ds.Tables["List"].Rows.Count == 0 )
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
            pagination.ClickFunctionName = "javascript:GetDealerListFromNavigator";
            pagination._class = " class=gradbutton ";

            Session["QSCurDL"] = ConvertCollectionToQuerystring(collection);
            ViewData["queryString"] = ConvertCollectionToQuerystring(collection);
            ViewData["product_IDs"] = product_IDs;

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




        public class Widget
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public decimal Price { get; set; }
            public string DealerLocator { get; set; }
            public string DealerListHeader { get; set; }
            public string DealerListNavigator { get; set; }
            public string DealerList{ get; set; }
            public decimal latitude { get; set; }
            public decimal longitude{ get; set; }
            public string Error { get; set; }
            public int totalMarker { get; set; }
            public Marker[] Markers{ get; set; }
            
        }

       

    }

    public class Marker
    {
        public int id { get; set; }
        public decimal latitude { get; set; }
        public decimal longitude { get; set; }
        public string title { get; set; }
        public string icon { get; set; }
        public string detail { get; set; }
        public int zIndex { get; set; }
        public string facebook { get; set; }

        public Marker(int _id, decimal _latitude, decimal _longitude,
            string _title, string _icon, string _detail, int _zIndex,string _facebook)
        {
            id = _id;
            latitude = _latitude;
            longitude = _longitude;
            title = _title;
            icon = _icon;
            detail = _detail;
            zIndex = _zIndex;
            facebook = _facebook;
        }
    }
}
