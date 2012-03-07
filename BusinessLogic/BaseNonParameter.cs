using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLogic
{
    public class BaseNonParameter : Base
    {
        protected DataAccessLayer.BaseNonParameter _base;

        protected DataSet.DS baseNew(DataSet.DS ds, string tableName)
        {
            _base.SetConnection();
            _db = _base.GetDatabase();
            ds.Load(_base.Insert(ds), LoadOption.OverwriteChanges, tableName);
            return ds;


        }

        protected DataSet.DS baseUpdate(DataSet.DS ds, string tableName)
        {
            _base.SetConnection();
            _db = _base.GetDatabase();
            ds.Load(_base.Update(ds), LoadOption.OverwriteChanges, tableName);
            return ds;


        }

        protected DataSet.DS baseGet()
        {
            DataSet.DS ds = new DataSet.DS();
            _base.SetConnection();
            _db = _base.GetDatabase();
            ds.Tables.Add(_listTable);
            ds.Load(_base.GetItem(), LoadOption.OverwriteChanges, _listTable);
            return ds;


        }

        protected System.Data.DataSet baseGetList()
        {
            System.Data.DataSet ds = new System.Data.DataSet();
            _base.SetConnection();
            //_db = _base.GetDatabase();
            ds.Tables.Add(_listTable);
            ds.Load(_base.GetList(), LoadOption.OverwriteChanges, _listTable);
            return ds;

        }

        protected bool baseDelete(DataSet.DS ds)
        {
            _base.SetConnection();
            _base.Delete();
            return true;
        }

    }
}
