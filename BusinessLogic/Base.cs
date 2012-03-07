using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLayer;
using System.Collections.Specialized;
using Microsoft.Practices.EnterpriseLibrary.Data;


namespace BusinessLogic
{
    public class Base
    {
        public Database _db;
        protected string _listTable = "List";
        protected bool _is_Single_Transaction = true;
        public int _ID;
        //public GlobalVariables.IMain _iMain;
        public int _location_ID = 0;

        public enum Tables
        {
            List,
            ItemLineDetail,
        }
    }
}
