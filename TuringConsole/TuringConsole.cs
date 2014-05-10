using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleUtils;
using TuringMachineCore;

namespace TuringConsole
{
    public class TuringConsole
    {
        private UniversalTuringMachine machine;
        private TuringTable table;
        private IOFunctions consoleIO;
        private TuringTableSerializer serializer;

        public TuringConsole()
        {
            consoleIO = new IOFunctions();
            serializer = new TuringTableSerializer();
            machine = new UniversalTuringMachine();
        }
        
        public void MainMenu()
        {
            Console.Clear();
            Console.WriteLine("1. Load machine");
            Console.WriteLine("2. Save machine");
            Console.WriteLine("3. Create machine");
            Console.WriteLine("4. Run machine");
            Console.WriteLine("5. Run in interactive mode");
            Console.WriteLine("6. Exit");
            Console.WriteLine();
            int choice = consoleIO.GetNumber("Choice: ");

            switch (choice)
            {
                case 1:
                    LoadMachine();
                    break;
                case 2:
                    SaveMachine();
                    break;
                case 3:
                    CreateMachine();
                    break;
                case 4:
                    RunMachine();
                    break; 
                case 6:
                    Environment.Exit(0);
                    break;
                case 5:
                    RunInteractive();
                    break;
                default:
                    break;
            }
            MainMenu();
        }

        public void CreateMachine()
        {
            Console.Clear();
            string symbols = GetSymbols();
            int states = GetStates();

            table = new TuringTable(symbols, states);
            FillTable(table, symbols, states);    
        }

        public void RunMachine()
        {
            Console.Clear();
            machine.Load(table);
            Console.WriteLine("Input:");
            string input = Console.ReadLine();
            machine.Run(input);
            Console.WriteLine(machine.Print());
            Console.ReadLine();
        }

        public void RunInteractive()
        {
            Console.Clear();
            machine.Load(table);
            Console.WriteLine("Input:");
            string input = Console.ReadLine();
            machine.LoadTape(input);
            machine.Reset();
            Console.WriteLine(machine.Print());
            PrintPointer(machine.GetPosition());
            while (machine.Halted() == false)
            {
                machine.Step();
                Console.WriteLine(machine.Print());
                PrintPointer(machine.GetPosition());
                Console.ReadLine();
            }
            Console.WriteLine("Done!");
            Console.ReadLine();
            
        }

        public void PrintPointer(int position)
        {
            for (int i = 0; i < position; i++)
            {
                Console.Write(" ");            
            }
            Console.WriteLine("^");
        }

        public void SaveMachine()
        {
            Console.Clear();
            Console.WriteLine("Name: ");
            string name = Console.ReadLine();
            serializer.SaveTable(table, name);
        }

        public void LoadMachine()
        {
            Console.Clear();
            IEnumerable<string> machines = serializer.ListTables();
            Console.WriteLine("Tables:");
            Console.WriteLine("-------");
            foreach (string machine in machines)
            {
                Console.WriteLine(machine);
            }
            Console.WriteLine();
            Console.WriteLine("Name: ");
            string nameToLoad = Console.ReadLine();
            table = serializer.LoadTable(nameToLoad);
        }

        private TuringOuput GetOutput()
        {
            char choice = consoleIO.GetRestrictedKeyChoice("wen", "Write, erase or nothing");
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

        private TuringMove GetMovement()
        {
            char choice = consoleIO.GetRestrictedKeyChoice("lrs", "Left, right, or stay");
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

        private int GetStates()
        {
            return consoleIO.GetNumber("How many states does your machine have? ");
        }

        private int GetNextState()
        {
            return consoleIO.GetNumber("Next state? ");
        }

        public string GetSymbols()
        {
            Console.Write("Enter all non-blank symbols: ");
            return Console.ReadLine() + " ";
        }

        public char GetOutputSymbol()
        {
            Console.Write("Enter the symbol to write: ");
            char key = Console.ReadKey().KeyChar;
            Console.WriteLine();
            return key;
        }

        public TuringInstruction GetInstruction()
        {
            if (consoleIO.GetYesOrNo("Halt?"))
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

        public void FillTable(TuringTable table, string symbols, int states)
        {
            Console.WriteLine();
            Console.WriteLine("Instructions");
            Console.WriteLine("------------");

            for (int state = 0; state < states; state++)
            {
                foreach (Char symbol in symbols)
                {
                    Console.WriteLine(String.Format("State: {0} Symbol:'{1}'", state, symbol));
                    table.SetInstruction(symbol, state, GetInstruction());
                    Console.WriteLine();
                }
            }
        }
    }
}
