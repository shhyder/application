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
    

    //----------------------------------------------------------------
    /// Class: Search
    //----------------------------------------------------------------
    public class Search : Base
    {

        System.Int32 _product_ID;

        public Search(Database db, DbTransaction dbTrans)
        {
            _db = db;
            _transaction = dbTrans;
        }
        public Search(Database db)
        {
            _db = db;
        }
        public Search()
        {

        }
        public Search(System.Int32 product_ID)
        {
            _product_ID = product_ID;
        }

        //----------------------------------------------------------------
        /// Get specific records: Product
        //----------------------------------------------------------------
        public IDataReader GetDealerAndProductCount(decimal startLatitude, decimal startLongitude,int? distance,
            string product_Type_ID, string attribute_ID, string attribute_Variant_ID)
        {

            _dbCommand = _db.GetStoredProcCommand("SearchDealerAndProductCount");
            _db.AddInParameter(_dbCommand, "startLatitude", DbType.Decimal, startLatitude);
            _db.AddInParameter(_dbCommand, "startLongitude", DbType.Decimal, startLongitude);
            _db.AddInParameter(_dbCommand, "distance", DbType.Int32, distance);
            _db.AddInParameter(_dbCommand, "product_Type_ID", DbType.String, product_Type_ID);
            _db.AddInParameter(_dbCommand, "attribute_ID", DbType.String, attribute_ID);
            _db.AddInParameter(_dbCommand, "attribute_Variant_ID", DbType.String, attribute_Variant_ID);
            return _db.ExecuteReader(_dbCommand);
        }




        //----------------------------------------------------------------
        /// Get specific records: ProductDealerCount
        //----------------------------------------------------------------
        public IDataReader GetProductDealerCount(decimal startLatitude, decimal startLongitude, int? distance,
            string product_IDs)
        {

            _dbCommand = _db.GetStoredProcCommand("SearchProductDealerCount");
            _db.AddInParameter(_dbCommand, "startLatitude", DbType.Decimal, startLatitude);
            _db.AddInParameter(_dbCommand, "startLongitude", DbType.Decimal, startLongitude);
            _db.AddInParameter(_dbCommand, "distance", DbType.Int32, distance);
            _db.AddInParameter(_dbCommand, "product_IDs", DbType.String, product_IDs);
            return _db.ExecuteReader(_dbCommand);
        }


        //----------------------------------------------------------------
        /// Get specific records: ProductDealerList
        //----------------------------------------------------------------
        public IDataReader GetProductDealerList(decimal startLatitude, decimal startLongitude, int? distance,
            string product_IDs)
        {

            _dbCommand = _db.GetStoredProcCommand("SearchProductDealerList");
            _db.AddInParameter(_dbCommand, "startLatitude", DbType.Decimal, startLatitude);
            _db.AddInParameter(_dbCommand, "startLongitude", DbType.Decimal, startLongitude);
            _db.AddInParameter(_dbCommand, "distance", DbType.Int32, distance);
            _db.AddInParameter(_dbCommand, "product_IDs", DbType.String, product_IDs);
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




        //----------------------------------------------------------------
        /// Get specific records: DealerList
        //----------------------------------------------------------------
        public IDataReader GetDealerList(decimal startLatitude, decimal startLongitude, int? distance,
            string product_Type_ID, string attribute_ID, string attribute_Variant_ID)
        {

            _dbCommand = _db.GetStoredProcCommand("SearchDealerList");
            _db.AddInParameter(_dbCommand, "startLatitude", DbType.Decimal, startLatitude);
            _db.AddInParameter(_dbCommand, "startLongitude", DbType.Decimal, startLongitude);
            _db.AddInParameter(_dbCommand, "distance", DbType.Int32, distance);
            _db.AddInParameter(_dbCommand, "product_Type_ID", DbType.String, product_Type_ID);
            _db.AddInParameter(_dbCommand, "attribute_ID", DbType.String, attribute_ID);
            _db.AddInParameter(_dbCommand, "attribute_Variant_ID", DbType.String, attribute_Variant_ID);
            return _db.ExecuteReader(_dbCommand);
        }


        //----------------------------------------------------------------
        /// Get specific records: ProductList
        //----------------------------------------------------------------
        public IDataReader GetProductList(decimal startLatitude, decimal startLongitude, int? distance,
            string product_Type_ID, string attribute_ID, string attribute_Variant_ID)
        {

            _dbCommand = _db.GetStoredProcCommand("SearchProductList");
            _db.AddInParameter(_dbCommand, "startLatitude", DbType.Decimal, startLatitude);
            _db.AddInParameter(_dbCommand, "startLongitude", DbType.Decimal, startLongitude);
            _db.AddInParameter(_dbCommand, "distance", DbType.Int32, distance);
            _db.AddInParameter(_dbCommand, "product_Type_ID", DbType.String, product_Type_ID);
            _db.AddInParameter(_dbCommand, "attribute_ID", DbType.String, attribute_ID);
            _db.AddInParameter(_dbCommand, "attribute_Variant_ID", DbType.String, attribute_Variant_ID);
            return _db.ExecuteReader(_dbCommand);
        }










        public override IDataReader Delete()
        {
            throw new NotImplementedException();
        }
    }

}
