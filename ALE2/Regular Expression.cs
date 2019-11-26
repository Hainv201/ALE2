using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE2
{
    class Regular_Expression
    {
        public Regular_Expression Left_Expression { get; set; }
        public Regular_Expression Right_Expression { get; set; }
        public Regular_Expression()
        {
            Left_Expression = null;
            Right_Expression = null;
        }
    }
}
