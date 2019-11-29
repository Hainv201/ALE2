using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE2
{
    class InvalidValueInFileException:Exception
    {
        public InvalidValueInFileException() : base()
        {
        }
        public InvalidValueInFileException(string message) : base(message) { }
    }
}
