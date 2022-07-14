using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZpSolutionWbBuyoutsParser.Models.Json;
using Global.ZennoLab.Json;

namespace ZpSolutionWbBuyoutsParser
{
    internal class ProjectSettings
    {
        private static ProjectSettingsModel _projectSettings;
        private static ProjectConfig _projectConfig;
        private static readonly object _lock = new object();
        private const string _fileName = "ProjectSettings.json";
        public ProjectSettings()
        {
            _projectConfig = ProjectConfig.GetInstance();
        }

        public ProjectSettingsModel GetSettings()
        {
            lock (_lock)
            {
                if (_projectSettings == null)
                {
                    _projectSettings = JsonFile.Load<ProjectSettingsModel>($@"{_projectConfig.ProjectPath}{_fileName}");
                }
            }
            return _projectSettings;
        }

        public void UpdateSettings(ProjectSettingsModel projectSettings)
        {
            lock( _lock)
            {
                JsonFile.Save($@"{_projectConfig.ProjectPath}{_fileName}", projectSettings);
                _projectSettings = projectSettings;
            }
        }
    }
}
