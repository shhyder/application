using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class AttributeVariantController : Controller
    {
        public ActionResult NewProductType(int? product_Type_ID,int? attribute_ID)
        {
            SetBlank(Convert.ToInt32(product_Type_ID), Convert.ToInt32(attribute_ID));
            return View("AttributeVariant");
        }

        public ActionResult NewProduct(int? product_Type_ID, int? attribute_ID)
        {
            SetBlank(Convert.ToInt32(product_Type_ID), Convert.ToInt32(attribute_ID));
            return View("AttributeVariant");
        }


        public ActionResult ViewProductType()
        {
            return View("AttributeVariant");
        }

        public ActionResult ViewProduct()
        {
            return View("AttributeVariant");
        }


        public ActionResult EditProductType()
        {
            return View("AttributeVariant");
        }

        public ActionResult EditProduct()
        {
            return View("AttributeVariant");
        }

        void SetBlank(int product_Type_ID,int attribute_ID)
        {
            ViewData[UIAttributeVariant.txtAttributeVariant.ToString()] = "";
            ViewData[UIAttributeVariant.hidAttribute_Variant_ID.ToString()] = "0";
            ViewData[UIAttributeVariant.hidProduct_Type_ID.ToString()] = product_Type_ID;
            ViewData[UIAttributeVariant.hidAttribute_ID.ToString()] = attribute_ID;
            

        }


        public ActionResult Save(FormCollection collection)
        {

            if (!Validation(collection))
            {
                SetCollectionToView(collection);
                return View("NewProductType");
            }



            DataSet.DSParameter ds = SetViewToDS(collection);

            BusinessLogic.Attribute.Attribute_Variant obj = new BusinessLogic.Attribute.Attribute_Variant();
            obj.New(ds);



            return Content("");
        }

        [NonAction]
        bool Validation(FormCollection collection)
        {
            bool is_Valid = true;
            string error_Message = "";
            //ViewData[UIProduct.hidLatitude.ToString()] = collection[UIProduct.hidLatitude.ToString()];
            //ViewData[UIProduct.hidLongitude.ToString()] = collection[UIProduct.hidLongitude.ToString()];










            ViewData["ErrorMessage"] = error_Message;
            return is_Valid;
        }


        [NonAction]
        void SetCollectionToView(FormCollection collection)
        {
            ViewData[UIAttributeVariant.hidAttribute_ID.ToString()] = collection[UIAttributeVariant.hidAttribute_ID.ToString()];
            ViewData[UIAttributeVariant.txtAttributeVariant.ToString()] = collection[UIAttributeVariant.txtAttributeVariant.ToString()];
        }

        [NonAction]
        DataSet.DSParameter SetViewToDS(FormCollection collection)
        {


            DataSet.DSParameter ds = new DataSet.DSParameter();
            DataSet.DSParameter.Attribute_VariantRow row = ds.Attribute_Variant.NewAttribute_VariantRow();
            row[ds.Attribute_Variant.Attribute_Variant_IDColumn] = collection[UIAttributeVariant.hidAttribute_ID.ToString()];

            row[ds.Attribute_Variant.Attribute_IDColumn] = collection[UIAttributeVariant.hidAttribute_ID.ToString()];
            row[ds.Attribute_Variant.Attribute_VariantColumn] = collection[UIAttributeVariant.txtAttributeVariant.ToString()];
         
            ds.Attribute_Variant.AddAttribute_VariantRow(row);

            ds.AcceptChanges();
            return ds;
        }
    }
}
