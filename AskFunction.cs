using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeniorProj
{
    public class AskFunction
    {
        public Func<string, string> ask { get; }

        public AskFunction() {
            this.ask = (string x) => x;
        }

    }
}
