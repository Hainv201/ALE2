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
            List<State> Initials = states.FindAll(x => x.IsInitial);
            foreach (var Initial in Initials)
            {
                int i = 0;
                List<Transition> processedEpsilonTransitions = new List<Transition>();
                if (transitions.Exists(x => x.GetPopStack() != null))
                {
                    List<Stack> stacks = new List<Stack>();
                    IsAccepted = CheckWordIsAcceptedWhenTransitionHasStack(i, processedEpsilonTransitions, transitions, false, Initial, stacks);
                }
                else
                {
                    IsAccepted = IsFinal(i, processedEpsilonTransitions, transitions, false, Initial);
                }
            }
        }

        // assignment 2
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

        private bool IsFinal(int i, List<Transition> processedEpsilonTransitions, List<Transition> transitions, bool UseEpsilonMove, State current_state)
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

        // assignment 6
        private bool CheckWordIsAcceptedWhenTransitionHasStack(int i, List<Transition> processedEpsilonTransitions, List<Transition> transitions, bool UseEpsilonMove, State current_state, List<Stack> currentStacks)
        {
            List<Stack> child_stacks = currentStacks.ToList();
            List<Transition> next_possible_transitions;
            if (ContainOnlyEpsilon())
            {
                next_possible_transitions = ThirdRule(tran)
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
            next_possible_transitions = PossibleMove(transitions, alp, current_state);
            if (i == Words.Length && !ContainOnlyEpsilon() && current_state.IsFinal && child_stacks.Count == 0)
            {
                return true;
            }
            if (ContainOnlyEpsilon() && current_state.IsFinal && child_stacks.Count == 0)
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
                if (CheckWordIsAcceptedWhenTransitionHasStack(i + 1, processedEpsilonTransitions, transitions, UseEpsilonMove, current_state,child_stacks))
                {
                    return true;
                }
            }
            return false;
        }

        private List<Transition> FirstRule(List<Transition> transitions, string alp, State current_State, List<Stack> stacks)
        {
            return transitions.FindAll(x => x.GetLeftState() == current_State && x.GetSymbol().Label == alp && stacks.Contains(x.GetPopStack()));
        }

        private List<Transition> SecondRule(List<Transition> transitions, string alp, State current_State, List<Stack> stacks)
        {
            return transitions.FindAll(x => x.GetLeftState() == current_State && x.GetSymbol().Label == alp && (x.GetPopStack().Character == "_" || x.GetPopStack() == null));
        }

        private List<Transition> ThirdRule(List<Transition> transitions, State current_State, List<Stack> stacks)
        {
            return transitions.FindAll(x => x.GetLeftState() == current_State && x.GetSymbol().Label == "_" && stacks.Contains(x.GetPopStack()));
        }

        private List<Transition> LastRule(List<Transition> transitions, State current_State, List<Stack> stacks)
        {
            return transitions.FindAll(x => x.GetLeftState() == current_State && x.GetSymbol().Label == "_" && (x.GetPopStack().Character == "_" || x.GetPopStack() == null));
        }
    }
}
