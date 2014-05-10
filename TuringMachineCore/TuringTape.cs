using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuringMachineCore
{
    public class TuringTape
    {
        private List<Char> positiveTape;
        private List<Char> negativeTape;
        private int position;

        public TuringTape(String input)
        {
            positiveTape = input.ToList();
            negativeTape = new List<Char>();
            position = 0;
        }

        public Char Read()
        {
            if (position >= 0)
            {
                return positiveTape[position]; 
            }
            else
            {
                return negativeTape[Math.Abs(position)];
            }
        }

        public void Write(Char symbol)
        {
            if (position >=0)
            {
                positiveTape[position] = symbol;
            }
            else
            {
                negativeTape[Math.Abs(position)] = symbol;
            }
        }
        public void Right()
        {
            position++;
            if (position == positiveTape.Count)
            {
                extend(positiveTape);
            }
        }
        public void Left()
        {
            position--;
            if (Math.Abs(position) == negativeTape.Count)
            {
                extend(negativeTape);
            }
        }
        private void extend(List<Char> tape)
        {
            tape.AddRange(new List<Char>("          "));
        }

        public string Print()
        {
            return new String(positiveTape.ToArray());
        }
    }
}