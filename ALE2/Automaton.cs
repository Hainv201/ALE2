using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE2
{
    class Automaton
    {
        public List<Alphabet> ListAlphabets { get; }
        public List<State> ListStates { get; }
        public List<Transition> ListTransitions { get; }
        public List<Word> ListWords { get; private set; }
        public List<Stack> ListStacks { get; }
        public string Comment { get; set; }
        public bool IsDFA { get; }
        public bool IsFinite { get; }
        List<string> words;
        bool hasLoop;
        public Automaton(List<Alphabet> alphabets, List<State> states, List<Transition> transitions,List<Word> words, List<Stack> stacks)
        {
            ListAlphabets = alphabets;
            ListStates = states;
            ListTransitions = transitions;
            ListWords = new List<Word>();
            IsDFA = isDFA();
            IsFinite = CheckIsFinite();
            if (words == null)
            {
                words = new List<Word>();
            }
            if (stacks == null)
            {
                stacks = new List<Stack>();
            }
            ListStacks = stacks;
            GetListWords(words);
        }
        private void GetListWords(List<Word> words)
        {
            ListWords.AddRange(words);
            ListWords = ListWords.Distinct(new WordComparer()).ToList();
            foreach (Word w in ListWords)
            {
                w.IsWordAccepted(ListStates, ListTransitions);
            }
        }
        public string CreateGraph()
        {
            string data = "digraph {" + Environment.NewLine + "rankdir = LR;" + Environment.NewLine + "\"\" [shape = none]";
            foreach (State state in ListStates)
            {
                data += Environment.NewLine + state.CreateGraph();
            }
            data += Environment.NewLine;
            data += Environment.NewLine + $"\"\" -> \"" + ListStates.Find(x => x.IsInitial).State_Name + "\"";
            foreach (Transition transition in ListTransitions)
            {
                data += Environment.NewLine + transition.CreateGraph();
            }
            data += Environment.NewLine + "}";
            return data;
        }

        private bool isDFA()
        {
            if (ListTransitions.Exists(x => x.GetSymbol().Label == "_"))
            {
                return false;
            }
            foreach (Alphabet alp in ListAlphabets)
            {
                List<Transition> list_transitions_contain_alp = ListTransitions.FindAll(x => x.GetSymbol().Label == alp.Character);
                List<State> list_states = new List<State>();
                foreach (Transition trans in list_transitions_contain_alp)
                {
                    list_states.Add(trans.GetLeftState());
                }
                bool are_2lists_equal = (list_states.All(ListStates.Contains) && list_states.Count == ListStates.Count);
                if (!are_2lists_equal)
                {
                    return false;
                }
            }
            return true;
        }

        public void CreateTextFile()
        {
            ListWords = ListWords.Distinct(new WordComparer()).ToList();
            string content = "";
            content += $"# {Comment}" + Environment.NewLine + Environment.NewLine;
            content += $"alphabet: {String.Join("", ListAlphabets)}" + Environment.NewLine;
            content += $"stack: {String.Join("", ListStacks)}" + Environment.NewLine;
            content += $"states: {String.Join(",", ListStates)}" + Environment.NewLine;
            content += $"final: {String.Join(",", ListStates.FindAll(x => x.IsFinal))}" + Environment.NewLine + Environment.NewLine;
            content += $"transitions:" + Environment.NewLine;
            content += $"{String.Join(Environment.NewLine, ListTransitions)}" + Environment.NewLine;
            content += "end." + Environment.NewLine + Environment.NewLine;
            content += "dfa: ";
            if (IsDFA)
            {
                content += "y" + Environment.NewLine;
            }
            else
            {
                content += "n" + Environment.NewLine;
            }
            content += "finite: ";
            if (IsFinite)
            {
                content += "y" + Environment.NewLine;
            }
            else
            {
                content += "n" + Environment.NewLine;
            }
            content += Environment.NewLine;
            content += $"words:" + Environment.NewLine;
            foreach (Word word in ListWords)
            {
                if (word.IsAccepted)
                {
                    content += word.Words + ",y" + Environment.NewLine;
                }
                else
                {
                    content += word.Words + ",n" + Environment.NewLine;
                }
            }
            content += "end.";
            string file_name = "automaton_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".txt";
            File.WriteAllText(file_name, content);
        }

        private bool CheckIsFinite()
        {
            words = new List<string>();
            hasLoop = false;
            List<State> initial_States = ListStates.FindAll(x => x.IsInitial);
            foreach (var initial in initial_States)
            {
                List<Transition> used_transitions = new List<Transition>();
                bool hasloop = false;
                GetWord(initial, used_transitions,hasloop);
            }
            if (hasLoop)
            {
                return false;
            }
            foreach (var item in words)
            {
                ListWords.Add(new Word(item));
            }
            return true;
        }
        private void GetWord(State current, List<Transition> parent_used_transitions, bool hasloop)
        {
            List<Transition> used_transitions = parent_used_transitions.ToList();
            if (used_transitions.Exists(y => y.GetLeftState() == current))
            {
                int index = used_transitions.FindIndex(y => y.GetLeftState() == current);
                for (int i = index; i < used_transitions.Count; i++)
                {
                    if (used_transitions[i].GetSymbol().Label != "_")
                    {
                        hasloop = true;
                        break;
                    }
                }
            }
            List<Transition> possible_transitions = AvailableTransition(current);
            possible_transitions = possible_transitions.Except(used_transitions).ToList();
            foreach (var t1 in possible_transitions)
            {
                foreach (var t2 in used_transitions)
                {
                    if (t1.GetRightState() == t2.GetLeftState())
                    {
                        int index = used_transitions.IndexOf(t2);
                        for (int i = index; i < used_transitions.Count; i++)
                        {
                            if (used_transitions[i].GetSymbol().Label != "_")
                            {
                                hasloop = true;
                                break;
                            }
                        }
                        if (t1.GetSymbol().Label != "_")
                        {
                            hasloop = true;
                        }
                    }
                }
            }
            if (current.IsFinal && hasloop)
            {
                hasLoop = true;
                return;
            }
            if (current.IsFinal && !hasloop && !hasLoop)
            {
                string word = "";
                foreach (var item in used_transitions)
                {
                    word += item.GetSymbol().Label;
                }
                word = word.Replace("_", "");
                words.Add(word);
                words = words.Distinct().ToList();
            }
            foreach (var item in possible_transitions)
            {
                State next_state = item.GetRightState();
                used_transitions.Add(item);
                GetWord(next_state, used_transitions, hasloop);
                used_transitions.Remove(item);
            }
        }

        private List<Transition> AvailableTransition(State current)
        {
            return ListTransitions.FindAll(x => x.GetLeftState() == current);
        }

        public Automaton ConvertToDFA()
        {
            List<State> NewListStates = new List<State>();
            List<Transition> NewListTransitions = new List<Transition>();
            State Sink = new State("Sink");
            foreach (State s in ListStates)
            {
                s.GetReachableState(ListAlphabets, ListTransitions);
            }
            if (ListTransitions.Exists(x => x.GetLeftState().IsInitial && x.GetSymbol().Label == "_"))
            {
                return ConvertToDFAWithEpsilonMove(NewListStates,NewListTransitions, Sink);
            }
            else
            {
                return ConvertToDFAWithoutEpsilonMove(NewListStates,NewListTransitions,Sink);
            }
        }

        private Automaton ConvertToDFAWithEpsilonMove(List<State> states, List<Transition> transitions,State Sink)
        {
            List<State> initialStatesUsedEpsilon = new List<State>();
            
            ListTransitions.FindAll(x => x.GetLeftState().IsInitial && x.GetSymbol().Label == "_").ForEach(x => initialStatesUsedEpsilon.Add(x.GetLeftState()));
            foreach (var ini_eps in initialStatesUsedEpsilon)
            {
                List<Transition> processedtransitions = new List<Transition>();
                List<State> epsilon_closure_of_ini_eps = ini_eps.GetEpsilonClosure(ListTransitions, processedtransitions);
                State newini = new State($"{String.Join("", epsilon_closure_of_ini_eps)}");
                newini.IsInitial = true;
                if (epsilon_closure_of_ini_eps.Exists(x => x.IsFinal))
                {
                    newini.IsFinal = true;
                }
                states.Add(newini);
                ProcessDFA(epsilon_closure_of_ini_eps, states, transitions, Sink,newini);
            }
            return new Automaton(ListAlphabets, states, transitions,null,null);
        }

        private Automaton ConvertToDFAWithoutEpsilonMove(List<State> states, List<Transition> transitions,State Sink)
        {
            List<State> initial_states = ListStates.FindAll(x => x.IsInitial);
            foreach (var initial in initial_states)
            {
                List<State> listini = new List<State>();
                listini.Add(initial);
                if (listini.Exists(x => x.IsFinal))
                {
                    initial.IsFinal = true;
                }
                states.Add(initial);
                ProcessDFA(listini, states, transitions, Sink,initial);
            } 
            return new Automaton(ListAlphabets, states, transitions,null,null);
        }

        private void ProcessDFA(List<State> curr_states, List<State> newStates, List<Transition> transitions, State Sink, State current_State)
        {
            foreach (Alphabet a in ListAlphabets)
            {
                List<State> reachableStateByA = new List<State>();
                foreach (State s in curr_states)
                {
                    List<Transition> processedtransitions = new List<Transition>();
                    List<State> value;
                    if (s.ReachableState().TryGetValue(a, out value))
                    {
                        reachableStateByA.AddRange(value);
                    }
                    // for epsilon move
                    foreach (State s1 in value)
                    {
                        reachableStateByA.AddRange(s1.GetEpsilonClosure(ListTransitions, processedtransitions).Except(reachableStateByA));
                    }
                }
                reachableStateByA = reachableStateByA.Distinct().ToList();
                string state_name = $"{String.Join("", reachableStateByA)}";
                State next_state;
                if (state_name!="")
                {
                    next_state = CheckExistState(state_name, newStates);
                    if (next_state == null)
                    {
                        next_state = new State(state_name);
                        if (reachableStateByA.Exists(x => x.IsFinal))
                        {
                            next_state.IsFinal = true;
                        }
                        newStates.Add(next_state);
                    }
                }
                else
                {
                    next_state = CheckExistState("Sink", newStates);
                    if (next_state == null)
                    {
                        next_state = Sink;
                        newStates.Add(next_state);
                    }
                }
                Transition t = new Transition(a.Character,null);
                t.SetLeftState(current_State);
                t.SetRightState(next_state);
                if (!transitions.Exists(x => x.ToString() == t.ToString()))
                {
                    transitions.Add(t);
                }
                else
                {
                    return;
                }
                ProcessDFA(reachableStateByA, newStates, transitions, Sink, next_state);
            }
        }
        private State CheckExistState(string name, List<State> listStates)
        {
            if (listStates.Any())
            {
                foreach (State state in listStates)
                {
                    if (state.State_Name == name)
                    {
                        return state;
                    }
                }
            }
            return null;
        }
    }
}
