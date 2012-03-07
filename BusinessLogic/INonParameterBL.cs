using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataSet;

namespace BusinessLogic
{
    interface INonParameterBL
    {
        DS New(DS ds);
        bool Update(DS ds);
        bool Delete(DS ds);
        DS Get();

    }
}
