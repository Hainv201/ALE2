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
        List<State> listStates;
        List<Alphabet> listAlphabets;
        List<Transition> listTransitions;
        List<Word> listWords;
        public bool IsDFA;
        List<string> listNotation;
        string comment;
        public Automaton Automaton;
        public Parser(){}

        public void ParsingFile(string fileName)
        {
            listStates = new List<State>();
            listAlphabets = new List<Alphabet>();
            listTransitions = new List<Transition>();
            listWords = new List<Word>();
            FileStream fs = null;
            StreamReader sr = null;
            try
            {
                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                sr = new StreamReader(fs);
                string line = sr.ReadLine();
                int count_line = 1;
                while (line != null)
                {
                    string s = line.Replace(" ", "");
                    if (s != "" && s[0] != '#')
                    {
                        string first_word = s.Substring(0, s.IndexOf(':'));
                        if (first_word == "alphabet")
                        {
                            string the_rest = s.Substring(s.IndexOf(':') + 1);
                            if (the_rest == "")
                            {
                                throw new InvalidValueInFileException($"Invalid value at line {count_line}");
                            }
                            foreach (char character in the_rest)
                            {
                                Alphabet alphabet = CheckExistAlphabet(character.ToString(), listAlphabets);
                                if (alphabet == null)
                                {
                                    alphabet = new Alphabet(character.ToString());
                                    listAlphabets.Add(alphabet);
                                }
                            }
                        }
                        if (first_word == "states")
                        {
                            string the_rest = s.Substring(s.LastIndexOf(':') + 1);
                            if (the_rest == "")
                            {
                                throw new InvalidValueInFileException($"Invalid value at line {count_line}");
                            }
                            string[] state_names = the_rest.Split(',');
                            foreach (string name in state_names)
                            {
                                State state = CheckExistState(name, listStates);
                                if (state == null)
                                {
                                    state = new State(name);
                                    listStates.Add(state);
                                }
                            }
                            listStates[0].IsInitial = true;
                        }
                        if (first_word == "final")
                        {
                            string the_rest = s.Substring(s.LastIndexOf(':') + 1);
                            if (the_rest == "")
                            {
                                throw new InvalidValueInFileException($"Invalid value at line {count_line}");
                            }
                            string[] state_names = the_rest.Split(',');
                            foreach (string name in state_names)
                            {
                                foreach (State state in listStates)
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
                            count_line++;
                            while (line != "end.")
                            {
                                s = line.Replace(" ", "");
                                string state_and_symbol = s.Substring(0, s.IndexOf('-'));
                                string left_state = state_and_symbol.Substring(0, state_and_symbol.IndexOf(','));
                                string symbol = state_and_symbol.Substring(state_and_symbol.IndexOf(',') + 1);
                                string right_state = s.Substring(s.IndexOf('>') + 1);
                                if (left_state == ""|| symbol == ""|| right_state == "")
                                {
                                    throw new InvalidValueInFileException($"Invalid transition at line {count_line}");
                                }
                                Transition transition = new Transition(symbol);
                                State Left = CheckExistState(left_state, listStates);
                                State Right = CheckExistState(right_state, listStates);
                                if (Left!= null && Right !=null)
                                {
                                    transition.SetLeftState(Left);
                                    transition.SetRightState(Right);
                                }
                                listTransitions.Add(transition);
                                line = sr.ReadLine();
                                count_line++;
                            }
                        }
                        if (first_word == "dfa")
                        {
                            string the_rest = s.Substring(s.LastIndexOf(':') + 1);
                            if (the_rest == "n")
                            {
                                IsDFA = false;
                            }
                            else if (the_rest == "y")
                            {
                                IsDFA = true;
                            }
                            else
                            {
                                throw new InvalidValueInFileException($"Invalid value at line {count_line}");
                            }
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
                            count_line++;
                            while (line != "end.")
                            {
                                s = line.Replace(" ", "");
                                string[] data = s.Split(',');
                                string words = data[0];
                                string indication = data[1];
                                if (DoesWordContainIncorrectCharacter(words,listAlphabets))
                                {
                                    throw new InvalidValueInFileException($"Invalid value at line {count_line}");
                                }
                                Word word = new Word(words);
                                if (indication == "y")
                                {
                                    word.IsAccepted = true;
                                }
                                else if (indication == "n")
                                {
                                    word.IsAccepted = false;
                                }
                                else
                                {
                                    throw new InvalidValueInFileException($"Invalid value at line {count_line}");
                                }
                                word.IsAccepted = word.IsWordAccepted(listStates, listTransitions);
                                listWords.Add(word);
                                line = sr.ReadLine();
                                count_line++;
                            }
                        }
                    }
                    else if (s != "" && s[0] == '#')
                    {
                        comment = s.Substring(1);
                    }
                    line = sr.ReadLine();
                    count_line++;
                }
                Automaton = new Automaton(listAlphabets, listStates, listTransitions, listWords);
                Automaton.Comment = comment;
                IsDFA = Automaton.IsDFA;
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
            prefix_notation = prefix_notation.Replace(" ", "");
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

        private bool DoesWordContainIncorrectCharacter(string words, List<Alphabet> alphabets)
        {
            string clone_words = words.Clone().ToString();
            foreach (Alphabet alphabet in alphabets)
            {
                clone_words = clone_words.Replace(alphabet.Character, "");
            }
            clone_words = clone_words.Replace("_", "");
            if (clone_words == "")
            {
                return false;
            }
            return true;
        }
    }
}
