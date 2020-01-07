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

        public override List<Transition> GetAutomaton(ref int i, ref List<Transition> ListTransitions, ref List<State> ListStates, ref List<Alphabet> ListAlphabets)
        {
            List<Transition> transitions_got_by_parse_alphabet = new List<Transition>();
            State initial = new State($"S{i}");
            State final = new State($"S{i+1}");
            i += 2;
            initial.IsInitial = true;
            final.IsFinal = true;
            Transition transition = new Transition($"_",null);
            transition.SetLeftState(initial);
            transition.SetRightState(final);
            transitions_got_by_parse_alphabet.Add(transition);
            ListTransitions.Add(transition);
            ListStates.Add(initial);
            ListStates.Add(final);
            return transitions_got_by_parse_alphabet;
        }

        public override string ToString()
        {
            return "\u03B5";
        }

    }
}
