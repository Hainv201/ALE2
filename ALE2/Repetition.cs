using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE2
{
    class Repetition: Regular_Expression
    {
        public Repetition() : base()
        {

        }

        public override string ToString()
        {
            return $"({Left_Expression})*";
        }
    }
}
