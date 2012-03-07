using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DataSet;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using System.Data.SqlTypes;
using System.Data.Common;

namespace DataAccessLayer.Event
{
    //----------------------------------------------------------------
    /// Class: Event_Item
    //----------------------------------------------------------------
    public class Event_Item : BaseParameter
    {

        System.Int32 _event_item_ID;

        public Event_Item(Database db, DbTransaction dbTrans)
        {
            _db = db;
            _transaction = dbTrans;
        }
        public Event_Item(Database db)
        {
            _db = db;
        }
        public Event_Item()
        {

        }
        public Event_Item(System.Int32 event_item_ID)
        {
            _event_item_ID = event_item_ID;
        }

        //----------------------------------------------------------------
        /// Get specific records: Event_Item
        //----------------------------------------------------------------
        public override IDataReader GetItem()
        {

            _dbCommand = _db.GetStoredProcCommand("GetEvent_Item");
            _db.AddInParameter(_dbCommand, _DSParam.Event_Item.Event_Item_IDColumn.ToString(), DbType.Int32, _event_item_ID);
            return _db.ExecuteReader(_dbCommand);
        }



        //----------------------------------------------------------------
        /// Get all records: Event_Item
        //----------------------------------------------------------------

        public override IDataReader GetAllItem()
        {

            _dbCommand = _db.GetStoredProcCommand("GetAllEvent_Item");
            return _db.ExecuteReader(_dbCommand);
        }



        //----------------------------------------------------------------
        /// Delete: Event_Item
        //----------------------------------------------------------------
        public override IDataReader Delete()
        {

            _dbCommand = _db.GetStoredProcCommand("GetEvent_Item");
            _db.AddInParameter(_dbCommand, _DSParam.Event_Item.Event_Item_IDColumn.ToString(), DbType.Int32, _event_item_ID);
            return _db.ExecuteReader(_dbCommand);
        }



        //----------------------------------------------------------------
        /// Insert record: Event_Item
        //----------------------------------------------------------------
        public override IDataReader Insert(DSParameter ds)
        {
            _dbCommand = _db.GetStoredProcCommand("InsertEvent_Item");
            _db.AddInParameter(_dbCommand, ds.Event_Item.Event_Item_IDColumn.ToString(), DbType.Int32, ds.Event_Item.Rows[0][ds.Event_Item.Event_Item_IDColumn.ToString()]);
            _db.AddInParameter(_dbCommand, ds.Event_Item.Event_IDColumn.ToString(), DbType.Int32, ds.Event_Item.Rows[0][ds.Event_Item.Event_IDColumn.ToString()]);
            _db.AddInParameter(_dbCommand, ds.Event_Item.Event_ItemColumn.ToString(), DbType.String, ds.Event_Item.Rows[0][ds.Event_Item.Event_ItemColumn.ToString()]);
            IDataReader dr = _db.ExecuteReader(_dbCommand, _transaction);
            dr.Close();
            return dr;
        }



        //----------------------------------------------------------------
        /// Update: Event_Item
        //----------------------------------------------------------------
        public override IDataReader Update(DSParameter ds)
        {
            _dbCommand = _db.GetStoredProcCommand("UpdateEvent_Item");
            _db.AddInParameter(_dbCommand, ds.Event_Item.Event_Item_IDColumn.ToString(), DbType.Int32, ds.Event_Item.Rows[0][ds.Event_Item.Event_Item_IDColumn.ToString()]);
            _db.AddInParameter(_dbCommand, ds.Event_Item.Event_IDColumn.ToString(), DbType.Int32, ds.Event_Item.Rows[0][ds.Event_Item.Event_IDColumn.ToString()]);
            _db.AddInParameter(_dbCommand, ds.Event_Item.Event_ItemColumn.ToString(), DbType.String, ds.Event_Item.Rows[0][ds.Event_Item.Event_ItemColumn.ToString()]);
            IDataReader dr = _db.ExecuteReader(_dbCommand, _transaction);
            dr.Close();
            return dr;
        }


    }





}
