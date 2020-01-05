using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE2
{
    class Symbol
    {
        public string Label { get; private set; }
        public Symbol(string label)
        {
             Label = label;
        }
    }
}
