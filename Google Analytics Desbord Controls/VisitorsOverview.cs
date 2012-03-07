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
    /// <summary>
    /// This is control to show visits per day with total pages viewd at perticuler day for the profileID
    /// </summary>
    [
    AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Minimal),
    AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal),
    ToolboxData("<{0}:GADVisitorsOverview runat=\"server\"> </{0}:GADVisitorsOverview>")
    ]
    public class VisitorsOverview : Base
    {

        

        public VisitorsOverview()
        {
            FromDate = DateTime.MinValue;
            ToDate = DateTime.MaxValue;
        }


        protected override void OnLoad(EventArgs e)
        {
            //if (String.IsNullOrEmpty(GAProfileId) == false)
            //{
            //    if (HttpContext.Current != null && String.IsNullOrEmpty(GAToken) == false)
            //    {
            //        ResgisterScript();
            //    }
            //    else if (HttpContext.Current != null && String.IsNullOrEmpty(GAEmailAddress) == false && String.IsNullOrEmpty(GAPassword) == false)
            //    {
            //        ResgisterScript();
            //    }
            //}
            base.OnLoad(e);
        }

        protected override void Render(HtmlTextWriter output)
        {
            if (HttpContext.Current != null && String.IsNullOrEmpty(GAProfileId) == false)
                output.Write("<div id='" + this.ClientID + "' style='width: " + Width + "px; height: " + Height + "px;clear:both;'></div>");
            else
                output.Write("<div style=\"width: 350px; height: 250px; display: inline-block; background-color: #f2f2f2; border: solid 1px #333333; text-align: center\">Visitors overview (Design Time)</div>");
            //this.ResgisterScript();
        }

        public string ResgisterScript(string clientID)
        {
            


            AnalyticsService service = new AnalyticsService("AnalyticsSampleApp");
            if (!string.IsNullOrEmpty( GAEmailAddress))
            {
                service.setUserCredentials(GAEmailAddress, GAPassword);
            }

        
            DataQuery query = new DataQuery(dataFeedUrl);
            query.Ids = "ga:" + GAProfileId;
            query.Metrics = "ga:visits,ga:pageviews";
            query.Dimensions = "ga:date";
            query.Sort = "";
            query.GAStartDate = FromDate.ToString("yyyy-MM-dd");
            query.GAEndDate = ToDate.ToString("yyyy-MM-dd");

            DataFeed dataFeed = service.Query(query);

            


           


                List<VisitorsOverviewQueryEntity> ResultData = HttpContext.Current.Cache["VisitorsOverview_" + GAProfileId] as List<VisitorsOverviewQueryEntity>;
                if (ResultData == null)
                {
                    //ResultData = VisitorsOverviewQuery.getVisitorsOverviewQueryResults(GAToken, GAProfileId, FromDate == DateTime.MinValue ? DateTime.Now.AddDays(-31) : FromDate, ToDate > DateTime.Now ? DateTime.Now : ToDate);
                    //HttpContext.Current.Cache.Add("VisitorsOverview_" + GAProfileId, ResultData, null, DateTime.Now.AddHours(2), System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Default, null);
                }

                StringBuilder JavascriptBuilder = new StringBuilder();

                JavascriptBuilder.Append("<script type=\"text/javascript\">");
                JavascriptBuilder.Append("google.load('visualization', '1', {'packages':['annotatedtimeline']});")
                .Append("google.setOnLoadCallback(drawChart);");

            JavascriptBuilder.Append(@"
             
              function drawChart() {
                var data = new google.visualization.DataTable();
                data.addColumn('date', 'Date');
                data.addColumn('number', 'Visite');
                data.addColumn('string', 'title1');
                data.addColumn('string', 'text1');
                data.addColumn('number', 'Page Viewed');
                data.addColumn('string', 'title1');
                data.addColumn('string', 'text1');
                data.addRows([");


                foreach (DataEntry entry in dataFeed.Entries)
                {
                    //entry.Metrics[0].Value;
                    JavascriptBuilder.Append(@"[new Date(" + entry.Dimensions[0].Value.Substring(0, 4) + "," + (Convert.ToInt16(entry.Dimensions[0].Value.Substring(4, 2)) - 1).ToString() + "," + (Convert.ToInt16(entry.Dimensions[0].Value.Substring(6))).ToString() + "), " + entry.Metrics[0].Value.ToString().ToString() + ", undefined, undefined," + entry.Metrics[1].Value.ToString() + ", undefined, undefined],");
                }

                
                
                //foreach (VisitorsOverviewQueryEntity obj in ResultData)
                //{

                //    JavascriptBuilder.Append(@"[new Date(" + obj.Date.Substring(0, 4) + "," + (Convert.ToInt16(obj.Date.Substring(4, 2)) - 1).ToString() + "," + (Convert.ToInt16(obj.Date.Substring(6))).ToString() + "), " + obj.Visit + ", undefined, undefined," + obj.Pages + ", undefined, undefined],");
                //}

                JavascriptBuilder.Remove(JavascriptBuilder.Length - 1, 1);
                JavascriptBuilder.Append(@"]);");

                JavascriptBuilder.Append(@"var chart2 = new google.visualization.AnnotatedTimeLine(document.getElementById('" + clientID + "'));");
                    JavascriptBuilder.Append(@"chart2.draw(data, {displayAnnotations: true,thickness:2,fill:10});
                }
                ");

           JavascriptBuilder.Append("</script>");

            return  JavascriptBuilder.ToString();
            
        }
    }
}
