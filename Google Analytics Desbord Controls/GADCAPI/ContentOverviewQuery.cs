using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Xml;
using System.Diagnostics;

namespace GADCAPI
{
    
    internal class ContentOverviewQuery
    {
        private const String FeedUrl = "https://www.google.com/analytics/feeds/data?ids=ga:{0}&dimensions=ga:pagePath,ga:pageTitle&metrics=ga:pageviews&sort=-ga:pageviews&start-date={1}&end-date={2}&start-index=1&max-results=10000";

        public static List<ContentOverviewEntity> getContentOverviewQueryResults(String Token, String ProfileID, DateTime StartDate, DateTime EndDate)
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

                List<ContentOverviewEntity> lstContentOverviewEntity = new List<ContentOverviewEntity>();

                for (int i = 0; i < entries.Count; i++)
                {
                    XmlNode nodePagePath = entries[i].SelectSingleNode("dxp:dimension[@name='ga:pagePath']", nsmgr);
                    XmlNode nodePageViews = entries[i].SelectSingleNode("dxp:metric[@name='ga:pageviews']", nsmgr);

                    ContentOverviewEntity obj = lstContentOverviewEntity.SingleOrDefault(c => c.PagePath == nodePagePath.Attributes["value"].Value);

                    if (obj == null)
                    {

                        lstContentOverviewEntity.Add(new ContentOverviewEntity(nodePagePath.Attributes["value"].Value, Convert.ToInt32(nodePageViews.Attributes["value"].Value)));
                    }
                    else
                    {
                        obj.AddCountToPageView(Convert.ToInt32(nodePageViews.Attributes["value"].Value));
                    }
                }

                if (lstContentOverviewEntity.Count > 0)
                    return lstContentOverviewEntity;
                else
                    return null;

            }
            catch (Exception ex)
            {

                return null;
            }
        }
    }

    internal class ContentOverviewEntity
    {

        private String _PagePath = "";
        private Int32 _PageViews = 0;

        internal ContentOverviewEntity(String PagePath, Int32 PageViews)
        {
            _PagePath = PagePath;
            _PageViews = PageViews;
        }

        public String PagePath
        {
            get { return _PagePath; }
            set { _PagePath = value; }
        }

        public Int32 PageViews
        {
            get { return _PageViews; }
            set { _PageViews = value; }
        }

        public void AddCountToPageView(Int32 count)
        {
            this.PageViews += count;
        }

    }
}
