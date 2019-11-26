using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ALE2
{
    class Parser
    {
        List<State> states;
        List<Alphabet> alphabets;
        List<Transition> listTransitions;
        public bool IsDFA;
        List<string> listNotation;
        public Parser()
        {

        }

        public void ParsingFile(string fileName)
        {
            states = new List<State>();
            alphabets = new List<Alphabet>();
            listTransitions = new List<Transition>();
            FileStream fs = null;
            StreamReader sr = null;
            try
            {
                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                sr = new StreamReader(fs);
                string line = sr.ReadLine();
                while (line != null)
                {
                    string s = line.Replace(" ", "");
                    if (s != "" && s[0] != '#')
                    {
                        string first_word = s.Substring(0, s.IndexOf(':'));
                        if (first_word == "alphabet")
                        {
                            string the_rest = s.Substring(s.IndexOf(':') + 1);
                            foreach (char character in the_rest)
                            {
                                Alphabet alphabet = CheckExistAlphabet(character.ToString(), alphabets);
                                if (alphabet == null)
                                {
                                    alphabet = new Alphabet(character.ToString());
                                    alphabets.Add(alphabet);
                                }
                            }
                        }
                        if (first_word == "states")
                        {
                            string the_rest = s.Substring(s.LastIndexOf(':') + 1);
                            string[] state_names = the_rest.Split(',');
                            foreach (string name in state_names)
                            {
                                State state = CheckExistState(name, states);
                                if (state == null)
                                {
                                    state = new State(name);
                                    states.Add(state);
                                }
                            }
                        }
                        if (first_word == "final")
                        {
                            string the_rest = s.Substring(s.LastIndexOf(':') + 1);
                            string[] state_names = the_rest.Split(',');
                            foreach (string name in state_names)
                            {
                                foreach (State state in states)
                                {
                                    if (state.State_Name == name)
                                    {
                                        state.IsFinal = true;
                                    }
                                }
                            }
                        }
                        if (first_word == "transitions")
                        {
                            line = sr.ReadLine();
                            while (line != "end.")
                            {
                                s = line.Replace(" ", "");
                                string state_and_symbol = s.Substring(0, s.IndexOf('-'));
                                string left_state = state_and_symbol.Substring(0, state_and_symbol.IndexOf(','));
                                string symbol = state_and_symbol.Substring(state_and_symbol.IndexOf(',') + 1);
                                string right_state = s.Substring(s.IndexOf('>') + 1);
                                Transition transition = new Transition(symbol);
                                State Left = CheckExistState(left_state, states);
                                State Right = CheckExistState(right_state, states);
                                if (Left!= null && Right !=null)
                                {
                                    transition.SetLeftState(Left);
                                    transition.SetRightState(Right);
                                }
                                listTransitions.Add(transition);
                                line = sr.ReadLine();
                            }
                        }
                        if (first_word == "dfa")
                        {
                            string the_rest = s.Substring(s.LastIndexOf(':') + 1);
                            if (the_rest == "n")
                            {
                                IsDFA = false;
                            }
                            if (the_rest == "y")
                            {
                                IsDFA = true;
                            }
                            IsDFA = isDFA();
                        }
                        if (first_word == "finite")
                        {

                        }
                        if (first_word == "stack")
                        {

                        }
                        if (first_word == "words")
                        {
                            line = sr.ReadLine();
                            while (line != "end.")
                            {
                                s = line.Replace(" ", "");
                                line = sr.ReadLine();
                            }
                        }
                    }
                    line = sr.ReadLine();
                }
            }
            catch (IOException)
            {
                throw new IOException();
            }
            finally
            {
                if (sr != null)
                {
                    sr.Close();
                }
                if (fs != null)
                {
                    fs.Close();
                }

            }
        }

        public void ParsingPrefix(string prefix_notation)
        {
            listNotation = new List<string>();
            string token = "";
            bool Read = true;
            for (int i = 0; i < prefix_notation.Length; i++)
            {
                if (prefix_notation[i] == '(' || prefix_notation[i] == ')' || prefix_notation[i] == ',')
                {
                    Read = false;
                }
                if (prefix_notation[i] != '(' && prefix_notation[i] != ')' && prefix_notation[i] != ',')
                {
                    token += prefix_notation[i];
                }
                if (!Read && token!="")
                {
                    listNotation.Add(token);
                    Read = true;
                    token = "";
                }
            }
        }

        public Regular_Expression GetExpression()
        {
            Regular_Expression expression = null;
            string token = listNotation.First();
            switch (token)
            {
                case "|":
                    expression = new Choice();
                    listNotation.Remove(token);
                    expression.Left_Expression = GetExpression();
                    expression.Right_Expression = GetExpression();
                    break;
                case ".":
                    expression = new Concatenation();
                    listNotation.Remove(token);
                    expression.Left_Expression = GetExpression();
                    expression.Right_Expression = GetExpression();
                    break;
                case "_":
                    expression = new Epsilon();
                    listNotation.Remove(token);
                    break;
                case "*":
                    expression = new Repetition();
                    listNotation.Remove(token);
                    expression.Left_Expression = GetExpression();
                    break;
                default:
                    expression = new Alphabet(token);
                    listNotation.Remove(token);
                    break;
            }
            return expression;
        }
        private Alphabet CheckExistAlphabet(string character, List<Alphabet> alphabets)
        {
            if (alphabets.Any())
            {
                foreach (Alphabet alphabet in alphabets)
                {
                    if (alphabet.Character == character)
                    {
                        return alphabet;
                    }
                }
            }
            return null;
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

        public string CreateGraph()
        {
            string data = "digraph {" + Environment.NewLine + "rankdir = LR;" + Environment.NewLine + "\"\" [shape = none]";
            foreach (State state in states)
            {
                data += Environment.NewLine + state.CreateGraph();
            }
            data += Environment.NewLine;
            data += Environment.NewLine + $"\"\" -> \"" + states[0].State_Name + "\"";
            foreach (Transition transition in listTransitions)
            {
                data += Environment.NewLine + transition.CreateGraph();
            }
            data += Environment.NewLine + "}";
            return data;
        }

        private bool isDFA()
        {
            if (listTransitions.Exists(x => x.GetLabeledTransition().Contains("_")))
            {
                return false;
            }
            foreach (Alphabet alp in alphabets)
            {
                List<Transition> list_transitions_contain_alp = listTransitions.FindAll(x => x.GetLabeledTransition().Contains(alp.Character));
                List<State> list_states = new List<State>();
                foreach (Transition trans in list_transitions_contain_alp)
                {
                    list_states.Add(trans.GetLeftState());
                }
                bool are_2lists_equal = (list_states.All(states.Contains) && list_states.Count == states.Count);
                if (!are_2lists_equal)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
