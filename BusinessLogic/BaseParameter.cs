using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLogic
{
    public class BaseParameter : Base
    {
        public DataAccessLayer.BaseParameter _base;

        protected DataSet.DSParameter baseNew(DataSet.DSParameter ds, string tableName)
        {
            if (_is_Single_Transaction)
            {
                try
                {
                    _base.BeginTransaction();
                    _base.SetConnection();
                    _db = _base.GetDatabase();
                    ds.Load(_base.Insert(ds), LoadOption.OverwriteChanges, tableName);
                    _base.CommitTransaction();
                }
                catch (Exception exp)
                {
                    _base.RollBackTransaction();
                    throw exp;
                }
                finally
                {

                }
                return ds;
            }


            _base.SetConnection();
            _db = _base.GetDatabase();
            ds.Load(_base.Insert(ds), LoadOption.OverwriteChanges, tableName);
            return ds;

        }

        protected DataSet.DSParameter baseUpdate(DataSet.DSParameter ds, string tableName)
        {
            if (_is_Single_Transaction)
            {
                try
                {
                    _base.BeginTransaction();
                    _base.SetConnection();
                    _db = _base.GetDatabase();
                    ds.Load(_base.Update(ds), LoadOption.OverwriteChanges, tableName);
                    _base.CommitTransaction();
                }
                catch (Exception exp)
                {
                    _base.RollBackTransaction();
                    throw exp;
                }
                finally
                {

                }
                return ds;
            }


            _base.SetConnection();
            _db = _base.GetDatabase();
            ds.Load(_base.Update(ds), LoadOption.OverwriteChanges, tableName);
            return ds;
        }

        protected DataSet.DSParameter baseGet()
        {
            DataSet.DSParameter ds = new DataSet.DSParameter();
            _base.SetConnection();
            _db = _base.GetDatabase();
            ds.Tables.Add(_listTable);
            ds.Load(_base.GetItem(), LoadOption.OverwriteChanges, _listTable);
            return ds;


        }

        protected System.Data.DataSet baseGetAll()
        {
            System.Data.DataSet ds = new System.Data.DataSet();
            _base.SetConnection();
            //_db = _base.GetDatabase();
            ds.Tables.Add(_listTable);
            ds.Load(_base.GetItem(), LoadOption.OverwriteChanges, _listTable);
            return ds;

        }

        protected bool baseDelete()
        {
            _base.SetConnection();
            _base.Delete();
            return true;
        }



    }
}
