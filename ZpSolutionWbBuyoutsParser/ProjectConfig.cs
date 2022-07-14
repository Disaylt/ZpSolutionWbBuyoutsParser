using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZennoLab.InterfacesLibrary.ProjectModel;
using ZpSolutionWbBuyoutsParser.Models.Json;

namespace ZpSolutionWbBuyoutsParser
{
    internal class ProjectConfig
    {
        private ProjectSettingsModel _projectSettings;
        private static ProjectConfig _projectConfig;
        private string _projectPath { get; set; }
        private ProjectConfig() { }
        private ProjectConfig(string projectPath)
        {
            _projectPath = projectPath;
        }

        public void Initialize(IZennoPosterProjectModel project)
        {
            if(_projectConfig == null)
            {
                _projectConfig = new ProjectConfig(project.Path);
            }
        }

        public ProjectConfig GetInstance()
        {
            if(_projectConfig != null)
            {
                return _projectConfig;
            }
            else
            {
                throw new NullReferenceException("ProjectConfig is not loaded");
            }
        }

        public ProjectSettingsModel GetProjectSettings()
        {
            if( _projectSettings == null)
            {
                string fileName = "projectSettings.json";
                _projectSettings = JsonLoader.LoadJson<ProjectSettingsModel>($@"{_projectPath}{fileName}");
            }
            return _projectSettings;
        }
    }
}
