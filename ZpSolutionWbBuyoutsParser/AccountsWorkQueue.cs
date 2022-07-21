using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZpSolutionWbBuyoutsParser.Models.Json;
using ZpSolutionWbBuyoutsParser.Mongo;

namespace ZpSolutionWbBuyoutsParser
{
    internal class AccountsWorkQueue
    {
        private List<string> _sessions { get; set; }

        private const string _fileNameSessionList = "sessions.json";
        private readonly WorkSettings _workSettings;
        private readonly ProjectSettingsModel _projectSettings;

        private static AccountsWorkQueue _instance;
        private readonly object  _lock = new object();

        private AccountsWorkQueue()
        {
            _workSettings = new WorkSettings();
            _projectSettings = _workSettings.GetSettings();
            _sessions = LoadFromJsonFile();
        }

        public static AccountsWorkQueue Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AccountsWorkQueue();
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
                    _sessions = CreateQueueSessions();
                    JsonFile.Save($"{ProjectConfig.GetInstance().ProjectPath}{_fileNameSessionList}", _sessions);
                    UpdateWorkDate();
                }
            }
        }

        public string TakeSession()
        {
            lock(_lock)
            {
                if(_sessions.Count != 0)
                {
                    string session = _sessions.First();
                    _sessions.Remove(session);
                    JsonFile.Save($"{ProjectConfig.GetInstance().ProjectPath}{_fileNameSessionList}", _sessions);
                    return session;
                }
                else
                {
                    throw new Exception("Queue sessions is empty");
                }
            }
        }

        private List<string> CreateQueueSessions()
        {
            if(_projectSettings.IsParseAllAccounts)
            {
                WbAccountsCollection accountsCollection = new WbAccountsCollection();
                return accountsCollection.GetWorkingAccounts();
            }
            else
            {
                WbPlanningCollection wbPlanningCollection = new WbPlanningCollection();
                return wbPlanningCollection.GetUniqueAccountsForLastDays(_projectSettings.NumberDaysForGettingSessions);
            }
        }

        private List<string> LoadFromJsonFile()
        {
            string pathFile = $"{ProjectConfig.GetInstance().ProjectPath}{_fileNameSessionList}";
            if (File.Exists(pathFile))
            {
                List<string> sessions = JsonFile.Load<List<string>>(pathFile);
                return sessions;
            }
            else
            {
                return new List<string>();
            }
        }

        private void UpdateWorkDate()
        {
            _projectSettings.LastWorkDate = DateTime.Now;
            _workSettings.UpdateSettings(_projectSettings);
        }
    }
}
