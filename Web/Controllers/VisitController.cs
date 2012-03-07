using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;

namespace Web.Controllers
{
    public class VisitController : _BaseController, GlobalVariables.Visit.IVisit
    {
        //
        // GET: /Visit/

        public ActionResult Visits()
        {

            int pageSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PageSize"].ToString());
            int pageCalled = 1;


            LoadVisitList(new FormCollection(), pageSize, pageCalled);

            return SetView("Search");
        }


        [NonAction]
        private void LoadVisitList(FormCollection collection, int pageSize, int pageCalled)
        {
            int _start;
            int _count;
            int _totalCount = 0;
            string _UCI;
            int? _company;
            string _first_Name;
            string _middle_Name;
            string _last_Name;
            string _phone_1;
            string _phone_2;
            string _from_Date;
            string _to_Date;


            _UCI = collection["txtUCI"];
            if (collection["cobCompany"] == null)
            {
                _company = null;
            }
            else
            {
                _company = null;// Convert.ToInt32(collection["cobCompany"].ToString());
            }

            _first_Name = collection["txtFirst_Name"];
            _middle_Name = collection["txtMiddle_Name"];
            _last_Name = collection["txtLast_Name"];
            _phone_1 = collection["txtPhone_1"];
            _phone_2 = collection["txtPhone_2"];
            _from_Date = collection["txtFrom_Date"];
            _to_Date = collection["txtTo_Date"];


            if (_UCI != null)
            {
                _UCI = _UCI.Trim().Length == 0 ? null : _UCI;
            }



            if (_first_Name != null)
                _first_Name = _first_Name.Trim().Length == 0 ? null : _first_Name;

            if (_middle_Name != null)
                _middle_Name = _middle_Name.Trim().Length == 0 ? null : _middle_Name;

            if (_last_Name != null)
                _last_Name = _last_Name.Trim().Length == 0 ? null : _last_Name;

            if (_phone_1 != null)
                _phone_1 = _phone_1.Trim().Length == 0 ? null : _phone_1;

            if (_phone_2 != null)
                _phone_2 = _phone_2.Trim().Length == 0 ? null : _phone_2;

            if (_from_Date != null)
                _from_Date = _from_Date.Trim().Length == 0 ? null : _from_Date;

            if (_to_Date != null)
                _to_Date = _to_Date.Trim().Length == 0 ? null : _to_Date;


            GlobalVariables.Visit.IVisit iv= this;

            iv.count = pageSize;
            iv.totalCount= _totalCount;
            iv.start= pageCalled;

            iv.City = collection["txtTo_Date"];;
            iv.Country= collection["txtTo_Date"];
            iv.start_Date= collection["txtTo_Date"];
            iv.end_Date= collection["txtTo_Date"];
            iv.State= collection["txtTo_Date"];
            
            

            BusinessLogic.Tracking.Visit con = new BusinessLogic.Tracking.Visit(iv);
            System.Data.DataSet ds = con.GetPage(ref _totalCount);








            try
            {

                //---------------------------------------------- 
                //Fill Custom Data 
                string temp = "";




                int totalItems = _totalCount;

                Web.Model.Pagination pagination = new Web.Model.Pagination(true);

                pagination.BaseUrl = Url.Content("~/Consumer/List/");
                pagination.TotalRows = totalItems;
                pagination.CurPage = pageCalled;
                pagination.PerPage = pageSize;

                pagination.PrevLink = "Prev";
                pagination.NextLink = "Next";
                pagination.UpdateTargetId = "mainSearchPanel";

                string pageLinks = pagination.GetPageLinks();
                int start = (pageCalled - 1) * pagination.PerPage;
                int offset = pagination.PerPage;
                ViewData["pageLinks"] = pageLinks;

                ViewData["Count"] = totalItems;

                ViewData["start"] = (start + 1).ToString();

                IQueryable<DataRow> dt = ds.Tables["List"].Rows.Cast<DataRow>().AsQueryable();
                ViewData["ConsumerList"] = dt.ToList();

            }
            catch (Exception e)
            {

            }
        }





        #region IVisit Members

        public int start
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public int count
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public int totalCount
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string start_Date
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string end_Date
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string City
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string State
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string Country
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion
    }
}
