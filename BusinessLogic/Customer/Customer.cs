using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLogic.Customer
{
    public class Customer : BaseParameter, IParameterBL//, IList
    {
        #region IParameterBL Members

        public DataSet.DSParameter New(DataSet.DSParameter ds)
        {
            throw new NotImplementedException();
        }

        public bool Update(DataSet.DSParameter ds)
        {
            throw new NotImplementedException();
        }

        public bool Delete(DataSet.DSParameter ds)
        {
            throw new NotImplementedException();
        }

        public DataSet.DSParameter Get()
        {
            throw new NotImplementedException();
        }

        public System.Data.DataSet GetAll()
        {
            throw new NotImplementedException();
        }

        #endregion

        public bool SetCustomerStatus(string id,bool is_Active)
        {
           
            _is_Single_Transaction = false;
            //DataAccessLayer.Customer.Customer cust = new DataAccessLayer.Customer.Customer();

            //try
            //{
               
            //    cust.BeginTransaction();
            //    cust.SetConnection();
            //    cust.setStatus(id, is_Active);


            //    cust.CommitTransaction();
            //}
            //catch
            //{
            //    cust.RollBackTransaction();
            //    return false;
            //}
            //finally
            //{

            //}
            return true;



        }
    }
}
