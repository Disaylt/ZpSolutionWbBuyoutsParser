using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZennoLab.InterfacesLibrary.ProjectModel.Collections;
using ZpSolutionWbBuyoutsParser.Models.Standard;

namespace ZpSolutionWbBuyoutsParser.ZennoPosterProjectObjects
{
    internal class ZennoPosterProfile
    {
        public SessionForQueueModel Session { get; private set; }
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

        public void Load(SessionForQueueModel sessionName)
        {
            if(!IsLoad)
            {
                Session = sessionName;
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
            Profile.Save($"{_pathToZpProfiles}{Session}.zpprofile", 
                saveLocalStorage: true, 
                saveWebRtc: true, 
                saveIndexedDb: true, 
                saveSuperCookie: true);
        }
    }
}
