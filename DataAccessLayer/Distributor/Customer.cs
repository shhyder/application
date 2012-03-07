using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DataSet;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using System.Data.SqlTypes;
using System.Data.Common;

namespace DataAccessLayer.Customer
{
    //----------------------------------------------------------------
    /// Class: Customer
    //----------------------------------------------------------------
    public class Customer : BaseParameter
    {

        System.Int32 _Customer_ID;

        public Customer(Database db, DbTransaction dbTrans)
        {
            _db = db;
            _transaction = dbTrans;
        }
        public Customer(Database db)
        {
            _db = db;
        }
        public Customer()
        {

        }
        public Customer(System.Int32 Customer_ID)
        {
            _Customer_ID = Customer_ID;
        }

        //----------------------------------------------------------------
        /// Get specific records: Customer
        //----------------------------------------------------------------
        public override IDataReader GetItem()
        {

            _dbCommand = _db.GetStoredProcCommand("GetCustomer");
            _db.AddInParameter(_dbCommand, _DSParam.Customer.IDColumn.ToString(), DbType.Int32, _Customer_ID);
            return _db.ExecuteReader(_dbCommand);
        }



        //----------------------------------------------------------------
        /// Get records List: Customer
        //----------------------------------------------------------------

        public override IDataReader GetAllItem()
        {

            _dbCommand = _db.GetStoredProcCommand("GetAllCustomer");
            return _db.ExecuteReader(_dbCommand);
        }



        //----------------------------------------------------------------
        /// Delete: Customer
        //----------------------------------------------------------------
        public override IDataReader Delete()
        {

            _dbCommand = _db.GetStoredProcCommand("GetCustomer");
            _db.AddInParameter(_dbCommand, _DSParam.Customer.IDColumn.ToString(), DbType.Int32, _Customer_ID);
            return _db.ExecuteReader(_dbCommand);
        }


        //----------------------------------------------------------------
        /// Get specific records: Customer State count
        //----------------------------------------------------------------
        public IDataReader GetStateCount()
        {

            _dbCommand = _db.GetStoredProcCommand("GetCustomerStateCount");
            return _db.ExecuteReader(_dbCommand);
        }



        //----------------------------------------------------------------
        /// Insert record: Customer
        //----------------------------------------------------------------
        public override IDataReader Insert(DSParameter ds)
        {
            _dbCommand = _db.GetStoredProcCommand("InsertCustomer");
            _db.AddOutParameter(_dbCommand, ds.Customer.IDColumn.ToString(), DbType.Int32, 20);
            _db.AddInParameter(_dbCommand, ds.Customer.Is_ActiveColumn.ToString(), DbType.Boolean, ds.Customer.Rows[0][ds.Customer.Is_ActiveColumn.ToString()]);
            _db.AddInParameter(_dbCommand, ds.Customer.Customer_IDColumn.ToString(), DbType.Int32, ds.Customer.Rows[0][ds.Customer.Customer_IDColumn.ToString()]);
            _db.AddInParameter(_dbCommand, ds.Customer.CustomerColumn.ToString(), DbType.String, ds.Customer.Rows[0][ds.Customer.CustomerColumn.ToString()]);
            _db.AddInParameter(_dbCommand, ds.Customer.CityColumn.ToString(), DbType.String, ds.Customer.Rows[0][ds.Customer.CityColumn.ToString()]);
            _db.AddInParameter(_dbCommand, ds.Customer.StateColumn.ToString(), DbType.String, ds.Customer.Rows[0][ds.Customer.StateColumn.ToString()]);
            _db.AddInParameter(_dbCommand, ds.Customer.AddressColumn.ToString(), DbType.String, ds.Customer.Rows[0][ds.Customer.AddressColumn.ToString()]);
            _db.AddInParameter(_dbCommand, ds.Customer.LatitudeColumn.ToString(), DbType.Decimal, ds.Customer.Rows[0][ds.Customer.LatitudeColumn.ToString()]);
            _db.AddInParameter(_dbCommand, ds.Customer.LongitudeColumn.ToString(), DbType.Decimal, ds.Customer.Rows[0][ds.Customer.LongitudeColumn.ToString()]);
            _db.AddInParameter(_dbCommand, ds.Customer.EmailColumn.ToString(), DbType.String, ds.Customer.Rows[0][ds.Customer.EmailColumn.ToString()]);
            _db.AddInParameter(_dbCommand, ds.Customer.Zip_CodeColumn.ToString(), DbType.String, ds.Customer.Rows[0][ds.Customer.Zip_CodeColumn.ToString()]);
            _db.AddInParameter(_dbCommand, ds.Customer.BuildingAppartmentColumn.ToString(), DbType.String, ds.Customer.Rows[0][ds.Customer.BuildingAppartmentColumn.ToString()]);
            _db.AddInParameter(_dbCommand, ds.Customer.Street_NoColumn.ToString(), DbType.String, ds.Customer.Rows[0][ds.Customer.Street_NoColumn.ToString()]);
            _db.AddInParameter(_dbCommand, ds.Customer.Street_NameColumn.ToString(), DbType.String, ds.Customer.Rows[0][ds.Customer.Street_NameColumn.ToString()]);
            _db.AddInParameter(_dbCommand, ds.Customer.CountryColumn.ToString(), DbType.String, ds.Customer.Rows[0][ds.Customer.CountryColumn.ToString()]);
            _db.AddInParameter(_dbCommand, ds.Customer.Phone1Column.ToString(), DbType.String, ds.Customer.Rows[0][ds.Customer.Phone1Column.ToString()]);
            _db.AddInParameter(_dbCommand, ds.Customer.Phone2Column.ToString(), DbType.String, ds.Customer.Rows[0][ds.Customer.Phone2Column.ToString()]);
            _db.AddInParameter(_dbCommand, ds.Customer.Email2Column.ToString(), DbType.String, ds.Customer.Rows[0][ds.Customer.Email2Column.ToString()]);
            _db.AddInParameter(_dbCommand, ds.Customer.WebsiteColumn.ToString(), DbType.String, ds.Customer.Rows[0][ds.Customer.WebsiteColumn.ToString()]);
            _db.AddInParameter(_dbCommand, ds.Customer.Is_Email1_DisplayColumn.ToString(), DbType.Boolean, ds.Customer.Rows[0][ds.Customer.Is_Email1_DisplayColumn.ToString()]);
            _db.AddInParameter(_dbCommand, ds.Customer.Is_Email2_DisplayColumn.ToString(), DbType.Boolean, ds.Customer.Rows[0][ds.Customer.Is_Email2_DisplayColumn.ToString()]);
            _db.AddInParameter(_dbCommand, ds.Customer.Is_Phone1_DisplayColumn.ToString(), DbType.Boolean, ds.Customer.Rows[0][ds.Customer.Is_Phone1_DisplayColumn.ToString()]);
            _db.AddInParameter(_dbCommand, ds.Customer.Is_Phone2_DisplayColumn.ToString(), DbType.Boolean, ds.Customer.Rows[0][ds.Customer.Is_Phone2_DisplayColumn.ToString()]);
            _db.AddInParameter(_dbCommand, ds.Customer.Is_Website_DisplayColumn.ToString(), DbType.Boolean, ds.Customer.Rows[0][ds.Customer.Is_Website_DisplayColumn.ToString()]);
            IDataReader dr = _db.ExecuteReader(_dbCommand, _transaction);
            dr.Close();
            _ID = System.Int32.Parse(_db.GetParameterValue(_dbCommand, "@ID").ToString());
            return dr;
        }




        //----------------------------------------------------------------
        /// Update: Customer
        //----------------------------------------------------------------
        public override IDataReader Update(DSParameter ds)
        {
            _dbCommand = _db.GetStoredProcCommand("UpdateCustomer");
            _db.AddInParameter(_dbCommand, ds.Customer.IDColumn.ToString(), DbType.Int32, ds.Customer.Rows[0][ds.Customer.IDColumn.ToString()]);
            _db.AddInParameter(_dbCommand, ds.Customer.Is_ActiveColumn.ToString(), DbType.Boolean, ds.Customer.Rows[0][ds.Customer.Is_ActiveColumn.ToString()]);
            _db.AddInParameter(_dbCommand, ds.Customer.Customer_IDColumn.ToString(), DbType.Int32, ds.Customer.Rows[0][ds.Customer.Customer_IDColumn.ToString()]);
            _db.AddInParameter(_dbCommand, ds.Customer.CustomerColumn.ToString(), DbType.String, ds.Customer.Rows[0][ds.Customer.CustomerColumn.ToString()]);
            _db.AddInParameter(_dbCommand, ds.Customer.CityColumn.ToString(), DbType.String, ds.Customer.Rows[0][ds.Customer.CityColumn.ToString()]);
            _db.AddInParameter(_dbCommand, ds.Customer.StateColumn.ToString(), DbType.String, ds.Customer.Rows[0][ds.Customer.StateColumn.ToString()]);
            _db.AddInParameter(_dbCommand, ds.Customer.AddressColumn.ToString(), DbType.String, ds.Customer.Rows[0][ds.Customer.AddressColumn.ToString()]);
            _db.AddInParameter(_dbCommand, ds.Customer.LatitudeColumn.ToString(), DbType.Decimal, ds.Customer.Rows[0][ds.Customer.LatitudeColumn.ToString()]);
            _db.AddInParameter(_dbCommand, ds.Customer.LongitudeColumn.ToString(), DbType.Decimal, ds.Customer.Rows[0][ds.Customer.LongitudeColumn.ToString()]);
            _db.AddInParameter(_dbCommand, ds.Customer.EmailColumn.ToString(), DbType.String, ds.Customer.Rows[0][ds.Customer.EmailColumn.ToString()]);
            _db.AddInParameter(_dbCommand, ds.Customer.Zip_CodeColumn.ToString(), DbType.String, ds.Customer.Rows[0][ds.Customer.Zip_CodeColumn.ToString()]);
            _db.AddInParameter(_dbCommand, ds.Customer.BuildingAppartmentColumn.ToString(), DbType.String, ds.Customer.Rows[0][ds.Customer.BuildingAppartmentColumn.ToString()]);
            _db.AddInParameter(_dbCommand, ds.Customer.Street_NoColumn.ToString(), DbType.String, ds.Customer.Rows[0][ds.Customer.Street_NoColumn.ToString()]);
            _db.AddInParameter(_dbCommand, ds.Customer.Street_NameColumn.ToString(), DbType.String, ds.Customer.Rows[0][ds.Customer.Street_NameColumn.ToString()]);
            _db.AddInParameter(_dbCommand, ds.Customer.CountryColumn.ToString(), DbType.String, ds.Customer.Rows[0][ds.Customer.CountryColumn.ToString()]);
            _db.AddInParameter(_dbCommand, ds.Customer.Phone1Column.ToString(), DbType.String, ds.Customer.Rows[0][ds.Customer.Phone1Column.ToString()]);
            _db.AddInParameter(_dbCommand, ds.Customer.Phone2Column.ToString(), DbType.String, ds.Customer.Rows[0][ds.Customer.Phone2Column.ToString()]);
            _db.AddInParameter(_dbCommand, ds.Customer.Email2Column.ToString(), DbType.String, ds.Customer.Rows[0][ds.Customer.Email2Column.ToString()]);
            _db.AddInParameter(_dbCommand, ds.Customer.WebsiteColumn.ToString(), DbType.String, ds.Customer.Rows[0][ds.Customer.WebsiteColumn.ToString()]);
            _db.AddInParameter(_dbCommand, ds.Customer.Is_Email1_DisplayColumn.ToString(), DbType.Boolean, ds.Customer.Rows[0][ds.Customer.Is_Email1_DisplayColumn.ToString()]);
            _db.AddInParameter(_dbCommand, ds.Customer.Is_Email2_DisplayColumn.ToString(), DbType.Boolean, ds.Customer.Rows[0][ds.Customer.Is_Email2_DisplayColumn.ToString()]);
            _db.AddInParameter(_dbCommand, ds.Customer.Is_Phone1_DisplayColumn.ToString(), DbType.Boolean, ds.Customer.Rows[0][ds.Customer.Is_Phone1_DisplayColumn.ToString()]);
            _db.AddInParameter(_dbCommand, ds.Customer.Is_Phone2_DisplayColumn.ToString(), DbType.Boolean, ds.Customer.Rows[0][ds.Customer.Is_Phone2_DisplayColumn.ToString()]);
            _db.AddInParameter(_dbCommand, ds.Customer.Is_Website_DisplayColumn.ToString(), DbType.Boolean, ds.Customer.Rows[0][ds.Customer.Is_Website_DisplayColumn.ToString()]);
            IDataReader dr = _db.ExecuteReader(_dbCommand, _transaction);
            dr.Close();
            return dr;
        }

        public bool setStatus(string id,bool is_Active)
        {
            _dbCommand = _db.GetStoredProcCommand("UpdateCustomerStatus");
            _db.AddInParameter(_dbCommand, "Customer_ID", DbType.Int32, Convert.ToInt32( id ));
            _db.AddInParameter(_dbCommand, "Is_Active", DbType.Boolean, is_Active);
           
            IDataReader dr = _db.ExecuteReader(_dbCommand, _transaction);
            dr.Close();
            return true;
        }


    }

}
