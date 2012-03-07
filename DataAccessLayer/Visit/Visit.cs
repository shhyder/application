using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DataSet;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using System.Data.SqlTypes;
using System.Data.Common;

namespace DataAccessLayer.Visit
{
    //----------------------------------------------------------------
/// Class: Visit
//----------------------------------------------------------------
public class Visit : BaseNonParameter
{

System.Int32  _visit_ID;
GlobalVariables.Visit.IVisit _iVisit;

public Visit (Database db,DbTransaction dbTrans) 
{
	_db = db;
	_transaction = dbTrans;
}
public Visit (Database db) 
{
	_db = db;
}

public Visit(GlobalVariables.Visit.IVisit iVisit)
{
    _iVisit = iVisit;
}


public Visit () 
{
 
}
public Visit (System.Int32  visit_ID) 
{
  _visit_ID = visit_ID;
}

//----------------------------------------------------------------
/// Get specific records: Visit
//----------------------------------------------------------------
public override IDataReader GetItem()
{

_dbCommand = _db.GetStoredProcCommand( "GetVisit");
	_db.AddInParameter(_dbCommand,_DS.Visit.Visit_IDColumn.ToString(), DbType.Int32,  _visit_ID );
	return _db.ExecuteReader( _dbCommand);
}



//----------------------------------------------------------------
/// Get all records: Visit
//----------------------------------------------------------------

public override IDataReader GetList()
{

_dbCommand = _db.GetStoredProcCommand( "GetAllVisit");
	return _db.ExecuteReader( _dbCommand);
}



//----------------------------------------------------------------
/// Delete: Visit
//----------------------------------------------------------------
public override IDataReader Delete( )
{

_dbCommand = _db.GetStoredProcCommand( "GetVisit");
_db.AddInParameter(_dbCommand, _DS.Visit.Visit_IDColumn.ToString(), DbType.Int32, _visit_ID);
	return _db.ExecuteReader( _dbCommand);
}


public System.Data.DataSet GetPage()
{
    _dbCommand = _db.GetStoredProcCommand("GetConsumerPage");
    _db.AddInParameter(_dbCommand, "start", DbType.Int32, _iVisit.start);
    _db.AddInParameter(_dbCommand, "count", DbType.Int32, _iVisit.count);
    _db.AddOutParameter(_dbCommand, "totalCount", DbType.Int32, _iVisit.totalCount);
    _db.AddInParameter(_dbCommand, "start_Date", DbType.String, _iVisit.start_Date);
    _db.AddInParameter(_dbCommand, "end_Date", DbType.Int32, _iVisit.end_Date);
    _db.AddInParameter(_dbCommand, "City", DbType.String, _iVisit.City);
    _db.AddInParameter(_dbCommand, "State", DbType.String, _iVisit.State);
    _db.AddInParameter(_dbCommand, "Country", DbType.String, _iVisit.Country);
    

    IDataReader iDR = _db.ExecuteReader(_dbCommand);
    System.Data.DataSet ds = new System.Data.DataSet();
    ds.Tables.Add("List");
    ds.Load(iDR, LoadOption.OverwriteChanges, "List");

    iDR.Close();


    _ID = System.Int32.Parse(_db.GetParameterValue(_dbCommand, "@TotalCount").ToString());

    return ds;


}


//----------------------------------------------------------------
/// Insert record: Visit
//----------------------------------------------------------------
public override IDataReader Insert(DS ds )
{
_dbCommand = _db.GetStoredProcCommand( "InsertVisit");
_db.AddOutParameter(_dbCommand, ds.Visit.Visit_IDColumn.ToString(), DbType.Int32, 20);
	_db.AddInParameter(_dbCommand, ds.Visit.Start_TimeColumn.ToString(), DbType.DateTime,ds.Visit.Rows[0][ds.Visit.Start_TimeColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Visit.Endt_TimeColumn.ToString(), DbType.DateTime,ds.Visit.Rows[0][ds.Visit.Endt_TimeColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Visit.Route_FromColumn.ToString(), DbType.String,ds.Visit.Rows[0][ds.Visit.Route_FromColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Visit.IP_AddressColumn.ToString(), DbType.String,ds.Visit.Rows[0][ds.Visit.IP_AddressColumn.ToString()]);
	IDataReader dr = _db.ExecuteReader( _dbCommand,_transaction);
dr.Close();
_ID = Int32.Parse(_db.GetParameterValue(_dbCommand, "@Visit_ID").ToString());
return dr;
}



//----------------------------------------------------------------
/// Update: Visit
//----------------------------------------------------------------
public override  IDataReader Update( DS ds )
{
_dbCommand = _db.GetStoredProcCommand( "UpdateVisit");
	_db.AddInParameter(_dbCommand, ds.Visit.Visit_IDColumn.ToString(), DbType.Int32,ds.Visit.Rows[0][ds.Visit.Visit_IDColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Visit.Start_TimeColumn.ToString(), DbType.DateTime,ds.Visit.Rows[0][ds.Visit.Start_TimeColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Visit.Endt_TimeColumn.ToString(), DbType.DateTime,ds.Visit.Rows[0][ds.Visit.Endt_TimeColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Visit.Route_FromColumn.ToString(), DbType.String,ds.Visit.Rows[0][ds.Visit.Route_FromColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Visit.IP_AddressColumn.ToString(), DbType.String,ds.Visit.Rows[0][ds.Visit.IP_AddressColumn.ToString()]);
	IDataReader dr = _db.ExecuteReader( _dbCommand,_transaction);
dr.Close();
return dr;
}


}



}
