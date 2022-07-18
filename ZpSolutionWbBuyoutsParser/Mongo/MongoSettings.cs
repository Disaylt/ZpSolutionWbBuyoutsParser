using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZpSolutionWbBuyoutsParser.Models.Json;

namespace ZpSolutionWbBuyoutsParser.Mongo
{
    internal class MongoSettings
    {
        private static MongoSettingsModel _settings;
        private static readonly object _lock = new object();
        private const string _fileName = "MongoSettings.json";
        private static string _projectPath;
        public MongoSettings()
        {
            if(_projectPath == null)
            {
                _projectPath = ProjectConfig.GetInstance().ProjectPath;
            }
        }

        public MongoSettingsModel GetSettings()
        {
            lock(_lock)
            {
                if(_settings == null)
                {
                    _settings = JsonFile.Load<MongoSettingsModel>($"{_projectPath}{_fileName}");
                }
                return _settings;
            }
        }
    }
}
