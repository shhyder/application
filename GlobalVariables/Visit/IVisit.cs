using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlobalVariables.Visit
{
    public interface IVisit
    {
        
        int start
            {
                get;
                set;
            }

        int count
            {
                get;
                set;
            }

        int totalCount
            {
                get;
                set;
            }


        string start_Date
        {
            get;
            set;
        }

        string end_Date
        {
            get;
            set;
        }

        string City
        {
            get;
            set;
        }

        string State
        {
            get;
            set;
        }


        string Country
        {
            get;
            set;
        }
    }
}
