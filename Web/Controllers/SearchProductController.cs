using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using BusinessLogic;
using System.Text;
using System.Globalization;

namespace Web.Controllers
{
    public class SearchProductController : _BaseController
    {
        //
        // GET: /SearchProduct/

        public ActionResult Index(FormCollection collection)
        {
            collection = new FormCollection(ConvertRawUrlToQuerystring());

            if (collection.Keys.Count > 0)
            {
                ListPopulate(1, collection);

                if (!Request.IsAjaxRequest())
                {
                    Init("");
                }

            }
            else
            {
                ListPopulate(1);
                Init("");
            }

            
            return View();
        }

        public ActionResult List(int? id, FormCollection collection)
        {
            collection = new FormCollection(ConvertRawUrlToQuerystring());

            if (collection.Keys.Count > 0)
            {
                ListPopulate(id, collection);
            }
            else
            {
                ListPopulate(id);
            }
            return View("List");
        }


        public ActionResult SelectProduct(int? id)
        {
            
            if (Session[UIProductSearch.svSelectedProduct.ToString()] == null)
            {
                DataSet.DS _ds = new DataSet.DS();
                _ds.SelectedProduct.AddSelectedProductRow(  Convert.ToInt32( id ) );
                Session[UIProductSearch.svSelectedProduct.ToString()] = _ds;
            }
            else
            {
                DataSet.DS _ds = (DataSet.DS)Session[UIProductSearch.svSelectedProduct.ToString()];
                _ds.SelectedProduct.AddSelectedProductRow(Convert.ToInt32(id));
                Session[UIProductSearch.svSelectedProduct.ToString()] = _ds;
            }

            return Content("");
        }

        public ActionResult DeSelectProduct(int? id)
        {
            if (Session[UIProductSearch.svSelectedProduct.ToString()] != null)
            {
                DataSet.DS _ds = (DataSet.DS)Session[UIProductSearch.svSelectedProduct.ToString()];
                _ds.SelectedProduct.FindByProduct_ID( Convert.ToInt32( id ) ).Delete();
                Session[UIProductSearch.svSelectedProduct.ToString()] = _ds;
            }
            return Content("");
        }


        public ActionResult ResetProduct(int? id)
        {
            Session[UIProductSearch.svSelectedProduct.ToString()] = new DataSet.DS();
            return Content("");
        }


        void ListPopulate(int? id, FormCollection collection)
        {
            int distance = 0;
            string postalCode = "";
            string filter = "";


            if (collection[UIProductSearch.search.ToString()] != null)
            {
                string text = collection[UIProductSearch.search.ToString()].ToString();
                if (Convert.ToInt32(collection["cat"]) == 0)
                {
                    filter = "( product like '*" + text + "*' or Description like '*" + text + "*' )";
                }
                else
                {
                    filter = " Product_Type_ID = " + collection["cat"].ToString() + " and ( product like '*" + text + "*' or Description like '*" + text + "*' )";
                }
                ViewData[UIProductSearch.is_Category_Search.ToString()] = true;
                ViewData[UIProductSearch.search.ToString()] = text;

                base.Activity((int)Activities.viewList, "category = " + collection["cat"].ToString() + " text= " + text, 0);

            }
            else
            {
                ViewData[UIProductSearch.is_Category_Search.ToString()] = false;
                ViewData[UIProductSearch.search.ToString()] = "";
                postalCode = collection[UISearch.txtzipcode.ToString()].ToString();// CultureInfo.InvariantCulture string.Format("{D5}", "210");// "210".ToString("D5");

                if (base.IsNumeric(postalCode))
                {
                    postalCode = String.Format("{0:d5}", Convert.ToInt32(postalCode));
                    distance = Convert.ToInt32(collection[UISearch.cobDistance.ToString()].ToString());
                }


                string product_IDs = "";


                foreach (string col_id in collection)
                {
                    if (col_id.Contains("pro-"))
                    {
                        product_IDs += col_id.Replace("pro-", "") + ",";
                    }
                }

                if (product_IDs.Length > 0)
                {
                    product_IDs += product_IDs.Remove(product_IDs.LastIndexOf(","));
                }

                filter = " product_ID in ( " + product_IDs + " ) ";

                base.Activity((int)Activities.viewList, "postalCode=" + postalCode + "  distance= " + distance.ToString() + " miles" + " product_IDs = " + product_IDs, Convert.ToInt32(id));

            }


            DataSet.DSParameter ds = (DataSet.DSParameter)System.Web.HttpContext.Current.Cache["data"];
            //DataRow[] rows = ds.Product_Type.Select();

            //BusinessLogic.Search.Search ser = new BusinessLogic.Search.Search();
            //System.Data.DataSet ds = ser.GetProductList(postalCode, distance,, "", "", "");




            int pageSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PageSize"].ToString());
            int pageCalled = 0;
            if (id == null)
                pageCalled = 1;
            else
                pageCalled = (int)id;



            //BusinessLogic.Search.Search obj = new BusinessLogic.Search.Search();
            //obj._ID = 1;
            //System.Data.DataSet ds = obj.GetSearchCritariaBy(1);
            DataRow[] rows = ds.Product.Select( filter );



            int totalItems = rows.Length;

            Web.Model.Pagination pagination = new Web.Model.Pagination(true);

            pagination.BaseUrl = Url.Content("~/SearchProduct/List/");
            pagination.TrailingQueryString = "?" + GetRawUrl();
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



            IQueryable<DataRow> dt = rows.Cast<DataRow>().AsQueryable();
            ViewData[ UIProductSearch.listProduct.ToString()] = dt.Skip(start).Take(offset).ToList();// dt.Skip((pageCalled) * pageSize).Take(pageSize).ToList();
            

        }

        void ListPopulate(int? id)
        {
            ViewData[UIProductSearch.is_Category_Search.ToString()] = false;
            ViewData[UIProductSearch.search.ToString()] = "";


            DataSet.DSParameter ds = (DataSet.DSParameter)System.Web.HttpContext.Current.Cache["data"];
            //DataRow[] rows = ds.Product_Type.Select();

            //BusinessLogic.Search.Search ser = new BusinessLogic.Search.Search();
            //System.Data.DataSet ds = ser.GetProductList(postalCode, distance,, "", "", "");




            int pageSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PageSize"].ToString());
            int pageCalled = 0;
            if (id == null)
                pageCalled = 1;
            else
                pageCalled = (int)id;



            //BusinessLogic.Search.Search obj = new BusinessLogic.Search.Search();
            //obj._ID = 1;
            //System.Data.DataSet ds = obj.GetSearchCritariaBy(1);
            DataRow[] rows = ds.Product.Select("  "," product_Type_ID asc ");



            int totalItems = rows.Length;

            Web.Model.Pagination pagination = new Web.Model.Pagination(true);

            pagination.BaseUrl = Url.Content("~/SearchProduct/List/");
            pagination.TrailingQueryString = "";
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



            IQueryable<DataRow> dt = rows.Cast<DataRow>().AsQueryable();
            ViewData[UIProductSearch.listProduct.ToString()] = dt.Skip(start).Take(offset).ToList();// dt.Skip((pageCalled) * pageSize).Take(pageSize).ToList();


        }


      



        [NonAction]
        void Init(string selectedProductType)
        {
            DataSet.DSParameter ds = (DataSet.DSParameter)System.Web.HttpContext.Current.Cache["data"];
          

            #region product type

            List<SelectListItem> list = new List<SelectListItem>();
            DataRow[] rows = ds.Product_Type.Select();

            list.Add(new SelectListItem
            {
                Text = "All",
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

            if (selectedProductType.Trim().Length == 0 || selectedProductType == "All")
                ViewData[ UIProductType.listProductType.ToString()] = new SelectList(list, "Value", "Text");
            else
                ViewData[UIProductType.listProductType.ToString()] = new SelectList(list, "Value", "Text", selectedProductType);


         
            #endregion

        }


      

    }
}
