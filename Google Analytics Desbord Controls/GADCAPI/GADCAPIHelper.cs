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
    public class GADCAPIHelper
    {

        public static List<ProfileInfo> getProfilesList(string EmailAddress, string Password)
        {
            try
            {
                String FeedUrl = "https://www.google.com/analytics/feeds/accounts/default";

                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(FeedUrl);
                myRequest.Headers.Add("Authorization: GoogleLogin auth=" + Authentication.getSessionTokenClientLogin(EmailAddress, Password));

                HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
                Stream responseBody = myResponse.GetResponseStream();

                Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
                StreamReader readStream = new StreamReader(responseBody, encode);

                XmlDocument resultsXML = new XmlDocument(); resultsXML.LoadXml(readStream.ReadToEnd());
                Debug.WriteLine(resultsXML);

                XmlNamespaceManager nsmgr = new XmlNamespaceManager(resultsXML.NameTable);
                nsmgr.AddNamespace("dxp", "http://schemas.google.com/analytics/2009");
                nsmgr.AddNamespace("", "http://www.w3.org/2005/Atom");


                XmlNodeList entries = resultsXML.GetElementsByTagName("entry");

                List<ProfileInfo> lstProfileInfo = new List<ProfileInfo>();

                for (int i = 0; i < entries.Count; i++)
                {
                    XmlNode xn = null;
                    foreach (XmlNode obj in entries[i].ChildNodes)
                    {
                        if (obj.Name == "title")
                        {
                            xn = obj;
                            break;
                        }
                    }

                    XmlNode title = xn;// entries[i].SelectSingleNode("title");
                    XmlNode profileId = entries[i].SelectSingleNode("dxp:property[@name='ga:profileId']", nsmgr);
                    XmlNode webPropertyId = entries[i].SelectSingleNode("dxp:property[@name='ga:webPropertyId']", nsmgr);

                    lstProfileInfo.Add(new ProfileInfo(title.InnerText.ToString(), webPropertyId.Attributes["value"].Value, profileId.Attributes["value"].Value));
                }

                if (lstProfileInfo.Count > 0)
                    return lstProfileInfo;
                else
                    return null;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public static string getTrackerCode(string WebPrppertyId)
        {
            try
            {
                StringBuilder strResult = new StringBuilder();

                strResult.Append(@"<script type=""text/javascript"">
var gaJsHost = ((""https:"" == document.location.protocol) ? ""https://ssl."" : ""http://www."");
document.write(unescape(""%3Cscript src='"" + gaJsHost + ""google-analytics.com/ga.js' type='text/javascript'%3E%3C/script%3E""));
</script><br />

<script type=""text/javascript"">
try {
var pageTracker = _gat._getTracker(""{0}"");
pageTracker._trackPageview();
} catch(err) {}</script>");

                return strResult.ToString().Replace("{0}",WebPrppertyId);
            }
            catch (Exception ex)
            {

                return String.Empty;
            }
        }
    }

    public class ProfileInfo
    {
        public string URL { get; set; }
        public string WebPropertyId { get; set; }
        public string ProfileId { get; set; }

        public ProfileInfo()
        {
        }
        public ProfileInfo(string URL, string WebPropertyId, string ProfileId)
        {
            this.URL = URL;
            this.WebPropertyId = WebPropertyId;
            this.ProfileId = ProfileId;
        }
    }

}
