using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE2
{
    abstract class Regular_Expression
    {
        public Regular_Expression Left_Expression { get; set; }
        public Regular_Expression Right_Expression { get; set; }
        public Regular_Expression()
        {
            Left_Expression = null;
            Right_Expression = null;
        }

        public abstract List<Transition> GetAutomaton(ref int i, ref List<Transition> ListTransitions, ref List<State> ListStates, ref List<Alphabet> ListAlphabets);

    }
}
