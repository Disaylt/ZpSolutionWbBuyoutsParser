using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZpSolutionWbBuyoutsParser.Models.Json;

namespace ZpSolutionWbBuyoutsParser
{
    internal class AccountsStore
    {
        private const string _fileNameSessionList = "sessions.json";
        private readonly WorkSettings _workSettings;
        private readonly ProjectSettingsModel _projectSettings;

        private static AccountsStore _instance;
        private readonly object  _lock = new object();

        private AccountsStore()
        {
            _workSettings = new WorkSettings();
            _projectSettings = _workSettings.GetSettings();
        }

        public static AccountsStore Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AccountsStore();
                }
                return _instance;
            }
        }

        public void SkipOrCreateQueue()
        {
            lock(_lock)
            {
                DateTime lastWorkDate = _projectSettings.LastWorkDate;
                if(lastWorkDate.Date != DateTime.Now.Date)
                {

                }
            }
        }



        private void UpdateWorkDate()
        {
            _projectSettings.LastWorkDate = DateTime.Now;
            _workSettings.UpdateSettings(_projectSettings);
        }
    }
}
