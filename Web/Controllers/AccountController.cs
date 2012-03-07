using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using Web.Model;
using BusinessLogic;
using BusinessLogic.Membership;
using DataSet;
using GADCAPI;

namespace Web.Controllers
{

    [HandleError]
    public class AccountController : Web.Controllers._BaseController
    {

        // This constructor is used by the MVC framework to instantiate the controller using
        // the default forms authentication and membership providers.
        string gMessage = "";

        public AccountController()
            : this(null, null)
        {

        }


        //protected override void OnException(ExceptionContext filterContext)
        //{
        //     View("Error");

        //}





        public ActionResult Error(string id)
        {
            if ("Http404" == id)
            {
                ViewData["GenericMessage"] = "Your desire resource has not been found.";
                ViewData["Partial_Name"] = "GenericMessage";
                return View("Common");
            }


            if (Request.IsAjaxRequest())
            {
                ViewData["GenericMessage"] = "Sorry, an error occurred while processing your request.";
                return View("GenericMessage");
            }
            else
            {
                ViewData["GenericMessage"] = "Sorry, an error occurred while processing your request.";
                ViewData["Partial_Name"] = "GenericMessage";
                return View("Common");
            }


            ViewData["GenericMessage"] = "Sorry, an error occurred while processing your request.";
            return View("GenericMessage");
        }

        public ActionResult GetChapcha()
        {

            Bitmap objBMP = new Bitmap(120, 40);
            Graphics objGraphics = Graphics.FromImage(objBMP);

            objGraphics.Clear(Color.Wheat);
            objGraphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            //' Configure font to use for text
            Font objFont = new Font("Arial", 19, FontStyle.Italic);
            string randomStr = "";


            char[] myArray = new char[5];
            int x;


            //That is to create the random # and add it to our string
            Random autoRand = new Random();
            for (x = 0; x < 5; x++)
            {
                myArray[x] = System.Convert.ToChar(autoRand.Next(65, 90));
                randomStr += (myArray[x].ToString());
            }
            //This is to add the string to session, to be compared later
            Session.Add("RandomStr", randomStr);

            //' Write out the text
            objGraphics.DrawString(randomStr, objFont, Brushes.Red, 3, 3);


            //' Set the content type and return the image
            Response.ContentType = "image/GIF";

            objBMP.Save(Response.OutputStream, ImageFormat.Gif);


            objFont.Dispose();


            objGraphics.Dispose();


            objBMP.Dispose();



            return View();


        }



        [NonAction]
        private void GetSecretQuestionList(string selectedValue)
        {
            DataSet.DSParameter ds = (DataSet.DSParameter)System.Web.HttpContext.Current.Cache["data"];

            List<SelectListItem> list = new List<SelectListItem>();
            DataRow[] rows = ds.Secret_Question.Select(); //secQu.GetAllSecretQuestion().t;//..LoadAllSecretQuestions().Tables[0].Select();
            for (int i = 0; i < rows.Length; i++)
            {
                list.Add(new SelectListItem
                {
                    Text = rows[i][ds.Secret_Question.Secret_QuestionColumn.ToString()].ToString(),
                    Value = rows[i][ds.Secret_Question.Secret_Question_IDColumn.ToString()].ToString(),


                });
            }


            if (selectedValue.Trim().Length == 0 || selectedValue == "All")
                ViewData["SecretQuestionsList"] = new SelectList(list, "Value", "Text");
            else
                ViewData["SecretQuestionsList"] = new SelectList(list, "Value", "Text", selectedValue);
        }




        public ActionResult Activation()
        {
            System.Web.HttpContext.Current.Session["Selected_Header"] = "";
            System.Web.HttpContext.Current.Session["Selected_Node"] = "";


            ViewData["txtEmployee_ID"] = "";
            ViewData["txtAuth"] = "";
            ViewData["txtUserName"] = "";
            ViewData["txtPassword"] = "";
            ViewData["txtConfirmPass"] = "";
            ViewData["cboQuestion"] = "";
            ViewData["txtAnswer"] = "";


            ViewData["errorTxtEmployee_ID"] = "";
            ViewData["errorTxtAuth"] = "";
            ViewData["errorTxtUserName"] = "";
            ViewData["errorTxtPassword"] = "";
            ViewData["errorTxtConfirmPass"] = "";
            ViewData["errorTxtAnswer"] = "";
            ViewData["error"] = "";
            ViewData["errorCaptcha"] = "";

            GetSecretQuestionList("");

            System.Web.HttpContext.Current.Session["Selected_Header"] = "";

            ViewData["Partial_Name"] = "Activation";

            return View("Common");
        }



        public ActionResult test()
        {
            return View();
        }





        [Authorize]
        public ActionResult Welcome()
        {
            if (!Web.Model.Utility.Is_Session_Active())
            {
                ViewData["Partial_Name"] = "LogOn";
                return View("Common");
            }

            ViewData["Partial_Name"] = "Welcome";
            return View("Common");
        }






        //        private bool FillDataRow(DSRegPatient dsRp) 
        //{ 

        //    DSRegistration ds = new DSRegistration(); 
        //    DataRow dr = null;
        //    PortalDataManager.Portal.BLLPortalRegistration objCls = new PortalDataManager.Portal.BLLPortalRegistration();




        //    if (Session[PortalConstants.PATIENT_ID] == null) { 
        //        dr = ds.Tables[DSRegistration.TABLE_PORTAL_REGISTRATION].NewRow(); 
        //    } 

        //    DataRow drRP = dsRp.Tables[DSRegPatient.TABLE_RegPatient].Rows[0]; 

        //    dr[DSRegistration.FIELD_LOGIN_USER_NAME] = ViewData["txtUserName"].ToString(); 
        //    dr[DSRegistration.FIELD_LOGIN_PASSWORD] = EncDec.Encrypt( ViewData["txtPassword"].ToString() ); 
        //    dr[DSRegistration.FIELD_SECRET_QUESTION_ID_FK] = ViewData["cboQuestion"].ToString(); 
        //    dr[DSRegistration.FIELD_SECRET_ANSWER] = ViewData["txtAnswer"].ToString(); 
        //    dr[DSRegistration.FIELD_ACTIVE] = 1; 
        //    dr[DSRegistration.FIELD_CREATED_DATE] = DateTime.Now.ToShortDateString(); 
        //    dr[DSRegistration.FIELD_UPDATE_DATETIME] = DateTime.Now.ToShortDateString(); 

        //    dr[DSRegistration.FIELD_FIRST_NAME] = drRP[DSRegPatient.FIELD_PERSONFIRSTNAME].ToString(); 
        //    dr[DSRegistration.FIELD_LAST_NAME] = drRP[DSRegPatient.FIELD_PERSONLASTNAME].ToString(); 

        //    dr[DSRegistration.FIELD_DOB] = drRP[DSRegPatient.FIELD_PATIENTBIRTHDATE].ToString(); 
        //    dr[DSRegistration.FIELD_SSN] = drRP[DSRegPatient.FIELD_PERSONSOCIALSECURITYNO].ToString(); 

        //    dr[DSRegistration.FIELD_GENDER] = drRP[DSRegPatient.FIELD_PERSONSEX].ToString(); 
        //    dr[DSRegistration.FIELD_ADDRESS1] = drRP[DSRegPatient.FIELD_PERSONADDRESS1].ToString(); 

        //    dr[DSRegistration.FIELD_CITY] = drRP[DSRegPatient.FIELD_PERSONCITYID].ToString(); 
        //    dr[DSRegistration.FIELD_STATE] = drRP[DSRegPatient.FIELD_PERSONSTATEID].ToString(); 
        //    dr[DSRegistration.FIELD_ZIP] = drRP[DSRegPatient.FIELD_PERSONZIPCODE].ToString(); 
        //    dr[DSRegistration.FIELD_Employee_ID] = ViewData["txtEmployee_ID"].ToString(); 
        //    dr[DSRegistration.FIELD_AUTHORIZATIONCODE] = ViewData["txtAuth"].ToString(); 


        //    ds.Tables[DSRegistration.TABLE_PORTAL_REGISTRATION].Rows.Add(dr); 

        //    if (Session[PortalConstants.PATIENT_ID] == null) {

        //        try
        //        {
        //            long ID = objCls.SavePatientRegistration(ds);

        //        }
        //        catch (Exception ex)
        //        {
        //            return false;
        //        }


        //    }
        //    return true;
        //} 






        [NonAction]
        public bool IsValidUser(string userName)
        {

            string[] username = userName.Split(' ');
            if (username.Length > 1)
            {
                return false;
            }
            else
            {
                return true;
            }
            return false;
        }






        // This constructor is not used by the MVC framework but is instead provided for ease
        // of unit testing this type. See the comments at the end of this file for more
        // information.
        public AccountController(IFormsAuthentication formsAuth, IMembershipService service)
        {
            FormsAuth = formsAuth ?? new FormsAuthenticationService();
            MembershipService = service ?? new AccountMembershipService();
        }

        public IFormsAuthentication FormsAuth
        {
            get;
            private set;
        }

        public IMembershipService MembershipService
        {
            get;
            private set;
        }


        public ActionResult LogOn(string returnUrl)
        {
            Session["UrlPath"] = returnUrl;
            //return View("Index");

            System.Web.HttpContext.Current.Session["Selected_Header"] = "";
            System.Web.HttpContext.Current.Session["Selected_Node"] = "";

            
            string viewName = "";

         
            


            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                viewName = "Product";
                return RedirectToAction("Index", "Store");
            }
            else
            {
                viewName = "LogOn";
            }


            if (Request.IsAjaxRequest())
            {
                return View(viewName);
            }

            ViewData["Partial_Name"] = viewName;
            return View(BrowserCompatibility());
        }

        [NonAction]
        private string BrowserCompatibility()
        {
            string str = "Common";

            string ver =  Request.Browser.Version.Split('.')[0].ToString().ToLower();

            switch (Request.Browser.Browser.ToLower())
            {
                case "firefox":
                    if( Convert.ToUInt32( ver ) < 4 )
                    {
                        str = "BrowserCompatibility";
                    }
                    
                    

                    break;

                case "ie":
                    str = "BrowserCompatibility";
                    break;

                case "opera":
                    if (Convert.ToUInt32(ver) < 11)
                    {
                        str = "BrowserCompatibility";
                    }
                    break;

                case "safari":
                    if (Convert.ToUInt32(ver) < 5)
                    {
                        str = "BrowserCompatibility";
                    }
                    break;

                case "chrome":
                    break;
            }

        

            return str;

        }




        [NonAction]
        ActionResult LogOnUser(string userName, string password, bool rememberMe, string returnUrl)
        {
            System.Web.HttpContext.Current.Session["Selected_Header"] = "";
            System.Web.HttpContext.Current.Session["Selected_Node"] = "";




            if (!ValidateLogOn(userName, password))
            {
                ModelState.Clear();
                ModelState.AddModelError("_Error", this.gMessage);
                //this.ViewData["LoginFaild"] = this.gMessage;


                if (Request.IsAjaxRequest())
                {
                    return View("LogOn");
                }

                ViewData["Partial_Name"] = "LogOn";
                return View("Common");
            }




            if (Membership.ValidateUser(userName, password))
            {
                //this.RedirectFromLoginPage(userName, ReturnUrl);

                FormsAuth.SignIn(userName, rememberMe);
                string str = "";
                for (int i = 0; i < HttpContext.Request.Cookies.Count; i++)
                {
                    str = HttpContext.Request.Cookies[i].Value;
                    HttpContext.Request.Cookies[i].HttpOnly = true;
                }
            }
            else
            {
                this.ViewData["LoginFaild"] = "Login faild! Make sure you have entered the right user name and password!";
                return View("Logon");
            }

            SetLeftPanel();

            if (!String.IsNullOrEmpty(returnUrl))
            {
                //Session["UrlPath"] = returnUrl;
                ViewData["IsCompletePageRefresh"] = "true";
                //return View("Loading");
                return RedirectToAction("Index", "Store");
            }
            else
            {
                //Session["UrlPath"] = returnUrl;// "/Account/Welcome";
                ViewData["IsCompletePageRefresh"] = "true";
                return RedirectToAction("Index", "Store");
            }
        }


        void SetLeftPanel()
        {
            int cEmployee = 0;
            int cConsumer = 0;
            int cCase = 0;
            int cService_Authorization = 0;
            int cLocation = 0;
            int cNews = 0;
            int cMessage = 0;

            int leafSpace = 23;


            string str = "";

            int reviewHight = 0;
            int requestHight = 0;
            int messageHight = 0;


            #region Employee

            //  New
            if (1 == 1)//ds.Tables[DSPortal_Feature.TABLE_PORTAL_FEATURE].Select(" Portal_Feature_ID =  1 and Is_Enable").Length == 1)
            {
                System.Web.HttpContext.Current.Session["Employee_New"] = "True";
                cEmployee++;
                reviewHight += leafSpace;
            }
            else
                System.Web.HttpContext.Current.Session["Employee_New"] = "False";


            //  Search
            if (1 == 1)
            {
                System.Web.HttpContext.Current.Session["Employee_Search"] = "True";
                cEmployee++;
                reviewHight += leafSpace;
            }
            else
                System.Web.HttpContext.Current.Session["Employee_Search"] = "False";


            //  Schedule Canval
            if (1 == 1)
            {
                System.Web.HttpContext.Current.Session["Employee_Schedule_Canvas"] = "True";
                cEmployee++;
                reviewHight += leafSpace;
            }
            else
                System.Web.HttpContext.Current.Session["Employee_Schedule_Canvas"] = "False";



            #endregion



            #region Consumer

            //  New
            if (1 == 1)//ds.Tables[DSPortal_Feature.TABLE_PORTAL_FEATURE].Select(" Portal_Feature_ID =  1 and Is_Enable").Length == 1)
            {
                System.Web.HttpContext.Current.Session["Consumer_New"] = "True";
                cConsumer++;
                reviewHight += leafSpace;
            }
            else
                System.Web.HttpContext.Current.Session["Consumer_New"] = "False";


            //  Search
            if (1 == 1)
            {
                System.Web.HttpContext.Current.Session["Consumer_Search"] = "True";
                cConsumer++;
                reviewHight += leafSpace;
            }
            else
                System.Web.HttpContext.Current.Session["Consumer_Search"] = "False";


            //  Schedule Canval
            if (1 == 1)
            {
                System.Web.HttpContext.Current.Session["Consumer_Schedule_Canvas"] = "True";
                cConsumer++;
                reviewHight += leafSpace;
            }
            else
                System.Web.HttpContext.Current.Session["Consumer_Schedule_Canvas"] = "False";



            #endregion


            #region Case

            //  New
            if (1 == 1)//ds.Tables[DSPortal_Feature.TABLE_PORTAL_FEATURE].Select(" Portal_Feature_ID =  1 and Is_Enable").Length == 1)
            {
                System.Web.HttpContext.Current.Session["Case_New"] = "True";
                cCase++;
                reviewHight += leafSpace;
            }
            else
                System.Web.HttpContext.Current.Session["Case_New"] = "False";


            //  Search
            if (1 == 1)
            {
                System.Web.HttpContext.Current.Session["Case_Search"] = "True";
                cCase++;
                reviewHight += leafSpace;
            }
            else
                System.Web.HttpContext.Current.Session["Case_Search"] = "False";


            //  Schedule Canval
            if (1 == 1)
            {
                System.Web.HttpContext.Current.Session["Case_Schedule_Canvas"] = "True";
                cCase++;
                reviewHight += leafSpace;
            }
            else
                System.Web.HttpContext.Current.Session["Case_Schedule_Canvas"] = "False";



            #endregion



            #region Service Authorization

            //  Un Approved
            if (1 == 1)//ds.Tables[DSPortal_Feature.TABLE_PORTAL_FEATURE].Select(" Portal_Feature_ID =  1 and Is_Enable").Length == 1)
            {
                System.Web.HttpContext.Current.Session["Service_Authorization_Un_Approved"] = "True";
                cService_Authorization++;
                reviewHight += leafSpace;
            }
            else
                System.Web.HttpContext.Current.Session["Service_Authorization_Un_Approved"] = "False";


            //  Active
            if (1 == 1)//ds.Tables[DSPortal_Feature.TABLE_PORTAL_FEATURE].Select(" Portal_Feature_ID =  1 and Is_Enable").Length == 1)
            {
                System.Web.HttpContext.Current.Session["Service_Authorization_Active"] = "True";
                cService_Authorization++;
                reviewHight += leafSpace;
            }
            else
                System.Web.HttpContext.Current.Session["Service_Authorization_Active"] = "False";


            //  Search
            if (1 == 1)
            {
                System.Web.HttpContext.Current.Session["Service_Authorization_Search"] = "True";
                cService_Authorization++;
                reviewHight += leafSpace;
            }
            else
                System.Web.HttpContext.Current.Session["Service_Authorization_Search"] = "False";

            #endregion


            #region Location

            //  New
            if (1 == 1)//ds.Tables[DSPortal_Feature.TABLE_PORTAL_FEATURE].Select(" Portal_Feature_ID =  1 and Is_Enable").Length == 1)
            {
                System.Web.HttpContext.Current.Session["Location_New"] = "True";
                cLocation++;
                reviewHight += leafSpace;
            }
            else
                System.Web.HttpContext.Current.Session["Location_New"] = "False";


            //  Search
            if (1 == 1)
            {
                System.Web.HttpContext.Current.Session["Location_Search"] = "True";
                cLocation++;
                reviewHight += leafSpace;
            }
            else
                System.Web.HttpContext.Current.Session["Location_Search"] = "False";

            #endregion


            #region Message

            //  Inbox
            if (1 == 1)//ds.Tables[DSPortal_Feature.TABLE_PORTAL_FEATURE].Select(" Portal_Feature_ID =  1 and Is_Enable").Length == 1)
            {
                System.Web.HttpContext.Current.Session["Inbox"] = "True";
                cMessage++;
                reviewHight += leafSpace;
            }
            else
                System.Web.HttpContext.Current.Session["Inbox"] = "False";


            //  Outbox
            if (1 == 1)
            {
                System.Web.HttpContext.Current.Session["Outbox"] = "True";
                cMessage++;
                reviewHight += leafSpace;
            }
            else
                System.Web.HttpContext.Current.Session["Outbox"] = "False";


            //  Compose
            if (1 == 1)
            {
                System.Web.HttpContext.Current.Session["Compose"] = "True";
                cMessage++;
                reviewHight += leafSpace;
            }
            else
                System.Web.HttpContext.Current.Session["Compose"] = "False";



            #endregion




            if (cEmployee > 0)
                System.Web.HttpContext.Current.Session["Employee"] = "True";
            else
                System.Web.HttpContext.Current.Session["Employee"] = "False";


            if (cConsumer > 0)
                System.Web.HttpContext.Current.Session["Consumer"] = "True";
            else
                System.Web.HttpContext.Current.Session["Consumer"] = "False";


            if (cCase > 0)
                System.Web.HttpContext.Current.Session["Case"] = "True";
            else
                System.Web.HttpContext.Current.Session["Case"] = "False";


            if (cService_Authorization > 0)
                System.Web.HttpContext.Current.Session["Service_Authorization"] = "True";
            else
                System.Web.HttpContext.Current.Session["Service_Authorization"] = "False";


            if (cLocation > 0)
                System.Web.HttpContext.Current.Session["Location"] = "True";
            else
                System.Web.HttpContext.Current.Session["Location"] = "False";


            if (cMessage > 0)
                System.Web.HttpContext.Current.Session["Message"] = "True";
            else
                System.Web.HttpContext.Current.Session["Message"] = "False";




            if (cMessage > 0)
                System.Web.HttpContext.Current.Session["Message"] = "True";
            else
                System.Web.HttpContext.Current.Session["Message"] = "False";


            System.Web.HttpContext.Current.Session["Selected_Header"] = "";
            System.Web.HttpContext.Current.Session["Selected_Node"] = "";
            System.Web.HttpContext.Current.Session["Is_Authorize"] = "True";

            System.Web.HttpContext.Current.Session["reviewHight"] = reviewHight.ToString();
            System.Web.HttpContext.Current.Session["requestHight"] = requestHight.ToString();
            System.Web.HttpContext.Current.Session["messageHight"] = messageHight.ToString();


        }


        [AcceptVerbs(HttpVerbs.Post)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings",
            Justification = "Needs to take same parameter type as Controller.Redirect()")]
        public ActionResult LogOn(string userName, string password, bool rememberMe, string returnUrl, string cobCompany, FormCollection collection)
        {
            var value = "";
            foreach (var key in collection.AllKeys)
            {
                value += "  " + collection[key];
                // etc.
            }

           

            System.Web.HttpContext.Current.Session["Selected_Header"] = "";
            System.Web.HttpContext.Current.Session["Selected_Node"] = "";

            return LogOnUser(userName, password, rememberMe, returnUrl);
        }

        public ActionResult LogOff()
        {
            System.Web.HttpContext.Current.Session["Selected_Header"] = "";
            System.Web.HttpContext.Current.Session["Selected_Node"] = "";

            FormsAuth.SignOut();

            return RedirectToAction("Logon", "Account");
        }

        public ActionResult Register()
        {

            ViewData["PasswordLength"] = MembershipService.MinPasswordLength;

            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Register(string userName, string email, string password, string confirmPassword)
        {

            ViewData["PasswordLength"] = MembershipService.MinPasswordLength;

            if (ValidateRegistration(userName, email, password, confirmPassword))
            {
                // Attempt to register the user
                MembershipCreateStatus createStatus = MembershipService.CreateUser(userName, password, email);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    FormsAuth.SignIn(userName, false /* createPersistentCookie */);
                    return RedirectToAction("Request", "Appointment");
                }
                else
                {
                    ModelState.AddModelError("_FORM", ErrorCodeToString(createStatus));
                }
            }

            // If we got this far, something failed, redisplay form
            return View();
        }



        public ActionResult ForgetPassword()
        {

            ViewData["txtUserName"] = "";
            ViewData["cboQuestion"] = "";
            ViewData["txtAnswer"] = "";

            ViewData["errorTxtUserName"] = "";
            ViewData["errorTxtAnswer"] = "";
            ViewData["error"] = "";
            ViewData["errorCaptcha"] = "";

            GetSecretQuestionList("");


            ViewData["Partial_Name"] = "ForgetPassword";
            return View("Common");
        }


        public ActionResult ForgetPasswordSubmit(string txtUserName, string cboQuestion, string txtAnswer, string captcha)
        {
            bool has_Error = false;
            ViewData["txtUserName"] = "";
            ViewData["cboQuestion"] = "";
            ViewData["txtAnswer"] = "";

            ViewData["errorTxtUserName"] = "";
            ViewData["errorTxtAnswer"] = "";
            ViewData["error"] = "";
            ViewData["errorCaptcha"] = "";

            GetSecretQuestionList("");


            if (txtAnswer.Trim().Length == 0)
            {
                ViewData["errorTxtAnswer"] = "Please enter secret answer.";
                has_Error = true;
            }


            if (!IsValidUser(txtUserName))
            {
                ViewData["errorTxtUserName"] = "Please enter your correct username";
                has_Error = true;
            }


            if (Session["RandomStr"].ToString() != captcha)
            {
                ViewData["errorCaptcha"] = "Please re-enter image text";
                has_Error = true;
            }

            PSMembershipUser u = (PSMembershipUser)Membership.GetUser(txtUserName, false);

            if (u == null)
            {
                ViewData["errorTxtUserName"] = "Please enter correct user name";
                has_Error = true;
            }
            else
            {
                if (u.SecretQuestion_ID == Convert.ToInt32(cboQuestion))
                {
                    PSMembershipProvider hmp = (PSMembershipProvider)Membership.Providers["PragmedicSQLProvider"];
                    string message = "";
                    string psw = hmp.GetPassword(u.UserName, txtAnswer, out message, Session.SessionID);

                    if (message.Trim().Length != 0)
                    {
                        ViewData["error"] = message;
                        has_Error = true;
                    }
                }
                else
                {
                    ViewData["errorTxtAnswer"] = "Please enter correct question and answer";
                    has_Error = true;
                }

            }


            if (has_Error)
                return View("ForgetPassword");

            string link = u.UserName + "|" + DateTime.Now.ToShortDateString();

            ViewData["GenericMessage"] = "Your password change link has been sent to you at " + u.Email + "     " + Request.ApplicationPath + "/Account/ChangePassword/" + BusinessLogic.Membership.EndDec.Encrypt(link);
            return View("GenericMessage");
        }





        public ActionResult ChangePassword(string id)
        {
            if (id != null)
            {

                string str = BusinessLogic.Membership.EndDec.Decrypt(id);

                TimeSpan span = DateTime.Now.Subtract(DateTime.Parse(str.Split('|')[1].ToString()));

                if (span.Days > 3)
                {
                    ViewData["GenericMessage"] = "Your password period has been expired";
                    return View("GenericMessage");
                }

                ViewData["UserName"] = str.Split('|')[0].ToString();
                ViewData["Partial_Name"] = "ForgetChangePassword";
                ViewData["errorTxtPassword"] = "";
                ViewData["errorTxtConfirmPass"] = "";
                return View("Common");

            }

            System.Web.HttpContext.Current.Session["Selected_Header"] = "Account";
            System.Web.HttpContext.Current.Session["Selected_Node"] = "ChangePassword";


            if (!Web.Model.Utility.Is_Session_Active())
            {
                //if (id == null)
                //{
                ViewData["Partial_Name"] = "LogOn";
                return View("Common");
                //}
                //else
                //    return View("LogOn");
            }


            ViewData["PasswordLength"] = MembershipService.MinPasswordLength;

            ViewData["Partial_Name"] = "ChangePassword";
            return View("Common");

        }



        [AcceptVerbs(HttpVerbs.Post)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes",
            Justification = "Exceptions result in password not being changed.")]
        public ActionResult ForgetChangePassword(string txtUsername, string txtPassword, string txtConfirmPass)
        {

            int passwordLength = MembershipService.MinPasswordLength;
            ViewData["UserName"] = txtUsername;

            bool has_Error = false;

            if (txtPassword.Trim().Length == 0)
            {
                ViewData["errorTxtPassword"] = "Please enter password.";
                has_Error = true;
            }

            if (txtPassword.Trim().Length < passwordLength)
            {
                ViewData["errorTxtPassword"] = "Your password needs to be atmost " + passwordLength.ToString() + " in length.";
                has_Error = true;
            }
            else
            {
                if (!IsPasswordStrong(txtPassword.Trim()))
                {
                    ViewData["errorTxtPassword"] = "Your password needs to be alphanumeric and it should not contain your username";
                    has_Error = true;
                }

                if (txtPassword.IndexOf(txtUsername) == 0)
                {
                    ViewData["errorTxtPassword"] = "Your password cannot be started with your username.";
                    has_Error = true;
                }
            }


            if (txtConfirmPass.Length == 0)
            {
                ViewData["errorTxtConfirmPass"] = "Please enter confirm password.";
                has_Error = true;
            }


            if (txtPassword.Trim() != txtConfirmPass.Trim())
            {
                ViewData["errorTxtConfirmPass"] = "Confirmed password is mismatched.";
                has_Error = true;
            }



            if (has_Error)
                return View("ForgetChangePassword");




            PSMembershipProvider hmp = (PSMembershipProvider)Membership.Providers["PragmedicSQLProvider"];
            if (hmp.ChangePassword(txtUsername, txtConfirmPass))
            {
                ViewData["GenericMessage"] = "Your password has been renewed.";
                //ViewData["Partial_Name"] = "GenericMessage";
                return View("GenericMessage");
            }
            else
            {
                ViewData["GenericMessage"] = "<span style='color: #FF0000'>Unable to renew your password at this point after time, please try later</span>";
                //ViewData["Partial_Name"] = "GenericMessage";
                return View("GenericMessage");
            }



            ViewData["Partial_Name"] = "ChangePassword";
            return View("Common");
        }


        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes",
            Justification = "Exceptions result in password not being changed.")]
        public ActionResult ChangePassword(string currentPassword, string newPassword, string confirmPassword)
        {
            System.Web.HttpContext.Current.Session["Selected_Header"] = "";
            System.Web.HttpContext.Current.Session["Selected_Node"] = "";


            System.Web.HttpContext.Current.Session["Selected_Header"] = "";


            ViewData["PasswordLength"] = MembershipService.MinPasswordLength;

            if (!ValidateChangePassword(currentPassword, newPassword, confirmPassword))
            {
                ViewData["Partial_Name"] = "ChangePassword";
                return View("Common");
            }

            try
            {
                if (MembershipService.ChangePassword(User.Identity.Name.Split('|')[0], currentPassword, newPassword))
                {
                    ViewData["GenericMessage"] = "Your password has been changed.";
                    ViewData["Partial_Name"] = "GenericMessage";
                    return View("Common");
                    //return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("_FORM", "The current password is incorrect or the new password is invalid.");
                    ViewData["Partial_Name"] = "ChangePassword";
                    return View("Common");
                }
            }
            catch
            {
                ModelState.AddModelError("_FORM", "The current password is incorrect or the new password is invalid.");
                ViewData["Partial_Name"] = "ChangePassword";
                return View("Common");
            }
        }

        public ActionResult ChangePasswordSuccess()
        {

            return View();
        }

      


        #region Validation Methods

        private bool ValidateChangePassword(string currentPassword, string newPassword, string confirmPassword)
        {
            if (String.IsNullOrEmpty(currentPassword))
            {
                ModelState.AddModelError("currentPassword", "You must specify a current password.");
            }
            if (newPassword == null || newPassword.Length < MembershipService.MinPasswordLength)
            {
                ModelState.AddModelError("newPassword",
                    String.Format(CultureInfo.CurrentCulture,
                         "You must specify a new password of {0} or more characters.",
                         MembershipService.MinPasswordLength));
            }

            if (!String.Equals(newPassword, confirmPassword, StringComparison.Ordinal))
            {
                ModelState.AddModelError("_FORM", "The new password and confirmation password do not match.");
            }

            return ModelState.IsValid;
        }

        private bool ValidateLogOn(string userName, string password)
        {
            gMessage = "";
            if (String.IsNullOrEmpty(userName))
            {
                ModelState.AddModelError("username", "You must specify a username.");
            }
            if (String.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("password", "You must specify a password.");
            }
            if (!MembershipService.ValidateUser(userName, password, Session.SessionID))
            {
                ModelState.AddModelError("_FORM", "The username or password provided is incorrect.");
            }
            gMessage = MembershipService.gMessage;
            return ModelState.IsValid;
        }

        private bool ValidateRegistration(string userName, string email, string password, string confirmPassword)
        {
            if (String.IsNullOrEmpty(userName))
            {
                ModelState.AddModelError("username", "You must specify a username.");
            }
            if (String.IsNullOrEmpty(email))
            {
                ModelState.AddModelError("email", "You must specify an email address.");
            }
            if (password == null || password.Length < MembershipService.MinPasswordLength)
            {
                ModelState.AddModelError("password",
                    String.Format(CultureInfo.CurrentCulture,
                         "You must specify a password of {0} or more characters.",
                         MembershipService.MinPasswordLength));
            }
            if (!String.Equals(password, confirmPassword, StringComparison.Ordinal))
            {
                ModelState.AddModelError("_FORM", "The new password and confirmation password do not match.");
            }
            return ModelState.IsValid;
        }

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://msdn.microsoft.com/en-us/library/system.web.security.membershipcreatestatus.aspx for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "Username already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A username for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }

    // The FormsAuthentication type is sealed and contains static members, so it is difficult to
    // unit test code that calls its members. The interface and helper class below demonstrate
    // how to create an abstract wrapper around such a type in order to make the AccountController
    // code unit testable.

    public interface IFormsAuthentication
    {
        void SignIn(string userName, bool createPersistentCookie);
        void SignOut();
    }

    public class FormsAuthenticationService : IFormsAuthentication
    {
        public void SignIn(string userName, bool createPersistentCookie)
        {

            System.Web.HttpContext.Current.Session["Is_Authorize"] = "True";
            FormsAuthentication.SetAuthCookie(userName + "|" + System.Web.HttpContext.Current.Session["Employee_ID"] + "|" +
                    System.Web.HttpContext.Current.Session["Employee_Name"], createPersistentCookie);



        }
        public void SignOut()
        {
            System.Web.HttpContext.Current.Session["Is_Authorize"] = "False";
            FormsAuthentication.SignOut();
        }
    }

    public interface IMembershipService
    {
        int MinPasswordLength { get; }
        string gMessage { get; }

        bool ValidateUser(string userName, string password, string sessionId);
        MembershipCreateStatus CreateUser(string userName, string password, string email);
        bool ChangePassword(string userName, string oldPassword, string newPassword);
    }

    public class AccountMembershipService : IMembershipService
    {
        private BusinessLogic.Membership.PSMembershipProvider _provider;

        public AccountMembershipService()
            : this(null)
        {
        }

        string message = "";
        public AccountMembershipService(BusinessLogic.Membership.PSMembershipProvider provider)
        {
            _provider = provider ?? (PSMembershipProvider)Membership.Providers["SPMSQLProvider"];// Membership.Provider;
        }

        public int MinPasswordLength
        {
            get
            {
                return _provider.MinRequiredPasswordLength;
            }
        }



        public string gMessage
        {
            get
            {
                return message;
            }
        }


        public bool ValidateUser(string userName, string password, string sessionId)
        {
            return _provider.ValidateUser(userName, password, out message, sessionId);
        }

        public MembershipCreateStatus CreateUser(string userName, string password, string email)
        {
            MembershipCreateStatus status;
            _provider.CreateUser(userName, password, email, null, null, true, null, out status);
            return status;
        }

        public bool ChangePassword(string userName, string oldPassword, string newPassword)
        {
            //MembershipUser currentUser = _provider.GetUser(userName, true /* userIsOnline */);

            PSMembershipProvider hmp = (PSMembershipProvider)Membership.Providers["PragmedicSQLProvider"];
            return hmp.ChangePassword(userName, oldPassword, newPassword);

            //return currentUser.ChangePassword(oldPassword, newPassword);
        }



    }
    
}
