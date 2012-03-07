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
/// Class: Company
//----------------------------------------------------------------
public class Company : BaseParameter
{

System.Int32  _company_ID;

public Company (Database db,DbTransaction dbTrans) 
{
	_db = db;
	_transaction = dbTrans;
}
public Company (Database db) 
{
	_db = db;
}
public Company () 
{
 
}
public Company (System.Int32  company_ID) 
{
  _company_ID = company_ID;
}

//----------------------------------------------------------------
/// Get specific records: Company
//----------------------------------------------------------------
public override IDataReader GetItem()
{

_dbCommand = _db.GetStoredProcCommand( "GetCompany");
_db.AddInParameter(_dbCommand, _DSParam.Company.Company_IDColumn.ToString(), DbType.Int32, _company_ID);
	return _db.ExecuteReader( _dbCommand);
}



//----------------------------------------------------------------
/// Get records List: Company
//----------------------------------------------------------------

public override IDataReader GetAllItem()
{

    _dbCommand = _db.GetStoredProcCommand("GetAllCompany");
    return _db.ExecuteReader(_dbCommand);
}




//----------------------------------------------------------------
/// Delete: Company
//----------------------------------------------------------------
public override IDataReader Delete( )
{

_dbCommand = _db.GetStoredProcCommand( "GetCompany");
_db.AddInParameter(_dbCommand, _DSParam.Company.Company_IDColumn.ToString(), DbType.Int32, _company_ID);
	return _db.ExecuteReader( _dbCommand);
}



//----------------------------------------------------------------
/// Insert record: Company
//----------------------------------------------------------------
public override IDataReader Insert( DSParameter ds )
{
_dbCommand = _db.GetStoredProcCommand( "InsertCompany");
	_db.AddOutParameter(_dbCommand, ds.Company.Company_IDColumn.ToString(), DbType.Int32,20);
	_db.AddInParameter(_dbCommand, ds.Company.CompanyColumn.ToString(), DbType.String,ds.Company.Rows[0][ds.Company.CompanyColumn.ToString()]);
	IDataReader dr = _db.ExecuteReader( _dbCommand,_transaction);
dr.Close();
_ID = System.Int32.Parse( _db.GetParameterValue(_dbCommand, "@Company_ID").ToString() );
return dr;
}



//----------------------------------------------------------------
/// Update: Company
//----------------------------------------------------------------
public override IDataReader Update(DSParameter ds)
{
_dbCommand = _db.GetStoredProcCommand( "UpdateCompany");
	_db.AddInParameter(_dbCommand, ds.Company.Company_IDColumn.ToString(), DbType.Int32,ds.Company.Rows[0][ds.Company.Company_IDColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Company.CompanyColumn.ToString(), DbType.String,ds.Company.Rows[0][ds.Company.CompanyColumn.ToString()]);
	IDataReader dr = _db.ExecuteReader( _dbCommand,_transaction);
dr.Close();
return dr;
}


}



}
