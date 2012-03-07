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
    public abstract class BaseNonParameter : Base
    {
        protected DS _DS;
        protected int _index = 0;


        protected BaseNonParameter()
            : base()
        {
            _DS = new DS();

        }

        public abstract IDataReader Insert(DS ds);

        public abstract IDataReader Update(DS ds);

        public abstract IDataReader GetList();

    }
}
