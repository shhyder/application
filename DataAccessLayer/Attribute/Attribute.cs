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
    /// Class: Attribute
    //----------------------------------------------------------------
    public class Attribute : BaseParameter
    {

        System.Int32 _attribute_ID;

        public Attribute(Database db, DbTransaction dbTrans)
        {
            _db = db;
            _transaction = dbTrans;
        }
        public Attribute(Database db)
        {
            _db = db;
        }
        public Attribute()
        {

        }
        public Attribute(System.Int32 attribute_ID)
        {
            _attribute_ID = attribute_ID;
        }

        //----------------------------------------------------------------
        /// Get specific records: Attribute
        //----------------------------------------------------------------
        public override IDataReader GetItem()
        {

            _dbCommand = _db.GetStoredProcCommand("GetAttribute");
            _db.AddInParameter(_dbCommand, _DSParam.Attribute.Attribute_IDColumn.ToString(), DbType.Int32, _attribute_ID);
            return _db.ExecuteReader(_dbCommand);
        }



        //----------------------------------------------------------------
        /// Get specific records: All Attribute
        //----------------------------------------------------------------
        public override IDataReader GetAllItem()
        {

            _dbCommand = _db.GetStoredProcCommand("GetAllAttribute");
            return _db.ExecuteReader(_dbCommand);
        }


        //----------------------------------------------------------------
        /// Delete: Attribute
        //----------------------------------------------------------------
        public override IDataReader Delete()
        {

            _dbCommand = _db.GetStoredProcCommand("GetAttribute");
            _db.AddInParameter(_dbCommand, _DSParam.Attribute.Attribute_IDColumn.ToString(), DbType.Int32, _attribute_ID);
            return _db.ExecuteReader(_dbCommand);
        }



        //----------------------------------------------------------------
        /// Insert record: Attribute
        //----------------------------------------------------------------
        public override IDataReader Insert(DSParameter ds)
        {
            _dbCommand = _db.GetStoredProcCommand("InsertAttribute");
            _db.AddOutParameter(_dbCommand, ds.Attribute.Attribute_IDColumn.ToString(), DbType.Int32, 20);
            _db.AddInParameter(_dbCommand, ds.Attribute.AttributeColumn.ToString(), DbType.String, ds.Attribute.Rows[0][ds.Attribute.AttributeColumn.ToString()]);
            IDataReader dr = _db.ExecuteReader(_dbCommand, _transaction);
            dr.Close();
            _ID = System.Int32.Parse(_db.GetParameterValue(_dbCommand, "@Attribute_ID").ToString());
            return dr;
        }



        //----------------------------------------------------------------
        /// Update: Attribute
        //----------------------------------------------------------------
        public override IDataReader Update(DSParameter ds)
        {
            _dbCommand = _db.GetStoredProcCommand("UpdateAttribute");
            _db.AddInParameter(_dbCommand, ds.Attribute.Attribute_IDColumn.ToString(), DbType.Int32, ds.Attribute.Rows[0][ds.Attribute.Attribute_IDColumn.ToString()]);
            _db.AddInParameter(_dbCommand, ds.Attribute.AttributeColumn.ToString(), DbType.String, ds.Attribute.Rows[0][ds.Attribute.AttributeColumn.ToString()]);
            IDataReader dr = _db.ExecuteReader(_dbCommand, _transaction);
            dr.Close();
            return dr;
        }


    }





}
