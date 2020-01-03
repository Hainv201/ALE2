using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE2
{
    class WordComparer : IEqualityComparer<Word>
    {
        public bool Equals(Word x, Word y)
        {
            return x.Words == y.Words;
        }

        public int GetHashCode(Word obj)
        {
            return obj.Words.GetHashCode();
        }
    }
}
