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
    internal class WorldMapQuery
    {
        private const String FeedUrl = "https://www.google.com/analytics/feeds/data?ids=ga:{0}&dimensions=ga:country&metrics=ga:visits&start-date={1}&end-date={2}&start-index=1&max-results=10000";

        internal static List<CountryPageViewResultEntity> GetWorldMapQueryResult(String Token, String ProfileID, DateTime StartDate, DateTime EndDate)
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

                List<CountryPageViewResultEntity> Results = new List<CountryPageViewResultEntity>();

                foreach (XmlElement entryElement in resultsXML.DocumentElement.GetElementsByTagName("entry"))
                {
                    String Country = "(not set}";
                    Int32 Visits = 0;
                    XmlElement dimensionElement = (XmlElement)entryElement.GetElementsByTagName("dxp:dimension")[0];
                    Country = dimensionElement.GetAttribute("value");
                    dimensionElement = (XmlElement)entryElement.GetElementsByTagName("dxp:metric")[0];
                    Visits = Int32.Parse(dimensionElement.GetAttribute("value"));

                    Results.Add(new CountryPageViewResultEntity(Country, Visits));
                }

                if (Results.Count > 0)
                    return Results;
                else
                    return null;

            }
            catch (Exception ex)
            {


                return null;
            }
        }
    }

    internal class CountryPageViewResultEntity
    {
        internal CountryPageViewResultEntity(String country, Int32 visitCount)
        {
            _Country = country;
            _VisitCount = visitCount;
        }

        private readonly String _Country;
        private readonly Int32 _VisitCount;

        public String Country
        {
            get { return _Country; }
        }
        public Int32 VisitCount
        {
            get { return _VisitCount; }
        }
    }
}
