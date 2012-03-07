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
/// Class: ZipCodes
//----------------------------------------------------------------
public class ZipCodes : BaseParameter
{

public  System.String  _zipcodes_ID;

public ZipCodes (Database db,DbTransaction dbTrans) 
{
	_db = db;
	_transaction = dbTrans;
}
public ZipCodes (Database db) 
{
	_db = db;
}
public ZipCodes () 
{
 
}
public ZipCodes (System.String  zipcodes_ID) 
{
  _zipcodes_ID = zipcodes_ID;
}

//----------------------------------------------------------------
/// Get specific records: ZipCodes
//----------------------------------------------------------------
public override IDataReader GetItem()
{

_dbCommand = _db.GetStoredProcCommand( "GetZipCodes");
	_db.AddInParameter(_dbCommand, _DSParam.ZipCodes.ZipCodeColumn.ToString(), DbType.String,  _zipcodes_ID );
	return _db.ExecuteReader( _dbCommand);
}

//----------------------------------------------------------------
/// Get specific records: ZipCodes
//----------------------------------------------------------------
public bool  Validate()
{
    _dbCommand = _db.GetStoredProcCommand("ValidateZipCodes");
    _db.AddInParameter(_dbCommand, _DSParam.ZipCodes.ZipCodeColumn.ToString(), DbType.String, _zipcodes_ID);
    IDataReader dr = _db.ExecuteReader(_dbCommand);
    dr.Read();

    if (dr.GetInt32(0) > 0)
        return true;
    
    return false;
}



public override IDataReader GetAllItem()
{

    _dbCommand = _db.GetStoredProcCommand("GetAllCategory");
    return _db.ExecuteReader(_dbCommand);
}


//----------------------------------------------------------------
/// Delete: ZipCodes
//----------------------------------------------------------------
public override IDataReader Delete( )
{

_dbCommand = _db.GetStoredProcCommand( "GetZipCodes");
_db.AddInParameter(_dbCommand, _DSParam.ZipCodes.CountryColumn.ToString(), DbType.String, _zipcodes_ID);
	return _db.ExecuteReader( _dbCommand);
}



//----------------------------------------------------------------
/// Insert record: ZipCodes
//----------------------------------------------------------------
public override IDataReader Insert( DSParameter ds )
{
_dbCommand = _db.GetStoredProcCommand( "InsertZipCodes");
	_db.AddInParameter(_dbCommand, ds.ZipCodes.CountryColumn.ToString(), DbType.String,ds.ZipCodes.Rows[0][ds.ZipCodes.CountryColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.ZipCodes.ZipCodeColumn.ToString(), DbType.String,ds.ZipCodes.Rows[0][ds.ZipCodes.ZipCodeColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.ZipCodes.CityColumn.ToString(), DbType.String,ds.ZipCodes.Rows[0][ds.ZipCodes.CityColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.ZipCodes.STATEColumn.ToString(), DbType.String,ds.ZipCodes.Rows[0][ds.ZipCodes.STATEColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.ZipCodes.StateAbbreviationColumn.ToString(), DbType.String,ds.ZipCodes.Rows[0][ds.ZipCodes.StateAbbreviationColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.ZipCodes.CountyColumn.ToString(), DbType.String,ds.ZipCodes.Rows[0][ds.ZipCodes.CountyColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.ZipCodes.LatitudeColumn.ToString(), DbType.Decimal,ds.ZipCodes.Rows[0][ds.ZipCodes.LatitudeColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.ZipCodes.LongitudeColumn.ToString(), DbType.Decimal,ds.ZipCodes.Rows[0][ds.ZipCodes.LongitudeColumn.ToString()]);
	IDataReader dr = _db.ExecuteReader( _dbCommand,_transaction);
dr.Close();
return dr;
}



//----------------------------------------------------------------
/// Update: ZipCodes
//----------------------------------------------------------------
public override IDataReader Update(DSParameter ds)
{
_dbCommand = _db.GetStoredProcCommand( "UpdateZipCodes");
	_db.AddInParameter(_dbCommand, ds.ZipCodes.CountryColumn.ToString(), DbType.String,ds.ZipCodes.Rows[0][ds.ZipCodes.CountryColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.ZipCodes.ZipCodeColumn.ToString(), DbType.String,ds.ZipCodes.Rows[0][ds.ZipCodes.ZipCodeColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.ZipCodes.CityColumn.ToString(), DbType.String,ds.ZipCodes.Rows[0][ds.ZipCodes.CityColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.ZipCodes.STATEColumn.ToString(), DbType.String,ds.ZipCodes.Rows[0][ds.ZipCodes.STATEColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.ZipCodes.StateAbbreviationColumn.ToString(), DbType.String,ds.ZipCodes.Rows[0][ds.ZipCodes.StateAbbreviationColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.ZipCodes.CountyColumn.ToString(), DbType.String,ds.ZipCodes.Rows[0][ds.ZipCodes.CountyColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.ZipCodes.LatitudeColumn.ToString(), DbType.Decimal,ds.ZipCodes.Rows[0][ds.ZipCodes.LatitudeColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.ZipCodes.LongitudeColumn.ToString(), DbType.Decimal,ds.ZipCodes.Rows[0][ds.ZipCodes.LongitudeColumn.ToString()]);
	IDataReader dr = _db.ExecuteReader( _dbCommand,_transaction);
dr.Close();
return dr;
}


}



}
