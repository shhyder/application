using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DataSet;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using System.Data.SqlTypes;
using System.Data.Common;

namespace DataAccessLayer
{
    














//----------------------------------------------------------------
/// Class: Product_Distributor
//----------------------------------------------------------------
public class Product_Distributor : BaseParameter
{

System.Int32  _product_distributor_ID;

public Product_Distributor (Database db,DbTransaction dbTrans) 
{
	_db = db;
	_transaction = dbTrans;
}
public Product_Distributor (Database db) 
{
	_db = db;
}
public Product_Distributor () 
{
 
}
public Product_Distributor (System.Int32  product_distributor_ID) 
{
  _product_distributor_ID = product_distributor_ID;
}

//----------------------------------------------------------------
/// Get specific records: Product_Distributor
//----------------------------------------------------------------
public override IDataReader GetItem()
{

_dbCommand = _db.GetStoredProcCommand( "GetProduct_Distributor");
	_db.AddInParameter(_dbCommand,_DSParam.Product_Distributor.Product_IDColumn.ToString(), DbType.Int32,  _product_distributor_ID );
	return _db.ExecuteReader( _dbCommand);
}



//----------------------------------------------------------------
/// Get specific records: All Brand
//----------------------------------------------------------------
public override IDataReader GetAllItem()
{

    _dbCommand = _db.GetStoredProcCommand("GetAllProduct_Distributor");
    return _db.ExecuteReader(_dbCommand);
}



//----------------------------------------------------------------
/// Delete: Product_Distributor
//----------------------------------------------------------------
public override IDataReader Delete( )
{

_dbCommand = _db.GetStoredProcCommand( "GetProduct_Distributor");
	_db.AddInParameter(_dbCommand,_DSParam.Product_Distributor.Product_IDColumn.ToString(), DbType.Int32,_product_distributor_ID  );
	return _db.ExecuteReader( _dbCommand);
}



//----------------------------------------------------------------
/// Insert record: Product_Distributor
//----------------------------------------------------------------
public override IDataReader Insert(DSParameter ds )
{
_dbCommand = _db.GetStoredProcCommand( "InsertProduct_Distributor");
	_db.AddInParameter(_dbCommand, ds.Product_Distributor.Product_IDColumn.ToString(), DbType.Int32,ds.Product_Distributor.Rows[0][ds.Product_Distributor.Product_IDColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Product_Distributor.Distributor_IDColumn.ToString(), DbType.Int32,ds.Product_Distributor.Rows[0][ds.Product_Distributor.Distributor_IDColumn.ToString()]);
	IDataReader dr = _db.ExecuteReader( _dbCommand,_transaction);
dr.Close();
return dr;
}



//----------------------------------------------------------------
/// Update: Product_Distributor
//----------------------------------------------------------------
public override  IDataReader Update( DSParameter ds )
{
_dbCommand = _db.GetStoredProcCommand( "UpdateProduct_Distributor");
	_db.AddInParameter(_dbCommand, ds.Product_Distributor.Product_IDColumn.ToString(), DbType.Int32,ds.Product_Distributor.Rows[0][ds.Product_Distributor.Product_IDColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Product_Distributor.Distributor_IDColumn.ToString(), DbType.Int32,ds.Product_Distributor.Rows[0][ds.Product_Distributor.Distributor_IDColumn.ToString()]);
	IDataReader dr = _db.ExecuteReader( _dbCommand,_transaction);
dr.Close();
return dr;
}


}






//----------------------------------------------------------------
/// Class: Product_Type_Attribute
//----------------------------------------------------------------
public class Product_Type_Attribute : BaseParameter
{

System.Int32  _product_type_attribute_ID;

public Product_Type_Attribute (Database db,DbTransaction dbTrans) 
{
	_db = db;
	_transaction = dbTrans;
}
public Product_Type_Attribute (Database db) 
{
	_db = db;
}
public Product_Type_Attribute () 
{
 
}
public Product_Type_Attribute (System.Int32  product_type_attribute_ID) 
{
  _product_type_attribute_ID = product_type_attribute_ID;
}

//----------------------------------------------------------------
/// Get specific records: Product_Type_Attribute
//----------------------------------------------------------------
public override IDataReader GetItem()
{

_dbCommand = _db.GetStoredProcCommand( "GetProduct_Type_Attribute");
	_db.AddInParameter(_dbCommand,_DSParam.Product_Type_Attribute.Product_Type_IDColumn.ToString(), DbType.Int32,  _product_type_attribute_ID );
	return _db.ExecuteReader( _dbCommand);
}



//----------------------------------------------------------------
/// Get specific records: All Brand
//----------------------------------------------------------------
public override IDataReader GetAllItem()
{

    _dbCommand = _db.GetStoredProcCommand("GetAllProduct_Type_Attribute");
    return _db.ExecuteReader(_dbCommand);
}



//----------------------------------------------------------------
/// Delete: Product_Type_Attribute
//----------------------------------------------------------------
public override IDataReader Delete( )
{

_dbCommand = _db.GetStoredProcCommand( "GetProduct_Type_Attribute");
	_db.AddInParameter(_dbCommand,_DSParam.Product_Type_Attribute.Product_Type_IDColumn.ToString(), DbType.Int32,_product_type_attribute_ID  );
	return _db.ExecuteReader( _dbCommand);
}



//----------------------------------------------------------------
/// Insert record: Product_Type_Attribute
//----------------------------------------------------------------
public override IDataReader Insert(DSParameter ds )
{
_dbCommand = _db.GetStoredProcCommand( "InsertProduct_Type_Attribute");
	_db.AddInParameter(_dbCommand, ds.Product_Type_Attribute.Product_Type_IDColumn.ToString(), DbType.Int32,ds.Product_Type_Attribute.Rows[0][ds.Product_Type_Attribute.Product_Type_IDColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Product_Type_Attribute.Attribute_IDColumn.ToString(), DbType.Int32,ds.Product_Type_Attribute.Rows[0][ds.Product_Type_Attribute.Attribute_IDColumn.ToString()]);
	IDataReader dr = _db.ExecuteReader( _dbCommand,_transaction);
dr.Close();
return dr;
}



//----------------------------------------------------------------
/// Update: Product_Type_Attribute
//----------------------------------------------------------------
public override  IDataReader Update( DSParameter ds )
{
_dbCommand = _db.GetStoredProcCommand( "UpdateProduct_Type_Attribute");
	_db.AddInParameter(_dbCommand, ds.Product_Type_Attribute.Product_Type_IDColumn.ToString(), DbType.Int32,ds.Product_Type_Attribute.Rows[0][ds.Product_Type_Attribute.Product_Type_IDColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Product_Type_Attribute.Attribute_IDColumn.ToString(), DbType.Int32,ds.Product_Type_Attribute.Rows[0][ds.Product_Type_Attribute.Attribute_IDColumn.ToString()]);
	IDataReader dr = _db.ExecuteReader( _dbCommand,_transaction);
dr.Close();
return dr;
}


}




//----------------------------------------------------------------
/// Class: Product_Type_Relationship
//----------------------------------------------------------------
public class Product_Type_Relationship : BaseParameter
{

System.Int32  _product_type_relationship_ID;

public Product_Type_Relationship (Database db,DbTransaction dbTrans) 
{
	_db = db;
	_transaction = dbTrans;
}
public Product_Type_Relationship (Database db) 
{
	_db = db;
}
public Product_Type_Relationship () 
{
 
}
public Product_Type_Relationship (System.Int32  product_type_relationship_ID) 
{
  _product_type_relationship_ID = product_type_relationship_ID;
}

//----------------------------------------------------------------
/// Get specific records: Product_Type_Relationship
//----------------------------------------------------------------
public override IDataReader GetItem()
{

_dbCommand = _db.GetStoredProcCommand( "GetProduct_Type_Relationship");
	_db.AddInParameter(_dbCommand,_DSParam.Product_Type_Relationship.Product_Type_IDColumn.ToString(), DbType.Int32,  _product_type_relationship_ID );
	return _db.ExecuteReader( _dbCommand);
}



//----------------------------------------------------------------
/// Get specific records: All Brand
//----------------------------------------------------------------
public override IDataReader GetAllItem()
{

    _dbCommand = _db.GetStoredProcCommand("GetAllProduct_Type_Relationship");
    return _db.ExecuteReader(_dbCommand);
}




//----------------------------------------------------------------
/// Delete: Product_Type_Relationship
//----------------------------------------------------------------
public override IDataReader Delete( )
{

_dbCommand = _db.GetStoredProcCommand( "GetProduct_Type_Relationship");
	_db.AddInParameter(_dbCommand,_DSParam.Product_Type_Relationship.Product_Type_IDColumn.ToString(), DbType.Int32,_product_type_relationship_ID  );
	return _db.ExecuteReader( _dbCommand);
}



//----------------------------------------------------------------
/// Insert record: Product_Type_Relationship
//----------------------------------------------------------------
public override IDataReader Insert(DSParameter ds )
{
_dbCommand = _db.GetStoredProcCommand( "InsertProduct_Type_Relationship");
	_db.AddInParameter(_dbCommand, ds.Product_Type_Relationship.Product_Type_IDColumn.ToString(), DbType.Int32,ds.Product_Type_Relationship.Rows[0][ds.Product_Type_Relationship.Product_Type_IDColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Product_Type_Relationship.Relationship_IDColumn.ToString(), DbType.Int32,ds.Product_Type_Relationship.Rows[0][ds.Product_Type_Relationship.Relationship_IDColumn.ToString()]);
	IDataReader dr = _db.ExecuteReader( _dbCommand,_transaction);
dr.Close();
return dr;
}



//----------------------------------------------------------------
/// Update: Product_Type_Relationship
//----------------------------------------------------------------
public override  IDataReader Update( DSParameter ds )
{
_dbCommand = _db.GetStoredProcCommand( "UpdateProduct_Type_Relationship");
	_db.AddInParameter(_dbCommand, ds.Product_Type_Relationship.Product_Type_IDColumn.ToString(), DbType.Int32,ds.Product_Type_Relationship.Rows[0][ds.Product_Type_Relationship.Product_Type_IDColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Product_Type_Relationship.Relationship_IDColumn.ToString(), DbType.Int32,ds.Product_Type_Relationship.Rows[0][ds.Product_Type_Relationship.Relationship_IDColumn.ToString()]);
	IDataReader dr = _db.ExecuteReader( _dbCommand,_transaction);
dr.Close();
return dr;
}


}




//----------------------------------------------------------------
/// Class: Relationship
//----------------------------------------------------------------
public class Relationship : BaseParameter
{

System.Int32  _relationship_ID;

public Relationship (Database db,DbTransaction dbTrans) 
{
	_db = db;
	_transaction = dbTrans;
}
public Relationship (Database db) 
{
	_db = db;
}
public Relationship () 
{
 
}
public Relationship (System.Int32  relationship_ID) 
{
  _relationship_ID = relationship_ID;
}

//----------------------------------------------------------------
/// Get specific records: Relationship
//----------------------------------------------------------------
public override IDataReader GetItem()
{

_dbCommand = _db.GetStoredProcCommand( "GetRelationship");
	_db.AddInParameter(_dbCommand,_DSParam.Relationship.Relationship_IDColumn.ToString(), DbType.Int32,  _relationship_ID );
	return _db.ExecuteReader( _dbCommand);
}



//----------------------------------------------------------------
/// Get specific records: All Brand
//----------------------------------------------------------------
public override IDataReader GetAllItem()
{

    _dbCommand = _db.GetStoredProcCommand("GetAllBrand");
    return _db.ExecuteReader(_dbCommand);
}




//----------------------------------------------------------------
/// Delete: Relationship
//----------------------------------------------------------------
public override IDataReader Delete( )
{

_dbCommand = _db.GetStoredProcCommand( "GetRelationship");
	_db.AddInParameter(_dbCommand,_DSParam.Relationship.Relationship_IDColumn.ToString(), DbType.Int32,_relationship_ID  );
	return _db.ExecuteReader( _dbCommand);
}



//----------------------------------------------------------------
/// Insert record: Relationship
//----------------------------------------------------------------
public override IDataReader Insert(DSParameter ds )
{
_dbCommand = _db.GetStoredProcCommand( "InsertRelationship");
	_db.AddOutParameter(_dbCommand, ds.Relationship.Relationship_IDColumn.ToString(), DbType.Int32,20);
	_db.AddInParameter(_dbCommand, ds.Relationship.RelationshipColumn.ToString(), DbType.String,ds.Relationship.Rows[0][ds.Relationship.RelationshipColumn.ToString()]);
	IDataReader dr = _db.ExecuteReader( _dbCommand,_transaction);
dr.Close();
_ID = System.Int32.Parse( _db.GetParameterValue(_dbCommand, "@Relationship_ID").ToString() );
return dr;
}



//----------------------------------------------------------------
/// Update: Relationship
//----------------------------------------------------------------
public override  IDataReader Update( DSParameter ds )
{
_dbCommand = _db.GetStoredProcCommand( "UpdateRelationship");
	_db.AddInParameter(_dbCommand, ds.Relationship.Relationship_IDColumn.ToString(), DbType.Int32,ds.Relationship.Rows[0][ds.Relationship.Relationship_IDColumn.ToString()]);
	_db.AddInParameter(_dbCommand, ds.Relationship.RelationshipColumn.ToString(), DbType.String,ds.Relationship.Rows[0][ds.Relationship.RelationshipColumn.ToString()]);
	IDataReader dr = _db.ExecuteReader( _dbCommand,_transaction);
dr.Close();
return dr;
}


}



}
