using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;

namespace Web.Controllers
{
    
    public class StoreRepeatationController : _BaseController
    {
        [Authorize]
        public ActionResult Index()
        {
            Init(System.Configuration.ConfigurationManager.AppSettings["PageSize"].ToString());
            LoadList(1, null);
            return View("Index");
        }


        #region list section
        [Authorize]
        public JsonResult GetList(int? id, FormCollection collection)
        {
            //FormCollection  collection = new FormCollection(ConvertRawUrlToQuerystring());

            Widget obj = new Widget();

            ViewDataDictionary vdd = LoadList(id, collection);
            obj.StoreList = RenderViewToStringAsHTML(this, "Grid", vdd);





            return Json(obj, JsonRequestBehavior.AllowGet);
        }



        [NonAction]
        private ViewDataDictionary LoadList(int? id, FormCollection collection)
        {
            int _totalCount = 0;

            DataSet.DSParameter ds = (DataSet.DSParameter)System.Web.HttpContext.Current.Cache["data"];




            //_totalCount = ds.Tables[BusinessLogic.Base.Tables.ItemLineDetail.ToString()].Rows.Count;


            int pageSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PageSize"].ToString());

            string filter = " 1=1 ";
            string searchCritaria = "";
            if (collection != null)
            {

                if (collection[UIStore.srhCode.ToString()].Trim().Length != 0)
                {
                    filter += " and Code like '" + collection[UIStore.srhCode.ToString()].ToString().Replace("'", "") + "*'";
                }

                if (collection[UIStore.srhStore.ToString()].Trim().Length != 0)
                {
                    filter += " and Distributor like '" + collection[UIStore.srhStore.ToString()].ToString().Replace("'", "") + "'";
                }

                if (collection[UIStore.srhAddress.ToString()].Trim().Length != 0)
                {
                    filter += " and Address like '" + collection[UIStore.srhAddress.ToString()].ToString().Replace("'", "") + "*'";
                }

                if (collection[UIStore.srhCity.ToString()].Trim().Length != 0)
                {
                    filter += " and City like '" + collection[UIStore.srhCity.ToString()].ToString() + "*'";
                }

                if (collection[UIStore.srhState.ToString()].Trim().Length != 0)
                {
                    filter += " and State like '" + collection[UIStore.srhState.ToString()].ToString() + "*'";
                }

                if (collection[UIStore.srhEmail.ToString()].Trim().Length != 0)
                {
                    filter += " and Email like '" + collection[UIStore.srhEmail.ToString()].ToString() + "*'";
                }

                if (collection[UIStore.srhPhone1.ToString()].Trim().Length != 0)
                {
                    filter += " and Phone1 like '" + collection[UIStore.srhPhone1.ToString()].ToString() + "*'";
                }


                if (collection[UIStore.srhStatus.ToString()] == null)
                {
                    filter += " and is_Active = false ";
                    searchCritaria += " Status : In-Active" + " ,";
                }
                else
                {
                    filter += " and is_Active = true ";
                    searchCritaria += " Code : Active" + " ,";
                }



                if (collection[UIAll.gridSize.ToString()].Trim().Length != 0)
                {
                    pageSize = Convert.ToInt32(collection[UIAll.gridSize.ToString()]);
                }
            }



            DataRow[] rows = ds.Distributor.Select(filter, " code ");

            _totalCount = rows.Length;

            int pageCalled = 0;
            if (id == null)
                pageCalled = 1;
            else
                pageCalled = (int)id;


            try
            {

                //---------------------------------------------- 
                //Fill Custom Data 
                string temp = "";

                int totalItems = _totalCount;

                Web.Model.Pagination pagination = new Web.Model.Pagination(true);
                pagination.BaseUrl = Url.Content("~/Store/GetList/");
                pagination.TrailingQueryString = "";

                pagination.TrailingQueryString = "";
                pagination.Is_UserClickFunction = true;
                pagination.ClickFunctionName = "javascript:GetList";

                Session["QSCurDL"] = "";
                ViewData["queryString"] = "";
                ViewData["product_IDs"] = "";

                pagination.TotalRows = totalItems;
                pagination.CurPage = pageCalled;
                pagination.PerPage = pageSize;

                pagination.PrevLink = "Prev";
                pagination.NextLink = "Next";
                pagination.UpdateTargetId = "dealerList";
                pagination._class = " class='navButton'";

                string pageLinks = pagination.GetPageLinks();
                int start = (pageCalled - 1) * pagination.PerPage;
                int offset = pagination.PerPage;
                ViewData["pageLinks"] = pageLinks;

                ViewData["Count"] = totalItems;

                ViewData["start"] = (start + 1).ToString();

                IQueryable<DataRow> dt = rows.Cast<DataRow>().AsQueryable();
                ViewData[UIStore.listStore.ToString()] = dt.Skip(start).Take(offset).ToList();

            }
            catch (Exception e)
            {

            }
            return ViewData;

        }


        [NonAction]
        void Init(string selectedGridSize)
        {



            initGridSize(selectedGridSize);



        }




        public ActionResult About()
        {
            return View();
        }


        public class Widget
        {
            public string StoreList { get; set; }
        }

        #endregion

        #region CRUD

        [Authorize]
        public ActionResult New()
        {


            ViewData[UIStore.hidID.ToString()] = "0";
            ViewData[UIStore.hidLatitude.ToString()] = "";
            ViewData[UIStore.hidLongitude.ToString()] = "";
            ViewData[UIStore.txtAddress.ToString()] = "";
            ViewData[UIStore.txtBuildingAppartment.ToString()] = "";
            ViewData[UIStore.txtCity.ToString()] = "";
            ViewData[UIStore.txtCode.ToString()] = "";
            ViewData[UIStore.txtCountry.ToString()] = "";
            ViewData[UIStore.txtEmail.ToString()] = "";
            ViewData[UIStore.txtEmail2.ToString()] = "";
            ViewData[UIStore.txtPhone1.ToString()] = "";
            ViewData[UIStore.txtPhone2.ToString()] = "";
            ViewData[UIStore.txtState.ToString()] = "";
            ViewData[UIStore.txtStore.ToString()] = "";
            ViewData[UIStore.txtStreetName.ToString()] = "";
            ViewData[UIStore.txtStreetNo.ToString()] = "";
            ViewData[UIStore.txtWebsite.ToString()] = "";
            ViewData[UIStore.txtZipCode.ToString()] = "";
            ViewData[UIStore.hidState.ToString()] = 3;

            ViewData[UIStore.txtFullAddress.ToString()] = "";
            ViewData[UIStore.txtCounty.ToString()] = "";

            return View("Store");
        }






        [Authorize]
        public JsonResult Save()
        {


            StoreManifest stm = new StoreManifest();


            if (!Validate(ref stm))
            {
                BusinessLogic.Distributor.Distributor sp = new BusinessLogic.Distributor.Distributor();
                DataSet.DSParameter ds = new DataSet.DSParameter();

                ds.Distributor.IDColumn.ReadOnly = false;


                ds.Distributor.AddDistributorRow(stm.Is_Active, Convert.ToInt32(stm.Code), stm.Store, stm.City, stm.State,
                    stm.FullAddress, Convert.ToDecimal(stm.Latitude), Convert.ToDecimal(stm.Longitude), stm.Email1, stm.ZipCode, stm.Address, stm.Street_No,
                    stm.Street_Name, stm.Country, stm.Phone1, stm.Phone2, stm.Email2, stm.Website, stm.Is_Email1_Display, stm.Is_Email2_Display,
                    stm.Is_Phone1_Display, stm.Is_Phone2_Display, stm.Is_Website_Display);
                ds.EnforceConstraints = false;
                ds.Distributor[0]["ID"] = Convert.ToInt32(stm.ID);

                if (stm.hidState == 3)
                {
                    sp.New(ds);
                }
                else
                {
                    sp.Update(ds);
                }
                UpdateCache();
            }


            return this.Json(stm, JsonRequestBehavior.AllowGet);
        }


        [Authorize]
        public JsonResult Delete(string codeSLSP, string SalesRepCode, string Territory)
        {


            StoreManifest spm = new StoreManifest();



            //BusinessLogic.SalesPerson.SalesPerson sp = new BusinessLogic.SalesPerson.SalesPerson();
            //DataSet.DSParameter ds = new DataSet.DSParameter();
            //ds.ARSAP.AddARSAPRow(codeSLSP, "", SalesRepCode, Territory, "");
            //if (sp.Delete(ds))
            //{
            //    spm.Has_Error = false;
            //    MvcApplication.RefreshData("", null, System.Web.Caching.CacheItemRemovedReason.DependencyChanged);
            //}
            //else
            //{
            //    spm.Has_Error = true;

            //}

            return this.Json(spm, JsonRequestBehavior.AllowGet);
        }



        [Authorize]
        public ActionResult Edit(string id)
        {
            DataSet.DSParameter ds = (DataSet.DSParameter)System.Web.HttpContext.Current.Cache["data"];
            string filter = " id = " + id.ToString();
            DataRow[] rows = ds.Distributor.Select(filter);

            if (rows.Length == 1)
            {
                DataSet.DSParameter.DistributorRow dispRow = (DataSet.DSParameter.DistributorRow)rows[0];
                ViewData[UIStore.hidID.ToString()] = dispRow.ID;
                ViewData[UIStore.hidLatitude.ToString()] = dispRow.IsLatitudeNull() ? 0 : dispRow.Latitude;
                ViewData[UIStore.hidLongitude.ToString()] = dispRow.IsLongitudeNull() ? 0 : dispRow.Longitude;
                ViewData[UIStore.txtAddress.ToString()] = dispRow.IsBuildingAppartmentNull() ? dispRow.Address : dispRow.BuildingAppartment;
                ViewData[UIStore.txtFullAddress.ToString()] = dispRow.Address;
                ViewData[UIStore.txtCity.ToString()] = dispRow.IsCityNull() ? "" : dispRow.City;
                ViewData[UIStore.txtCode.ToString()] = dispRow.Distributor_ID;
                ViewData[UIStore.txtCountry.ToString()] = dispRow.IsCountryNull() ? "" : dispRow.Country;
                ViewData[UIStore.txtEmail.ToString()] = dispRow.Email;
                ViewData[UIStore.txtEmail2.ToString()] = dispRow.Email2;
                ViewData[UIStore.txtPhone1.ToString()] = dispRow.Phone1;
                ViewData[UIStore.txtPhone2.ToString()] = dispRow.Phone2;
                ViewData[UIStore.txtState.ToString()] = dispRow.IsStateNull() ? "" : dispRow.State;
                ViewData[UIStore.txtStore.ToString()] = dispRow.Distributor;
                //ViewData[UIStore.txtStreetName.ToString()] = dispRow.Street_Name;
                ViewData[UIStore.txtStreetNo.ToString()] = dispRow.IsStreet_NoNull() ? "" : dispRow.Street_No;
                ViewData[UIStore.txtWebsite.ToString()] = dispRow.Website;
                ViewData[UIStore.txtZipCode.ToString()] = dispRow.IsZip_CodeNull() ? "" : dispRow.Zip_Code;
                ViewData[UIStore.hidState.ToString()] = 3;


                ViewData[UIStore.txtCounty.ToString()] = "";
                ViewData[UIStore.Is_Active.ToString()] = dispRow.Is_Active;


                ViewData[UIStore.Is_Email1_Display.ToString()] = dispRow.Is_Email1_Display;
                ViewData[UIStore.Is_Email2_Display.ToString()] = dispRow.Is_Email2_Display;
                ViewData[UIStore.Is_Phone1_Display.ToString()] = dispRow.Is_Phone1_Display;
                ViewData[UIStore.Is_Phone2_Display.ToString()] = dispRow.Is_Phone2_Display;
                ViewData[UIStore.Is_Website_Display.ToString()] = dispRow.Is_Website_Display;

            }
            else
            {
                ViewData[UIStore.hidID.ToString()] = "";
                ViewData[UIStore.hidLatitude.ToString()] = "";
                ViewData[UIStore.hidLongitude.ToString()] = "";
                ViewData[UIStore.txtAddress.ToString()] = "";
                ViewData[UIStore.txtBuildingAppartment.ToString()] = "";
                ViewData[UIStore.txtCity.ToString()] = "";
                ViewData[UIStore.txtCode.ToString()] = "";
                ViewData[UIStore.txtCountry.ToString()] = "";
                ViewData[UIStore.txtEmail.ToString()] = "";
                ViewData[UIStore.txtEmail2.ToString()] = "";
                ViewData[UIStore.txtPhone1.ToString()] = "";
                ViewData[UIStore.txtPhone2.ToString()] = "";
                ViewData[UIStore.txtState.ToString()] = "";
                ViewData[UIStore.txtStore.ToString()] = "";
                ViewData[UIStore.txtStreetName.ToString()] = "";
                ViewData[UIStore.txtStreetNo.ToString()] = "";
                ViewData[UIStore.txtWebsite.ToString()] = "";
                ViewData[UIStore.txtZipCode.ToString()] = "";
                ViewData[UIStore.hidState.ToString()] = 3;

                ViewData[UIStore.txtFullAddress.ToString()] = "";
                ViewData[UIStore.txtCounty.ToString()] = "";

                ViewData[UIStore.Is_Email1_Display.ToString()] = 0;
                ViewData[UIStore.Is_Email2_Display.ToString()] = 0;
                ViewData[UIStore.Is_Phone1_Display.ToString()] = 0;
                ViewData[UIStore.Is_Phone2_Display.ToString()] = 0;
                ViewData[UIStore.Is_Website_Display.ToString()] = 0;
            }

            ViewData[UIStore.hidState.ToString()] = 2;

            return View("Store");

        }







        [NonAction]
        bool Validate(ref StoreManifest stm)
        {
            FormCollection collection = new FormCollection(ConvertRawUrlToQuerystring());

            stm.Store = collection[UIStore.txtStore.ToString()].ToString();
            stm.Address = collection[UIStore.txtAddress.ToString()].ToString();
            stm.FullAddress = collection[UIStore.txtFullAddress.ToString()].ToString();
            stm.City = collection[UIStore.txtCity.ToString()].ToString();
            stm.Country = collection[UIStore.txtCountry.ToString()].ToString();
            stm.Email1 = collection[UIStore.txtEmail.ToString()].ToString();
            stm.Email2 = collection[UIStore.txtEmail2.ToString()].ToString();
            stm.hidState = Convert.ToInt32(collection[UIStore.hidState.ToString()]);
            stm.ID = Convert.ToInt32(collection[UIStore.hidID.ToString()].ToString());
            stm.Latitude = collection[UIStore.hidLatitude.ToString()].ToString(); ;
            stm.Longitude = collection[UIStore.hidLongitude.ToString()].ToString(); ;

            stm.Phone1 = collection[UIStore.txtPhone1.ToString()].ToString(); ;
            stm.Phone2 = collection[UIStore.txtPhone2.ToString()].ToString(); ;
            stm.State = collection[UIStore.txtState.ToString()].ToString(); ;
            stm.Code = collection[UIStore.txtCode.ToString()].ToString(); ;
            //stm.Street_Name = collection[UIStore.txtStreetName.ToString()].ToString(); ;
            stm.Street_No = collection[UIStore.txtStreetNo.ToString()].ToString(); ;
            stm.Website = collection[UIStore.txtWebsite.ToString()].ToString(); ;
            stm.ZipCode = collection[UIStore.txtZipCode.ToString()].ToString(); ;

            stm.Is_Email1_Display = collection[UIStore.Is_Email1_Display.ToString()] == null ? false : true;
            stm.Is_Email2_Display = collection[UIStore.Is_Email2_Display.ToString()] == null ? false : true;
            stm.Is_Phone1_Display = collection[UIStore.Is_Phone1_Display.ToString()] == null ? false : true;
            stm.Is_Phone2_Display = collection[UIStore.Is_Phone2_Display.ToString()] == null ? false : true;
            stm.Is_Website_Display = collection[UIStore.Is_Website_Display.ToString()] == null ? false : true;


            if (collection[UIStore.Is_Active.ToString()] == null)
            {
                stm.Is_Active = false;
            }
            else
            {
                stm.Is_Active = true;
            }



            if (stm.Code.Length <= 2)
            {
                stm.CodeError = "Enter Valid account id";
                stm.Has_Error = true;
            }



            if (stm.Store.Length <= 2)
            {
                stm.StoreError = "Enter Valid store name";
                stm.Has_Error = true;
            }


            if (stm.Address.Length <= 3)
            {
                stm.AddressError = "Enter Valid Address";
                stm.Has_Error = true;
            }

            if (stm.FullAddress.Length <= 3)
            {
                stm.FullAddressError = "Enter Valid Address";
                stm.Has_Error = true;
            }


            if (stm.City.Length <= 2)
            {
                stm.CityError = "Enter valid city";
                stm.Has_Error = true;
            }

            if (stm.Country.Length <= 0)
            {
                stm.CountryError = "Enter sales country";
                stm.Has_Error = true;
            }

            if (!isEmail(stm.Email1.Trim()) && stm.Email1.Trim().Length != 0)
            {
                stm.Email1Error = "Enter valid Email";
                stm.Has_Error = true;
            }

            if (!isEmail(stm.Email2.Trim()) && stm.Email2.Trim().Length != 0)
            {
                stm.Email2Error = "Enter valid Email2";
                stm.Has_Error = true;
            }

            if (stm.Latitude.Length == 0 || stm.Longitude.Length == 0)
            {
                stm.AddressError = "Enter valid address";
                stm.Has_Error = true;
            }

            if (stm.Phone1.Length < 2 && stm.Phone1.Length != 0)
            {
                stm.Phone1Error = "Enter valid Phone";
                stm.Has_Error = true;
            }

            if (stm.Phone2.Length < 2 && stm.Phone2.Length != 0)
            {
                stm.Phone2Error = "Enter valid Phone";
                stm.Has_Error = true;
            }






            if (!IsUrlValid(stm.Website.Trim()) && stm.Website.Trim().Length != 0)
            {
                stm.WebsiteError = "Enter valid Website";
                stm.Has_Error = true;
            }

            if (!IsNumeric(stm.ZipCode))
            {
                stm.ZipCodeError = "Enter valid Zip Code";
                stm.Has_Error = true;
            }


            return stm.Has_Error;
        }




        #endregion


        public class StoreManifest
        {
            public int ID { get; set; }
            public bool Is_Active { get; set; }
            public string Code { get; set; }
            public string Store { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string Address { get; set; }
            public string FullAddress { get; set; }
            public string Latitude { get; set; }
            public string Longitude { get; set; }
            public string Email1 { get; set; }
            public string Email2 { get; set; }
            public string Street_No { get; set; }
            public string Street_Name { get; set; }
            public string Country { get; set; }
            public string Phone1 { get; set; }
            public string Phone2 { get; set; }
            public string Website { get; set; }
            public string ZipCode { get; set; }

            public bool Is_Email1_Display { get; set; }
            public bool Is_Email2_Display { get; set; }
            public bool Is_Phone1_Display { get; set; }
            public bool Is_Phone2_Display { get; set; }
            public bool Is_Website_Display { get; set; }


            public int hidState { get; set; }
            public string CodeError { get; set; }
            public string StoreError { get; set; }
            public string CityError { get; set; }
            public string StateError { get; set; }
            public string FullAddressError { get; set; }
            public string AddressError { get; set; }
            public string Email1Error { get; set; }
            public string Email2Error { get; set; }
            public string Street_NoError { get; set; }
            public string Street_NameError { get; set; }
            public string CountryError { get; set; }
            public string Phone1Error { get; set; }
            public string Phone2Error { get; set; }
            public string WebsiteError { get; set; }
            public string ZipCodeError { get; set; }
            public bool Has_Error { get; set; }
        }

    }
}
