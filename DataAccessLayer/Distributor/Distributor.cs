using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DataSet;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using System.Data.SqlTypes;
using System.Data.Common;

namespace DataAccessLayer.Distributor
{
    //----------------------------------------------------------------
/// Class: Distributor
//----------------------------------------------------------------
public class Distributor : BaseParameter
{

System.Int32  _distributor_ID;

public Distributor (Database db,DbTransaction dbTrans) 
{
	_db = db;
	_transaction = dbTrans;
}
public Distributor (Database db) 
{
	_db = db;
}
public Distributor () 
{
 
}
public Distributor (System.Int32  distributor_ID) 
{
  _distributor_ID = distributor_ID;
}

//----------------------------------------------------------------
/// Get specific records: Distributor
//----------------------------------------------------------------
public override IDataReader GetItem()
{

_dbCommand = _db.GetStoredProcCommand( "GetDistributor");
	_db.AddInParameter(_dbCommand,_DSParam.Distributor.IDColumn.ToString(), DbType.Int32,  _distributor_ID );
	return _db.ExecuteReader( _dbCommand);
}



//----------------------------------------------------------------
/// Get records List: Distributor
//----------------------------------------------------------------

public override IDataReader GetAllItem()
{

_dbCommand = _db.GetStoredProcCommand( "GetAllDistributor");
	return _db.ExecuteReader( _dbCommand);
}



//----------------------------------------------------------------
/// Delete: Distributor
//----------------------------------------------------------------
public override IDataReader Delete( )
{

_dbCommand = _db.GetStoredProcCommand( "GetDistributor");
_db.AddInParameter(_dbCommand, _DSParam.Distributor.IDColumn.ToString(), DbType.Int32, _distributor_ID);
	return _db.ExecuteReader( _dbCommand);
}


//----------------------------------------------------------------
/// Get specific records: distributor State count
//----------------------------------------------------------------
public IDataReader GetStateCount()
{

    _dbCommand = _db.GetStoredProcCommand("GetDistributorStateCount");
    return _db.ExecuteReader(_dbCommand);
}



    //----------------------------------------------------------------
/// Insert record: Distributor
//----------------------------------------------------------------
public override IDataReader Insert(DSParameter ds)
{
_dbCommand = _db.GetStoredProcCommand( "InsertDistributor");
	_db.AddOutParameter(_dbCommand, ds.Distributor.IDColumn.ToString(), DbType.Int32,20);
	_db.AddInParameter(_dbCommand, ds.Distributor.Is_ActiveColumn.ToString(), DbType.Boolean,ds.Distributor.Rows[0][ds.Distributor.Is_ActiveColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Distributor.Distributor_IDColumn.ToString(), DbType.Int32,ds.Distributor.Rows[0][ds.Distributor.Distributor_IDColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Distributor.DistributorColumn.ToString(), DbType.String,ds.Distributor.Rows[0][ds.Distributor.DistributorColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Distributor.CityColumn.ToString(), DbType.String,ds.Distributor.Rows[0][ds.Distributor.CityColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Distributor.StateColumn.ToString(), DbType.String,ds.Distributor.Rows[0][ds.Distributor.StateColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Distributor.AddressColumn.ToString(), DbType.String,ds.Distributor.Rows[0][ds.Distributor.AddressColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Distributor.LatitudeColumn.ToString(), DbType.Decimal,ds.Distributor.Rows[0][ds.Distributor.LatitudeColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Distributor.LongitudeColumn.ToString(), DbType.Decimal,ds.Distributor.Rows[0][ds.Distributor.LongitudeColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Distributor.EmailColumn.ToString(), DbType.String,ds.Distributor.Rows[0][ds.Distributor.EmailColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Distributor.Zip_CodeColumn.ToString(), DbType.String,ds.Distributor.Rows[0][ds.Distributor.Zip_CodeColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Distributor.BuildingAppartmentColumn.ToString(), DbType.String,ds.Distributor.Rows[0][ds.Distributor.BuildingAppartmentColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Distributor.Street_NoColumn.ToString(), DbType.String,ds.Distributor.Rows[0][ds.Distributor.Street_NoColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Distributor.Street_NameColumn.ToString(), DbType.String,ds.Distributor.Rows[0][ds.Distributor.Street_NameColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Distributor.CountryColumn.ToString(), DbType.String,ds.Distributor.Rows[0][ds.Distributor.CountryColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Distributor.Phone1Column.ToString(), DbType.String,ds.Distributor.Rows[0][ds.Distributor.Phone1Column.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Distributor.Phone2Column.ToString(), DbType.String,ds.Distributor.Rows[0][ds.Distributor.Phone2Column.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Distributor.Email2Column.ToString(), DbType.String,ds.Distributor.Rows[0][ds.Distributor.Email2Column.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Distributor.WebsiteColumn.ToString(), DbType.String,ds.Distributor.Rows[0][ds.Distributor.WebsiteColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Distributor.Is_Email1_DisplayColumn.ToString(), DbType.Boolean,ds.Distributor.Rows[0][ds.Distributor.Is_Email1_DisplayColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Distributor.Is_Email2_DisplayColumn.ToString(), DbType.Boolean,ds.Distributor.Rows[0][ds.Distributor.Is_Email2_DisplayColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Distributor.Is_Phone1_DisplayColumn.ToString(), DbType.Boolean,ds.Distributor.Rows[0][ds.Distributor.Is_Phone1_DisplayColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Distributor.Is_Phone2_DisplayColumn.ToString(), DbType.Boolean,ds.Distributor.Rows[0][ds.Distributor.Is_Phone2_DisplayColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Distributor.Is_Website_DisplayColumn.ToString(), DbType.Boolean,ds.Distributor.Rows[0][ds.Distributor.Is_Website_DisplayColumn.ToString()]);
	IDataReader dr = _db.ExecuteReader( _dbCommand,_transaction);
dr.Close();
_ID = System.Int32.Parse( _db.GetParameterValue(_dbCommand, "@ID").ToString() );
return dr;
}




//----------------------------------------------------------------
/// Update: Distributor
//----------------------------------------------------------------
public override IDataReader Update(DSParameter ds)
{
_dbCommand = _db.GetStoredProcCommand( "UpdateDistributor");
	_db.AddInParameter(_dbCommand, ds.Distributor.IDColumn.ToString(), DbType.Int32,ds.Distributor.Rows[0][ds.Distributor.IDColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Distributor.Is_ActiveColumn.ToString(), DbType.Boolean,ds.Distributor.Rows[0][ds.Distributor.Is_ActiveColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Distributor.Distributor_IDColumn.ToString(), DbType.Int32,ds.Distributor.Rows[0][ds.Distributor.Distributor_IDColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Distributor.DistributorColumn.ToString(), DbType.String,ds.Distributor.Rows[0][ds.Distributor.DistributorColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Distributor.CityColumn.ToString(), DbType.String,ds.Distributor.Rows[0][ds.Distributor.CityColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Distributor.StateColumn.ToString(), DbType.String,ds.Distributor.Rows[0][ds.Distributor.StateColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Distributor.AddressColumn.ToString(), DbType.String,ds.Distributor.Rows[0][ds.Distributor.AddressColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Distributor.LatitudeColumn.ToString(), DbType.Decimal,ds.Distributor.Rows[0][ds.Distributor.LatitudeColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Distributor.LongitudeColumn.ToString(), DbType.Decimal,ds.Distributor.Rows[0][ds.Distributor.LongitudeColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Distributor.EmailColumn.ToString(), DbType.String,ds.Distributor.Rows[0][ds.Distributor.EmailColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Distributor.Zip_CodeColumn.ToString(), DbType.String,ds.Distributor.Rows[0][ds.Distributor.Zip_CodeColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Distributor.BuildingAppartmentColumn.ToString(), DbType.String,ds.Distributor.Rows[0][ds.Distributor.BuildingAppartmentColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Distributor.Street_NoColumn.ToString(), DbType.String,ds.Distributor.Rows[0][ds.Distributor.Street_NoColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Distributor.Street_NameColumn.ToString(), DbType.String,ds.Distributor.Rows[0][ds.Distributor.Street_NameColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Distributor.CountryColumn.ToString(), DbType.String,ds.Distributor.Rows[0][ds.Distributor.CountryColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Distributor.Phone1Column.ToString(), DbType.String,ds.Distributor.Rows[0][ds.Distributor.Phone1Column.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Distributor.Phone2Column.ToString(), DbType.String,ds.Distributor.Rows[0][ds.Distributor.Phone2Column.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Distributor.Email2Column.ToString(), DbType.String,ds.Distributor.Rows[0][ds.Distributor.Email2Column.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Distributor.WebsiteColumn.ToString(), DbType.String,ds.Distributor.Rows[0][ds.Distributor.WebsiteColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Distributor.Is_Email1_DisplayColumn.ToString(), DbType.Boolean,ds.Distributor.Rows[0][ds.Distributor.Is_Email1_DisplayColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Distributor.Is_Email2_DisplayColumn.ToString(), DbType.Boolean,ds.Distributor.Rows[0][ds.Distributor.Is_Email2_DisplayColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Distributor.Is_Phone1_DisplayColumn.ToString(), DbType.Boolean,ds.Distributor.Rows[0][ds.Distributor.Is_Phone1_DisplayColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Distributor.Is_Phone2_DisplayColumn.ToString(), DbType.Boolean,ds.Distributor.Rows[0][ds.Distributor.Is_Phone2_DisplayColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Distributor.Is_Website_DisplayColumn.ToString(), DbType.Boolean,ds.Distributor.Rows[0][ds.Distributor.Is_Website_DisplayColumn.ToString()]);
	IDataReader dr = _db.ExecuteReader( _dbCommand,_transaction);
dr.Close();
return dr;
}


}






}
