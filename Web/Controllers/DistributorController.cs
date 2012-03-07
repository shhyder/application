using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogic;
using System.Text.RegularExpressions;
using DataSet;



namespace Web.Controllers
{
    public class DistributorController : Controller
    {
        //
        // GET: /Distributor/

        public ActionResult Index()
        {
            ViewData[UIDistributor.txtErrorMessage.ToString()] = "";
            return View();
        }

        public ActionResult New()
        {
            return View("Distributor");
        }

        public ActionResult Address()
        {
            ViewData[UIDistributor.txtErrorMessage.ToString()] = "";
            return View("Address");
        }

        public ActionResult Distributor(FormCollection collection)
        {

            if (collection["btnSubmit"] != null)
            {
                switch (collection["btnSubmit"].ToString())
                {
                    // Save Click
                    case "Save":
                        return Save(collection);
                        break;

                    //// Edit click
                    //case "Edit":
                    //    SetDSView(Convert.ToInt32(collection[UIServiceAuthorization.hidService_Authorization.ToString()]), Status.Edit);
                    //    return SetView("ServiceAuthorization"); 
                    //    break!

                    // Cancel click
                    case "Cancel":
                        break;

                    //// New Click
                    //case "New":
                    //    SetBlankView();
                    //    return SetView("ServiceAuthorization");
                    //    break;
                    case "Back":
                        return RedirectToAction("Address", "Distributor");
                        break;
                }
            }


            if (collection[UIDistributor.hidLatitude.ToString()].Length == 0 || collection[UIDistributor.hidLongitude.ToString()].Length == 0)
            {
                ViewData[UIDistributor.txtErrorMessage.ToString()] = "Invalid address";
                return View("Address");
            }

            ViewData[UIDistributor.hidLatitude.ToString()] = collection[UIDistributor.hidLatitude.ToString()];
            ViewData[UIDistributor.hidLongitude.ToString()] = collection[UIDistributor.hidLongitude.ToString()];
            setBlank();
            return View("Distributor");
        }

        [NonAction]
        void setBlank()
        {
            ViewData[UIDistributor.txtEmail.ToString()] = "";
            ViewData[UIDistributor.txtErrorMessage.ToString()] = "";
            ViewData[UIDistributor.txtDistributor.ToString()] = "";
            ViewData[UIDistributor.txtZipCode.ToString()] = "";
            
            ViewData[UIDistributor.txtBuildingAppartment.ToString()] = "";
            ViewData[UIDistributor.txtStreet_No.ToString()] = "";
            ViewData[UIDistributor.txtStreet_Name.ToString()] = "";
            ViewData[UIDistributor.txtZipCode.ToString()] = "";
            ViewData[UIDistributor.txtCity.ToString()] = "";
            ViewData[UIDistributor.txtState.ToString()] = "";
            ViewData[UIDistributor.txtCountry.ToString()] = "";
            ViewData[UIDistributor.txtPhone.ToString()] = "";
            ViewData[UIDistributor.txtFax.ToString()] = "";



            ViewData["ErrorMessage"] = "";
        }


        [NonAction]
        ActionResult Save(FormCollection collection)
        {
           
            if (!Validation(collection))
            {
                SetCollectionToView(collection);
                return View("Distributor");
            }



            DataSet.DSParameter ds = SetViewToDS(collection);

            BusinessLogic.Distributor.Distributor obj = new BusinessLogic.Distributor.Distributor();
            DSParameter _ds = obj.New(ds);



            //string fromEmail = "eric.Hyder@aiminsight.com";//sending email from...
            //string ToEmail = _ds.Distributor[0]["Email"].ToString();
            //string body = "testing testing testing";
            //string subject = "mQuote process initiation Notification";


            //try
            //{
            //    SmtpClient sMail = new SmtpClient("smtp.gmail.com");//"aimexch.aiminsight.com");//exchange or smtp server goes here.
            //    sMail.Port = 587;
            //    sMail.EnableSsl = true;

            //    sMail.Credentials = new NetworkCredential("shhyder@gmail.com", "has537167");// "aiminsight/eric", "aiminsight1");// ( this line most likely wont be needed if you are already an authenticated user.

            //    ViewData[UIDistributorReceivedMail.lblCustomer.ToString()] = _ds.Distributor[0]["Customer_Name"].ToString();
            //    ViewData[UIDistributorReceivedMail.lblQuote_No.ToString()] = "000" + _ds.Distributor[0]["Distributor_ID"].ToString();
            //    ViewData[UIDistributorReceivedMail.lblEmail.ToString()] = _ds.Distributor[0]["Email"].ToString();
            //    ViewData[UIDistributorReceivedMail.lblLogonLink.ToString()] = " http://wdllc.aiminsight.com/Martifer/Tracking/Logon";// Model.Utility.Get_Path() + "/Tracking/Logon";

            //    MailMessage mm = new MailMessage(fromEmail, ToEmail, subject, RenderViewToStringAsHTML(this, "Mail", ViewData));
            //    mm.IsBodyHtml = true;
            //    sMail.Send(mm);
            //}
            //catch (Exception ex)
            //{
            //    //do something after error if there is one
            //}

            ViewData["GenericMessage"] = "your business detial has been saved<br/> <br/>and is puted forward for further process";

            return View("GenericMessage");
        }

        [NonAction]
        bool Validation(FormCollection collection)
        {
            bool is_Valid = true;
            string error_Message = "";
            ViewData[UIDistributor.hidLatitude.ToString()] = collection[UIDistributor.hidLatitude.ToString()];
            ViewData[UIDistributor.hidLongitude.ToString()] = collection[UIDistributor.hidLongitude.ToString()];




            if (collection[UIDistributor.txtDistributor.ToString()].Length < 2)
            {
                error_Message += "* Enter valid name. <br>";
                is_Valid = false;
            }


            string pattern = @"^[a-z][a-z|0-9|]*([_][a-z|0-9]+)*([.][a-z|" +
              @"0-9]+([_][a-z|0-9]+)*)?@[a-z][a-z|0-9|]*\.([a-z]" +
              @"[a-z|0-9]*(\.[a-z][a-z|0-9]*)?)$";
            System.Text.RegularExpressions.Match match =
                Regex.Match(collection[UIDistributor.txtEmail.ToString()].Trim(), pattern, RegexOptions.IgnoreCase);


            if (!match.Success)
            {
                error_Message += "* Invalid email address. <br>";
                is_Valid = false;
            }

            if (match.Success)
            {
                BusinessLogic.Distributor.Distributor con = new BusinessLogic.Distributor.Distributor();
                int _totalCount = 0;
                //con.GetPage(1, 10, ref _totalCount, null, null, null, collection[UIDistributor.txtEmail.ToString()].Trim(), null, null, null, null, null);
                if (_totalCount > 0)
                {
                    error_Message += "* User " + collection[UIDistributor.txtEmail.ToString()].Trim() + " had already been mQuoted<br>";
                    is_Valid = false;
                }
            }

            if (collection[UIDistributor.txtPhone.ToString()].Length < 5)
            {
                error_Message += "* Enter valid day phone No. <br>";
                is_Valid = false;
            }



            if (collection[UIDistributor.txtZipCode.ToString()].Length < 2)
            {
                error_Message += "* Enter valid zip code. <br>";
                is_Valid = false;
            }

            //if (collection[UIDistributor.txtBuildingAppartment.ToString()].Trim().Length < 2)
            //{
            //    error_Message += "Enter valid building/Apartment.<br>";
            //    is_Valid = false;
            //}


           


            ViewData["ErrorMessage"] = error_Message;
            return is_Valid;
        }


        [NonAction]
        void SetCollectionToView(FormCollection collection)
        {
            ViewData[UIDistributor.hidLatitude.ToString()] = collection[UIDistributor.hidLatitude.ToString()];
            ViewData[UIDistributor.hidLongitude.ToString()] = collection[UIDistributor.hidLongitude.ToString()];
            ViewData[UIDistributor.txtPhone.ToString()] = collection[UIDistributor.txtPhone.ToString()];
            ViewData[UIDistributor.txtFax.ToString()] = collection[UIDistributor.txtFax.ToString()];
            ViewData[UIDistributor.txtEmail.ToString()] = collection[UIDistributor.txtEmail.ToString()];
            ViewData[UIDistributor.txtDistributor.ToString()] = collection[UIDistributor.txtDistributor.ToString()];
            ViewData[UIDistributor.txtBuildingAppartment.ToString()] = collection[UIDistributor.txtBuildingAppartment.ToString()];
            ViewData[UIDistributor.txtZipCode.ToString()] = collection[UIDistributor.txtZipCode.ToString()];
            
            
            ViewData[UIDistributor.txtBuildingAppartment.ToString()] = collection[UIDistributor.txtBuildingAppartment.ToString()];
            ViewData[UIDistributor.txtStreet_No.ToString()] = collection[UIDistributor.txtStreet_No.ToString()];
            ViewData[UIDistributor.txtStreet_Name.ToString()] = collection[UIDistributor.txtStreet_Name.ToString()];
            ViewData[UIDistributor.txtZipCode.ToString()] = collection[UIDistributor.txtZipCode.ToString()];
            ViewData[UIDistributor.txtCity.ToString()] = collection[UIDistributor.txtCity.ToString()];
            ViewData[UIDistributor.txtState.ToString()] = collection[UIDistributor.txtState.ToString()];
            ViewData[UIDistributor.txtCountry.ToString()] = collection[UIDistributor.txtCountry.ToString()];


        }

        [NonAction]
        DataSet.DSParameter SetViewToDS(FormCollection collection)
        {


            DataSet.DSParameter ds = new DataSet.DSParameter ();
            DataSet.DSParameter.DistributorRow row = ds.Distributor.NewDistributorRow();


            row[ds.Distributor.LatitudeColumn] = collection[UIDistributor.hidLatitude.ToString()];
            row[ds.Distributor.LongitudeColumn] = collection[UIDistributor.hidLongitude.ToString()];
            //row[ds.Distributor.pho.pp.Best_Day_Time_PhoneColumn] = collection[UIDistributor.txtDayPhone.ToString()];
            row[ds.Distributor.EmailColumn] = collection[UIDistributor.txtEmail.ToString()];
            row[ds.Distributor.Distributor_IDColumn] = 1;
            row[ds.Distributor.DistributorColumn] = collection[UIDistributor.txtDistributor.ToString()];
            row[ds.Distributor.AddressColumn] = "";// collection[UIDistributor.txtAddress.ToString()];
            row[ds.Distributor.Zip_CodeColumn] = collection[UIDistributor.txtZipCode.ToString()];


            row[ds.Distributor.BuildingAppartmentColumn] = collection[UIDistributor.txtBuildingAppartment.ToString()];
            row[ds.Distributor.Street_NoColumn] = collection[UIDistributor.txtStreet_No.ToString()];
            row[ds.Distributor.Street_NameColumn] = collection[UIDistributor.txtStreet_Name.ToString()];
            row[ds.Distributor.Zip_CodeColumn] = collection[UIDistributor.txtZipCode.ToString()];
            row[ds.Distributor.CityColumn] = collection[UIDistributor.txtCity.ToString()];
            row[ds.Distributor.StateColumn] = collection[UIDistributor.txtState.ToString()];
            row[ds.Distributor.CountryColumn] = collection[UIDistributor.txtCountry.ToString()];
           
           
           
           




            ds.Distributor.AddDistributorRow(row);
            ds.AcceptChanges();
            return ds;
        }



    }
}
