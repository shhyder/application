using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data;
using System.Collections.Specialized;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using DataSet;

namespace DataAccessLayer
{
    public abstract class Base : IDisposable
    {

        public Database _db;
        protected DbCommand _dbCommand;
        protected DbConnection _connection;
        public DbTransaction _transaction;
        public string _strID;
        public int _ID;
        public int _location_ID;
        //public GlobalVariables.IMain _iMain;



        public Base(DbTransaction transaction)
            : this()
        {
            _transaction = transaction;
        }

        protected Base()
            : base()
        {


        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(true);
            // as a service to those who might inherit from us
        }

        public DbTransaction BeginTransaction()
        {
            _db = DatabaseFactory.CreateDatabase();
            _connection = _db.CreateConnection();
            _connection.Open();
            _transaction = _connection.BeginTransaction();
            return _transaction;
        }

        public void SetConnection()
        {
            _db = DatabaseFactory.CreateDatabase();
            _connection = _db.CreateConnection();
        }

        public Database GetDatabase()
        {
            return _db;
        }

        public void CommitTransaction()
        {

            _transaction.Commit();
        }

        public void RollBackTransaction()
        {


            _transaction.Rollback();
        }

        protected virtual void Dispose(bool disposing)
        {
            if ((!disposing))
            {
                return;
                // we're being collected, so let the GC take care of this object
            }

        }


        public abstract IDataReader Delete();

        //public abstract DataSet List();

        public abstract IDataReader GetItem();





    }
}
