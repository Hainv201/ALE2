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
        public bool IsAccepted { get; private set; }
        public Word(string _words)
        {
            Words = _words;
        }


        public void IsWordAccepted(List<State> states, List<Transition> transitions)
        {
            State Initial = states.Find(x => x.IsInitial);
            int i = 0;
            List<Transition> processedEpsilonTransitions = new List<Transition>();
            if (transitions.Exists(x => x.GetPopStack() != null))
            {

            }
            else
            {
                IsAccepted = IsFinal(i, processedEpsilonTransitions, transitions,false, Initial);
            }
        }

        private List<Transition> PossibleMove(List<Transition> transitions, string alp, State current_State)
        {
            if (alp != "_")
            {
                List<Transition> possible_move_by_alp = transitions.FindAll(x => (x.GetSymbol().Label == alp && x.GetLeftState() == current_State));
                List<Transition> possible_move_by_epsilon = transitions.FindAll(x => (x.GetSymbol().Label == "_" && x.GetLeftState() == current_State && x.GetRightState() != current_State));
                return possible_move_by_alp.Union(possible_move_by_epsilon).ToList();
            }
            else
            {
                return transitions.FindAll(x => (x.GetSymbol().Label == "_" && x.GetLeftState() == current_State && x.GetRightState() != current_State));
            }
        }

        private bool IsFinal(int i,List<Transition> processedEpsilonTransitions, List<Transition> transitions, bool UseEpsilonMove, State current_state)
        {
            string alp = "?";
            if (ContainOnlyEpsilon())
            {
                alp = "_";
            }
            else
            {
                if (UseEpsilonMove)
                {
                    i -= 1;
                }
                if (i < Words.Length)
                {
                    alp = Words[i].ToString();
                }
            }
            List<Transition> next_possible_transitions = PossibleMove(transitions, alp, current_state);
            if (i == Words.Length && !ContainOnlyEpsilon() && current_state.IsFinal)
            {
                return true;
            }
            if (ContainOnlyEpsilon() && next_possible_transitions.Exists(x => x.GetRightState().IsFinal))
            {
                return true;
            }
            next_possible_transitions = next_possible_transitions.Except(processedEpsilonTransitions).ToList();
            if (next_possible_transitions.Count == 0)
            {
                return false;
            }
            foreach (Transition transition in next_possible_transitions)
            {
                if (transition.GetSymbol().Label == "_")
                {
                    UseEpsilonMove = true;
                    processedEpsilonTransitions.Add(transition);
                }
                else
                {
                    UseEpsilonMove = false;
                }
                current_state = transition.GetRightState();
                if (IsFinal(i + 1, processedEpsilonTransitions, transitions, UseEpsilonMove, current_state))
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
