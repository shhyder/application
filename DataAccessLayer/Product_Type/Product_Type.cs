using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DataSet;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using System.Data.SqlTypes;
using System.Data.Common;

namespace DataAccessLayer.Product_Type
{
    //----------------------------------------------------------------
    /// Class: Product_Type
    //----------------------------------------------------------------
    public class Product_Type : BaseParameter
    {

        System.Int32 _product_type_ID;

         int _start;
        int _count;
        int _totalCount;
        string _Product_Type;

        public Product_Type(Database db, DbTransaction dbTrans)
        {
            _db = db;
            _transaction = dbTrans;
        }
        public Product_Type(Database db)
        {
            _db = db;
        }
        public Product_Type()
        {

        }
        public Product_Type(System.Int32 product_type_ID)
        {
            _product_type_ID = product_type_ID;
        }

        public Product_Type(int start, int count, int totalCount,
            string product_Type)
        {
            _start = start;
            _count = count;
            _totalCount = totalCount;
            _Product_Type = product_Type;
        }

        //----------------------------------------------------------------
        /// Get specific records: Product_Type
        //----------------------------------------------------------------
        public override IDataReader GetItem()
        {

            _dbCommand = _db.GetStoredProcCommand("GetProduct_Type");
            _db.AddInParameter(_dbCommand, _DSParam.Product_Type.Product_Type_IDColumn.ToString(), DbType.Int32, _ID);
            return _db.ExecuteReader(_dbCommand);
        }



        //----------------------------------------------------------------
        /// Get specific records: All Brand
        //----------------------------------------------------------------
        public override IDataReader GetAllItem()
        {

            _dbCommand = _db.GetStoredProcCommand("GetAllProduct_Type");
            return _db.ExecuteReader(_dbCommand);
        }



          public System.Data.DataSet GetPage()
        {
            _dbCommand = _db.GetStoredProcCommand("GetProductTypePage");
            _db.AddInParameter(_dbCommand, "Start", DbType.Int32, _start);
            _db.AddInParameter(_dbCommand, "count", DbType.Int32, _count);
            _db.AddOutParameter(_dbCommand, "TotalCount", DbType.Int32, _totalCount);
            _db.AddInParameter(_dbCommand, "Product_Type", DbType.String, _Product_Type);
            
            IDataReader iDR = _db.ExecuteReader(_dbCommand);
            System.Data.DataSet ds = new System.Data.DataSet();
            ds.Tables.Add("List");
            ds.Load(iDR, LoadOption.OverwriteChanges, "List");

            iDR.Close();


            _ID = System.Int32.Parse(_db.GetParameterValue(_dbCommand, "@TotalCount").ToString());

            return ds;


        }





        //----------------------------------------------------------------
        /// Delete: Product_Type
        //----------------------------------------------------------------
        public override IDataReader Delete()
        {

            _dbCommand = _db.GetStoredProcCommand("GetProduct_Type");
            _db.AddInParameter(_dbCommand, _DSParam.Product_Type.Product_Type_IDColumn.ToString(), DbType.Int32, _product_type_ID);
            return _db.ExecuteReader(_dbCommand);
        }

        //----------------------------------------------------------------
/// Insert record: Product_Type
//----------------------------------------------------------------
public override IDataReader Insert( DSParameter ds )
{
_dbCommand = _db.GetStoredProcCommand( "InsertProduct_Type");
	_db.AddOutParameter(_dbCommand, ds.Product_Type.Product_Type_IDColumn.ToString(), DbType.Int32,20);
	_db.AddInParameter(_dbCommand, ds.Product_Type.Product_TypeColumn.ToString(), DbType.String,ds.Product_Type.Rows[0][ds.Product_Type.Product_TypeColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Product_Type.Category_IDColumn.ToString(), DbType.Int32,ds.Product_Type.Rows[0][ds.Product_Type.Category_IDColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Product_Type.Entered_DateColumn.ToString(), DbType.DateTime,ds.Product_Type.Rows[0][ds.Product_Type.Entered_DateColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Product_Type.DescriptionColumn.ToString(), DbType.String,ds.Product_Type.Rows[0][ds.Product_Type.DescriptionColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Product_Type.Is_ActiveColumn.ToString(), DbType.Boolean,ds.Product_Type.Rows[0][ds.Product_Type.Is_ActiveColumn.ToString()]);
	IDataReader dr = _db.ExecuteReader( _dbCommand,_transaction);
dr.Close();
_ID = System.Int32.Parse( _db.GetParameterValue(_dbCommand, "@Product_Type_ID").ToString() );
return dr;
}



//----------------------------------------------------------------
/// Update: Product_Type
//----------------------------------------------------------------
public override IDataReader Update(DSParameter ds)
{
_dbCommand = _db.GetStoredProcCommand( "UpdateProduct_Type");
	_db.AddInParameter(_dbCommand, ds.Product_Type.Product_Type_IDColumn.ToString(), DbType.Int32,ds.Product_Type.Rows[0][ds.Product_Type.Product_Type_IDColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Product_Type.Product_TypeColumn.ToString(), DbType.String,ds.Product_Type.Rows[0][ds.Product_Type.Product_TypeColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Product_Type.Category_IDColumn.ToString(), DbType.Int32,ds.Product_Type.Rows[0][ds.Product_Type.Category_IDColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Product_Type.Entered_DateColumn.ToString(), DbType.DateTime,ds.Product_Type.Rows[0][ds.Product_Type.Entered_DateColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Product_Type.DescriptionColumn.ToString(), DbType.String,ds.Product_Type.Rows[0][ds.Product_Type.DescriptionColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Product_Type.Is_ActiveColumn.ToString(), DbType.Boolean,ds.Product_Type.Rows[0][ds.Product_Type.Is_ActiveColumn.ToString()]);
	IDataReader dr = _db.ExecuteReader( _dbCommand,_transaction);
dr.Close();
return dr;
}


       
    }
}
