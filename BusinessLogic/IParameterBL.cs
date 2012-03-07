using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataSet;

namespace BusinessLogic
{
    interface IParameterBL
    {
        DSParameter New(DSParameter ds);
        bool Update(DSParameter ds);
        bool Delete(DSParameter ds);
        DSParameter Get();
        System.Data.DataSet GetAll();

    }
}
