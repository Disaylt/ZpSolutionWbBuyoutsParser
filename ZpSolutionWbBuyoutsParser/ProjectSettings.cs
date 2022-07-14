using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZpSolutionWbBuyoutsParser.Models.Json;

namespace ZpSolutionWbBuyoutsParser
{
    internal class ProjectSettings
    {
        private ProjectSettingsModel _projectSettings;
        private static ProjectConfig _projectConfig;
        private static object _lock = new object();
        private const string _fileName = "ProjectSettings.json";
        public ProjectSettings()
        {
            _projectConfig = ProjectConfig.GetInstance();
        }

        public ProjectSettingsModel GetProjectSettings()
        {
            lock (_lock)
            {
                if (_projectSettings == null)
                {
                    _projectSettings = JsonLoader.LoadJson<ProjectSettingsModel>($@"{_projectConfig.ProjectPath}{_fileName}");
                }
            }
            return _projectSettings;
        }
    }
}
