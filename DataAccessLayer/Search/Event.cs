using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DataSet;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using System.Data.SqlTypes;
using System.Data.Common;

namespace DataAccessLayer.Search
{
    public class Event : Base
    {
        System.Int32 _product_ID;

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
        public Event(System.Int32 product_ID)
        {
            _product_ID = product_ID;
        }


        //----------------------------------------------------------------
        /// Get specific records: EventList
        //----------------------------------------------------------------
        public IDataReader GetEventList(decimal startLatitude, decimal startLongitude, int? distance,
            string start_Date, string end_Date)
        {

            _dbCommand = _db.GetStoredProcCommand("SearchEventList");
            _db.AddInParameter(_dbCommand, "startLatitude", DbType.Decimal, startLatitude);
            _db.AddInParameter(_dbCommand, "startLongitude", DbType.Decimal, startLongitude);
            _db.AddInParameter(_dbCommand, "distance", DbType.Int32, distance);
            _db.AddInParameter(_dbCommand, "start_Date", DbType.String, start_Date);
            _db.AddInParameter(_dbCommand, "end_Date", DbType.String, end_Date);
            return _db.ExecuteReader(_dbCommand);
        }


        //----------------------------------------------------------------
        /// Get specific records: ZipCodes
        //----------------------------------------------------------------
        public override IDataReader GetItem()
        {

            _dbCommand = _db.GetStoredProcCommand("GetZipCodes");
            //_db.AddInParameter(_dbCommand, _DSParam.ZipCodes.ZipCodeColumn.ToString(), DbType.String, _zipcodes_ID);
            return _db.ExecuteReader(_dbCommand);
        }

        public override IDataReader Delete()
        {
            throw new NotImplementedException();
        }



    }
}
