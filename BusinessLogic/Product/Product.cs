using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLogic.Product
{
   

      public class Product: BaseParameter, IParameterBL//, IList
    {
        


        #region INonParameterBL Members

        public DataSet.DSParameter New(DataSet.DSParameter ds)
        {
            _is_Single_Transaction = true;
            try
            {

                int product_ID = 0;
                _base = new DataAccessLayer.Product.Product();
                _base.BeginTransaction();
                _base.SetConnection();
                base.baseNew(ds, ds.Product.TableName);
                product_ID  = (int)_base._ID;

                _ID = (int)_base._ID;
                //_base.CommitTransaction();
            }
            catch
            {
                _base.RollBackTransaction();
                throw;
            }

            return this.Get();
        }

        public bool Update(DataSet.DSParameter ds)
        {
            _is_Single_Transaction = false;
            try
            {
                int patient_ID = ds.Product[0].Product_ID;
                
                _base = new DataAccessLayer.Product.Product();
                _base._ID = patient_ID;
                _base.BeginTransaction();
                _base.SetConnection();
                base.baseUpdate(ds, ds.Product.TableName);



                _base.CommitTransaction();
            }
            catch
            {
                return false;
            }
            finally
            {

            }
            return true;
        }

        public bool Delete(DataSet.DSParameter ds)
        {
            _is_Single_Transaction = false;
            try
            {
                int product_ID = ds.Product[0].Product_ID;
                _base = new DataAccessLayer.Product.Product();
                _base.BeginTransaction();
                _base.SetConnection();
                _base._ID = product_ID ;
                base.baseDelete();

                _base.CommitTransaction();
            }
            catch
            {
                return false;
            }
            finally
            {

            }
            return true;
        }

        public DataSet.DSParameter Get()
        {
            DataSet.DSParameter ds = new DataSet.DSParameter();
            //_is_Single_Transaction = false;
            try
            {
                _base = new DataAccessLayer.Product.Product();
                _base.SetConnection();
                _db = _base.GetDatabase();
                _base._ID = _ID;
                ds.Load(_base.GetItem(), LoadOption.OverwriteChanges, ds.Product.TableName);
            }
            catch
            {
                //_base.RollBackTransaction();
                //throw;
            }

            return ds;
        }






        public System.Data.DataSet GetPage(int start, int count, ref int totalCount,
          string product_Type)
        {
            System.Data.DataSet ds = new System.Data.DataSet();
            //_is_Single_Transaction = false;
            try
            {
               DataAccessLayer.Product.Product obj = new DataAccessLayer.Product.Product(start, count, totalCount,
               product_Type);
                obj.SetConnection();
                _db = obj.GetDatabase();
                ds = obj.GetPage();
                totalCount = obj._ID;
            }
            catch
            {
                //_base.RollBackTransaction();
                //throw;
            }

            return ds;
        }


        //public System.Data.DataSet GetProductTypeAttrubuteList(int product_Type_ID)
        //{
        //    System.Data.DataSet ds = new System.Data.DataSet();
        //    //_is_Single_Transaction = false;
        //    try
        //    {
        //        DataAccessLayer.Attribute.Product_Type_Attribute obj = new DataAccessLayer.Attribute.Product_Type_Attribute();
        //        obj.SetConnection();
        //        _db = obj.GetDatabase();
        //        obj._ID = product_Type_ID;

        //        ds.Tables.Add("List");
        //        ds.Load(obj.GetList(), LoadOption.OverwriteChanges, "List");
               
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


      public  bool ProductWebsiteViewStatus()
      {
          DataAccessLayer.Product.Product obj = new DataAccessLayer.Product.Product();
          obj.SetConnection();
          obj.ProductWebsiteViewStatus();   
          return true;
      }


      public bool UpdateProductStoreFromAccPac()
        {
            DataAccessLayer.Product.Product obj = new DataAccessLayer.Product.Product();
            obj.SetConnection();
            obj.UpdateProductStoreFromAccPac();   
            return true;
        }


        //#region IList Members

        //public System.Data.DataSet GetList()
        //{
        //    _base = new DataAccessLayer.Distributor.Distributor();
        //    return base.base.base.baseGetList();
        //}

        //#endregion

        #region IParameterBL Members


        public System.Data.DataSet GetAll()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
