using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZpSolutionWbBuyoutsParser.CustomExceptions;
using ZpSolutionWbBuyoutsParser.Models.Json;
using ZpSolutionWbBuyoutsParser.Models.Standard;
using ZpSolutionWbBuyoutsParser.Mongo;
using ZpSolutionWbBuyoutsParser.Mongo.CollectionStorage;

namespace ZpSolutionWbBuyoutsParser
{
    internal class AccountsWorkQueue
    {
        private List<SessionForQueueModel> _sessions;

        private const string _fileNameSessionList = "sessions.json";

        private readonly int _maxAttempt = 3;
        private readonly WorkSettings _workSettings;
        private readonly ProjectConfig _projectConfig;
        private readonly ProjectSettingsModel _projectSettings;

        private static object  _lock = new object();
        private static AccountsWorkQueue _instance;

        private AccountsWorkQueue()
        {
            _projectConfig = ProjectConfig.GetInstance();
            _workSettings = WorkSettings.Instance;
            _projectSettings = _workSettings.GetSettings();
            _sessions = LoadFromJsonFile();
        }

        private AccountsWorkQueue(int maxAttempt) : this()
        {
            _maxAttempt = maxAttempt;
        }

        public int Count
        {
            get
            {
                if( _sessions == null )
                {
                    return 0;
                }
                else
                {
                    return _sessions.Count;
                }
            }
        }

        public static AccountsWorkQueue GetIstance()
        {
            lock(_lock)
            {
                if (_instance == null)
                {
                    _instance = new AccountsWorkQueue();
                }
                return _instance;
            }
        }

        public static AccountsWorkQueue GetIstance(int maxAttempt)
        {
            if (_instance == null)
            {
                _instance = new AccountsWorkQueue(maxAttempt);
            }
            return _instance;
        }

        public bool IsFirstStart()
        {
            lock(_lock)
            {
                DateTime lastWorkDate = _projectSettings.LastWorkDate;
                if (lastWorkDate.Date != DateTime.Now.Date)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public void CreateQueue()
        {
            lock(_lock)
            {
                _sessions = CreateQueueSessions();
                JsonFile.Save($"{_projectConfig.ProjectPath}{_fileNameSessionList}", _sessions);
                UpdateWorkDate();
            }
        }

        public void AddSessionPlusAttempt(SessionForQueueModel session)
        {
            lock(_lock)
            {
                session.CurrentAttempt += 1;
                _sessions.Add(session);
                JsonFile.Save($"{_projectConfig.ProjectPath}{_fileNameSessionList}", _sessions);
            }
        }

        public SessionForQueueModel TakeSession()
        {
            lock(_lock)
            {
                SessionForQueueModel session = TakeSessionWithRemoveMaxAttempts();
                JsonFile.Save($"{_projectConfig.ProjectPath}{_fileNameSessionList}", _sessions);
                return session;
            }
        }

        private SessionForQueueModel TakeSessionWithRemoveMaxAttempts()
        {
            if(_sessions.Count != 0)
            {
                SessionForQueueModel session = _sessions.First();
                _sessions.Remove(session);
                if (session.CurrentAttempt >= _maxAttempt)
                {
                    SessionForQueueModel newSession = TakeSessionWithRemoveMaxAttempts();
                    return newSession;
                }
                else
                {
                    return session;
                }
            }
            else
            {
                JsonFile.Save($"{_projectConfig.ProjectPath}{_fileNameSessionList}", _sessions);
                throw new EmptyQueueException("Queue sessions is empty");
            }
        }

        private List<SessionForQueueModel> CreateQueueSessions()
        {
            if(_projectSettings.IsParseAllAccounts)
            {
                WbAccountsCollection accountsCollection = new WbAccountsCollection();
                IEnumerable<string> sessions = accountsCollection.GetWorkingAccounts();
                List<SessionForQueueModel> sessionFromQueue = AddToFirstAttemptSessions(sessions);
                return sessionFromQueue;
            }
            else
            {
                WbPlanningCollection wbPlanningCollection = new WbPlanningCollection();
                IEnumerable<string> sessions = wbPlanningCollection.GetUniqueAccountsForLastDays(_projectSettings.NumberDaysForGettingSessions);
                List<SessionForQueueModel> sessionFromQueue = AddToFirstAttemptSessions(sessions);
                return sessionFromQueue;
            }
        }

        private List<SessionForQueueModel> AddToFirstAttemptSessions(IEnumerable<string> sessions)
        {
            List<SessionForQueueModel> sessionFromQueues = sessions
                .Select(x=> new SessionForQueueModel { Name = x, CurrentAttempt = 0})
                .ToList();
            return sessionFromQueues;
        }

        private List<SessionForQueueModel> LoadFromJsonFile()
        {
            lock(_lock)
            {
                string pathFile = $"{_projectConfig.ProjectPath}{_fileNameSessionList}";
                if (File.Exists(pathFile))
                {
                    List<SessionForQueueModel> sessions = JsonFile.Load<List<SessionForQueueModel>>(pathFile);
                    return sessions;
                }
                else
                {
                    return new List<SessionForQueueModel>();
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
