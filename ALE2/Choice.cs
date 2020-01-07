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

        public override List<Transition> GetAutomaton(ref int i, ref List<Transition> ListTransitions, ref List<State> ListStates, ref List<Alphabet> ListAlphabets)
        {
            List<Transition> transitions_got_by_parsing_choice = new List<Transition>();
            State initial = new State($"S{i}");
            i++;
            ListStates.Add(initial);
            List<Transition> transitions_got_by_parsing_leftexpression = Left_Expression.GetAutomaton(ref i, ref ListTransitions, ref ListStates, ref ListAlphabets);
            List<Transition> transitions_got_by_parsing_rightexpression = Right_Expression.GetAutomaton(ref i, ref ListTransitions, ref ListStates, ref ListAlphabets);
            State final = new State($"S{i}");
            i++;
            ListStates.Add(final);

            State left_left = transitions_got_by_parsing_leftexpression.Find(x => x.GetLeftState().IsInitial).GetLeftState();
            State right_left = transitions_got_by_parsing_leftexpression.Find(x => x.GetRightState().IsFinal).GetRightState();

            State left_right = transitions_got_by_parsing_rightexpression.Find(x => x.GetLeftState().IsInitial).GetLeftState();
            State right_right = transitions_got_by_parsing_rightexpression.Find(x => x.GetRightState().IsFinal).GetRightState();

            initial.IsInitial = true;
            final.IsFinal = true;
            left_left.IsInitial = false;
            right_left.IsFinal = false;
            left_right.IsInitial = false;
            right_right.IsFinal = false;

            Transition transition1 = new Transition("_",null);
            transition1.SetLeftState(initial);
            transition1.SetRightState(left_left);
            transitions_got_by_parsing_choice.Add(transition1);
            ListTransitions.Add(transition1);

            Transition transition2 = new Transition("_",null);
            transition2.SetLeftState(initial);
            transition2.SetRightState(left_right);
            transitions_got_by_parsing_choice.Add(transition2);
            ListTransitions.Add(transition2);

            transitions_got_by_parsing_choice.AddRange(transitions_got_by_parsing_leftexpression);
            transitions_got_by_parsing_choice.AddRange(transitions_got_by_parsing_rightexpression);

            Transition transition3 = new Transition("_",null);
            transition3.SetLeftState(right_left);
            transition3.SetRightState(final);
            transitions_got_by_parsing_choice.Add(transition3);
            ListTransitions.Add(transition3);

            Transition transition4 = new Transition("_",null);
            transition4.SetLeftState(right_right);
            transition4.SetRightState(final);
            transitions_got_by_parsing_choice.Add(transition4);
            ListTransitions.Add(transition4);


            return transitions_got_by_parsing_choice;

        }

        public override string ToString()
        {
            return $"({Left_Expression}|{Right_Expression})";
        }
    }
}
