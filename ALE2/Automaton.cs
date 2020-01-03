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
        public string Comment { get; set; }
        public bool IsDFA { get; }
        public bool IsFinite { get; }
        List<string> words;
        bool hasLoop;
        public Automaton(List<Alphabet> alphabets, List<State> states, List<Transition> transitions)
        {
            ListAlphabets = alphabets;
            ListStates = states;
            ListTransitions = transitions;
            ListWords = new List<Word>();
            IsDFA = isDFA();
            IsFinite = CheckIsFinite();
        }
        public void GetListWords(List<Word> words)
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
            if (ListTransitions.Exists(x => x.GetLabeledTransition().Contains("_")))
            {
                return false;
            }
            foreach (Alphabet alp in ListAlphabets)
            {
                List<Transition> list_transitions_contain_alp = ListTransitions.FindAll(x => x.GetLabeledTransition().Contains(alp.Character));
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
                    if (used_transitions[i].GetLabeledTransition() != "_")
                    {
                        hasloop = true;
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
                    word += item.GetLabeledTransition();
                }
                word = word.Replace("_", "");
                words.Add(word);
                words = words.Distinct().ToList();
                return;
            }
            List<Transition> possible_transitions = AvailableTransition(current);
            for (int i = 0; i < possible_transitions.Count; i++)
            {
                Transition transition = possible_transitions[i];
                if (used_transitions.Contains(transition))
                {
                    if (transition.GetLabeledTransition() != "_")
                    {
                        hasloop = true;
                    }
                    possible_transitions.Remove(transition);
                }
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
    }
}
