using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE2
{
    class Concatenation:Regular_Expression
    {
        public Concatenation() : base()
        {
        }

        public override List<Transition> GetAutomaton(ref int i, ref List<Transition> ListTransitions, ref List<State> ListStates, ref List<Alphabet> ListAlphabets)
        {
            List<Transition> transitions_got_by_parsing_concatenation = new List<Transition>();
            List<Transition> transitions_got_by_parsing_leftexpression = Left_Expression.GetAutomaton(ref i, ref ListTransitions, ref ListStates, ref ListAlphabets);
            List<Transition> transitions_got_by_parsing_rightexpression = Right_Expression.GetAutomaton(ref i, ref ListTransitions, ref ListStates, ref ListAlphabets);
            State final_left = transitions_got_by_parsing_leftexpression.Find(x => x.GetRightState().IsFinal).GetRightState();
            final_left.IsFinal = false;
            State initial_right = transitions_got_by_parsing_rightexpression.Find(x => x.GetLeftState().IsInitial).GetLeftState();
            List<Transition> transitions_contain_initial_right = transitions_got_by_parsing_rightexpression.FindAll(x => x.GetLeftState() == initial_right);
            foreach (Transition transition in transitions_contain_initial_right)
            {
                transition.SetLeftState(final_left);
            }
            ListStates.Remove(initial_right);
            transitions_got_by_parsing_concatenation.AddRange(transitions_got_by_parsing_leftexpression);
            transitions_got_by_parsing_concatenation.AddRange(transitions_got_by_parsing_rightexpression);
            return transitions_got_by_parsing_concatenation;
        }

        public override string ToString()
        {
            return $"({Left_Expression}.{Right_Expression})";
        }
    }
}
