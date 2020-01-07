using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE2
{
    class Transition
    {
        private State left_State;
        private State right_State;
        private Symbol symbol;
        private Stack pop_Stack;
        private Stack push_Stack;
        string labeled_transition;
        public Transition(string labeled_Transition, List<Stack> stacks)
        {
            labeled_transition = labeled_Transition;
            if (stacks == null)
            {
                stacks = new List<Stack>();
            }
            ParseLabeledTransition(stacks);
        }

        public void SetLeftState(State state)
        {
            left_State = state;
        }
        public void SetRightState(State state)
        {
            right_State = state;
        }

        public State GetLeftState()
        {
            return left_State;
        }

        public State GetRightState()
        {
            return right_State;
        }

        public string CreateGraph()
        {
            return $"\""+left_State.State_Name+"\" -> \""+right_State.State_Name+$"\"[label = \"{labeled_transition}\"]";
        }

        public Symbol GetSymbol()
        {
            return symbol;
        }

        public Stack GetPopStack()
        {
            return pop_Stack;
        }

        public Stack GetPushStack()
        {
            return push_Stack;
        }

        public override string ToString()
        {
            return $"{left_State},{labeled_transition} --> {right_State}";
        }

        private void ParseLabeledTransition(List<Stack> stacks)
        {
            if (labeled_transition.Contains("["))
            {
                if (stacks.Count == 0)
                {
                    throw new InvalidValueInFileException("Invalid File. Missing Stack");
                }
                if (!labeled_transition.Contains(",")|| !labeled_transition.Contains("]"))
                {
                    throw new InvalidValueInFileException("Incorrect format for transitions");
                }
                int indexofcomma = labeled_transition.IndexOf(",");
                int indexofleftbracket = labeled_transition.IndexOf("[");
                int indexofrightbracket = labeled_transition.IndexOf("]");
                string symbol_character = labeled_transition.Substring(0, indexofleftbracket);
                string pop_stack_character = labeled_transition.Substring(indexofleftbracket + 1, indexofcomma - indexofleftbracket - 1);
                string push_stack_character = labeled_transition.Substring(indexofcomma + 1, indexofrightbracket - indexofcomma - 1);
                if (symbol_character == "" || pop_stack_character == "" || push_stack_character == "")
                {
                    throw new InvalidValueInFileException("Incorrect format for transitions");
                }
                symbol = new Symbol(symbol_character);

                pop_Stack = GetStack(pop_stack_character,stacks);
                push_Stack = GetStack(push_stack_character,stacks);
            }
            else
            {
                symbol = new Symbol(labeled_transition);
            }
        }

        private Stack GetStack(string character, List<Stack> stacks)
        {
            if (stacks.Any())
            {
                foreach (Stack stack in stacks)
                {
                    if (stack.Character == character)
                    {
                        return stack;
                    }
                }
            }
            return null;
        }
    }
}
