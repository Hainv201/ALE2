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
        private Dictionary<Alphabet, List<State>> reachable_State;
        private List<State> EpsilonClosure = new List<State>();
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

        public Dictionary<Alphabet, List<State>> ReachableState()
        {
            return reachable_State;
        }
        public void GetReachableState(List<Alphabet> alphabets, List<Transition> transitions)
        {
            reachable_State = new Dictionary<Alphabet, List<State>>();
            foreach (Alphabet alp in alphabets)
            {
                List<State> states = new List<State>();
                var usable_transitions = transitions.FindAll(x => x.GetLeftState() == this && x.GetSymbol().Label == alp.Character);
                foreach (var t in usable_transitions)
                {
                    states.Add(t.GetRightState());
                }
                reachable_State.Add(alp,states);
            }
        }

        public List<State> GetEpsilonClosure(List<Transition> transitions, List<Transition> processedtransitions)
        {
            var usable_transitions = transitions.FindAll(x => x.GetLeftState() == this && x.GetSymbol().Label == "_");
            usable_transitions = usable_transitions.Except(processedtransitions).ToList();
            if (usable_transitions.Count == 0)
            {
                EpsilonClosure = EpsilonClosure.Distinct().ToList();
                return EpsilonClosure;
            }
            foreach (var t in usable_transitions)
            {
                var next_state = t.GetRightState();
                EpsilonClosure.Add(this);
                processedtransitions.Add(t);
                next_state.GetEpsilonClosure(transitions,processedtransitions);
                EpsilonClosure.AddRange(next_state.EpsilonClosure);
            }
            EpsilonClosure = EpsilonClosure.Distinct().ToList();
            return EpsilonClosure;
        }
    }
}
