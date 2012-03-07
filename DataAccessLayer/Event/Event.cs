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
    /// Class: Event
    //----------------------------------------------------------------
    public class Event : BaseParameter
    {

        System.Int32 _event_ID;

        public Event(Database db, DbTransaction dbTrans)
        {
            _db = db;
            _transaction = dbTrans;
        }
        public Event(Database db)
        {
            _db = db;
        }
        public Event()
        {

        }
        public Event(System.Int32 event_ID)
        {
            _event_ID = event_ID;
        }

        //----------------------------------------------------------------
        /// Get specific records: Event
        //----------------------------------------------------------------
        public override IDataReader GetItem()
        {

            _dbCommand = _db.GetStoredProcCommand("GetEvent");
            _db.AddInParameter(_dbCommand, _DSParam.Event.Event_IDColumn.ToString(), DbType.Int32, _event_ID);
            return _db.ExecuteReader(_dbCommand);
        }



        //----------------------------------------------------------------
        /// Get all records: Event
        //----------------------------------------------------------------

        public override IDataReader GetAllItem()
        {

            _dbCommand = _db.GetStoredProcCommand("GetAllEvent");
            return _db.ExecuteReader(_dbCommand);
        }




        //----------------------------------------------------------------
        /// Delete: Event
        //----------------------------------------------------------------
        public override IDataReader Delete()
        {

            _dbCommand = _db.GetStoredProcCommand("GetEvent");
            _db.AddInParameter(_dbCommand, _DSParam.Event.Event_IDColumn.ToString(), DbType.Int32, _event_ID);
            return _db.ExecuteReader(_dbCommand);
        }



        //----------------------------------------------------------------
        /// Insert record: Event
        //----------------------------------------------------------------
        public override IDataReader Insert(DSParameter ds)
        {
            _dbCommand = _db.GetStoredProcCommand("InsertEvent");
            _db.AddOutParameter(_dbCommand, ds.Event.Event_IDColumn.ToString(), DbType.Int32, 20);
            _db.AddInParameter(_dbCommand, ds.Event.EventColumn.ToString(), DbType.String, ds.Event.Rows[0][ds.Event.EventColumn.ToString()]);
            _db.AddInParameter(_dbCommand, ds.Event.Has_Event_ItemColumn.ToString(), DbType.Boolean, ds.Event.Rows[0][ds.Event.Has_Event_ItemColumn.ToString()]);
            _db.AddInParameter(_dbCommand, ds.Event.DateColumn.ToString(), DbType.DateTime, ds.Event.Rows[0][ds.Event.DateColumn.ToString()]);
            _db.AddInParameter(_dbCommand, ds.Event.LatitudeColumn.ToString(), DbType.Decimal, ds.Event.Rows[0][ds.Event.LatitudeColumn.ToString()]);
            _db.AddInParameter(_dbCommand, ds.Event.LongitudeColumn.ToString(), DbType.Decimal, ds.Event.Rows[0][ds.Event.LongitudeColumn.ToString()]);
            _db.AddInParameter(_dbCommand, ds.Event.Company_IDColumn.ToString(), DbType.Int32, ds.Event.Rows[0][ds.Event.Company_IDColumn.ToString()]);
            _db.AddInParameter(_dbCommand, ds.Event.Event_DaysColumn.ToString(), DbType.Int32, ds.Event.Rows[0][ds.Event.Event_DaysColumn.ToString()]);
            IDataReader dr = _db.ExecuteReader(_dbCommand, _transaction);
            dr.Close();
            _ID = Int32.Parse(_db.GetParameterValue(_dbCommand, "@Event_ID").ToString());
            return dr;
        }



        //----------------------------------------------------------------
        /// Update: Event
        //----------------------------------------------------------------
        public override IDataReader Update(DSParameter ds)
        {
            _dbCommand = _db.GetStoredProcCommand("UpdateEvent");
            _db.AddInParameter(_dbCommand, ds.Event.Event_IDColumn.ToString(), DbType.Int32, ds.Event.Rows[0][ds.Event.Event_IDColumn.ToString()]);
            _db.AddInParameter(_dbCommand, ds.Event.EventColumn.ToString(), DbType.String, ds.Event.Rows[0][ds.Event.EventColumn.ToString()]);
            _db.AddInParameter(_dbCommand, ds.Event.Has_Event_ItemColumn.ToString(), DbType.Boolean, ds.Event.Rows[0][ds.Event.Has_Event_ItemColumn.ToString()]);
            _db.AddInParameter(_dbCommand, ds.Event.DateColumn.ToString(), DbType.DateTime, ds.Event.Rows[0][ds.Event.DateColumn.ToString()]);
            _db.AddInParameter(_dbCommand, ds.Event.LatitudeColumn.ToString(), DbType.Decimal, ds.Event.Rows[0][ds.Event.LatitudeColumn.ToString()]);
            _db.AddInParameter(_dbCommand, ds.Event.LongitudeColumn.ToString(), DbType.Decimal, ds.Event.Rows[0][ds.Event.LongitudeColumn.ToString()]);
            _db.AddInParameter(_dbCommand, ds.Event.Company_IDColumn.ToString(), DbType.Int32, ds.Event.Rows[0][ds.Event.Company_IDColumn.ToString()]);
            _db.AddInParameter(_dbCommand, ds.Event.Event_DaysColumn.ToString(), DbType.Int32, ds.Event.Rows[0][ds.Event.Event_DaysColumn.ToString()]);
            IDataReader dr = _db.ExecuteReader(_dbCommand, _transaction);
            dr.Close();
            return dr;
        }


    }




}