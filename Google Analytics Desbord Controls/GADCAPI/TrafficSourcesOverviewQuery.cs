using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Xml;
using System.IO;
using System.Diagnostics;
using System.Collections;

namespace GADCAPI
{

    internal class TrafficSourcesOverviewQuery
    {
        private const String FeedUrl = "https://www.google.com/analytics/feeds/data?ids=ga:{0}&dimensions=ga:source,ga:medium&metrics=ga:visits&sort=-ga:visits&start-date={1}&end-date={2}&start-index=1&max-results=10000";

        public static Hashtable getTrafficSourcesOverviewQueryResults(String Token, String ProfileID, DateTime StartDate, DateTime EndDate)
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


                Hashtable h1 = new Hashtable();

                for (int i = 0; i < entries.Count; i++)
                {
                    XmlNode nodeMedium = entries[i].SelectSingleNode("dxp:dimension[@name='ga:medium']", nsmgr);
                    XmlNode nodeVisits = entries[i].SelectSingleNode("dxp:metric[@name='ga:visits']", nsmgr);

                    string Medium = nodeMedium.Attributes["value"].Value;
                    string visit = nodeVisits.Attributes["value"].Value;

                    if (h1.Contains(Medium) == false)
                    {
                        h1.Add(Medium, Convert.ToInt32(visit));
                    }
                    else
                    {
                        h1[Medium] = Convert.ToInt32(h1[Medium]) + Convert.ToInt32(visit);
                    }
                }

                if (h1.Count > 0)
                    return h1;
                else
                    return null;

            }
            catch (Exception ex)
            {

                return null;
            }
        }
    }
}
