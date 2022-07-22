using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZennoLab.CommandCenter;
using ZennoLab.InterfacesLibrary.ProjectModel;

namespace ZpSolutionWbBuyoutsParser.ZennoPosterProjectObjects
{
    internal class ZennoPosterAplicationHandler
    {
        public Guid GuidTaskId { get; }
        public ZennoPosterAplicationHandler(IZennoPosterProjectModel project)
        {
            string taskId = project.TaskId;
            GuidTaskId = Guid.Parse(taskId);
        }

        public void SetTries(int numTries)
        {
            ZennoPoster.SetTries(GuidTaskId, numTries);
        }
    }
}
