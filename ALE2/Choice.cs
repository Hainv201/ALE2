using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE2
{
    class Choice:Regular_Expression
    {
        public Choice():base()
        {

        }

        public override string ToString()
        {
            return $"({Left_Expression}|{Right_Expression})";
        }
    }
}
