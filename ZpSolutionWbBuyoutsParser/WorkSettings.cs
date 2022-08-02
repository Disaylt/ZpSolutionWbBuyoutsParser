using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZpSolutionWbBuyoutsParser.Models.Json;
using Global.ZennoLab.Json;

namespace ZpSolutionWbBuyoutsParser
{
    internal class WorkSettings
    {
        private ProjectConfig _projectConfig;
        private static readonly object _lock = new object();
        private static WorkSettings _instance;
        private const string _fileName = "ProjectSettings.json";
        private WorkSettings()
        {
            _projectConfig = ProjectConfig.GetInstance();
        }

        public static WorkSettings Instance
        {
            get
            {
                lock( _lock)
                {
                    if (_instance == null)
                    {
                        _instance = new WorkSettings();
                    }
                    return _instance;
                }
            }
        }

        public ProjectSettingsModel GetSettings()
        {
            lock (_lock)
            {
                var projectSettings = JsonFile.Load<ProjectSettingsModel>($@"{_projectConfig.ProjectPath}{_fileName}");
                return projectSettings;
            }
        }

        public void UpdateSettings(ProjectSettingsModel projectSettings)
        {
            lock( _lock)
            {
                JsonFile.Save($@"{_projectConfig.ProjectPath}{_fileName}", projectSettings);
            }
        }
    }
}
