using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZpSolutionWbBuyoutsParser.Models.Json
{
    internal class ProjectSettingsModel
    {
        public DateTime LastWorkDate { get; set; }
        public int AmountOfDaysForParsing { get; set; } = 7;
        public bool UseDaysFilter { get; set; } = true;

    }
}
