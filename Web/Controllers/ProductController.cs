using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.IO;

namespace Web.Controllers
{
    
    public class ProductController : _BaseController
    {
        [Authorize]
        public ActionResult Index()
        {
            Init(System.Configuration.ConfigurationManager.AppSettings["PageSize"].ToString(),"");
            LoadList(1, null);
            return View("Index");
        }


        [Authorize]
        public ActionResult ProductWebsiteViewStatus()
        {
           BusinessLogic.Product.Product sp = new BusinessLogic.Product.Product();
           sp.ProductWebsiteViewStatus();
           UpdateCache();
           return Content("");
        }

        [Authorize]
        public ActionResult UpdateProductStoreFromAccPac()
        {
            BusinessLogic.Product.Product sp = new BusinessLogic.Product.Product();
            sp.UpdateProductStoreFromAccPac();
            
            UpdateCache();
            return Content("");
        }


        #region list section
        [Authorize]
        public JsonResult GetList(int? id, FormCollection collection)
        {
            //FormCollection  collection = new FormCollection(ConvertRawUrlToQuerystring());

            Product obj = new Product();

            ViewDataDictionary vdd = LoadList(id, collection);
            obj.ProductList = RenderViewToStringAsHTML(this, "Grid", vdd);





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

                if (collection[UIProduct.srhCode.ToString()].Trim().Length != 0)
                {
                    filter += " and Product_Code like '" + collection[UIProduct.srhCode.ToString()].ToString() + "*'";
                    searchCritaria += " Code : " + collection[UIProduct.srhCode.ToString()].ToString() + " ,";
                }


                if (collection[UIProduct.srhDisplayCode.ToString()].Trim().Length != 0)
                {
                    filter += " and Product_Code_To_Display like '" + collection[UIProduct.srhDisplayCode.ToString()].ToString() + "*'";
                    searchCritaria += " Display Code : " + collection[UIProduct.srhDisplayCode.ToString()].ToString() + " ,";
                }

                

                if (collection[UIProduct.srhProduct.ToString()].Trim().Length != 0)
                {
                    filter += " and Product like '" + collection[UIProduct.srhProduct.ToString()].ToString() + "*'";
                    searchCritaria += " Product : " + collection[UIProduct.srhProduct.ToString()].ToString() + " ,";
                }


                if (collection[UIProduct.srhDescription.ToString()].Trim().Length != 0)
                {
                    filter += " and Description like '" + collection[UIProduct.srhDescription.ToString()].ToString() + "*'";
                    searchCritaria += " Description : " + collection[UIProduct.srhDescription.ToString()].ToString() + " ,";
                }

                if (Convert.ToInt32(collection[UIProduct.srhProductType.ToString()]) != 0)
                {
                    filter += " and Product_Type_ID = " + collection[UIProduct.srhProductType.ToString()].ToString();
                    searchCritaria += " Type : " + ds.Product_Type.FindByProduct_Type_ID(Convert.ToInt32(collection[UIProduct.srhProductType.ToString()])).Product_Type + " ,";
                }
                else
                {
                    searchCritaria += " Type : All ,";
                }


                if (collection[UIProduct.srhStatus.ToString()] == null)
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

            else
            {
                searchCritaria += " Status : In-Active" + " ,";
                searchCritaria += " Type : All ,";
            }

            ViewData["searchCritaria"] = searchCritaria.Remove(searchCritaria.Length-1);

            DataRow[] rows = ds.Product.Select(filter);

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
                pagination.BaseUrl = Url.Content("~/Product/GetList/");
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
                ViewData[UIProduct.listProduct.ToString()] = dt.Skip(start).Take(offset).ToList();

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


        public class Product
        {
            public string ProductList { get; set; }
        }

        #endregion


        #region CRUD

        [Authorize]
        public ActionResult New()
        {
        

            ViewData[ UIProduct.cobProductType.ToString()] = "";
            ViewData[UIProduct.hidProduct_ID.ToString()] = "0";
            ViewData[UIProduct.is_Active.ToString()] = true;
            ViewData[UIProduct.txtCodeToDisplay.ToString()] = "";
            ViewData[UIProduct.txtDescription.ToString()] = "";
            ViewData[UIProduct.txtLink.ToString()] = "";
            ViewData[UIProduct.txtProduct.ToString()] = "";
            ViewData[UIProduct.txtProduct_Code.ToString()] = "";
            ViewData[UIProduct.hidGUID.ToString()] = Guid.NewGuid().ToString().GetHashCode().ToString("x");
            ViewData[UIProduct.hidFileName.ToString()] = ""; 
            ViewData[UIProduct.hidFolderName.ToString()] = "Temp";

            Init("", "");
            ViewData[UIProduct.hidState.ToString()] = 3;
            return View("Product");
        }



        [NonAction]
        void Init(string selectedGridSize,string selectedProductType)
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
                ViewData[ UIProduct.listProductType.ToString()] = new SelectList(list, "Value", "Text");
            else
                ViewData[UIProduct.listProductType.ToString()] = new SelectList(list, "Value", "Text", selectedProductType);


            #endregion

        }


        [Authorize]
        public JsonResult Save()
        {


            ProductManifest pm = new ProductManifest();

            // 
            if (!Validate(ref pm))
            {
                BusinessLogic.Product.Product sp = new BusinessLogic.Product.Product();
                DataSet.DSParameter ds = new DataSet.DSParameter();

                ds.Product.Product_IDColumn.ReadOnly = false;
                
                ds.Product.AddProductRow(pm.product, pm.code_To_Display, pm.product_Code, 1, pm.product_Type,
                        1, DateTime.Now, pm.description, pm.is_Active, pm.file_Name, "", "", pm.product_Link);
                ds.EnforceConstraints = false;
                ds.Product[0]["Product_ID"] = Convert.ToInt32( pm.product_ID );

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


            ProductManifest spm = new ProductManifest();



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
            string filter = " Product_ID = '" + id.ToString() ;
            
           
                DataSet.DSParameter.ProductRow proRow = ds.Product.FindByProduct_ID( id );
                ViewData[UIProduct.cobProductType.ToString()] = proRow.Product_Type_ID;
                ViewData[UIProduct.hidProduct_ID.ToString()] = proRow.Product_ID;
                ViewData[UIProduct.is_Active.ToString()] = proRow.Is_Active;
                ViewData[UIProduct.txtCodeToDisplay.ToString()] = proRow.Product_Code_To_Display;
                ViewData[UIProduct.txtDescription.ToString()] = proRow.Description;
                ViewData[UIProduct.txtLink.ToString()] = proRow.Heading_Link;
                ViewData[UIProduct.txtProduct.ToString()] = proRow.Product;
                ViewData[UIProduct.txtProduct_Code.ToString()] = proRow.Product_Code;
                ViewData[UIProduct.hidGUID.ToString()] = Guid.NewGuid().ToString().GetHashCode().ToString("x");
                ViewData[UIProduct.hidFileName.ToString()] = proRow.Product_Image_Link;
                ViewData[UIProduct.hidFolderName.ToString()] = "Temp";

                ViewData[UIProduct.hidProduct_ID.ToString()] = proRow.Product_ID;

                Init("", proRow.Product_Type_ID.ToString());

            ViewData[UIProduct.hidState.ToString()] = 2;

            return View("Product");

        }



        



        [NonAction]
        bool Validate(ref  ProductManifest prm)
        {
            FormCollection collection = new FormCollection(ConvertRawUrlToQuerystring());



            prm.code_To_Display = collection[UIProduct.txtCodeToDisplay.ToString()];
            prm.code_To_DisplayError = "";
            prm.product_Code = collection[UIProduct.txtProduct_Code.ToString()];
            prm.product_CodeError = "";

            prm.file_Name = collection[UIProduct.hidFileName.ToString()];
            prm.file_NameError = "";

            prm.code_To_Display = collection[UIProduct.txtCodeToDisplay.ToString()];
            prm.code_To_DisplayError= "";

            prm.product = collection[UIProduct.txtProduct.ToString()];
            prm.productError = "";

            prm.product_Type = Convert.ToInt32( collection[UIProduct.cobProductType.ToString()] );
            prm.product_TypeError = "";


            prm.description = collection[UIProduct.txtDescription.ToString()];
            prm.descriptionError = "";

            prm.product_Link = collection[UIProduct.txtLink.ToString()];
            prm.product_LinkError = "";

            prm.product_ID = collection[UIProduct.hidProduct_ID.ToString()];

            if (collection[UIProduct.is_Active.ToString()] == null)
            {
                prm.is_Active = false;
            }
            else
            {
                prm.is_Active = true;
            }

            


            if (collection[UIProduct.is_Active.ToString()] == null)
            {
                prm.is_Active = false;
            }
            else
            {
                prm.is_Active = true;
            }



            prm.hidState = Convert.ToInt32( collection[UIProduct.hidState.ToString()] );
            

            if ( prm.code_To_Display.Trim().Length == 0  )
            {
                prm.code_To_DisplayError = "Enter valid display code";
                prm.Has_Error= true;
            }


            if ( prm.file_Name.Trim().Length == 0  )
            {
                prm.file_NameError= "select product image";
                prm.Has_Error= true;
            }

            if ( prm.product.Trim().Length == 0  )
            {
                prm.productError= "Enter product name";
                prm.Has_Error= true;
            }

            if ( prm.product_Code.Trim().Length == 0  )
            {
                prm.product_CodeError= "Enter Product Code In AccPac";
                prm.Has_Error= true;
            }

            if ( prm.product_Type == 0  )
            {
                prm.product_TypeError = "select an product type";
                prm.Has_Error= true;
            }


          

            if (prm.description.Trim().Length == 0)
            {
                prm.descriptionError= "Enter product description";
                prm.Has_Error = true;
            }

            if (!base.IsUrlValid(prm.product_Link.Trim()) && prm.product_Link.Trim().Length != 0)
            {
                prm.product_LinkError = "Enter valid product link to Jatai website";
                prm.Has_Error = true;
            }
            


          


            return prm.Has_Error;
        }




        #endregion



        #region
        [Authorize]
        public ActionResult Upload(FormCollection col)
        {
            var r = new System.Collections.Generic.List<ViewDataUploadFilesResult>();

            ViewDataUploadFilesResult vs = new ViewDataUploadFilesResult();
            FormCollection collection = new FormCollection(ConvertRawUrlToQuerystring());
            string ext = "";


            foreach (string inputTagName in Request.Files)
            {

                HttpPostedFileBase file = Request.Files[inputTagName];
                if (file.ContentType.Contains("image"))
                {

                    if (file.ContentLength > 0)
                    {
                        if (file.FileName == "blob")
                        {
                            ext = file.ContentType.Substring(file.ContentType.IndexOf("/")).Replace('/', '.');
                        }
                        else
                        {
                            ext = file.FileName.Substring(file.FileName.IndexOf("."));// "." + file.ContentType.Substring(file.ContentType.IndexOf("/") + 1);
                        }


                        string filePath = Path.Combine(HttpContext.Server.MapPath("../ProdImg")
                            , Path.GetFileName(col["hidGUID"].ToString() + ext));// file.FileName));
                        file.SaveAs(filePath);
                        vs.Name = col["hidGUID"].ToString() + ext;
                    }


                }
            }



            return this.Json(vs, JsonRequestBehavior.AllowGet);
        }



        public class ViewDataUploadFilesResult
        {
            public string Thumbnail_url { get; set; }
            public string Name { get; set; }
            public int Length { get; set; }
            public string Type { get; set; }
        }



        #endregion


        public class ProductManifest
        {
            public string product_ID { get; set; }
            public string product { get; set; }
            public string code_To_Display { get; set; }
            public string product_Code{ get; set; }
            public int product_Type { get; set; }
            public string product_Link{ get; set; }
            public string description { get; set; }
            public bool is_Active { get; set; }
            public string file_Name { get; set; }
            public bool Has_Error { get; set; }
            public int hidState { get; set; }


            public string productError { get; set; }
            public string code_To_DisplayError { get; set; }
            public string product_CodeError { get; set; }
            public string product_TypeError { get; set; }
            public string file_NameError { get; set; }
            public string descriptionError { get; set; }
            public string product_LinkError { get; set; }


        }



    }
}
