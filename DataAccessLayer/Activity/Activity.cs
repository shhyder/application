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
/// Class: Activity
//----------------------------------------------------------------
public class Activity : BaseNonParameter
{

System.Int32  _activity_ID;

public Activity (Database db,DbTransaction dbTrans) 
{
	_db = db;
	_transaction = dbTrans;
}
public Activity (Database db) 
{
	_db = db;
}
public Activity () 
{
 
}
public Activity (System.Int32  activity_ID) 
{
  _activity_ID = activity_ID;
}

//----------------------------------------------------------------
/// Get specific records: Activity
//----------------------------------------------------------------
public override IDataReader GetItem()
{

_dbCommand = _db.GetStoredProcCommand( "GetActivity");
	_db.AddInParameter(_dbCommand,_DS.Activity.Visit_IDColumn.ToString(), DbType.Int32,  _activity_ID );
	return _db.ExecuteReader( _dbCommand);
}



//----------------------------------------------------------------
/// Get all records: Activity
//----------------------------------------------------------------

public override IDataReader GetList()
{

_dbCommand = _db.GetStoredProcCommand( "GetAllActivity");
	return _db.ExecuteReader( _dbCommand);
}



//----------------------------------------------------------------
/// Delete: Activity
//----------------------------------------------------------------
public override IDataReader Delete( )
{

_dbCommand = _db.GetStoredProcCommand( "GetActivity");
	_db.AddInParameter(_dbCommand,_DS.Activity.Visit_IDColumn.ToString(), DbType.Int32,_activity_ID  );
	return _db.ExecuteReader( _dbCommand);
}



//----------------------------------------------------------------
/// Insert record: Activity
//----------------------------------------------------------------
public override IDataReader Insert(DS ds )
{
_dbCommand = _db.GetStoredProcCommand( "InsertActivity");
	_db.AddInParameter(_dbCommand, ds.Activity.Visit_IDColumn.ToString(), DbType.Int32,ds.Activity.Rows[0][ds.Activity.Visit_IDColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Activity.Activity_SequenceColumn.ToString(), DbType.Int32,ds.Activity.Rows[0][ds.Activity.Activity_SequenceColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Activity.Activity_Type_IDColumn.ToString(), DbType.Int32,ds.Activity.Rows[0][ds.Activity.Activity_Type_IDColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Activity.TimeColumn.ToString(), DbType.DateTime,ds.Activity.Rows[0][ds.Activity.TimeColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Activity.TextColumn.ToString(), DbType.String,ds.Activity.Rows[0][ds.Activity.TextColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Activity.IDColumn.ToString(), DbType.Int32,ds.Activity.Rows[0][ds.Activity.IDColumn.ToString()]);
	IDataReader dr = _db.ExecuteReader( _dbCommand,_transaction);
dr.Close();
return dr;
}



//----------------------------------------------------------------
/// Update: Activity
//----------------------------------------------------------------
public override  IDataReader Update( DS ds )
{
_dbCommand = _db.GetStoredProcCommand( "UpdateActivity");
	_db.AddInParameter(_dbCommand, ds.Activity.Visit_IDColumn.ToString(), DbType.Int32,ds.Activity.Rows[0][ds.Activity.Visit_IDColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Activity.Activity_SequenceColumn.ToString(), DbType.Int32,ds.Activity.Rows[0][ds.Activity.Activity_SequenceColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Activity.Activity_Type_IDColumn.ToString(), DbType.Int32,ds.Activity.Rows[0][ds.Activity.Activity_Type_IDColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Activity.TimeColumn.ToString(), DbType.DateTime,ds.Activity.Rows[0][ds.Activity.TimeColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Activity.TextColumn.ToString(), DbType.String,ds.Activity.Rows[0][ds.Activity.TextColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Activity.IDColumn.ToString(), DbType.Int32,ds.Activity.Rows[0][ds.Activity.IDColumn.ToString()]);
	IDataReader dr = _db.ExecuteReader( _dbCommand,_transaction);
dr.Close();
return dr;
}


}


}
