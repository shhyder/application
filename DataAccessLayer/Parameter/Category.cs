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
/// Class: Category
//----------------------------------------------------------------
    public class Category : BaseParameter
{

System.Int32  _category_ID;

public Category (Database db,DbTransaction dbTrans) 
{
	_db = db;
	_transaction = dbTrans;
}
public Category (Database db) 
{
	_db = db;
}
public Category () 
{
 
}
public Category (System.Int32  category_ID) 
{
  _category_ID = category_ID;
}

//----------------------------------------------------------------
/// Get specific records: Category
//----------------------------------------------------------------
public override IDataReader GetItem()
{

_dbCommand = _db.GetStoredProcCommand( "GetCategory");
_db.AddInParameter(_dbCommand, _DSParam.Category.Category_IDColumn.ToString(), DbType.Int32, _category_ID);
	return _db.ExecuteReader( _dbCommand);
}



public override IDataReader GetAllItem()
{

    _dbCommand = _db.GetStoredProcCommand("GetAllCategory");
    return _db.ExecuteReader(_dbCommand);
}




//----------------------------------------------------------------
/// Delete: Category
//----------------------------------------------------------------
public override IDataReader Delete( )
{

_dbCommand = _db.GetStoredProcCommand( "GetCategory");
_db.AddInParameter(_dbCommand, _DSParam.Category.Category_IDColumn.ToString(), DbType.Int32, _category_ID);
	return _db.ExecuteReader( _dbCommand);
}



//----------------------------------------------------------------
/// Insert record: Category
//----------------------------------------------------------------
public override IDataReader Insert(DSParameter ds)
{
_dbCommand = _db.GetStoredProcCommand( "InsertCategory");
	_db.AddOutParameter(_dbCommand, ds.Category.Category_IDColumn.ToString(), DbType.Int32,20);
	_db.AddInParameter(_dbCommand, ds.Category.CategoryColumn.ToString(), DbType.String,ds.Category.Rows[0][ds.Category.CategoryColumn.ToString()]);
	IDataReader dr = _db.ExecuteReader( _dbCommand,_transaction);
dr.Close();
_ID = System.Int32.Parse( _db.GetParameterValue(_dbCommand, "@Category_ID").ToString() );
return dr;
}



//----------------------------------------------------------------
/// Update: Category
//----------------------------------------------------------------
public override IDataReader Update(DSParameter ds)
{
_dbCommand = _db.GetStoredProcCommand( "UpdateCategory");
	_db.AddInParameter(_dbCommand, ds.Category.Category_IDColumn.ToString(), DbType.Int32,ds.Category.Rows[0][ds.Category.Category_IDColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Category.CategoryColumn.ToString(), DbType.String,ds.Category.Rows[0][ds.Category.CategoryColumn.ToString()]);
	IDataReader dr = _db.ExecuteReader( _dbCommand,_transaction);
dr.Close();
return dr;
}


}




}
