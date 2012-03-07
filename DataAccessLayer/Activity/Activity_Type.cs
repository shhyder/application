using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DataSet;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using System.Data.SqlTypes;
using System.Data.Common;

namespace DataAccessLayer.Activity
{
    //----------------------------------------------------------------
/// Class: Activity_Type
//----------------------------------------------------------------
public class Activity_Type : BaseParameter
{

System.Int32  _activity_type_ID;

public Activity_Type (Database db,DbTransaction dbTrans) 
{
	_db = db;
	_transaction = dbTrans;
}
public Activity_Type (Database db) 
{
	_db = db;
}
public Activity_Type () 
{
 
}
public Activity_Type (System.Int32  activity_type_ID) 
{
  _activity_type_ID = activity_type_ID;
}

//----------------------------------------------------------------
/// Get specific records: Activity_Type
//----------------------------------------------------------------
public override IDataReader GetItem()
{

_dbCommand = _db.GetStoredProcCommand( "GetActivity_Type");
_db.AddInParameter(_dbCommand, _DSParam.Activity_Type.Activity_Type_IDColumn.ToString(), DbType.Int32, _activity_type_ID);
	return _db.ExecuteReader( _dbCommand);
}



//----------------------------------------------------------------
/// Get all records: Activity_Type
//----------------------------------------------------------------

public override IDataReader GetAllItem()
{

_dbCommand = _db.GetStoredProcCommand( "GetAllActivity_Type");
	return _db.ExecuteReader( _dbCommand);
}



//----------------------------------------------------------------
/// Delete: Activity_Type
//----------------------------------------------------------------
public override IDataReader Delete( )
{

_dbCommand = _db.GetStoredProcCommand( "GetActivity_Type");
	_db.AddInParameter(_dbCommand,_DSParam.Activity_Type.Activity_Type_IDColumn.ToString(), DbType.Int32,_activity_type_ID  );
	return _db.ExecuteReader( _dbCommand);
}



//----------------------------------------------------------------
/// Insert record: Activity_Type
//----------------------------------------------------------------
public override IDataReader Insert(DSParameter ds )
{
_dbCommand = _db.GetStoredProcCommand( "InsertActivity_Type");
_db.AddOutParameter(_dbCommand, ds.Activity_Type.Activity_Type_IDColumn.ToString(), DbType.Int32, 20);
	_db.AddInParameter(_dbCommand, ds.Activity_Type.Activity_TypeColumn.ToString(), DbType.String,ds.Activity_Type.Rows[0][ds.Activity_Type.Activity_TypeColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Activity_Type.Has_IDColumn.ToString(), DbType.Boolean,ds.Activity_Type.Rows[0][ds.Activity_Type.Has_IDColumn.ToString()]);
	IDataReader dr = _db.ExecuteReader( _dbCommand,_transaction);
dr.Close();
_ID = Int32.Parse(_db.GetParameterValue(_dbCommand, "@Activity_Type_ID").ToString());
return dr;
}



//----------------------------------------------------------------
/// Update: Activity_Type
//----------------------------------------------------------------
public override  IDataReader Update( DSParameter ds )
{
_dbCommand = _db.GetStoredProcCommand( "UpdateActivity_Type");
_db.AddInParameter(_dbCommand, ds.Activity_Type.Activity_Type_IDColumn.ToString(), DbType.Int32, ds.Activity_Type.Rows[0][ds.Activity_Type.Activity_Type_IDColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Activity_Type.Activity_TypeColumn.ToString(), DbType.String,ds.Activity_Type.Rows[0][ds.Activity_Type.Activity_TypeColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Activity_Type.Has_IDColumn.ToString(), DbType.Boolean,ds.Activity_Type.Rows[0][ds.Activity_Type.Has_IDColumn.ToString()]);
	IDataReader dr = _db.ExecuteReader( _dbCommand,_transaction);
dr.Close();
return dr;
}


}

}
