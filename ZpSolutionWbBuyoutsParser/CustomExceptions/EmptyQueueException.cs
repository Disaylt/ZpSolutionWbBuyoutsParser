using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZpSolutionWbBuyoutsParser.CustomExceptions
{
    internal class EmptyQueueException : Exception
    {
        public EmptyQueueException(string message) : base(message) { }
    }
}
