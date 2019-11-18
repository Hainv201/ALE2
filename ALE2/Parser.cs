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
        public Parser(string file_Name)
        {
            ParsingFile(file_Name);
        }

        private void ParsingFile(string fileName)
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
                string s = sr.ReadLine();
                while (s != null)
                {
                    string first_word = Regex.Replace(s.Split()[0], @"[^0-9a-zA-Z\ ]+", "");
                    if (first_word == "alphabet")
                    {
                        string the_rest = s.Substring(s.LastIndexOf(':') + 2);
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
                        string the_rest = s.Substring(s.LastIndexOf(':') + 2);
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
                        string the_rest = s.Substring(s.LastIndexOf(':') + 2);
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
                        s = sr.ReadLine();
                        while (s != "end.")
                        {
                            string state_and_symbol = s.Substring(0, s.IndexOf('-') - 1);
                            string left_state = state_and_symbol.Substring(0, state_and_symbol.IndexOf(','));
                            string symbol = state_and_symbol.Substring(state_and_symbol.IndexOf(',') + 1);
                            string right_state = s.Substring(s.IndexOf('>') + 2);
                            Transition transition = new Transition(symbol);
                            State Left = CheckExistState(left_state, states);
                            State Right = CheckExistState(right_state, states);
                            if (Left!= null && Right !=null)
                            {
                                transition.SetLeftState(Left);
                                transition.SetRightState(Right);
                            }
                            listTransitions.Add(transition);
                            s = sr.ReadLine();
                        }
                    }
                    if (first_word == "dfa")
                    {
                        string the_rest = s.Substring(s.LastIndexOf(':') + 2);
                        if (the_rest == "n")
                        {
                            IsDFA = false;
                        }
                        if (the_rest == "y")
                        {
                            IsDFA = true;
                        }
                    }
                    s = sr.ReadLine();
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
    }
}
