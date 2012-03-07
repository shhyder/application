using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLogic.Tracking
{
   

    public class Activity : BaseNonParameter//--, INonParameterBL//, IList
    {



        #region INonParameterBL Members

        public DataSet.DS New(DataSet.DS ds)
        {
            _is_Single_Transaction = true;
            try
            {

                
                _base = new DataAccessLayer.Activity.Activity();
                _base.BeginTransaction();
                _base.SetConnection();
                _base.Insert(ds);
                
                _base.CommitTransaction();
            }
            catch
            {
                _base.RollBackTransaction();
                throw;
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
