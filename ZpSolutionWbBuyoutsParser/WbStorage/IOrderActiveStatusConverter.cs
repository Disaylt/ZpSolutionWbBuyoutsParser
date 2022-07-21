using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZpSolutionWbBuyoutsParser.WbStorage
{
    internal interface IOrderActiveStatusConverter
    {
        string GetDbFormatStatus(bool orderStatus);
    }
}
