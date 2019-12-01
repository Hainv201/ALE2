using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE2
{
    class Word
    {
        public string Words { get; }
        public bool IsAccepted { get; set; }
        public Word(string _words)
        {
            Words = _words;
        }


        public bool IsWordAccepted(List<State> states, List<Transition> transitions)
        {
            State Initial = states.Find(x => x.IsInitial);
            int i = 0;
            return IsFinal(i, transitions,false, Initial);
        }

        private List<Transition> PossibleMove(List<Transition> transitions, string alp, State current_State)
        {
            if (alp != "_")
            {
                List<Transition> possible_move_by_alp = transitions.FindAll(x => (x.GetLabeledTransition().Contains(alp) && x.GetLeftState() == current_State));
                List<Transition> possible_move_by_epsilon = transitions.FindAll(x => (x.GetLabeledTransition().Contains("_") && x.GetLeftState() == current_State && x.GetRightState() != current_State));
                return possible_move_by_alp.Union(possible_move_by_epsilon).ToList();
            }
            else
            {
                return transitions.FindAll(x => (x.GetLabeledTransition().Contains("_") && x.GetLeftState() == current_State && x.GetRightState() != current_State));
            }
        }

        private bool IsFinal(int i, List<Transition> transitions, bool UseEpsilonMove, State current_state)
        {
            string alp = "";
            if (ContainOnlyEpsilon())
            {
                alp = "_";
            }
            else
            {
                if (!UseEpsilonMove)
                {
                    if (i < Words.Length)
                    {
                        alp = Words[i].ToString();
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    alp = Words[i - 1].ToString();
                    i = i - 1;
                }
            }
            List<Transition> next_possible_transitions = PossibleMove(transitions, alp, current_state);
            if (i == Words.Length - 1 && !ContainOnlyEpsilon() && next_possible_transitions.Exists(x => x.GetRightState().IsFinal && x.GetLabeledTransition().Contains(alp)))
            {
                return true;
            }
            if (ContainOnlyEpsilon() && next_possible_transitions.Exists(x => x.GetRightState().IsFinal))
            {
                return true;
            }
            foreach (Transition transition in next_possible_transitions)
            {
                if (transition.GetLabeledTransition().Contains("_"))
                {
                    UseEpsilonMove = true;
                }
                else
                {
                    UseEpsilonMove = false;
                }
                current_state = transition.GetRightState();
                if (IsFinal(i + 1, transitions, UseEpsilonMove, current_state))
                {
                    return true;
                }
            }
            return false;
        }

        private bool ContainOnlyEpsilon()
        {
            string clone_words = Words.Clone().ToString();
            clone_words = clone_words.Replace("_", "");
            clone_words = clone_words.Replace(" ", "");
            if (clone_words == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
