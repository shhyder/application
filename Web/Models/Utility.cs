using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using System.Text;

namespace Web.Model
{
    public static class Utility
    {
        //public static bool IsNumeric(string text)
        //{
        //    return Regex.IsMatch(text, "^\\d+$");
        //}

        public static bool Is_Session_Active()//bool Is_Session_Active
        {

            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated &&
                System.Web.HttpContext.Current.Session["Employee_ID"] != null)
            {
                if (System.Web.HttpContext.Current.Session["Employee_ID"] == null)
                    System.Web.HttpContext.Current.Session["Employee_ID"] = System.Web.HttpContext.Current.User.Identity.Name.Split('|')[1];
                //System.Web.Security.FormsAuthentication.SignOut();
                //bool val =  ;

                return true;
            }
            else
                return false;
        }
        public static string Get_User()
        {


            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                return System.Web.HttpContext.Current.User.Identity.Name.Split('|')[2];
            }
            else
            {
                return "";
            }

        }

        public static string Get_Path()
        {
            string temp = "";
            if (System.Web.HttpContext.Current.Request.Url.AbsolutePath == "/")
            {
                temp = System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);
                temp = temp.Remove(temp.Length - 1);
            }
            else
                temp = System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path).Replace(System.Web.HttpContext.Current.Request.Url.AbsolutePath, "") + Web.Model.Utility.GetGSite();

            return temp;
        }

        public static string GetGSite()
        {
            return System.Configuration.ConfigurationManager.AppSettings["GSite"].ToString();
        }

        public static string HightLigthText(string str,bool is_Category_Search,string search)
        {
            if (!is_Category_Search && search.Trim().Length == 0 )
                return str;

            StringBuilder strb = new StringBuilder(str);
            string repStr = "<SPAN style='BACKGROUND-COLOR: #ffff00'>" + search + "</SPAN>";
            if( search.Length != 0 )
                strb.Replace(search, repStr);
            return strb.ToString();
        }


        public static bool IsNumeric(string anyString)
        {
            if (anyString == null)
            {
                anyString = "";
            }
            if (anyString.Length > 0)
            {
                double dummyOut = new double();
                System.Globalization.CultureInfo cultureInfo =
                    new System.Globalization.CultureInfo("en-US", true);

                return Double.TryParse(anyString, System.Globalization.NumberStyles.Any,
                    cultureInfo.NumberFormat, out dummyOut);
            }
            else
            {
                return false;
            }
        }

    }


}
