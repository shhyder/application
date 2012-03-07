using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DataSet;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using System.Data.SqlTypes;
using System.Data.Common;

namespace DataAccessLayer.Parameter
{
    //----------------------------------------------------------------
/// Class: Product_Type_Attribute
//----------------------------------------------------------------
    public class Product_Type_Attribute : BaseParameter
{

System.Int32  _product_type_attribute_ID;

public Product_Type_Attribute (Database db,DbTransaction dbTrans) 
{
	_db = db;
	_transaction = dbTrans;
}
public Product_Type_Attribute (Database db) 
{
	_db = db;
}
public Product_Type_Attribute () 
{
 
}
public Product_Type_Attribute (System.Int32  product_type_attribute_ID) 
{
  _product_type_attribute_ID = product_type_attribute_ID;
}

//----------------------------------------------------------------
/// Get specific records: Product_Type_Attribute
//----------------------------------------------------------------
public override IDataReader GetItem()
{

_dbCommand = _db.GetStoredProcCommand( "GetProduct_Type_Attribute");
_db.AddInParameter(_dbCommand, _DSParam.Product_Type_Attribute.Product_Type_IDColumn.ToString(), DbType.Int32, _product_type_attribute_ID);
	return _db.ExecuteReader( _dbCommand);
}



//----------------------------------------------------------------
/// Get records List: Product_Type_Attribute
//----------------------------------------------------------------

public override IDataReader GetAllItem()
{

    _dbCommand = _db.GetStoredProcCommand("GetAllProduct_Type_Attribute");
    return _db.ExecuteReader(_dbCommand);
}




//----------------------------------------------------------------
/// Delete: Product_Type_Attribute
//----------------------------------------------------------------
public override IDataReader Delete( )
{

_dbCommand = _db.GetStoredProcCommand( "GetProduct_Type_Attribute");
_db.AddInParameter(_dbCommand, _DSParam.Product_Type_Attribute.Product_Type_IDColumn.ToString(), DbType.Int32, _product_type_attribute_ID);
	return _db.ExecuteReader( _dbCommand);
}



//----------------------------------------------------------------
/// Insert record: Product_Type_Attribute
//----------------------------------------------------------------
public override IDataReader Insert(DSParameter ds)
{
_dbCommand = _db.GetStoredProcCommand( "InsertProduct_Type_Attribute");
	_db.AddInParameter(_dbCommand, ds.Product_Type_Attribute.Product_Type_IDColumn.ToString(), DbType.Int32,ds.Product_Type_Attribute.Rows[0][ds.Product_Type_Attribute.Product_Type_IDColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Product_Type_Attribute.Attribute_IDColumn.ToString(), DbType.Int32,ds.Product_Type_Attribute.Rows[0][ds.Product_Type_Attribute.Attribute_IDColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Product_Type_Attribute.Category_IDColumn.ToString(), DbType.Int32,ds.Product_Type_Attribute.Rows[0][ds.Product_Type_Attribute.Category_IDColumn.ToString()]);
	IDataReader dr = _db.ExecuteReader( _dbCommand,_transaction);
dr.Close();
return dr;
}



//----------------------------------------------------------------
/// Update: Product_Type_Attribute
//----------------------------------------------------------------
public override IDataReader Update(DSParameter ds)
{
_dbCommand = _db.GetStoredProcCommand( "UpdateProduct_Type_Attribute");
	_db.AddInParameter(_dbCommand, ds.Product_Type_Attribute.Product_Type_IDColumn.ToString(), DbType.Int32,ds.Product_Type_Attribute.Rows[0][ds.Product_Type_Attribute.Product_Type_IDColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Product_Type_Attribute.Attribute_IDColumn.ToString(), DbType.Int32,ds.Product_Type_Attribute.Rows[0][ds.Product_Type_Attribute.Attribute_IDColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Product_Type_Attribute.Category_IDColumn.ToString(), DbType.Int32,ds.Product_Type_Attribute.Rows[0][ds.Product_Type_Attribute.Category_IDColumn.ToString()]);
	IDataReader dr = _db.ExecuteReader( _dbCommand,_transaction);
dr.Close();
return dr;
}


}

}
