using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace BusinessLogic.Search
{
    public class Event : BaseParameter
    {
        public System.Data.DataSet GetEventList(string zipcode, int? distance,
        ref decimal startLatitude, ref decimal startLongitude, ref bool is_Valid_Postal_Code, string start_Date, string end_Date)
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
                DataAccessLayer.Search.Event obj = new DataAccessLayer.Search.Event();
                obj.SetConnection();
                _db = obj.GetDatabase();
                IDataReader dr = obj.GetEventList(startLatitude, startLongitude, distance, start_Date, end_Date);
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

    }
}