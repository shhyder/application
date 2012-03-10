using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataSet;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace BusinessLogic.Parameter
{
    public class Parameter
    {
        DataAccessLayer.BaseParameter _base;
        Database _db;

        public DSParameter GetParamerter()
        {
            //DataSet ds = new DataSet();

            DSParameter ds = new DSParameter();
            _base = new DataAccessLayer.Attribute.Attribute();
            _base.SetConnection();
            _db = _base.GetDatabase();

            ds.EnforceConstraints = false;
            ds.Load(_base.GetAllItem(), LoadOption.OverwriteChanges, ds.Attribute);

            _base = new DataAccessLayer.Attribute.Attribute_Variant(_db);
            ds.Load(_base.GetAllItem(), LoadOption.OverwriteChanges, ds.Attribute_Variant);

            _base = new DataAccessLayer.Attribute.Product_Type_Attribute(_db);
            ds.Load(_base.GetAllItem(), LoadOption.OverwriteChanges, ds.Product_Type_Attribute);

            _base = new DataAccessLayer.Distributor.Distributor(_db);
            ds.Load(_base.GetAllItem(), LoadOption.OverwriteChanges, ds.Distributor);

            _base = new DataAccessLayer.Parameter.Category(_db);
            ds.Load(_base.GetAllItem(), LoadOption.OverwriteChanges, ds.Category);

            _base = new DataAccessLayer.Parameter.Company(_db);
            ds.Load(_base.GetAllItem(), LoadOption.OverwriteChanges, ds.Company);

            _base = new DataAccessLayer.Parameter.Company_Product_Type(_db);
            ds.Load(_base.GetAllItem(), LoadOption.OverwriteChanges, ds.Company_Product_Type);

            _base = new DataAccessLayer.Parameter.Product_Attribute(_db);
            ds.Load(_base.GetAllItem(), LoadOption.OverwriteChanges, ds.Product_Attribute);

            _base = new DataAccessLayer.Parameter.Product_Type_Attribute(_db);
            ds.Load(_base.GetAllItem(), LoadOption.OverwriteChanges, ds.Product_Type_Attribute);

            _base = new DataAccessLayer.Product.Product(_db);
            ds.Load(_base.GetAllItem(), LoadOption.OverwriteChanges, ds.Product);

            _base = new DataAccessLayer.Product_Distributor(_db);
            ds.Load(_base.GetAllItem(), LoadOption.OverwriteChanges, ds.Product_Distributor);

            _base = new DataAccessLayer.Product_Type.Product_Type(_db);
            ds.Load(_base.GetAllItem(), LoadOption.OverwriteChanges, ds.Product_Type);

            _base = new DataAccessLayer.Product_Type_Attribute(_db);
            ds.Load(_base.GetAllItem(), LoadOption.OverwriteChanges, ds.Product_Type_Attribute);

            _base = new DataAccessLayer.Product_Type_Relationship(_db);
            ds.Load(_base.GetAllItem(), LoadOption.OverwriteChanges, ds.Product_Type_Relationship);

            _base = new DataAccessLayer.Activity.Activity_Type(_db);
            ds.Load(_base.GetAllItem(), LoadOption.OverwriteChanges, ds.Activity_Type);


            _base = new DataAccessLayer.Event.Event(_db);
            ds.Load(_base.GetAllItem(), LoadOption.OverwriteChanges, ds.Event);

            _base = new DataAccessLayer.Event.Event_Item(_db);
            ds.Load(_base.GetAllItem(), LoadOption.OverwriteChanges, ds.Event_Item);


            _base = new DataAccessLayer.Parameter.Secret_Question(_db);
            ds.Load(_base.GetAllItem(), LoadOption.OverwriteChanges, ds.Secret_Question);

            //_base = new DataAccessLayer.Customer.Customer(_db);
            //ds.Load(_base.GetAllItem(), LoadOption.OverwriteChanges, ds.Customer);


            

            _base.Dispose();
            
            return ds;
        }


    }
}
