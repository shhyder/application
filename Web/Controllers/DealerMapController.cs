using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Text;

namespace Web.Controllers
{
    public class DealerMapController : Controller
    {
        //
        // GET: /DealerMap/
        public ActionResult Index()
        {
            BusinessLogic.Distributor.Distributor obj = new BusinessLogic.Distributor.Distributor();
            System.Data.DataSet ds =  obj.GetDistributorStateCount();
            StringBuilder strBld = new StringBuilder();
            strBld.Append("[");
            int percentage = 0;
            int id = 1;
            string icon = Url.Content("~/icon/state.png");
            foreach ( DataRow row in ds.Tables[0].Rows )
            {
                percentage = Convert.ToInt32(  Convert.ToDecimal(row["Dealers"]) / (Convert.ToDecimal(row["total_Outlet"]) == 0 ? 1 : Convert.ToInt32(row["total_Outlet"])) * 100 );


                
                strBld.Append( "{");
                strBld.Append("'id': '").Append( id.ToString() ).Append("',");
                strBld.Append("'title': '").Append(row["State"].ToString()).Append("',");
                strBld.Append("'abbr': '").Append(row["Abbr"].ToString()).Append("',");
                strBld.Append( "'state': '").Append( row["State"].ToString() ).Append("',");
                strBld.Append("'region': '").Append(row["Region"].ToString()).Append("',");
                strBld.Append("'totalDealers': '").Append(row["Dealers"].ToString()).Append("',");
                strBld.Append("'totalOutlets': '").Append(row["total_Outlet"].ToString()).Append("',");
                strBld.Append("'totalCities': '").Append(row["total_Outlet"].ToString()).Append("',");
                strBld.Append("'totalJatiaCities': '").Append(row["Presence_cities"].ToString()).Append("',");
                strBld.Append( "'latitude': '").Append( row["Latitude"].ToString() ).Append("',");
                strBld.Append( "'longitude': '").Append( row["Longitude"].ToString() ).Append("',");
                strBld.Append("'icon': '").Append( icon ).Append("',");
                strBld.Append( "'wp': '").Append( "" ).Append("'");
                strBld.Append( "},");
                id++;
            }
            ViewData["Array"] = strBld.Remove(strBld.Length- 1,1).Append("]");
            return View("Index");
        }

    }
}
