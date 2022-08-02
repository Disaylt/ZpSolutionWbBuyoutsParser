using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZpSolutionWbBuyoutsParser.Models.Standard;

namespace ZpSolutionWbBuyoutsParser.WbStorage
{
    internal interface IRidConverter<out T>
    {
        T ConvertToDifferentTypes();
    }
}
