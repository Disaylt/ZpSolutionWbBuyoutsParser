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
        public string ProjectPath { get; private set; }
        private static ProjectConfig _projectConfig;
        private static readonly object _lock = new object();
        private ProjectConfig() { }
        private ProjectConfig(string projectPath)
        {
            ProjectPath = projectPath;
        }

        public static void Initialize(IZennoPosterProjectModel project)
        {
            lock (_lock)
            {
                if (_projectConfig == null)
                {
                    _projectConfig = new ProjectConfig(project.Path);
                }
            }
        }

        public static ProjectConfig GetInstance()
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
    }
}
