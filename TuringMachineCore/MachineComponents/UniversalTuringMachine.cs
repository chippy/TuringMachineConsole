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
        private TuringInstruction currentInstruction;
        private int state;
        private bool halted;
        private int position;

        public void Load(TuringTable table)
        {
            this.table = table;
        }

        public void Run(string input)
        {
            this.tape = new TuringTape(input);
            Reset();
            RunToEnd();
        }
        
        public void Step()
        {
            halted = false;
            currentSymbol = tape.Read(position);
            currentInstruction = table.GetInstruction(currentSymbol, state);
            if (currentInstruction != null)
            {
                Execute(currentInstruction);
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
                tape.Write(instruction.OutputSymbol.Value, position);
            }
            else if (instruction.Output == TuringOuput.Erase)
            {
                tape.Write(' ', position);
            }

            if (instruction.Move == TuringMove.Left)
            {
                position--;
            }
            else if (instruction.Move == TuringMove.Right)
            {
                position++;
            }

            state = instruction.NextState;
        }

        public string Print()
        {
            return tape.Print();
        }

        public void Reset()
        {
            position = 0;
            state = 0;
            tape.Initialise();
        }
    }
}
