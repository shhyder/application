using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.IO;


namespace Web.Controllers
{
    public class CategoryController : _BaseController
    {
        [Authorize]
        public ActionResult Index()
        {
            Init(System.Configuration.ConfigurationManager.AppSettings["PageSize"].ToString(), "");
            LoadList(1, null);
            return View("Index");
        }


       

        #region list section
        [Authorize]
        public JsonResult GetList(int? id, FormCollection collection)
        {
            //FormCollection  collection = new FormCollection(ConvertRawUrlToQuerystring());

            Category obj = new Category();

            ViewDataDictionary vdd = LoadList(id, collection);
            obj.CategoryList = RenderViewToStringAsHTML(this, "Grid", vdd);





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
            string searchCritaria = " ";


            if (collection != null)
            {

                if (collection[UIProductType.srhProductType.ToString()].Trim().Length != 0)
                {
                    filter += " and  Product_Type like '" + collection[UIProductType.srhProductType.ToString()].ToString() + "*'";
                    searchCritaria += " Category : " + collection[UIProductType.srhProductType.ToString()].ToString() + " ,";
                }


                





                if (collection[UIAll.gridSize.ToString()].Trim().Length != 0)
                {
                    pageSize = Convert.ToInt32(collection[UIAll.gridSize.ToString()]);

                }
            }

            else
            {
                searchCritaria += " Status : In-Active" + " ,";
                searchCritaria += " Type : All ,";
            }

            ViewData["searchCritaria"] = searchCritaria.Remove(searchCritaria.Length - 1);

            DataRow[] rows = ds.Product_Type.Select(filter);

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
                pagination.BaseUrl = Url.Content("~/Category/GetList/");
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
                ViewData[UIProductType.listProductType.ToString()] = dt.Skip(start).Take(offset).ToList();

            }
            catch (Exception e)
            {

            }
            return ViewData;

        }




        [Authorize]
        public ActionResult About()
        {
            return View();
        }


        public class Category
        {
            public string CategoryList { get; set; }
        }

        #endregion


        #region CRUD

        [Authorize]
        public ActionResult New()
        {


            ViewData[UIProductType.txtProductType.ToString()] = "";
            ViewData[UIProductType.hidProductTypeID.ToString()] = "0";
            
            
            ViewData[UIProductType.hidState.ToString()] = 3;
            return View("Category");
        }



        [NonAction]
        void Init(string selectedGridSize, string selectedProductType)
        {
            DataSet.DSParameter ds = (DataSet.DSParameter)System.Web.HttpContext.Current.Cache["data"];

            initGridSize(selectedGridSize);


            #region product type



            List<SelectListItem> list = new List<SelectListItem>();
            DataRow[] rows = ds.Product_Type.Select();


            list.Add(new SelectListItem
            {
                Text = "< Select >",
                Value = "0",
            });


            for (int i = 0; i < rows.Length; i++)
            {
                list.Add(new SelectListItem
                {
                    Text = rows[i][ds.Product_Type.Product_TypeColumn].ToString(),
                    Value = rows[i][ds.Product_Type.Product_Type_IDColumn].ToString(),
                });
            }


            if (selectedProductType.Trim().Length == 0 || selectedProductType == "< Select >")
                ViewData[UIProductType.listProductType.ToString()] = new SelectList(list, "Value", "Text");
            else
                ViewData[UIProductType.listProductType.ToString()] = new SelectList(list, "Value", "Text", selectedProductType);


            #endregion

        }


        [Authorize]
        public JsonResult Save()
        {


            CategoryManifest pm = new CategoryManifest();

            // 
            if (!Validate(ref pm))
            {
                BusinessLogic.Product_Type.Product_Type sp = new BusinessLogic.Product_Type.Product_Type();
                DataSet.DSParameter ds = new DataSet.DSParameter();

                ds.Product_Type.Product_Type_IDColumn.ReadOnly = false;

                ds.Product_Type.AddProduct_TypeRow(pm.category, 1, DateTime.Now, "", pm.is_Active);
                ds.EnforceConstraints = false;
                ds.Product_Type[0]["Product_Type_ID"] = Convert.ToInt32(pm.category_ID);

                if (pm.hidState == 3)
                {
                    sp.New(ds);
                }
                else
                {
                    sp.Update(ds);
                }
                UpdateCache();
            }


            return this.Json(pm, JsonRequestBehavior.AllowGet);
        }


        [Authorize]
        public JsonResult Delete(string codeSLSP, string SalesRepCode, string Territory)
        {


            CategoryManifest spm = new CategoryManifest();



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
        public ActionResult Edit(int id)
        {
            DataSet.DSParameter ds = (DataSet.DSParameter)System.Web.HttpContext.Current.Cache["data"];
            string filter = " Product_Type_ID = '" + id.ToString();


            DataSet.DSParameter.Product_TypeRow proRow = ds.Product_Type.FindByProduct_Type_ID(id);
            ViewData[UIProductType.txtProductType.ToString()] = proRow.Product_Type;
            
            ViewData[UIProductType.hidProductTypeID.ToString()] = proRow.Product_Type_ID;
            ViewData[UIProductType.is_Active.ToString()] = proRow.Is_Active;


            Init("", proRow.Product_Type_ID.ToString());

            ViewData[UIProductType.hidState.ToString()] = 2;

            return View("Category");

        }







        [NonAction]
        bool Validate(ref  CategoryManifest prm)
        {
            FormCollection collection = new FormCollection(ConvertRawUrlToQuerystring());



            prm.category = collection[UIProductType.txtProductType.ToString()];
            prm.categoryError = "";
            
            prm.category_ID = collection[UIProductType.hidProductTypeID.ToString()];

            if (collection[UIStore.Is_Active.ToString()] == null)
            {
                prm.is_Active = false;
            }
            else
            {
                prm.is_Active = true;
            }


            prm.hidState = Convert.ToInt32(collection[UIProductType.hidState.ToString()]);


            if (prm.category.Trim().Length == 0)
            {
                prm.categoryError = "Enter valid category";
                prm.Has_Error = true;
            }

            return prm.Has_Error;
        }




        #endregion



    

        public class CategoryManifest
        {
            public string category_ID { get; set; }
            public string category { get; set; }
            public string categoryError { get; set; }
            public bool is_Active { get; set; }

            public int hidState { get; set; }
            public bool Has_Error { get; set; }
        }


    }
}
