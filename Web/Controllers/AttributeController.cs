using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class AttributeController : Controller
    {
       

        

        public ActionResult NewProductType(int? id)
        {
            SetBlank(Convert.ToInt32(id) );
            return View("Attribute");
        }

        public ActionResult NewProduct(int? id)
        {
            SetBlank(Convert.ToInt32(id));
            return View("Attribute");
        }


        public ActionResult ViewProductType()
        {
            return View("Attribute");
        }

        public ActionResult ViewProduct()
        {
            return View("Attribute");
        }


        public ActionResult EditProductType()
        {
            return View("Attribute");
        }

        public ActionResult EditProduct()
        {
            return View("Attribute");
        }

        void SetBlank(int id)
        {
            ViewData[ UIAttribute.txtAttribute.ToString()] = "";
            ViewData[UIAttribute.hidAttribute_ID.ToString()] = "0";
            ViewData[UIAttribute.hidProduct_Type_ID.ToString()] = id.ToString();

            
        }


        public ActionResult Save(FormCollection collection)
        {

            if (!Validation(collection))
            {
                SetCollectionToView(collection);
                return View("NewProductType");
            }



            DataSet.DSParameter ds = SetViewToDS(collection);

            BusinessLogic.Attribute.Attribute obj = new BusinessLogic.Attribute.Attribute();
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
            ViewData[UIAttribute.hidAttribute_ID.ToString()] = collection[UIAttribute.hidAttribute_ID.ToString()];
            ViewData[UIAttribute.txtAttribute.ToString()] = collection[UIAttribute.txtAttribute.ToString()];
        }

        [NonAction]
        DataSet.DSParameter SetViewToDS(FormCollection collection)
        {


            DataSet.DSParameter ds = new DataSet.DSParameter();
            DataSet.DSParameter.AttributeRow row = ds.Attribute.NewAttributeRow();


            row[ds.Attribute.Attribute_IDColumn] = collection[ UIAttribute.hidAttribute_ID.ToString()];
            row[ds.Attribute.AttributeColumn] = collection[UIAttribute.txtAttribute.ToString()];
            
            ds.Attribute.AddAttributeRow(row);


            DataSet.DSParameter.Product_Type_AttributeRow row2 = ds.Product_Type_Attribute.NewProduct_Type_AttributeRow();
            row2.Attribute_ID = 0;
            row2.Category_ID = 0;
            row2.Product_Type_ID = Convert.ToInt32( collection[UIAttribute.hidProduct_Type_ID.ToString()] );

            ds.Product_Type_Attribute.AddProduct_Type_AttributeRow(row2);

            ds.AcceptChanges();
            return ds;
        }

    }
}
