using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuringMachineCore
{
    public class TuringInstruction
    {
        public TuringOuput Output { get; private set;  }
        public TuringMove Move { get; private set; }
        public int NextState { get; private set; }
        public Char? OutputSymbol { get; private set; }

        public TuringInstruction(TuringOuput output, TuringMove move, int nextState, Char? outputsymbol = null)
        {
            Output = output;
            Move = move;
            NextState = nextState;
            OutputSymbol = outputsymbol;
        }
    }
}
