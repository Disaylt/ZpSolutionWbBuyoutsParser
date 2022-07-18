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
        public int NumberDaysForGettingSessions { get; set; } = 7;
        public bool IsParseAllAccounts { get; set; } = false;
        public string PathToZpProfiles { get; set; }

    }
}