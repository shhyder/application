using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Permissions;
using Google.GData.Analytics;
using Google.GData.Extensions;


namespace GADCAPI
{
    [
    AspNetHostingPermission(SecurityAction.Demand,Level = AspNetHostingPermissionLevel.Minimal),
    AspNetHostingPermission(SecurityAction.InheritanceDemand,Level = AspNetHostingPermissionLevel.Minimal),
    ToolboxData("<{0}:GADWorldMap runat=\"server\"> </{0}:GADWorldMap>")
    ]
    public class WorldMap : Base
    {
       

        public WorldMap()
        {
            FromDate = DateTime.MinValue;
            ToDate = DateTime.MaxValue;
        }

        protected override void OnLoad(EventArgs e)
        {
            if (String.IsNullOrEmpty(GAProfileId) == false)
            {
                if (HttpContext.Current != null && String.IsNullOrEmpty(GAToken) == false)
                {
                   // ResgisterScript();
                }
                else if (HttpContext.Current != null && String.IsNullOrEmpty(GAEmailAddress) == false && String.IsNullOrEmpty(GAPassword) == false)
                {
                    //ResgisterScript();
                }
            }
            base.OnLoad(e);
        }

        protected override void Render(HtmlTextWriter output)
        {
            if (HttpContext.Current != null && String.IsNullOrEmpty(GAProfileId) == false)
                output.Write("<div id='" + this.ClientID + "'></div>");
            else
                output.Write("<div style=\"width: 350px; height: 250px; display: inline-block; background-color: #f2f2f2; border: solid 1px #333333; text-align: center\">Visitors overview (Design Time)</div>");

        }

        public string ResgisterScript(string clientID)
        {
            AnalyticsService service = new AnalyticsService("AnalyticsSampleApp");
            if (!string.IsNullOrEmpty(GAEmailAddress))
            {
                service.setUserCredentials(GAEmailAddress, GAPassword);
            }



            DataQuery query = new DataQuery(dataFeedUrl);
            query.Ids = "ga:" + GAProfileId;
            query.Metrics = "ga:visits";
            query.Dimensions = "ga:country";
            query.Sort = "";
            query.GAStartDate = FromDate.ToString("yyyy-MM-dd");
            query.GAEndDate = ToDate.ToString("yyyy-MM-dd");

            DataFeed dataFeed = service.Query(query);


            //if (String.IsNullOrEmpty(GAToken) == false)
            {

               

                StringBuilder JavascriptBuilder = new StringBuilder();
                JavascriptBuilder.Append("<script type=\"text/javascript\">");
                JavascriptBuilder.Append(@"

   google.load('visualization', '1', {'packages': ['geomap']});
   google.setOnLoadCallback(drawMap);

    function drawMap() {
      var data = new google.visualization.DataTable();
      data.addRows(" + dataFeed.Entries.Count.ToString() + @");
      data.addColumn('string', 'Country');
      data.addColumn('number', 'Popularity');
");
                Int32 CountryIndex = 0;
//                foreach (CountryPageViewResultEntity dataEntity in ResultData)
//                {
//                    JavascriptBuilder.Append(@"
//      data.setValue(" + CountryIndex.ToString() + @", 0, '" + dataEntity.Country + @"');
//      data.setValue(" + CountryIndex.ToString() + @", 1, " + dataEntity.VisitCount.ToString() + @");
//");
//                    CountryIndex++;
//                }


                foreach (DataEntry entry in dataFeed.Entries)
                {
                    //entry.Metrics[0].Value;
                    JavascriptBuilder.Append(@"
      data.setValue(" + CountryIndex.ToString() + @", 0, '" + entry.Dimensions[0].Value.ToString()+ @"');
      data.setValue(" + CountryIndex.ToString() + @", 1, " + entry.Metrics[0].Value.ToString() + @");
");
                    CountryIndex++;
                }


                JavascriptBuilder.Append(@"
      var options = {};
      options['dataMode'] = 'regions';");
                JavascriptBuilder.Append(@"options['width'] = '" + Width + "';");
                JavascriptBuilder.Append(@"options['height'] = '" + Height + "';");
                JavascriptBuilder.Append(@"var container = document.getElementById('" + clientID + @"');
      var geomap = new google.visualization.GeoMap(container);
      geomap.draw(data, options);
  };
");

                JavascriptBuilder.Append("</script>");

                return JavascriptBuilder.ToString();
            }

        }
    }
}
