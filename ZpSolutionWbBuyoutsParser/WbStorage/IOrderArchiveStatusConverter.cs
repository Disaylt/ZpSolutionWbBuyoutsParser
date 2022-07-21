using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZpSolutionWbBuyoutsParser.WbStorage
{
    internal interface IOrderArchiveStatusConverter
    {
        string GetDbFormatStatus(string orderStatus);
    }
}
