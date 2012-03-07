using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Xml;
using System.Text;
using System.Data;
using System.Threading;
using System.Text.RegularExpressions;
using System.Web.Mvc.Html;
using System.IO;



namespace Web.Controllers
{
    public class _BaseController : Controller
    {
        public enum Activities:int { clickDealer = 1, clickProduct, clickSearch, viewList, clickDistributorMap,
            popUpSearch};


        protected override void OnActionExecuted(ActionExecutedContext ctx)
        {
            base.OnActionExecuted(ctx);
            
        }



        protected static bool IsPasswordStrong(string password)
        {
            return Regex.IsMatch(password, @"(?=.{8,})[a-zA-Z]+[^a-zA-Z]+|[^a-zA-Z]+[a-zA-Z]+");
        }

        protected ActionResult SetView(string view_Name)
        {
            if (isMobileBrowser())
                view_Name = "Mobile" + view_Name;


            if (Request.IsAjaxRequest())
            {
                return View(view_Name);
            }

            ViewData["Partial_Name"] = view_Name;
            return View("Common");

        }

        protected ActionResult SetViewPage(string view_Name)
        {
            if (isMobileBrowser())
            view_Name = "Mobile" + view_Name;


            if (Request.IsAjaxRequest())
            {
                return View(view_Name);
            }

            return View(view_Name);

        }


        protected ActionResult SetView(string view_Name, ViewDataDictionary vdd)
        {
            if (Request.IsAjaxRequest())
            {
                return View(view_Name, vdd);
            }

            ViewData["Partial_Name"] = view_Name;
            return View("Common", vdd);

        }


        public bool isMobileBrowser()
        {
           
            //FIRST TRY BUILT IN ASP.NT CHECK
            if (Request.Browser.IsMobileDevice)
            {
                return true;
            }
            //THEN TRY CHECKING FOR THE HTTP_X_WAP_PROFILE HEADER
            if (Request.ServerVariables["HTTP_X_WAP_PROFILE"] != null)
            {
                return true;
            }
            //THEN TRY CHECKING THAT HTTP_ACCEPT EXISTS AND CONTAINS WAP
            if (Request.ServerVariables["HTTP_ACCEPT"] != null &&
                Request.ServerVariables["HTTP_ACCEPT"].ToLower().Contains("wap"))
            {
                return true;
            }
            //AND FINALLY CHECK THE HTTP_USER_AGENT 
            //HEADER VARIABLE FOR ANY ONE OF THE FOLLOWING
            if (Request.ServerVariables["HTTP_USER_AGENT"] != null)
            {
                //Create a list of all mobile types
                string[] mobiles =
                    new[]
                {
                    "midp", "j2me", "avant", "docomo", 
                    "novarra", "palmos", "palmsource", 
                    "240x320", "opwv", "chtml",
                    "pda", "windows ce", "mmp/", 
                    "blackberry", "mib/", "symbian", 
                    "wireless", "nokia", "hand", "mobi",
                    "phone", "cdm", "up.b", "audio", 
                    "SIE-", "SEC-", "samsung", "HTC", 
                    "mot-", "mitsu", "sagem", "sony"
                    , "alcatel", "lg", "eric", "vx", 
                    "NEC", "philips", "mmm", "xx", 
                    "panasonic", "sharp", "wap", "sch",
                    "rover", "pocket", "benq", "java", 
                    "pt", "pg", "vox", "amoi", 
                    "bird", "compal", "kg", "voda",
                    "sany", "kdd", "dbt", "sendo", 
                    "sgh", "gradi", "jb", "dddi", 
                    "moto", "iphone"
                };

                //Loop through each item in the list created above 
                //and check if the header contains that text
                foreach (string s in mobiles)
                {
                    if (Request.ServerVariables["HTTP_USER_AGENT"].
                                                        ToLower().Contains(s.ToLower()))
                    {
                        return true;
                    }
                }
            }

            return false;
        }


        


        ///// <summary>Renders a view to string.</summary>
        protected string RenderViewToStringAsHTML(Controller controller,
                                                string viewName, object viewData)
        {

            if (Request.Browser.IsMobileDevice)
            viewName = "Mobile" + viewName;
            
            //Create memory writer
            var sb = new StringBuilder();
            var memWriter = new StringWriter(sb);

            //Create fake http context to render the view
            var fakeResponse = new HttpResponse(memWriter);
            var fakeContext = new HttpContext(System.Web.HttpContext.Current.Request, fakeResponse);
            var fakeControllerContext = new ControllerContext(
                new HttpContextWrapper(fakeContext),
                controller.ControllerContext.RouteData,
                controller.ControllerContext.Controller);

            var oldContext = System.Web.HttpContext.Current;
            //System.Web.HttpContext.Current = fakeContext;

            //Use HtmlHelper to render partial view to fake context
            var html = new HtmlHelper(
                new ViewContext(fakeControllerContext, new FakeView(),
                    new ViewDataDictionary(), new TempDataDictionary(), memWriter),
                new ViewPage());

            html.RenderPartial(viewName, (ViewDataDictionary)viewData);



            //Restore context
            //System.Web.HttpContext.Current = oldContext;

            //Flush memory and return output
            memWriter.Flush();
            return sb.ToString();
        }




        public bool IsNumeric(string anyString)
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


        protected System.Collections.Specialized.NameValueCollection ConvertRawUrlToQuerystring()
        {
            System.Collections.Specialized.NameValueCollection nvc = new System.Collections.Specialized.NameValueCollection();

           

            string str = Request.RawUrl.Substring(Request.RawUrl.LastIndexOf('?') + 1).Replace("data=","");


            if (Request.RawUrl.LastIndexOf('?') == -1 )
            {
                return nvc;
            }

           // str = HttpUtility.UrlDecode(str);
            //str = HttpUtility.UrlDecode(str);
            string queryString = str;// url.Substring(queryStringBegin + 1);
           
            string[] sections = queryString.Split(new char[] { '&' });

            foreach (string section in sections)
            {
                string[] pair =  section.Split(new char[] { '=' });
                nvc.Add(pair[0].ToString(),HttpUtility.UrlDecode( pair[1].ToString() ) );
            }

            return nvc;
        }




        protected string ConvertCollectionToQuerystring(FormCollection collection)
        {
            StringBuilder str = new StringBuilder();
            foreach( string key in collection.AllKeys )
            {
                str.Append( key ).Append("=").Append( collection[key]).Append("&");
            }
            return str.ToString().Remove(str.ToString().Length-1);
        }

        protected string GetRawUrl()
        {
            return Request.RawUrl.Substring(Request.RawUrl.LastIndexOf('?') + 1);
        }


        protected void Activity(int activity_Type_ID, string text, int id)
        {
            BusinessLogic.Tracking.Activity obj = new BusinessLogic.Tracking.Activity();
            DataSet.DS ds = new DataSet.DS();
            int actvity_Sequence = Convert.ToInt32( Session["Activity_Sequence"] ) + 1;
            //ds.Activity.AddActivityRow( Convert.ToInt32( Session["Session_ID"] ),actvity_Sequence,activity_Type_ID,DateTime.Now,text,id);
              
            //obj.New(ds);
            Session["Activity_Sequence"] = actvity_Sequence;
        }


        protected void initGridSize(string selectedGridSize)
        {
            List<SelectListItem> list = new List<SelectListItem>();


            #region Grid Size

            list.Clear();



            list.Add(new SelectListItem
            {
                Text = "5",
                Value = "5",
            });

            list.Add(new SelectListItem
            {
                Text = "10",
                Value = "10",
            });


            list.Add(new SelectListItem
            {
                Text = "15",
                Value = "15",
            });

            list.Add(new SelectListItem
            {
                Text = "25",
                Value = "25",
            });


            list.Add(new SelectListItem
            {
                Text = "50",
                Value = "50",
            });

            list.Add(new SelectListItem
            {
                Text = "100",
                Value = "100",
            });


            list.Add(new SelectListItem
            {
                Text = "250",
                Value = "250",
            });

            list.Add(new SelectListItem
            {
                Text = "500",
                Value = "500",
            });

            list.Add(new SelectListItem
            {
                Text = "1000",
                Value = "1000",
            });


            ViewData[UIAll.gridSizeList.ToString()] = new SelectList(list, "Value", "Text", selectedGridSize);


            #endregion

        }


        public static bool isEmail(string inputEmail)
        {
            if (inputEmail == null || inputEmail.Length == 0)
            {
                return false;
            }

            const string expression = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                                      @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                                      @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

            Regex regex = new Regex(expression);
            return regex.IsMatch(inputEmail);
        }

        protected bool IsUrlValid(string url)
        {
            return Regex.IsMatch(url, @"(http|https)://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?");
        }


        protected void UpdateCache()
        {
            BusinessLogic.Parameter.Parameter par = new BusinessLogic.Parameter.Parameter();
            DataSet.DSParameter ds = par.GetParamerter();


            if (ds != null)
            {
                lock ( MvcApplication._cache )
                {
                    MvcApplication._cache["data"] = ds;
                }

            }

        }

    }


    /// <summary>Fake IView implementation used to instantiate an HtmlHelper.</summary>
    public class FakeView : IView
    {
        #region IView Members

        public void Render(ViewContext viewContext, System.IO.TextWriter writer)
        {
            throw new NotImplementedException();
        }

        #endregion
    }


    




    //public class SecureQueryString : NameValueCollection
    //{

    //    private string timeStampKey = "__TS__";
    //    private string dateFormat = "G";
    //    private IEncryptionUtility mEncryptionUtil;
    //    private DateTime m_expireTime = DateTime.MaxValue;

    //     <summary>
    //     Creates an instance with a specified key.
    //     </summary>
    //     <param name="key">The key used for cryptographic functions, required 16 chars in length.
    //    public SecureQueryString(string key)
    //        : base()
    //    {
    //        mEncryptionUtil = new EncryptionUtility(key);
    //    }

    //     <summary>
    //     Creates an instance with a specified key and an encrypted query string.
    //     </summary>
    //     <param name="key">The key used for cryptographic functions, required 16 chars in length.
    //     <param name="queryString">An encrypted query string generated by a <see cref="SecureQueryString"> instance.
    //    public SecureQueryString(string key, string queryString)
    //        : this(key)
    //    {
    //        Deserialize(DecryptAndVerify(queryString));
    //        CheckExpiration();
    //    }

    //     <summary>
    //     Returns a encrypted query string.
    //     </summary>
    //     <returns></returns>
    //    public override string ToString()
    //    {
    //        return EncryptAndSign(Serialize());
    //    }

    //    private void Deserialize(string queryString)
    //    {
    //        string[] nameValuePairs = queryString.Split('&');
    //        for (int i = 0; i <= nameValuePairs.Length - 1; i++)
    //        {
    //            string[] nameValue = nameValuePairs(i).Split('=');
    //            if (nameValue.Length == 2)
    //            {
    //                base.Add(nameValue(0), nameValue(1));
    //            }
    //        }

    //        if (base.GetValues(timeStampKey) != null)
    //        {
    //            string[] strExpireTime = base.GetValues(timeStampKey);
    //            m_expireTime = Convert.ToDateTime(strExpireTime(0));
    //        }
    //    }

    //    private string Serialize()
    //    {
    //        StringBuilder sb = new StringBuilder();
    //        foreach (string key in base.AllKeys)
    //        {
    //            sb.Append(key);
    //            sb.Append('=');
    //            sb.Append(base.GetValues(key)(0).ToString());
    //            sb.Append('&');
    //        }

    //        sb.Append(timeStampKey);
    //        sb.Append('=');
    //        sb.Append(m_expireTime.ToString(dateFormat));

    //        return sb.ToString();
    //    }

    //    private string DecryptAndVerify(string input)
    //    {
    //        return mEncryptionUtil.Decrypt(input);
    //    }

    //    private string EncryptAndSign(string input)
    //    {
    //        return mEncryptionUtil.Encrypt(input);
    //    }

    //    private void CheckExpiration()
    //    {
    //        if (DateTime.Compare(m_expireTime, DateTime.Now) < 0)
    //        {
    //            throw new ExpiredQueryStringException();
    //        }
    //    }

    //     <summary>
    //     Gets or sets the timestamp in which this string should expire
    //     </summary>
    //    public DateTime ExpireTime
    //    {
    //        get { return m_expireTime; }
    //        set { m_expireTime = value; }
    //    }
    //}
}
