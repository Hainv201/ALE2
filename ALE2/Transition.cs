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
        string alphabet_symbol;
        public Transition(string symbol)
        {
            alphabet_symbol = symbol;
        }

        public void SetLeftState(State state)
        {
            left_State = state;
        }
        public void SetRightState(State state)
        {
            right_State = state;
        }

        public string CreateGraph()
        {
            return $"\""+left_State.State_Name+"\" -> \""+right_State.State_Name+$"\"[label = \"{alphabet_symbol}\"]";
        }
    }
}
