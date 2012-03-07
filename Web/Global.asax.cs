using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Caching;


namespace Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static System.Web.Caching.Cache _cache = null;

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );


            routes.MapRoute(
               "AttributeVariant",                                              // Route name
               "AttributeVariant/NewProductType/{product_Type_ID}/{attribute_ID}",                           // URL with parameters
               new { controller = "AttributeVariant", action = "NewProductType", product_Type_ID = "", attribute_ID = "" }  // Parameter defaults
           );


            routes.MapRoute(
              "SetCustomerStatus",                                              // Route name
              "Customer/SetCustomerStatus/{id}/{is_Active}",                           // URL with parameters
              new { controller = "Customer", action = "SetCustomerStatus", id = "", is_Active = "" }  // Parameter defaults
          );

        }

      
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterRoutes(RouteTable.Routes);


            _cache = Context.Cache;

            BusinessLogic.Parameter.Parameter par = new BusinessLogic.Parameter.Parameter();
            DataSet.DSParameter ds = par.GetParamerter();


            System.TimeSpan span = new TimeSpan(0, 0, 2, 20, 0);

            _cache.Insert("data", ds, null,
          Cache.NoAbsoluteExpiration, span,
          CacheItemPriority.Default,
          new CacheItemRemovedCallback(RefreshData));
        }

        

        public static void RefreshData(String key, Object item,
                CacheItemRemovedReason reason)
        {

          
            try
            {
                
               

                lock (_cache)
                {
                    BusinessLogic.Parameter.Parameter par = new BusinessLogic.Parameter.Parameter();
                    DataSet.DSParameter ds = par.GetParamerter();
                    System.TimeSpan span = new TimeSpan(0, 0, 30, 20, 0);


                    //if( ds != null )
                    //    _cache["data"] = ds;
                    
                    
                    _cache.Insert("data", ds, null,
                        Cache.NoAbsoluteExpiration, span,
                        CacheItemPriority.High,
                        new CacheItemRemovedCallback(RefreshData));
                }
            }
            catch
            {

            }

        }

        void Session_Start()
        {
            //BusinessLogic.Tracking.Visit  obj = new BusinessLogic.Tracking.Visit();
            //DataSet.DS ds = new DataSet.DS();
            //ds.Visit.AddVisitRow(DateTime.Now, DateTime.Now, "", Request.ServerVariables["REMOTE_ADDR"].ToString());
            //obj.New(ds);

            //Session["Session_ID"] = obj._ID;
            //Session["Activity_Sequence"] = 0;
            
        }

        void Session_End()
        {
            //BusinessLogic.Tracking.Visit obj = new BusinessLogic.Tracking.Visit();
            //DataSet.DS ds = new DataSet.DS();
            //ds.Visit.AddVisitRow(DateTime.Now, DateTime.Now, "", Request.ServerVariables["REMOTE_ADDR"].ToString()).Visit_ID = Convert.ToInt32( Session["Session_ID"] );
            //obj.Update(ds);

        }
        
    }
}