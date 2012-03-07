using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLogic.Search
{
   

    public class Search : BaseParameter//, IParameterBL//, IList
    {
        #region INonParameterBL Members

        

        public DataSet.DSParameter Get()
        {
            DataSet.DSParameter ds = new DataSet.DSParameter();
            //_is_Single_Transaction = false;
            try
            {
                _base = new DataAccessLayer.Distributor.Distributor();
                _base.SetConnection();
                _db = _base.GetDatabase();
                _base._ID = _ID;
                ds.Load(_base.GetItem(), LoadOption.OverwriteChanges, ds.Distributor.TableName);
            }
            catch
            {
                //_base.RollBackTransaction();
                //throw;
            }

            return ds;
        }


        public bool ValidateZipcode(string zipcode)
        {
            System.Data.DataSet ds = new System.Data.DataSet();
            //_is_Single_Transaction = false;
            bool is_Valid = false;
            try
            {
                DataAccessLayer.Parameter.ZipCodes obj = new DataAccessLayer.Parameter.ZipCodes();
                obj.SetConnection();
                _db = obj.GetDatabase();
                obj._zipcodes_ID = zipcode;

                is_Valid =  obj.Validate() ;


            }
            catch
            {
                //_base.RollBackTransaction();
                //throw;
            }

            return is_Valid;
        }


        //public void GetDealerAndProductCount( string zipcode, int? distance,
        //    string product_Type_ID, string attribute_ID, string attribute_Variant_ID,
        //    ref int total_Dealers,ref int total_Products )
        //{
        //    decimal startLatitude;
        //    decimal startLongitude;


        //    DataAccessLayer.Parameter.ZipCodes obj2 = new DataAccessLayer.Parameter.ZipCodes();
        //    obj2.SetConnection();
        //    _db = obj2.GetDatabase();
        //    obj2._zipcodes_ID = zipcode;

        //    DataSet.DSParameter ds = new DataSet.DSParameter();
        //    ds.Load(obj2.GetItem(), LoadOption.OverwriteChanges, ds.ZipCodes.TableName);
        //    startLatitude = ds.ZipCodes[0].Latitude;
        //    startLongitude = ds.ZipCodes[0].Longitude;


        //    //_is_Single_Transaction = false;
        //    bool is_Valid = false;
        //    try
        //    {
        //        DataAccessLayer.Search.Search obj = new DataAccessLayer.Search.Search();
        //        obj.SetConnection();
        //        _db = obj.GetDatabase();
        //        IDataReader dr = obj.GetDealerAndProductCount(startLatitude, startLongitude, distance, product_Type_ID, attribute_ID, attribute_Variant_ID);
        //        //System.Data.DataSet __ds = new System.Data.DataSet();
        //        //__ds.Load(dr, LoadOption.OverwriteChanges, "List");
        //        dr.Read();
        //        total_Dealers = dr.GetInt32(0);
        //        total_Products = dr.GetInt32(1);

              

        //    }
        //    catch
        //    {
        //        //_base.RollBackTransaction();
        //        //throw;
        //    }

        //    return;
        //}




        public int GetProductDealerCount(string zipcode, int? distance,
            string product_IDs)
        {
            decimal startLatitude;
            decimal startLongitude;


            DataAccessLayer.Parameter.ZipCodes obj2 = new DataAccessLayer.Parameter.ZipCodes();
            obj2.SetConnection();
            _db = obj2.GetDatabase();
            obj2._zipcodes_ID = zipcode;

            DataSet.DSParameter ds = new DataSet.DSParameter();
            ds.Load(obj2.GetItem(), LoadOption.OverwriteChanges, ds.ZipCodes.TableName);
            startLatitude = ds.ZipCodes[0].Latitude;
            startLongitude = ds.ZipCodes[0].Longitude;


            //_is_Single_Transaction = false;
            bool is_Valid = false;
            try
            {
                DataAccessLayer.Search.Search obj = new DataAccessLayer.Search.Search();
                obj.SetConnection();
                _db = obj.GetDatabase();
                IDataReader dr = obj.GetProductDealerCount(startLatitude, startLongitude, distance, product_IDs);
                //System.Data.DataSet __ds = new System.Data.DataSet();
                //__ds.Load(dr, LoadOption.OverwriteChanges, "List");
                dr.Read();
                return dr.GetInt32(0);

            }
            catch
            {
                //_base.RollBackTransaction();
                //throw;
            }

            return 0 ;
        }




        public System.Data.DataSet GetProductDealerList(string zipcode, int? distance,
         string product_IDs, ref decimal startLatitude, ref decimal startLongitude,ref bool is_Valid_Postal_Code)
        {
           


            DataAccessLayer.Parameter.ZipCodes obj2 = new DataAccessLayer.Parameter.ZipCodes();
            obj2.SetConnection();
            _db = obj2.GetDatabase();
            obj2._zipcodes_ID = zipcode;

            DataSet.DSParameter ds = new DataSet.DSParameter();
            ds.Load(obj2.GetItem(), LoadOption.OverwriteChanges, ds.ZipCodes.TableName);

            System.Data.DataSet __ds = new System.Data.DataSet();

            if (ds.ZipCodes.Count == 0)
            {
                __ds.Tables.Add("List");
                is_Valid_Postal_Code = false;
                return __ds; 
            }


            startLatitude = ds.ZipCodes[0].Latitude;
            startLongitude = ds.ZipCodes[0].Longitude;

            
            //_is_Single_Transaction = false;
            //bool is_Valid = false;
            try
            {
                DataAccessLayer.Search.Search obj = new DataAccessLayer.Search.Search();
                obj.SetConnection();
                _db = obj.GetDatabase();
                IDataReader dr = obj.GetProductDealerList(startLatitude, startLongitude, distance, product_IDs);
                __ds.Load(dr, LoadOption.OverwriteChanges, "List");

            }
            catch
            {
                //_base.RollBackTransaction();
                //throw;
            }

            is_Valid_Postal_Code = true;
            return __ds;
        }





        public System.Data.DataSet GetDealerList(string zipcode, int? distance,
          string product_Type_ID, string attribute_ID, string attribute_Variant_ID)
        {
            decimal startLatitude;
            decimal startLongitude;


            DataAccessLayer.Parameter.ZipCodes obj2 = new DataAccessLayer.Parameter.ZipCodes();
            obj2.SetConnection();
            _db = obj2.GetDatabase();
            obj2._zipcodes_ID = zipcode;

            DataSet.DSParameter ds = new DataSet.DSParameter();
            ds.Load(obj2.GetItem(), LoadOption.OverwriteChanges, ds.ZipCodes.TableName);
            startLatitude = ds.ZipCodes[0].Latitude;
            startLongitude = ds.ZipCodes[0].Longitude;

            System.Data.DataSet __ds = new System.Data.DataSet();
            //_is_Single_Transaction = false;
            bool is_Valid = false;
            try
            {
                DataAccessLayer.Search.Search obj = new DataAccessLayer.Search.Search();
                obj.SetConnection();
                _db = obj.GetDatabase();
                IDataReader dr = obj.GetDealerList(startLatitude, startLongitude, distance, product_Type_ID, attribute_ID, attribute_Variant_ID);
                
                __ds.Load(dr, LoadOption.OverwriteChanges, "List");
               



            }
            catch
            {
                //_base.RollBackTransaction();
                //throw;
            }

            return __ds;
        }




        public System.Data.DataSet GetProductList(string zipcode, int? distance,
         string product_Type_ID, string attribute_ID, string attribute_Variant_ID)
        {
            decimal startLatitude;
            decimal startLongitude;


            DataAccessLayer.Parameter.ZipCodes obj2 = new DataAccessLayer.Parameter.ZipCodes();
            obj2.SetConnection();
            _db = obj2.GetDatabase();
            obj2._zipcodes_ID = zipcode;

            DataSet.DSParameter ds = new DataSet.DSParameter();
            ds.Load(obj2.GetItem(), LoadOption.OverwriteChanges, ds.ZipCodes.TableName);
            startLatitude = ds.ZipCodes[0].Latitude;
            startLongitude = ds.ZipCodes[0].Longitude;

            System.Data.DataSet __ds = new System.Data.DataSet();
            //_is_Single_Transaction = false;
            bool is_Valid = false;
            try
            {
                DataAccessLayer.Search.Search obj = new DataAccessLayer.Search.Search();
                obj.SetConnection();
                _db = obj.GetDatabase();
                IDataReader dr = obj.GetProductList(startLatitude, startLongitude, distance, product_Type_ID, attribute_ID, attribute_Variant_ID);

                __ds.Load(dr, LoadOption.OverwriteChanges, "List");




            }
            catch
            {
                //_base.RollBackTransaction();
                //throw;
            }

            return __ds;
        }






        public System.Data.DataSet GetSearchCritariaBy(int category_ID)
        {
            System.Data.DataSet ds = new System.Data.DataSet();
            //_is_Single_Transaction = false;
            try
            {
                DataAccessLayer.Attribute.Product_Type_Attribute obj = new DataAccessLayer.Attribute.Product_Type_Attribute();
                obj.SetConnection();
                _db = obj.GetDatabase();
                obj._ID = category_ID;

                ds.Tables.Add("List");
                ds.Load(obj.GetSearchList(), LoadOption.OverwriteChanges, "List");

            }
            catch
            {
                //_base.RollBackTransaction();
                //throw;
            }

            return ds;
        }







        //public System.Data.DataSet GetPage(int start, int count, ref int totalCount,
        //   string Distributor_No, string customer, string address, string email,
        //   string phone, string country, string state, string from_Date,
        //   string to_Date)
        //{
        //    System.Data.DataSet ds = new System.Data.DataSet();
        //    //_is_Single_Transaction = false;
        //    try
        //    {
        //        DataAccessLayer.Distributor.Distributor obj = new DataAccessLayer.Distributor.Distributor(start, count, totalCount,
        //       Distributor_No, customer, address, email, phone, country, state, from_Date, to_Date);
        //        obj.SetConnection();
        //        _db = obj.GetDatabase();
        //        ds = obj.GetPage();
        //        totalCount = obj._ID;
        //    }
        //    catch
        //    {
        //        //_base.RollBackTransaction();
        //        //throw;
        //    }

        //    return ds;
        //}

        //public System.Data.DataSet GetPage(int start, int count, ref int totalCount,
        //    string UCI, int? company, string first_Name,
        //    string middle_Name, string last_Naemm, string phone_1,
        //    string phone_2, string from_Date, string to_Date)
        //{
        //    System.Data.DataSet ds = new System.Data.DataSet();
        //    //_is_Single_Transaction = false;
        //    try
        //    {
        //        DataAccessLayer.Consumer.Consumer cons = new DataAccessLayer.Consumer.Consumer(start, count, totalCount,
        //            UCI, company, first_Name, middle_Name, last_Naemm, phone_1, phone_2, from_Date, to_Date);
        //        cons.SetConnection();
        //        _db = cons.GetDatabase();
        //        ds = cons.GetPage();
        //        totalCount = cons._ID;
        //    }
        //    catch
        //    {
        //        //_base.RollBackTransaction();
        //        //throw;
        //    }

        //    return ds;
        //}



        //public DataSet.DSParameter GetCaseByConsumer(int consumer_ID)
        //{
        //    DataSet.DSParameter ds = new DataSet.DSParameter();
        //    //_is_Single_Transaction = false;
        //    try
        //    {
        //        DataAccessLayer.Consumer.Case Cons = new DataAccessLayer.Consumer.Case();
        //        Cons.SetConnection();
        //        _db = Cons.GetDatabase();
        //        Cons._ID = consumer_ID;
        //        ds.Load(Cons.GetItemByConsumer(), LoadOption.OverwriteChanges, ds.Case.TableName);
        //    }
        //    catch
        //    {
        //        //_base.RollBackTransaction();
        //        //throw;
        //    }

        //    return ds;
        //}


        #endregion

       

        #region IParameterBL Members


        public System.Data.DataSet GetAll()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
