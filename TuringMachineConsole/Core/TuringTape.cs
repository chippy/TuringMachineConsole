using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuringMachineConsole.Core
{
    public class TuringTape
    {
        private List<Char> positiveTape;
        private List<Char> negativeTape;
        private int position;

        public TuringTape(String aString)
        {
            positiveTape = aString.ToList();
            negativeTape = new List<Char>();
            position = 0;
        }

        public Char Read()
        {
            if (position >= 0)
            {
                return positiveTape[position] == null ? ' ' : positiveTape[position]; 
            }
            else
            {
                return negativeTape[Math.Abs(position)];
            }
        }

        public void Write(Char symbol)
        {
            if (position >=0 )
            {
                positiveTape[position] = symbol;
            }
        }
    }
}
