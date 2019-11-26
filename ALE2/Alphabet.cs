﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE2
{
    class Alphabet:Regular_Expression
    {
        public string Character { get; private set; }
        public Alphabet(string character):base()
        {
            this.Character = character;
        }

        public override string ToString()
        {
            return Character;
        }
    }
}
