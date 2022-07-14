using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZpSolutionWbBuyoutsParser.Models.Json;

namespace ZpSolutionWbBuyoutsParser
{
    internal class AccountsListLoader
    {
        private const string _fileNameSessionList = "sessions.json";
        private readonly WorkSettings _workSettings;
        private readonly ProjectSettingsModel _projectSettings;

        private static AccountsListLoader _instance;
        private readonly object  _lock = new object();

        private AccountsListLoader()
        {
            _workSettings = new WorkSettings();
            _projectSettings = _workSettings.GetSettings();
        }

        public static AccountsListLoader Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AccountsListLoader();
                }
                return _instance;
            }
        }

        public void SkipOrFillSessionList()
        {
            lock(_lock)
            {
                DateTime lastWorkDate = _projectSettings.LastWorkDate;
                if(lastWorkDate.Date != DateTime.Now.Date)
                {
                    _projectSettings.LastWorkDate = DateTime.Now;
                    _workSettings.UpdateSettings(_projectSettings);
                }
            }
        }
    }
}
