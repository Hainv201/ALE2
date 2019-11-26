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
        string labeled_transition;
        public Transition(string labeled_Transition)
        {
            labeled_transition = labeled_Transition;
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

        public string GetLabeledTransition()
        {
            return labeled_transition;
        }
    }
}
