using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuringMachineCore
{
    public class UniversalTuringMachine
    {
        public bool Halted { get; private set; }
        public int Position { get; private set; }
        
        private TuringTable table;
        private TuringTape tape;
        private char currentSymbol;
        private TuringInstruction currentInstruction;
        private int state;

        public void Load(TuringTable table)
        {
            this.table = table;
        }

        public void Run(string input)
        {
            LoadTape(input);
            RunToEnd();
        }
        
        public void Step()
        {
            Halted = false;
            currentSymbol = tape.Read(Position);
            currentInstruction = table.GetInstruction(currentSymbol, state);
            if (currentInstruction != null)
            {
                Execute(currentInstruction);
            }
            else
            {
                Halted = true;
            }
        }

        public void RunToEnd()
        {
            Halted = false;
            while (Halted == false)
            {
                Step();
            }
        }

        public void Execute(TuringInstruction instruction)
        {
            if (instruction.Output == TuringOuput.Write)
            {
                tape.Write(instruction.OutputSymbol.Value, Position);
            }
            else if (instruction.Output == TuringOuput.Erase)
            {
                tape.Write(' ', Position);
            }

            if (instruction.Move == TuringMove.Left)
            {
                Position--;
            }
            else if (instruction.Move == TuringMove.Right)
            {
                Position++;
            }

            state = instruction.NextState;
        }

        public string Print()
        {
            return tape.Print();
        }

        public void Reset()
        {
            Position = 0;
            state = 0;
            tape.Initialise();
            Halted = false;
        }

        public void LoadTape(string input)
        {
            this.tape = new TuringTape(input);
            Reset();
        }
    }
}
