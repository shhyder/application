using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DataSet;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using System.Data.SqlTypes;
using System.Data.Common;

namespace DataAccessLayer.Attribute
{
   

    
//----------------------------------------------------------------
/// Class: Attribute_Variant
//----------------------------------------------------------------
public class Attribute_Variant : BaseParameter
{

System.Int32  _attribute_variant_ID;

public Attribute_Variant (Database db,DbTransaction dbTrans) 
{
	_db = db;
	_transaction = dbTrans;
}
public Attribute_Variant (Database db) 
{
	_db = db;
}
public Attribute_Variant () 
{
 
}
public Attribute_Variant (System.Int32  attribute_variant_ID) 
{
  _attribute_variant_ID = attribute_variant_ID;
}

//----------------------------------------------------------------
/// Get specific records: Attribute_Variant
//----------------------------------------------------------------
public override IDataReader GetItem()
{

_dbCommand = _db.GetStoredProcCommand( "GetAttribute_Variant");
	_db.AddInParameter(_dbCommand,_DSParam.Attribute_Variant.Attribute_Variant_IDColumn.ToString(), DbType.Int32,  _attribute_variant_ID );
	return _db.ExecuteReader( _dbCommand);
}

//----------------------------------------------------------------
/// Get specific records: All Attribute
//----------------------------------------------------------------
public override IDataReader GetAllItem()
{

    _dbCommand = _db.GetStoredProcCommand("GetAllAttribute_Variant");
    return _db.ExecuteReader(_dbCommand);
}

//----------------------------------------------------------------
/// Get records List: Attribute_Variant
//----------------------------------------------------------------

public  IDataReader GetList()
{

_dbCommand = _db.GetStoredProcCommand( "GetListAttribute_VariantList");
	return _db.ExecuteReader( _dbCommand);
}



//----------------------------------------------------------------
/// Delete: Attribute_Variant
//----------------------------------------------------------------
public override IDataReader Delete( )
{

_dbCommand = _db.GetStoredProcCommand( "GetAttribute_Variant");
_db.AddInParameter(_dbCommand, _DSParam.Attribute_Variant.Attribute_Variant_IDColumn.ToString(), DbType.Int32, _attribute_variant_ID);
	return _db.ExecuteReader( _dbCommand);
}



//----------------------------------------------------------------
/// Insert record: Attribute_Variant
//----------------------------------------------------------------
public override IDataReader Insert( DSParameter ds )
{
_dbCommand = _db.GetStoredProcCommand( "InsertAttribute_Variant");
	_db.AddOutParameter(_dbCommand, ds.Attribute_Variant.Attribute_Variant_IDColumn.ToString(), DbType.Int32,20);
	_db.AddInParameter(_dbCommand, ds.Attribute_Variant.Attribute_IDColumn.ToString(), DbType.Int32,ds.Attribute_Variant.Rows[0][ds.Attribute_Variant.Attribute_IDColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Attribute_Variant.Attribute_VariantColumn.ToString(), DbType.String,ds.Attribute_Variant.Rows[0][ds.Attribute_Variant.Attribute_VariantColumn.ToString()]);
	IDataReader dr = _db.ExecuteReader( _dbCommand,_transaction);
dr.Close();
_ID = System.Int32.Parse( _db.GetParameterValue(_dbCommand, "@Attribute_Variant_ID").ToString() );
return dr;
}



//----------------------------------------------------------------
/// Update: Attribute_Variant
//----------------------------------------------------------------
public override IDataReader Update(DSParameter ds)
{
_dbCommand = _db.GetStoredProcCommand( "UpdateAttribute_Variant");
	_db.AddInParameter(_dbCommand, ds.Attribute_Variant.Attribute_Variant_IDColumn.ToString(), DbType.Int32,ds.Attribute_Variant.Rows[0][ds.Attribute_Variant.Attribute_Variant_IDColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Attribute_Variant.Attribute_IDColumn.ToString(), DbType.Int32,ds.Attribute_Variant.Rows[0][ds.Attribute_Variant.Attribute_IDColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Attribute_Variant.Attribute_VariantColumn.ToString(), DbType.String,ds.Attribute_Variant.Rows[0][ds.Attribute_Variant.Attribute_VariantColumn.ToString()]);
	IDataReader dr = _db.ExecuteReader( _dbCommand,_transaction);
dr.Close();
return dr;
}


}

}
