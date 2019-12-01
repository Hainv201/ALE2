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

        public override List<Transition> GetAutomaton(ref int i, ref List<Transition> ListTransitions, ref List<State> ListStates, ref List<Alphabet> ListAlphabets)
        {
            List<Transition> transitions_got_by_parsed_repetition = new List<Transition>();
            State initial = new State($"S{i}");
            initial.IsInitial = true;
            i++;
            List<Transition> transitions_got_by_parsing_leftexpression = Left_Expression.GetAutomaton(ref i, ref ListTransitions, ref ListStates, ref ListAlphabets);
            State final = new State($"S{i}");
            final.IsFinal = true;
            i++;
            State left_left_state = transitions_got_by_parsing_leftexpression.Find(x => x.GetLeftState().IsInitial).GetLeftState();
            State right_left_state = transitions_got_by_parsing_leftexpression.Find(x => x.GetRightState().IsFinal).GetRightState();
            left_left_state.IsInitial = false;
            right_left_state.IsFinal = false;

            Transition transition1 = new Transition("_");
            transition1.SetLeftState(initial);
            transition1.SetRightState(left_left_state);
            transitions_got_by_parsed_repetition.Add(transition1);
            ListTransitions.Add(transition1);

            Transition transition2 = new Transition("_");
            transition2.SetLeftState(initial);
            transition2.SetRightState(final);
            transitions_got_by_parsed_repetition.Add(transition2);
            ListTransitions.Add(transition2);

            Transition transition3 = new Transition("_");
            transition3.SetLeftState(right_left_state);
            transition3.SetRightState(left_left_state);
            transitions_got_by_parsed_repetition.Add(transition3);
            ListTransitions.Add(transition3);

            transitions_got_by_parsed_repetition.AddRange(transitions_got_by_parsing_leftexpression);

            Transition transition4 = new Transition("_");
            transition4.SetLeftState(right_left_state);
            transition4.SetRightState(final);
            transitions_got_by_parsed_repetition.Add(transition4);
            ListTransitions.Add(transition4);

            ListStates.Add(initial);
            ListStates.Add(final);
            return transitions_got_by_parsed_repetition;
        }

        public override string ToString()
        {
            return $"({Left_Expression})*";
        }


    }
}
