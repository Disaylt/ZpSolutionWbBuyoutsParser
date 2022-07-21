using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZennoLab.InterfacesLibrary.ProjectModel.Collections;

namespace ZpSolutionWbBuyoutsParser.ZennoPoster
{
    internal class ZennoPosterProfile
    {
        public string SessionName { get; private set; }
        public IProfile Profile { get; private set; }

        private readonly string _pathToZpProfiles;

        public ZennoPosterProfile(IProfile profile, string sessionName)
        {
            SessionName = sessionName;
            Profile = profile;
            WorkSettings workSettings = new WorkSettings();
            _pathToZpProfiles = workSettings.GetSettings().PathToZpProfiles;
        }
        public void Load()
        {
            Profile.Load($"{_pathToZpProfiles}{SessionName}.zpprofile");
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
