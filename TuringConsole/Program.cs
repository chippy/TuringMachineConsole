using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuringMachineCore;

namespace TuringConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            string symbols = GetSymbols();
            int states = GetStates();

            TuringTable table = new TuringTable(symbols, states);
            FillTable(table, symbols, states);
            UniversalTuringMachine machine = new UniversalTuringMachine();
            machine.Load(table);
            do
            {
                Console.WriteLine("Input:" );
                string input = Console.ReadLine();
                machine.Run(input);
                Console.WriteLine(machine.Print());
                Console.ReadLine();
            }
            while (ConsoleUtils.IOFunctions.GetYesOrNo("Again? "));
        }

        private static TuringOuput GetOutput()
        {
            char choice = ConsoleUtils.IOFunctions.GetRestrictedKeyChoice("wen", "Write, erase or nothing");
            switch (choice)
            {
                case 'w':
                    return TuringOuput.Write;
                case 'e':
                    return TuringOuput.Erase;
                default:
                    return TuringOuput.Nothing;
            }
        }

        private static TuringMove GetMovement()
        {
            char choice = ConsoleUtils.IOFunctions.GetRestrictedKeyChoice("lrs", "Left, right, or stay");
            switch (choice)
            {
                case 'l':
                    return TuringMove.Left;
                case 'r': 
                    return TuringMove.Right;
                default:
                    return TuringMove.Stay;
            }
        }

        private static int GetStates()
        {
            return ConsoleUtils.IOFunctions.GetNumber("How many states does your machine have? ");
        }

        private static int GetNextState()
        {
            return ConsoleUtils.IOFunctions.GetNumber("Next state? ");
        }

        public static string GetSymbols()
        {
            Console.Write("Enter all non-blank symbols: ");
            return Console.ReadLine() + " ";
        }

        public static char GetOutputSymbol()
        {
            Console.Write("Enter the symbol to write: ");
            char key = Console.ReadKey().KeyChar;
            Console.WriteLine();
            return key;
        }

        public static TuringInstruction GetInstruction()
        {
            if (ConsoleUtils.IOFunctions.GetYesOrNo("Halt?"))
            {
                return null;
            }

            TuringOuput output = GetOutput();

            char? symbol = null;
            if (output == TuringOuput.Write)
            {
                symbol = GetOutputSymbol();
            }

            TuringMove move = GetMovement();
            int nextState = GetNextState();

            return new TuringInstruction(output, move, nextState, symbol);
        }

        public static void FillTable(TuringTable table, string symbols, int states)
        {
            Console.WriteLine();
            Console.WriteLine("Instructions");
            Console.WriteLine("------------");
            
            for (int state= 0; state < states; state++)
            {
                foreach (Char symbol in symbols)
	            {
		            Console.WriteLine(String.Format("State: {0} Symbol:'{1}'", state, symbol));
                    table.SetInstruction(symbol, state, GetInstruction());
	            }
            }
        }
    }
}
