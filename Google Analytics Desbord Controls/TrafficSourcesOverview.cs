using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Security.Permissions;
using Google.GData.Analytics;
using Google.GData.Extensions;


namespace GADCAPI
{
    [
    AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Minimal),
    AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal),
    ToolboxData("<{0}:GADTrafficSourcesOverview runat=\"server\"> </{0}:TrafficSourcesOverview>")
    ]
    public class TrafficSourcesOverview : Base
    {
        
       

  

        protected override void OnLoad(EventArgs e)
        {
            if (String.IsNullOrEmpty(GAProfileId) == false)
            {
                if (HttpContext.Current != null && String.IsNullOrEmpty(GAToken) == false)
                {
                    //ResgisterScript();
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
                output.Write("<div style=\"width: 350px; height: 250px; display: inline-block; background-color: #f2f2f2; border: solid 1px #333333; text-align: center\">Traffic Sources Overview (Design Time)</div>");

        }

        public string ResgisterScript( string clientID)
        {


            AnalyticsService service = new AnalyticsService("AnalyticsSampleApp");
            if (!string.IsNullOrEmpty(GAEmailAddress))
            {
                service.setUserCredentials(GAEmailAddress, GAPassword);
            }


            //"https://www.google.com/analytics/feeds/data?ids=ga:{0}&dimensions=ga:source,ga:medium&metrics=ga:visits&sort=-ga:visits&start-date={1}&end-date={2}&start-index=1&max-results=10000";

            DataQuery query = new DataQuery(dataFeedUrl);
            query.Ids = "ga:" + GAProfileId;
            query.Metrics = "ga:visits";
            query.Dimensions = "ga:source,ga:medium";
            query.Sort = "-ga:visits";
            query.GAStartDate = FromDate.ToString("yyyy-MM-dd");
            query.GAEndDate = ToDate.ToString("yyyy-MM-dd");

            DataFeed dataFeed = service.Query(query);

           

           
           
           
                //Hashtable ResultData = HttpContext.Current.Cache["TrafficSourcesOverview_" + GAProfileId] as Hashtable;
                //if (ResultData == null)
                //{
                //    ResultData = TrafficSourcesOverviewQuery.getTrafficSourcesOverviewQueryResults(GAToken, GAProfileId, FromDate == DateTime.MinValue ? DateTime.Now.AddDays(-31) : FromDate, ToDate > DateTime.Now ? DateTime.Now : ToDate);
                //    HttpContext.Current.Cache.Add("TrafficSourcesOverview_" + GAProfileId, ResultData, null, DateTime.Now.AddHours(2), System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Default, null);
                //}

                StringBuilder JavascriptBuilder = new StringBuilder();
                JavascriptBuilder.Append("<script type=\"text/javascript\">");
                JavascriptBuilder.Append(@"
      google.load('visualization', '1', {packages:['piechart']});
      google.setOnLoadCallback(drawPieChart);
      var chart;
      function drawPieChart() {
        var data = new google.visualization.DataTable();
        data.addColumn('string', 'Task');
        data.addColumn('number', 'Hours per Day');");
                JavascriptBuilder.Append(@"data.addRows(" + dataFeed.Entries.Count.ToString() + ");");

                
                //IDictionaryEnumerator en = dataFeed.GetEnumerator();
                //while (en.MoveNext())
                //{
                //    string key = en.Key.ToString();
                //    string val = en.Value.ToString();

                //    key = key == "(none)" ? "Direct" : key;

                //    JavascriptBuilder.Append(@"data.setValue(" + count.ToString() + ",0,'" + key + "');data.setValue(" + count.ToString() + ",1," + val + ");");
                //    count++;
                //}

                int count = 0;
                foreach (DataEntry entry in dataFeed.Entries)
                {
                    string key = entry.Dimensions[0].Value;
                    string val = entry.Metrics[0].Value;
                    key = key == "(none)" ? "Direct" : key;
                    //entry.Metrics[0].Value;
                    //JavascriptBuilder.Append(@"[new Date(" + entry.Dimensions[0].Value.Substring(0, 4) + "," + (Convert.ToInt16(entry.Dimensions[0].Value.Substring(4, 2)) - 1).ToString() + "," + (Convert.ToInt16(entry.Dimensions[0].Value.Substring(6))).ToString() + "), " + entry.Metrics[0].Value.ToString().ToString() + ", undefined, undefined," + entry.Metrics[1].Value.ToString() + ", undefined, undefined],");
                    JavascriptBuilder.Append(@"data.setValue(" + count.ToString() + ",0,'" + key + "');data.setValue(" + count.ToString() + ",1," + val + ");");
                    count++;
                }



                JavascriptBuilder.Append(@"chart = new google.visualization.PieChart(document.getElementById('" + clientID + "'));");
                JavascriptBuilder.Append(@"chart.draw(data, {width: " + Width + ", height: " + Height + ", is3D: true, title: 'Traffic Source Overview'});");

                JavascriptBuilder.Append(@"google.visualization.events.addListener(chart, 'onmouseover',DisplayData);
                }      
                function DisplayData(e) {
                    chart.setSelection([e]);
                }
                ");

                JavascriptBuilder.Append("</script>");

               
            return JavascriptBuilder.ToString();
        }
    }
}
