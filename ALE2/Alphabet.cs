using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE2
{
    class Alphabet
    {
        public string Character { get; private set; }
        public Alphabet(string character)
        {
            this.Character = character;
        }
    }
}
