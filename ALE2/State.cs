using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE2
{
    class State
    {
        public bool IsFinal;
        public bool IsInitial;
        public string State_Name { get; private set; }
        public State(string statename)
        {
            IsFinal = false;
            State_Name = statename;
            IsInitial = false;
        }

        public string CreateGraph()
        {
            if (IsFinal)
            {
                return $"{State_Name}" + " [shape = doublecircle]";
            }
            return $"{State_Name}" + " [shape = circle]";
        }

        public override string ToString()
        {
            return State_Name;
        }
    }
}
