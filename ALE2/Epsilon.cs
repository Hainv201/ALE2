using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE2
{
    class Epsilon:Regular_Expression
    {
        public Epsilon():base()
        {

        }

        public override string ToString()
        {
            return "\u03B5";
        }
    }
}
