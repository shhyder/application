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
/// Class: Company_Product_Type
//----------------------------------------------------------------
    public class Company_Product_Type : BaseParameter
{

System.Int32  _company_product_type_ID;

public Company_Product_Type (Database db,DbTransaction dbTrans) 
{
	_db = db;
	_transaction = dbTrans;
}
public Company_Product_Type (Database db) 
{
	_db = db;
}
public Company_Product_Type () 
{
 
}
public Company_Product_Type (System.Int32  company_product_type_ID) 
{
  _company_product_type_ID = company_product_type_ID;
}

//----------------------------------------------------------------
/// Get specific records: Company_Product_Type
//----------------------------------------------------------------
public override IDataReader GetItem()
{

_dbCommand = _db.GetStoredProcCommand( "GetCompany_Product_Type");
_db.AddInParameter(_dbCommand, _DSParam.Company_Product_Type.Company_IDColumn.ToString(), DbType.Int32, _company_product_type_ID);
	return _db.ExecuteReader( _dbCommand);
}



//----------------------------------------------------------------
/// Get records List: Company_Product_Type
//----------------------------------------------------------------

public override IDataReader GetAllItem()
{

    _dbCommand = _db.GetStoredProcCommand("GetAllCompany_Product_Type");
    return _db.ExecuteReader(_dbCommand);
}




//----------------------------------------------------------------
/// Delete: Company_Product_Type
//----------------------------------------------------------------
public override IDataReader Delete( )
{

_dbCommand = _db.GetStoredProcCommand( "GetCompany_Product_Type");
_db.AddInParameter(_dbCommand, _DSParam.Company_Product_Type.Company_IDColumn.ToString(), DbType.Int32, _company_product_type_ID);
	return _db.ExecuteReader( _dbCommand);
}



//----------------------------------------------------------------
/// Insert record: Company_Product_Type
//----------------------------------------------------------------
public override IDataReader Insert(DSParameter ds)
{
_dbCommand = _db.GetStoredProcCommand( "InsertCompany_Product_Type");
	_db.AddInParameter(_dbCommand, ds.Company_Product_Type.Company_IDColumn.ToString(), DbType.Int32,ds.Company_Product_Type.Rows[0][ds.Company_Product_Type.Company_IDColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Company_Product_Type.Product_Type_IDColumn.ToString(), DbType.Int32,ds.Company_Product_Type.Rows[0][ds.Company_Product_Type.Product_Type_IDColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Company_Product_Type.Category_IDColumn.ToString(), DbType.Int32,ds.Company_Product_Type.Rows[0][ds.Company_Product_Type.Category_IDColumn.ToString()]);
	IDataReader dr = _db.ExecuteReader( _dbCommand,_transaction);
dr.Close();
return dr;
}



//----------------------------------------------------------------
/// Update: Company_Product_Type
//----------------------------------------------------------------
public override IDataReader Update(DSParameter ds)
{
_dbCommand = _db.GetStoredProcCommand( "UpdateCompany_Product_Type");
	_db.AddInParameter(_dbCommand, ds.Company_Product_Type.Company_IDColumn.ToString(), DbType.Int32,ds.Company_Product_Type.Rows[0][ds.Company_Product_Type.Company_IDColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Company_Product_Type.Product_Type_IDColumn.ToString(), DbType.Int32,ds.Company_Product_Type.Rows[0][ds.Company_Product_Type.Product_Type_IDColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Company_Product_Type.Category_IDColumn.ToString(), DbType.Int32,ds.Company_Product_Type.Rows[0][ds.Company_Product_Type.Category_IDColumn.ToString()]);
	IDataReader dr = _db.ExecuteReader( _dbCommand,_transaction);
dr.Close();
return dr;
}


}



}
