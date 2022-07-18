﻿using System;
using System.Collections.Generic;
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
            _sessions = new List<string>();
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
                    int numDays = 14;
                    WbPlanningCollection wbPlanningCollection = new WbPlanningCollection();
                    _sessions = wbPlanningCollection.GetUniqueAccountsForLastDays(numDays);
                    JsonFile.Save($"{ProjectConfig.GetInstance().ProjectPath}{_fileNameSessionList}", _sessions);
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
                    throw new Exception("uqeue sessions is empty");
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
