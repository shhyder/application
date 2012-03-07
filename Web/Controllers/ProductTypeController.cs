using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using BusinessLogic;
using System.Text;

namespace Web.Controllers
{
    public class ProductTypeController : _BaseController
    {
        //
        // GET: /ProductType/

        public ActionResult Index()
        {
            int pageSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PageSize"].ToString());
            int pageCalled = 1;
            LoadProductTypeList(new FormCollection(), pageSize, pageCalled);
            SetListSearchSessionBlank();
            return View();
        }

        [NonAction]
        protected void SetListSearchSessionBlank()
        {
            Session[UIProductType.txtSerProductType.ToString()] = ViewData[UIProductType.txtSerProductType.ToString()] = "";
        }

        public ActionResult New()
        {
            setBlank();
            return View("NewProductType");
        }

        public ActionResult ProductType()
        {
            setBlank();
            return View("ProductType");
        }

        public ActionResult View(int? id)
        {
            SetProductType(Convert.ToInt32(id));
            
            return View("ProductType");
        }

        public ActionResult AttributeList(int? id)
        {
            GetList(Convert.ToInt32(id));
            return View("ProductTypeAttributes");
        }




        [NonAction]
        void GetList(int product_Type_ID)
        {
            ViewData[UIProductType.hidProductTypeID.ToString()] = product_Type_ID.ToString();
            BusinessLogic.Product_Type.Product_Type obj = new BusinessLogic.Product_Type.Product_Type();
            obj._ID = product_Type_ID;
            System.Data.DataSet ds = obj.GetProductTypeAttrubuteList(product_Type_ID);
            DataRow[] rows = ds.Tables["List"].Select(""," Attribute_ID Asc");
            int previous_attribute_ID = 0;

            StringBuilder sb = new StringBuilder();
            bool is_From_Detail = false;
            foreach (DataRow row in rows)
            {
                if ( Convert.ToInt32( row["Attribute_ID"] ) != previous_attribute_ID)
                {
                    if (previous_attribute_ID != 0)
                        sb.Append("</table>");
                    is_From_Detail = false;
                    previous_attribute_ID = Convert.ToInt32(row["Attribute_ID"]);


                    sb.Append("<table width=\"100%\"  ").Append("style = 'z-index: 1; top: 0px; left: 0px; display: none;'");
                    sb.Append(" id='attr").Append(row["Attribute_ID"].ToString()).Append("'   class='attributeVariantTable' >");
                    sb.Append("<tr><td>");
                    sb.Append("<a href='javascript:NewProductTypeAttributeVariant(").Append(row["Attribute_ID"].ToString()).Append(")' class='abtn ui-state-default ui-corner-all'>").Append(" Add ").Append(row["Attribute"].ToString() ).Append(" variant" ).Append("</a>");
                    sb.Append("</td></tr>");

                    if (row["Attribute_Variant"] != null)
                    {
                        sb.Append("<tr><td>").Append(row["Attribute_Variant"].ToString()).Append("</td></tr>");
                        is_From_Detail = true;
                    }

                }
                else
                {
                    if (row["Attribute_Variant"] != null)
                        sb.Append("<tr><td>").Append(row["Attribute_Variant"].ToString()).Append("</td></tr>");
                    ds.Tables["List"].Rows.Remove( row );
                    is_From_Detail = true;
                }
            }

            if( is_From_Detail )
                sb.Append("</table>");

            ViewData[UIProductType.htmlProductTypeAttributeVariant.ToString()] = sb.ToString();

            IQueryable<DataRow> dt = ds.Tables["List"].Rows.Cast<DataRow>().AsQueryable();
                ViewData[ UIProductType.listProductTypeAttribute.ToString()] = dt.ToList();

         
          
        }


        [NonAction]
        protected void SetProductType(int product_Type_ID)
        {

            BusinessLogic.Product_Type.Product_Type obj = new BusinessLogic.Product_Type.Product_Type();
            obj._ID = product_Type_ID;
            DataSet.DSParameter.Product_TypeRow row = obj.Get().Product_Type.FindByProduct_Type_ID(product_Type_ID);

            ViewData[UIProductType.hidProductTypeID.ToString()] = row.Product_Type_ID;
            ViewData[UIProductType.txtDescription.ToString()] = "";
            ViewData[UIProductType.txtEnterDate.ToString()] = "";
            ViewData[UIProductType.txtProductType.ToString()] = row.Product_Type;

            ViewData[UIProductType.txtStatus.ToString()] = "";

            ViewData["ErrorMessage"] = "";
            GetList(product_Type_ID);
        }





        public ActionResult List(int? id, FormCollection collection)
        {




            int pageSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PageSize"].ToString());
            int pageCalled = 0;
            if (id == null)
                pageCalled = 1;
            else
                pageCalled = (int)id;

            if (Session[UIProductType.txtSerProductType.ToString()] != null)
            {
                SetSessionInView();
            }

            if (collection.AllKeys.Length > 1)
            {
                pageCalled = 1;
                SetCollectionInSession(collection);
                SetSessionInView();
            }



            LoadProductTypeList(collection, pageSize, pageCalled);


            return SetView("List");

        }


        [NonAction]
        void SetSessionInView()
        {
            ViewData[UIProductType.txtSerProductType.ToString()] = Session[UIProductType.txtSerProductType.ToString()];
            
       

        }

        [NonAction]
        void SetCollectionInSession(FormCollection collection)
        {

            Session[UIProductType.txtSerProductType.ToString()] = collection[UIProductType.txtSerProductType.ToString()];
            
            //Init(ViewData[UIProductType.cobState.ToString()].ToString());

        }



        [NonAction]
        private void LoadProductTypeList(FormCollection collection, int pageSize, int pageCalled)
        {
            int _start;
            int _count;
            int _totalCount = 0;
            string _product_Type;



            _product_Type = collection[UIProductType.txtSerProductType.ToString()];




            if (_product_Type != null)
                _product_Type = _product_Type.Trim().Length == 0 ? null : _product_Type;

            


            BusinessLogic.Product_Type.Product_Type con = new BusinessLogic.Product_Type.Product_Type();
            System.Data.DataSet ds = con.GetPage(((pageCalled - 1) * pageSize + 1), pageSize, ref _totalCount, _product_Type);


            try
            {

                //---------------------------------------------- 
                //Fill Custom Data 
                string temp = "";




                int totalItems = _totalCount;

                Web.Model.Pagination pagination = new Web.Model.Pagination(true);

                pagination.BaseUrl = Url.Content("~/ProductType/List/");
                pagination.TotalRows = totalItems;
                pagination.CurPage = pageCalled;
                pagination.PerPage = pageSize;

                pagination.PrevLink = "Prev";
                pagination.NextLink = "Next";
                pagination.UpdateTargetId = "mainSearchPanel";

                string pageLinks = pagination.GetPageLinks();
                int start = (pageCalled - 1) * pagination.PerPage;
                int offset = pagination.PerPage;
                ViewData["pageLinks"] = pageLinks;

                ViewData["Count"] = totalItems;

                IQueryable<DataRow> dt = ds.Tables["List"].Rows.Cast<DataRow>().AsQueryable();
                ViewData[UIProductType.listProductType.ToString()] = dt.ToList();
                ViewData[UIProductType.hidCurrentPage.ToString()] = pageCalled;
            }
            catch (Exception e)
            {

            }
        }


        [NonAction]
        void setBlank()
        {
            
            ViewData[UIProductType.hidProductTypeID.ToString()] = 0;
            ViewData[UIProductType.txtDescription.ToString()] = "";
            ViewData[UIProductType.txtEnterDate.ToString()] = "";
            ViewData[UIProductType.txtProductType.ToString()] = "";

            ViewData[UIProductType.txtStatus.ToString()] = "";
            
            ViewData["ErrorMessage"] = "";
        }

        public ActionResult Save(FormCollection collection)
        {

            if (!Validation(collection))
            {
                SetCollectionToView(collection);
                return View("NewProductType");
            }



            DataSet.DSParameter ds = SetViewToDS(collection);

            BusinessLogic.Product_Type.Product_Type obj = new BusinessLogic.Product_Type.Product_Type();
            obj.New(ds);



            return Content(obj._ID.ToString());
        }

        [NonAction]
        bool Validation(FormCollection collection)
        {
            bool is_Valid = true;
            string error_Message = "";
            //ViewData[UIProductType.hidLatitude.ToString()] = collection[UIProductType.hidLatitude.ToString()];
            //ViewData[UIProductType.hidLongitude.ToString()] = collection[UIProductType.hidLongitude.ToString()];




            //if (collection[UIProductType.txtProduct_Type.ToString()].Length < 2)
            //{
            //    error_Message += "* Enter valid name. <br>";
            //    is_Valid = false;
            //}


            //string pattern = @"^[a-z][a-z|0-9|]*([_][a-z|0-9]+)*([.][a-z|" +
            //  @"0-9]+([_][a-z|0-9]+)*)?@[a-z][a-z|0-9|]*\.([a-z]" +
            //  @"[a-z|0-9]*(\.[a-z][a-z|0-9]*)?)$";
            //System.Text.RegularExpressions.Match match =
            //    Regex.Match(collection[UIProductType.txtEmail.ToString()].Trim(), pattern, RegexOptions.IgnoreCase);


            //if (!match.Success)
            //{
            //    error_Message += "* Invalid email address. <br>";
            //    is_Valid = false;
            //}

            //if (match.Success)
            //{
            //    BusinessLogic.Product_Type.Product_Type con = new BusinessLogic.Product_Type.Product_Type();
            //    int _totalCount = 0;
            //    //con.GetPage(1, 10, ref _totalCount, null, null, null, collection[UIProductType.txtEmail.ToString()].Trim(), null, null, null, null, null);
            //    if (_totalCount > 0)
            //    {
            //        error_Message += "* User " + collection[UIProductType.txtEmail.ToString()].Trim() + " had already been mQuoted<br>";
            //        is_Valid = false;
            //    }
            //}

            //if (collection[UIProductType.txtPhone.ToString()].Length < 5)
            //{
            //    error_Message += "* Enter valid day phone No. <br>";
            //    is_Valid = false;
            //}



            //if (collection[UIProductType.txtZipCode.ToString()].Length < 2)
            //{
            //    error_Message += "* Enter valid zip code. <br>";
            //    is_Valid = false;
            //}

            ////if (collection[UIProductType.txtBuildingAppartment.ToString()].Trim().Length < 2)
            ////{
            ////    error_Message += "Enter valid building/Apartment.<br>";
            ////    is_Valid = false;
            ////}





            ViewData["ErrorMessage"] = error_Message;
            return is_Valid;
        }


        [NonAction]
        void SetCollectionToView(FormCollection collection)
        {
            ViewData[UIProductType.hidProductTypeID.ToString()] = collection[UIProductType.hidProductTypeID.ToString()];
            ViewData[UIProductType.txtDescription.ToString()] = collection[UIProductType.txtDescription.ToString()];
            ViewData[UIProductType.txtEnterDate.ToString()] = collection[UIProductType.txtEnterDate.ToString()];
            ViewData[UIProductType.txtProductType.ToString()] = collection[UIProductType.txtProductType.ToString()];
            ViewData[UIProductType.txtStatus.ToString()] = collection[UIProductType.txtStatus.ToString()];
          

        }

        [NonAction]
        DataSet.DSParameter SetViewToDS(FormCollection collection)
        {


            DataSet.DSParameter ds = new DataSet.DSParameter();
            DataSet.DSParameter.Product_TypeRow row = ds.Product_Type.NewProduct_TypeRow();


            row[ds.Product_Type.Product_Type_IDColumn] = collection[ UIProductType.hidProductTypeID.ToString()];
            //row[ds.Product_Type..LongitudeColumn] = collection[UIProductType.txtDescription.ToString()];
            row[ds.Product_Type.Product_Type_IDColumn] = 1;
            row[ds.Product_Type.Product_TypeColumn] = collection[UIProductType.txtEnterDate.ToString()];
            

            //row[ds.Product_Type.BuildingAppartmentColumn] = collection[UIProduct_Type.txtBuildingAppartment.ToString()];
            //row[ds.Product_Type.Street_NoColumn] = collection[UIProduct_Type.txtStreet_No.ToString()];
            //row[ds.Product_Type.Street_NameColumn] = collection[UIProduct_Type.txtStreet_Name.ToString()];
            //row[ds.Product_Type.Zip_CodeColumn] = collection[UIProduct_Type.txtZipCode.ToString()];
            //row[ds.Product_Type.CityColumn] = collection[UIProductType.txtProductType.ToString()];
            //row[ds.Product_Type.StateColumn] = collection[UIProductType.txtStatus.ToString()];
            //row[ds.Product_Type.CountryColumn] = collection[UIProduct_Type.txtCountry.ToString()];








            ds.Product_Type.AddProduct_TypeRow(row);
            ds.AcceptChanges();
            return ds;
        }


    }
}
