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
        private string originalInput;

        public TuringTape(String input)
        {
            originalInput = input;
            Initialise();
        }

        public void Initialise()
        {
            positiveTape = originalInput.ToList();
            negativeTape = new List<Char>();
        }

        public Char Read(int position)
        {
            if (position >= 0)
            {
                while (position >= positiveTape.Count)
                {
                    positiveTape.Add(' ');
                }
                return positiveTape[position];
            }
            else
            {
                while (Math.Abs(position) >= negativeTape.Count)
                {
                    negativeTape.Add(' ');
                }
                return negativeTape[Math.Abs(position)];
            }
        }

        public void Write(Char symbol, int position)
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
        public string Print()
        {
            return new String(positiveTape.ToArray());
        }
    }
}