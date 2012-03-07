using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Xml;
using System.Diagnostics;
using Google.GData.Analytics;
using Google.GData.Extensions;


namespace GADCAPI
{

    internal class VisitorsOverviewQuery
    {
        private const String FeedUrl = "https://www.google.com/analytics/feeds/data?ids=ga:{0}&dimensions=ga:date&metrics=ga:visits,ga:pageviews&start-date={1}&end-date={2}&start-index=1&max-results=10000";

        public static List<VisitorsOverviewQueryEntity> getVisitorsOverviewQueryResults(String Token, String ProfileID, DateTime StartDate, DateTime EndDate)
        {
            try
            {
                String RequestUrl = String.Format(FeedUrl, new object[] { ProfileID, StartDate.ToString("yyyy-MM-dd"), EndDate.ToString("yyyy-MM-dd") });


               


                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(RequestUrl);
                myRequest.Headers.Add("Authorization: GoogleLogin auth=" + Token);

                HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
                Stream responseBody = myResponse.GetResponseStream();

                Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
                StreamReader readStream = new StreamReader(responseBody, encode);

                XmlDocument resultsXML = new XmlDocument(); resultsXML.LoadXml(readStream.ReadToEnd());
                Debug.WriteLine(resultsXML);

                XmlNamespaceManager nsmgr = new XmlNamespaceManager(resultsXML.NameTable);
                nsmgr.AddNamespace("dxp", "http://schemas.google.com/analytics/2009");

                XmlNodeList entries = resultsXML.GetElementsByTagName("entry");

                List<VisitorsOverviewQueryEntity> lstVisitorsOverviewQueryEntity = new List<VisitorsOverviewQueryEntity>();

                for (int i = 0; i < entries.Count; i++)
                {
                    XmlNode nodeDate = entries[i].SelectSingleNode("dxp:dimension[@name='ga:date']", nsmgr);
                    XmlNode nodeVisits = entries[i].SelectSingleNode("dxp:metric[@name='ga:visits']", nsmgr);
                    XmlNode nodePageViews = entries[i].SelectSingleNode("dxp:metric[@name='ga:pageviews']", nsmgr);


                    lstVisitorsOverviewQueryEntity.Add(new VisitorsOverviewQueryEntity(nodeDate.Attributes["value"].Value, nodeVisits.Attributes["value"].Value, nodePageViews.Attributes["value"].Value));
                }

                if (lstVisitorsOverviewQueryEntity.Count > 0)
                    return lstVisitorsOverviewQueryEntity;
                else
                    return null;

            }
            catch (Exception ex)
            {

                return null;
            }
        }
    }

    internal class VisitorsOverviewQueryEntity
    {
        internal VisitorsOverviewQueryEntity()
        {
        }
        internal VisitorsOverviewQueryEntity(String date, String visit, String Pages)
        {
            _Date = date;
            _Visit = visit;
            _Pages = Pages;
        }

        private String _Date;
        private String _Visit;
        private String _Pages;

        public String Date
        {
            get { return _Date; }
            set { _Date = value; }
        }

        public String Visit
        {
            get { return _Visit; }
            set { _Visit = value; }
        }

        public String Pages
        {
            get { return _Pages; }
            set { _Pages = value; }
        }
    }
}
