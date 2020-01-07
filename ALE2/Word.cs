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
                    IsAccepted = CheckWordIsAcceptedWhenTransitionHasStack(i, processedEpsilonTransitions, transitions, Initial, stacks);
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
        private bool CheckWordIsAcceptedWhenTransitionHasStack(int i, List<Transition> processedEpsilonTransitions, List<Transition> transitions, State current_state, List<Stack> currentStacks)
        {
            if (ContainOnlyEpsilon())
            {
                if(CheckIfWordContainsOnlyEpsilonAccepted(processedEpsilonTransitions, transitions, current_state, currentStacks))
                {
                    return true;
                }
            }
            else
            {
                if (CheckIfWordIsAccepted(i,processedEpsilonTransitions,transitions,current_state,currentStacks))
                {
                    return true;
                }
            }
            return false;
        }

        private bool CheckIfWordContainsOnlyEpsilonAccepted(List<Transition> processedEpsilonTransitions, List<Transition> transitions, State current_state, List<Stack> currentStacks)
        {
            List<Stack> child_stacks = currentStacks.ToList();
            if (current_state.IsFinal && child_stacks.Count == 0)
            {
                return true;
            }
            var next_possible_transitions = ThirdRule(transitions, current_state, child_stacks);
            if (next_possible_transitions.Count == 0)
            {
                next_possible_transitions = LastRule(transitions, current_state, child_stacks);
                if (next_possible_transitions.Count == 0)
                {
                    return false;
                }
                else
                {
                    next_possible_transitions = next_possible_transitions.Except(processedEpsilonTransitions).ToList();
                    if (next_possible_transitions.Count == 0)
                    {
                        return false;
                    }
                    for (int i4 = 0; i4 < next_possible_transitions.Count; i4++)
                    {
                        var t4 = next_possible_transitions[i4];
                        next_possible_transitions.Remove(t4);
                        processedEpsilonTransitions.Add(t4);
                        var st4 = t4.GetPushStack();
                        if (st4 != null && st4.Character != "_")
                        {
                            child_stacks.Add(st4);
                        }
                        if(CheckIfWordContainsOnlyEpsilonAccepted(processedEpsilonTransitions, transitions, t4.GetRightState(), child_stacks))
                        {
                            return true;
                        }
                        child_stacks.Remove(st4);
                    }
                }
            }
            else
            {
                for (int i3 = 0; i3 < next_possible_transitions.Count; i3++)
                {
                    var t3 = next_possible_transitions[i3];
                    next_possible_transitions.Remove(t3);
                    processedEpsilonTransitions.Add(t3);
                    var stpop3 = child_stacks.Find(x => x.Character == t3.GetPopStack().Character);
                    child_stacks.Remove(stpop3);
                    var st3 = t3.GetPushStack();
                    if (st3 != null && st3.Character != "_")
                    {
                        child_stacks.Add(st3);
                    }
                    if(CheckIfWordContainsOnlyEpsilonAccepted(processedEpsilonTransitions, transitions, t3.GetRightState(), child_stacks))
                    {
                        return true;
                    }
                    child_stacks.Add(stpop3);
                    child_stacks.Remove(st3);
                }
            }
            return false;
        }

        private bool CheckIfWordIsAccepted(int i, List<Transition> processedEpsilonTransitions, List<Transition> transitions, State current_state, List<Stack> currentStacks)
        {
            List<Stack> child_stacks = currentStacks.ToList();
            if (i == Words.Length && current_state.IsFinal && child_stacks.Count == 0)
            {
                return true;
            }
            string alp = "?";
            if (i < Words.Length)
            {
                alp = Words[i].ToString();
            }
            var next_possible_transitions = FirstRule(transitions, alp, current_state, child_stacks);
            if (next_possible_transitions.Count == 0)
            {
                next_possible_transitions = SecondRule(transitions, alp, current_state, child_stacks);
                if (next_possible_transitions.Count == 0)
                {
                    next_possible_transitions = ThirdRule(transitions, current_state, child_stacks);
                    if (next_possible_transitions.Count == 0)
                    {
                        next_possible_transitions = LastRule(transitions, current_state, child_stacks);
                        if (next_possible_transitions.Count == 0)
                        {
                            return false;
                        }
                        else
                        {
                            next_possible_transitions = next_possible_transitions.Except(processedEpsilonTransitions).ToList();
                            if (next_possible_transitions.Count == 0)
                            {
                                return false;
                            }
                            for (int i4 = 0; i4 < next_possible_transitions.Count; i4++)
                            {
                                var t4 = next_possible_transitions[i4];
                                next_possible_transitions.Remove(t4);
                                processedEpsilonTransitions.Add(t4);
                                var st4 = t4.GetPushStack();
                                if (st4 != null && st4.Character != "_")
                                {
                                    child_stacks.Add(st4);
                                }
                                if (CheckIfWordIsAccepted(i,processedEpsilonTransitions, transitions, t4.GetRightState(), child_stacks))
                                {
                                    return true;
                                }
                                child_stacks.Remove(st4);
                            }
                        }
                    }
                    else
                    {
                        for (int i3 = 0; i3 < next_possible_transitions.Count; i3++)
                        {
                            var t3 = next_possible_transitions[i3];
                            next_possible_transitions.Remove(t3);
                            processedEpsilonTransitions.Add(t3);
                            var stpop3 = child_stacks.Find(x => x.Character == t3.GetPopStack().Character);
                            child_stacks.Remove(stpop3);
                            var st3 = t3.GetPushStack();
                            if (st3 != null && st3.Character != "_")
                            {
                                child_stacks.Add(st3);
                            }
                            if (CheckIfWordIsAccepted(i, processedEpsilonTransitions, transitions, t3.GetRightState(), child_stacks))
                            {
                                return true;
                            }
                            child_stacks.Add(stpop3);
                            child_stacks.Remove(st3);
                        }
                    }
                }
                else
                {
                    for (int i2 = 0; i2 < next_possible_transitions.Count; i2++)
                    {
                        var t2 = next_possible_transitions[i2];
                        next_possible_transitions.Remove(t2);
                        var st2 = t2.GetPushStack();
                        if (st2 != null && st2.Character != "_")
                        {
                            child_stacks.Add(st2);
                        }
                        if (CheckIfWordIsAccepted(i+1, processedEpsilonTransitions, transitions, t2.GetRightState(), child_stacks))
                        {
                            return true;
                        }
                        child_stacks.Remove(st2);
                    }
                }
            }
            else
            {
                for (int i1 = 0; i1 < next_possible_transitions.Count; i1++)
                {
                    var t1 = next_possible_transitions[i1];
                    next_possible_transitions.Remove(t1);
                    var stpop1 = child_stacks.Find(x => x.Character == t1.GetPopStack().Character);
                    child_stacks.Remove(stpop1);
                    var st1 = t1.GetPushStack();
                    if (st1 != null && st1.Character != "_")
                    {
                        child_stacks.Add(st1);
                    }
                    if (CheckIfWordIsAccepted(i+1, processedEpsilonTransitions, transitions, t1.GetRightState(), child_stacks))
                    {
                        return true;
                    }
                    child_stacks.Add(stpop1);
                    child_stacks.Remove(st1);
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
            return transitions.FindAll(x => x.GetLeftState() == current_State && x.GetSymbol().Label == alp && (x.GetPopStack() == null || x.GetPopStack().Character == "_"));
        }

        private List<Transition> ThirdRule(List<Transition> transitions, State current_State, List<Stack> stacks)
        {
            return transitions.FindAll(x => x.GetLeftState() == current_State && x.GetSymbol().Label == "_" && stacks.Contains(x.GetPopStack()));
        }

        private List<Transition> LastRule(List<Transition> transitions, State current_State, List<Stack> stacks)
        {
            return transitions.FindAll(x => x.GetLeftState() == current_State && x.GetSymbol().Label == "_" && (x.GetPopStack() == null || x.GetPopStack().Character == "_"));
        }
    }
}
