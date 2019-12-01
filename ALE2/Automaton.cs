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
        public List<Word> ListWords { get; set; }
        public string Comment { get; set; }
        public bool IsDFA { get; }
        public Automaton(List<Alphabet> alphabets, List<State> states, List<Transition> transitions)
        {
            ListAlphabets = alphabets;
            ListStates = states;
            ListTransitions = transitions;
            ListWords = new List<Word>();
            IsDFA = isDFA();
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
            string content = "";
            content += $"# {Comment}" + Environment.NewLine + Environment.NewLine;
            content += $"alphabet: {String.Join("", ListAlphabets)}" + Environment.NewLine;
            content += $"states: {String.Join(",", ListStates)}" + Environment.NewLine;
            content += $"final: {String.Join(",", ListStates.FindAll(x => x.IsFinal))}" + Environment.NewLine + Environment.NewLine;
            content += $"transitions:" + Environment.NewLine;
            content += $"{String.Join(Environment.NewLine, ListTransitions)}" + Environment.NewLine;
            content += "end." + Environment.NewLine + Environment.NewLine;
            content += $"dfa: ";
            if (IsDFA)
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
                if (word.IsWordAccepted(ListStates, ListTransitions))
                {
                    content += word.Words + ",y" + Environment.NewLine;
                }
                else
                {
                    content += word.Words + ",n" + Environment.NewLine;
                }
            }
            content += "end.";
            string file_name = "automaton_" + DateTime.Now.ToString("yyyymmdd_HHmmss") + ".txt";
            File.WriteAllText(file_name, content);
        }
    }
}
