using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZennoLab.InterfacesLibrary.ProjectModel.Collections;

namespace ZpSolutionWbBuyoutsParser.ZennoPosterProjectObjects
{
    internal class ZennoPosterProfile
    {
        public string SessionName { get; private set; } = string.Empty;
        public IProfile Profile { get; private set; }
        public bool IsLoad { get; private set; }

        private readonly string _pathToZpProfiles;

        public ZennoPosterProfile(IProfile profile)
        {
            Profile = profile;
            IsLoad = false;
            WorkSettings workSettings = new WorkSettings();
            _pathToZpProfiles = workSettings.GetSettings().PathToZpProfiles;
        }

        public void Load(string sessionName)
        {
            if(!IsLoad)
            {
                SessionName = sessionName;
                Profile.Load($"{_pathToZpProfiles}{sessionName}.zpprofile");
                IsLoad = true;
            }
            else
            {
                throw new Exception("Ыession already loaded");
            }
        }

        public void Save()
        {
            Profile.Save($"{_pathToZpProfiles}{SessionName}.zpprofile", 
                saveLocalStorage: true, 
                saveWebRtc: true, 
                saveIndexedDb: true, 
                saveSuperCookie: true);
        }
    }
}
