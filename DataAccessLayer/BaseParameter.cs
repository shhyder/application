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
    public abstract class BaseParameter : Base
    {
        protected DSParameter _DSParam;

        protected BaseParameter()
            : base()
        {
            _DSParam = new DSParameter();

        }



        public abstract IDataReader Insert(DSParameter ds);

        public abstract IDataReader Update(DSParameter ds);

        public abstract IDataReader GetAllItem();
    }
}
