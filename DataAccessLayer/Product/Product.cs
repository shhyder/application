using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DataSet;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using System.Data.SqlTypes;
using System.Data.Common;


namespace DataAccessLayer.Product
{
    //----------------------------------------------------------------
    /// Class: Product
    //----------------------------------------------------------------
    public class Product : BaseParameter
    {

        System.Int32 _product_ID;
        int _start;
        int _count;
        int _totalCount;
        string _product;



        public Product(int start, int count, int totalCount,
          string product)
        {
            _start = start;
            _count = count;
            _totalCount = totalCount;
            _product = product;
        }



        public Product(Database db, DbTransaction dbTrans)
        {
            _db = db;
            _transaction = dbTrans;
        }
        public Product(Database db)
        {
            _db = db;
        }
        public Product()
        {

        }
        public Product(System.Int32 product_ID)
        {
            _product_ID = product_ID;
        }

        //----------------------------------------------------------------
        /// Get specific records: Product
        //----------------------------------------------------------------
        public override IDataReader GetItem()
        {

            _dbCommand = _db.GetStoredProcCommand("GetProduct");
            _db.AddInParameter(_dbCommand, _DSParam.Product.Product_IDColumn.ToString(), DbType.Int32, _product_ID);
            return _db.ExecuteReader(_dbCommand);
        }



        //----------------------------------------------------------------
        /// Get specific records: All Brand
        //----------------------------------------------------------------
        public override IDataReader GetAllItem()
        {

            _dbCommand = _db.GetStoredProcCommand("GetAllProduct");
            return _db.ExecuteReader(_dbCommand);
        }




        //----------------------------------------------------------------
        /// Delete: Product
        //----------------------------------------------------------------
        public override IDataReader Delete()
        {

            _dbCommand = _db.GetStoredProcCommand("GetProduct");
            _db.AddInParameter(_dbCommand, _DSParam.Product.Product_IDColumn.ToString(), DbType.Int32, _product_ID);
            return _db.ExecuteReader(_dbCommand);
        }



//----------------------------------------------------------------
/// Insert record: Product
//----------------------------------------------------------------
public override IDataReader Insert(DSParameter ds )
{
_dbCommand = _db.GetStoredProcCommand( "InsertProduct");
	_db.AddOutParameter(_dbCommand, ds.Product.Product_IDColumn.ToString(), DbType.Int32,20);
	_db.AddInParameter(_dbCommand, ds.Product.ProductColumn.ToString(), DbType.String,ds.Product.Rows[0][ds.Product.ProductColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Product.Product_Code_To_DisplayColumn.ToString(), DbType.String,ds.Product.Rows[0][ds.Product.Product_Code_To_DisplayColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Product.Product_CodeColumn.ToString(), DbType.String,ds.Product.Rows[0][ds.Product.Product_CodeColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Product.Company_IDColumn.ToString(), DbType.Int32,ds.Product.Rows[0][ds.Product.Company_IDColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Product.Product_Type_IDColumn.ToString(), DbType.Int32,ds.Product.Rows[0][ds.Product.Product_Type_IDColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Product.Category_IDColumn.ToString(), DbType.Int32,ds.Product.Rows[0][ds.Product.Category_IDColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Product.Entered_DateColumn.ToString(), DbType.DateTime,ds.Product.Rows[0][ds.Product.Entered_DateColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Product.DescriptionColumn.ToString(), DbType.String,ds.Product.Rows[0][ds.Product.DescriptionColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Product.Is_ActiveColumn.ToString(), DbType.Boolean,ds.Product.Rows[0][ds.Product.Is_ActiveColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Product.Product_Image_LinkColumn.ToString(), DbType.String,ds.Product.Rows[0][ds.Product.Product_Image_LinkColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Product.Product_Brand_Image_LinkColumn.ToString(), DbType.String,ds.Product.Rows[0][ds.Product.Product_Brand_Image_LinkColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Product.Product_HeadingColumn.ToString(), DbType.String,ds.Product.Rows[0][ds.Product.Product_HeadingColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Product.Heading_LinkColumn.ToString(), DbType.String,ds.Product.Rows[0][ds.Product.Heading_LinkColumn.ToString()]);
	IDataReader dr = _db.ExecuteReader( _dbCommand,_transaction);
dr.Close();
_ID = System.Int32.Parse( _db.GetParameterValue(_dbCommand, "@Product_ID").ToString() );
return dr;
}


//----------------------------------------------------------------
/// Update: Product
//----------------------------------------------------------------
public override IDataReader Update(DSParameter ds)
{
_dbCommand = _db.GetStoredProcCommand( "UpdateProduct");
	_db.AddInParameter(_dbCommand, ds.Product.Product_IDColumn.ToString(), DbType.Int32,ds.Product.Rows[0][ds.Product.Product_IDColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Product.ProductColumn.ToString(), DbType.String,ds.Product.Rows[0][ds.Product.ProductColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Product.Product_Code_To_DisplayColumn.ToString(), DbType.String,ds.Product.Rows[0][ds.Product.Product_Code_To_DisplayColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Product.Product_CodeColumn.ToString(), DbType.String,ds.Product.Rows[0][ds.Product.Product_CodeColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Product.Company_IDColumn.ToString(), DbType.Int32,ds.Product.Rows[0][ds.Product.Company_IDColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Product.Product_Type_IDColumn.ToString(), DbType.Int32,ds.Product.Rows[0][ds.Product.Product_Type_IDColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Product.Category_IDColumn.ToString(), DbType.Int32,ds.Product.Rows[0][ds.Product.Category_IDColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Product.Entered_DateColumn.ToString(), DbType.DateTime,ds.Product.Rows[0][ds.Product.Entered_DateColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Product.DescriptionColumn.ToString(), DbType.String,ds.Product.Rows[0][ds.Product.DescriptionColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Product.Is_ActiveColumn.ToString(), DbType.Boolean,ds.Product.Rows[0][ds.Product.Is_ActiveColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Product.Product_Image_LinkColumn.ToString(), DbType.String,ds.Product.Rows[0][ds.Product.Product_Image_LinkColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Product.Product_Brand_Image_LinkColumn.ToString(), DbType.String,ds.Product.Rows[0][ds.Product.Product_Brand_Image_LinkColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Product.Product_HeadingColumn.ToString(), DbType.String,ds.Product.Rows[0][ds.Product.Product_HeadingColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Product.Heading_LinkColumn.ToString(), DbType.String,ds.Product.Rows[0][ds.Product.Heading_LinkColumn.ToString()]);
	IDataReader dr = _db.ExecuteReader( _dbCommand,_transaction);
dr.Close();
return dr;
}




public System.Data.DataSet GetPage()
{
    _dbCommand = _db.GetStoredProcCommand("GetProductTypePage");
    _db.AddInParameter(_dbCommand, "Start", DbType.Int32, _start);
    _db.AddInParameter(_dbCommand, "count", DbType.Int32, _count);
    _db.AddOutParameter(_dbCommand, "TotalCount", DbType.Int32, _totalCount);
    _db.AddInParameter(_dbCommand, "Product_Type", DbType.String, _product);

    IDataReader iDR = _db.ExecuteReader(_dbCommand);
    System.Data.DataSet ds = new System.Data.DataSet();
    ds.Tables.Add("List");
    ds.Load(iDR, LoadOption.OverwriteChanges, "List");

    iDR.Close();


    _ID = System.Int32.Parse(_db.GetParameterValue(_dbCommand, "@TotalCount").ToString());

    return ds;


}


public void ProductWebsiteViewStatus()
{
    _dbCommand = _db.GetStoredProcCommand("ProductWebsiteViewStatus");
    _db.ExecuteReader(_dbCommand);
}


public void UpdateProductStoreFromAccPac()
{

    _dbCommand = _db.GetStoredProcCommand("UpdateProductStoreFromAccPac");
    _db.ExecuteReader(_dbCommand);
}
    
    
    
    
    
    
    
    
    }



}
