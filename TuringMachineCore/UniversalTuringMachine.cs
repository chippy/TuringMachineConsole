using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuringMachineCore
{
    public class UniversalTuringMachine
    {
        private TuringTable table;
        private TuringTape tape;
        private char currentSymbol;
        private TuringInstruction currenInstruction;
        private int state;
        private bool halted;

        public void Run(TuringTable table, string input)
        {

        }

        public void Step()
        {
            halted = false;
            currentSymbol = tape.Read();
            currenInstruction = table.GetInstruction(currentSymbol, state);
            if (currenInstruction != null)
            {
                Execute(currenInstruction);
            }
            else
            {
                halted = true;
            }
        }

        public void RunToEnd()
        {
            halted = false;
            while (halted == false)
            {
                Step();
            }
        }

        public void Execute(TuringInstruction instruction)
        {
            if (instruction.Output == TuringOuput.Write)
            {
                tape.Write(instruction.OutputSymbol.Value);
            }
            else if (instruction.Output == TuringOuput.Erase)
            {
                tape.Write(' ');
            }

            if (instruction.Move == TuringMove.Left)
            {
                tape.Left();
            }
            else if (instruction.Move == TuringMove.Right)
            {
                tape.Right();
            }

            state = instruction.NextState;
        }

        public string Print()
        {
            return tape.Print();
        }
    }
}
