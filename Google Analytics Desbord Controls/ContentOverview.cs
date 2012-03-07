using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Permissions;

namespace GADCAPI
{
    /// <summary>
    /// This is control to show page paths with total visits for From Date to To Date
    /// </summary>
    [
    AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Minimal),
    AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal),
    ToolboxData("<{0}:GADContentOverview runat=\"server\"> </{0}:GADContentOverview>")
    ]
    public class ContentOverview : GridView
    {
        private String _GAEmailAddress = "";
        private String _GAPassword = "";
        private String _GAProfileId = "";
        private String _GAToken = "";
        private DateTime _FromDate = DateTime.MinValue;
        private DateTime _ToDate = DateTime.MaxValue;

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


        public ContentOverview()
        {
            FromDate = DateTime.MinValue;
            ToDate = DateTime.MaxValue;
        }

        protected override void Render(HtmlTextWriter output)
        {
            if (HttpContext.Current != null && String.IsNullOrEmpty(GAProfileId) == false)
                output.Write(GetHTML());
            else
                output.Write("<div style=\"width: 350px; height: 50px; display: inline-block; background-color: #f2f2f2; border: solid 1px #333333; text-align: center\">Pages overview (Design Time)</div>");

        }

        protected string GetHTML()
        {
            if (HttpContext.Current != null && String.IsNullOrEmpty(GAEmailAddress) == false && String.IsNullOrEmpty(GAPassword) == false && String.IsNullOrEmpty(GAToken))
            {
                GAToken = Authentication.getSessionTokenClientLogin(GAEmailAddress, GAPassword);
            }

            if (String.IsNullOrEmpty(GAToken) == false)
            {

                List<ContentOverviewEntity> VisitData = HttpContext.Current.Cache["ContentOverviewEntity_" + GAProfileId] as List<ContentOverviewEntity>;
                if (VisitData == null)
                {
                    VisitData = ContentOverviewQuery.getContentOverviewQueryResults(GAToken, GAProfileId, FromDate == DateTime.MinValue ? DateTime.Now.AddDays(-31) : FromDate, ToDate > DateTime.Now ? DateTime.Now : ToDate);
                    HttpContext.Current.Cache.Add("ContentOverviewEntity_" + GAProfileId, VisitData, null, DateTime.Now.AddHours(2), System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Default, null);
                }

                if (VisitData != null)
                {

                    StringBuilder JavascriptBuilder = new StringBuilder();
                    JavascriptBuilder.Append(@"<table><tr><th width='300'>Pages</th><th>Page Views</th></tr>");

                    foreach (ContentOverviewEntity obj in VisitData)
                    {
                        JavascriptBuilder.Append(@"<tr><td>" + obj.PagePath + "</td><td align='right'>" + obj.PageViews.ToString() + "</td></tr>");
                    }

                    JavascriptBuilder.Append(@"</table>");

                    return JavascriptBuilder.ToString();
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }
    }
}
