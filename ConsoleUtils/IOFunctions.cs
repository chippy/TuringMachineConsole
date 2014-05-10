using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUtils
{
    public class IOFunctions
    {
        public static char GetRestrictedKeyChoice(IEnumerable<Char> characters, string prompt)
        {
            IEnumerable<ConsoleKey> keys = characters.Select(c => (ConsoleKey)Enum.Parse(typeof(ConsoleKey), c.ToString().ToUpper()));

            Console.Write(prompt + " (" + String.Join(",", characters + "):"));
            
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);
            }
            while (!keys.Contains(key.Key));

            Console.WriteLine(key.KeyChar);
            return key.KeyChar;
        }

        public static bool GetYesOrNo(string prompt)
        {
            char choice = GetRestrictedKeyChoice("yn", prompt);
            return choice == 'y';
        }

        public static int GetNumber(string prompt)
        {
            Console.Write(prompt);

            int number;
            bool numberEntered = false;
            do
            {
                string input = Console.ReadLine();
                numberEntered = int.TryParse(input, out number);
                if (!numberEntered)
                {
                    Console.Write("Please enter a number: ");
                }
            }
            while (!numberEntered);

            return number;
        }
    }
}
