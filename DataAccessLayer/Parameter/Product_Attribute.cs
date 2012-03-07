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
/// Class: Product_Attribute
//----------------------------------------------------------------
    public class Product_Attribute : BaseParameter
{

System.Int32  _product_attribute_ID;

public Product_Attribute (Database db,DbTransaction dbTrans) 
{
	_db = db;
	_transaction = dbTrans;
}
public Product_Attribute (Database db) 
{
	_db = db;
}
public Product_Attribute () 
{
 
}
public Product_Attribute (System.Int32  product_attribute_ID) 
{
  _product_attribute_ID = product_attribute_ID;
}

//----------------------------------------------------------------
/// Get specific records: Product_Attribute
//----------------------------------------------------------------
public override IDataReader GetItem()
{

_dbCommand = _db.GetStoredProcCommand( "GetProduct_Attribute");
	_db.AddInParameter(_dbCommand, _DSParam.Product_Attribute.Product_IDColumn.ToString(), DbType.Int32,  _product_attribute_ID );
	return _db.ExecuteReader( _dbCommand);
}


public override IDataReader GetAllItem()
{

    _dbCommand = _db.GetStoredProcCommand("GetAllProduct_Attribute");
    return _db.ExecuteReader(_dbCommand);
}







//----------------------------------------------------------------
/// Delete: Product_Attribute
//----------------------------------------------------------------
public override IDataReader Delete( )
{

_dbCommand = _db.GetStoredProcCommand( "GetProduct_Attribute");
_db.AddInParameter(_dbCommand, _DSParam.Product_Attribute.Product_IDColumn.ToString(), DbType.Int32, _product_attribute_ID);
	return _db.ExecuteReader( _dbCommand);
}



//----------------------------------------------------------------
/// Insert record: Product_Attribute
//----------------------------------------------------------------
public override IDataReader Insert(DSParameter ds)
{
_dbCommand = _db.GetStoredProcCommand( "InsertProduct_Attribute");
	_db.AddInParameter(_dbCommand, ds.Product_Attribute.Product_IDColumn.ToString(), DbType.Int32,ds.Product_Attribute.Rows[0][ds.Product_Attribute.Product_IDColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Product_Attribute.Attribute_IDColumn.ToString(), DbType.Int32,ds.Product_Attribute.Rows[0][ds.Product_Attribute.Attribute_IDColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Product_Attribute.Attribute_Variant_IDColumn.ToString(), DbType.Int32,ds.Product_Attribute.Rows[0][ds.Product_Attribute.Attribute_Variant_IDColumn.ToString()]);
	IDataReader dr = _db.ExecuteReader( _dbCommand,_transaction);
dr.Close();
return dr;
}



//----------------------------------------------------------------
/// Update: Product_Attribute
//----------------------------------------------------------------
public override  IDataReader Update( DSParameter ds )
{
_dbCommand = _db.GetStoredProcCommand( "UpdateProduct_Attribute");
	_db.AddInParameter(_dbCommand, ds.Product_Attribute.Product_IDColumn.ToString(), DbType.Int32,ds.Product_Attribute.Rows[0][ds.Product_Attribute.Product_IDColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Product_Attribute.Attribute_IDColumn.ToString(), DbType.Int32,ds.Product_Attribute.Rows[0][ds.Product_Attribute.Attribute_IDColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Product_Attribute.Attribute_Variant_IDColumn.ToString(), DbType.Int32,ds.Product_Attribute.Rows[0][ds.Product_Attribute.Attribute_Variant_IDColumn.ToString()]);
	IDataReader dr = _db.ExecuteReader( _dbCommand,_transaction);
dr.Close();
return dr;
}


}



}
