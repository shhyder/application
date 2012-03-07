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
    public class Base:Control
    {
        protected String _GAEmailAddress = "";
        protected String _GAPassword = "";
        protected String _GAProfileId = "";
        protected String _GAToken = "";
        protected DateTime _FromDate = DateTime.MinValue;
        protected DateTime _ToDate = DateTime.MaxValue;
        protected String _Width = "700";
        protected String _Height = "240";

        [DefaultValue("700"), Browsable(true), Bindable(true), Category("Dimension"), Description("Width of graph.")]
        public String Width
        {
            get { return _Width; }
            set { _Width = value; }
        }
        [DefaultValue("240"), Browsable(true), Bindable(true), Category("Dimension"), Description("Height of graph")]
        public String Height
        {
            get { return _Height; }
            set { _Height = value; }
        }

        [DefaultValue(""), Browsable(true), Bindable(true), Category("Analytics Account"), Description("This is the profile ID for the Google Analytics profile that you would like to display the data from.  The profile ID can be found by going to the setttings page of the profile on the Google Analytics website.")]
        public String GAProfileId
        {
            get { return _GAProfileId; }
            set { _GAProfileId = value; }
        }
        [DefaultValue(""), Browsable(true), Bindable(true), Category("Analytics Account"), Description("The password to your Google Analytics account.")]
        public String GAPassword
        {
            get { return _GAPassword; }
            set { _GAPassword = value; }
        }
        [DefaultValue(""), Browsable(true), Bindable(true), Category("Analytics Account"), Description("The email address you use to login to your Google Analytics account.")]
        public String GAEmailAddress
        {
            get { return _GAEmailAddress; }
            set { _GAEmailAddress = value; }
        }
        [Browsable(true), Bindable(true), Category("Analytics Data"), Description("The start date for the time range in which to gather data.")]
        public DateTime FromDate
        {
            get { return _FromDate; }
            set { _FromDate = value; }
        }
        [Browsable(true), Bindable(true), Category("Analytics Data"), Description("The end date for the time range in which to gather data.")]
        public DateTime ToDate
        {
            get { return _ToDate; }
            set { _ToDate = value; }
        }
        [DefaultValue(""), Browsable(true), Bindable(true), Category("Analytics Token"), Description("Token use to login to your Google Analytics account.")]
        public String GAToken
        {
            get { return _GAToken; }
            set { _GAToken = value; }
        }


        protected const string dataFeedUrl = "https://www.google.com/analytics/feeds/data";


        public string TrafficSourcesOverview(string clientID)
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


        public string WorldMap(string clientID)
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
      data.setValue(" + CountryIndex.ToString() + @", 0, '" + entry.Dimensions[0].Value.ToString() + @"');
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


        public string VisitorsOverview(string clientID)
        {



            AnalyticsService service = new AnalyticsService("AnalyticsSampleApp");
            if (!string.IsNullOrEmpty(GAEmailAddress))
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







            HttpContext.Current.Cache["VisitorsOverview_" + GAProfileId] = dataFeed;
            
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
                JavascriptBuilder.Append(@"[new Date(" + entry.Dimensions[0].Value.Substring(0, 4) + "," + (Convert.ToInt16(entry.Dimensions[0].Value.Substring(4, 2)) - 1).ToString() + "," + (Convert.ToInt16(entry.Dimensions[0].Value.Substring(6))).ToString() + "), " + entry.Metrics[0].Value.ToString().ToString() + ", undefined, undefined," + entry.Metrics[1].Value.ToString() + ", undefined, undefined],");
            }



            

            JavascriptBuilder.Remove(JavascriptBuilder.Length - 1, 1);
            JavascriptBuilder.Append(@"]);");

            JavascriptBuilder.Append(@"var chart2 = new google.visualization.AnnotatedTimeLine(document.getElementById('" + clientID + "'));");
            JavascriptBuilder.Append(@"chart2.draw(data, {displayAnnotations: true,thickness:2,fill:10});
                }
                ");

            JavascriptBuilder.Append("</script>");

            return JavascriptBuilder.ToString();

        }


        public string GetHTMLVisitorsOverview()
        {
           


                DataFeed dataFeed = HttpContext.Current.Cache["VisitorsOverview_" + GAProfileId] as DataFeed;

                if (dataFeed != null)
                {

                    StringBuilder JavascriptBuilder = new StringBuilder();
                    JavascriptBuilder.Append(@"<table><tr><th width='300'>Pages</th><th>Page Views</th></tr>");

                    foreach (DataEntry entry in dataFeed.Entries)
                    {
                       
                        JavascriptBuilder.Append(@"<tr><td>" + entry.Dimensions[0].Value + "</td><td align='right'>" + entry.Metrics[0].Value + "</td></tr>");
                    }


                    JavascriptBuilder.Append(@"</table>");

                    return JavascriptBuilder.ToString();
                }
                else
                {
                    return "";
                }
           
        }




        public string ContentOverview()
        {
            AnalyticsService service = new AnalyticsService("AnalyticsSampleApp");
            if (!string.IsNullOrEmpty(GAEmailAddress))
            {
                service.setUserCredentials(GAEmailAddress, GAPassword);
            }


            DataQuery query = new DataQuery(dataFeedUrl);
            query.Ids = "ga:" + GAProfileId;
            query.Metrics = "ga:pageviews";
            query.Dimensions = "ga:pagePath,ga:pageTitle";
            query.Sort = "-ga:pageviews";
            query.GAStartDate = FromDate.ToString("yyyy-MM-dd");
            query.GAEndDate = ToDate.ToString("yyyy-MM-dd");

            DataFeed dataFeed = service.Query(query);

     
            if ( dataFeed == null)
            {

                    StringBuilder JavascriptBuilder = new StringBuilder();
                    JavascriptBuilder.Append(@"<table><tr><th width='300'>Pages</th><th>Page Views</th></tr>");


                    foreach (DataEntry entry in dataFeed.Entries)
                    {
                        JavascriptBuilder.Append(@"<tr><td>" + entry.Dimensions[0].Value + "</td><td align='right'>" + entry.Metrics[0].Value.ToString() + "</td></tr>");
                    }

                   

                    JavascriptBuilder.Append(@"</table>");

                    return JavascriptBuilder.ToString();
                
            }
            else
            {
                return "";
            }
        }

    }
}
