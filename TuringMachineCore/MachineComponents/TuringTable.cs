using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuringMachineCore
{
    [Serializable]
    public class TuringTable
    {
        private List<Dictionary<Char, TuringInstruction>> table;

        public TuringTable(IEnumerable<Char> symbols, int states)
        {
            table = new List<Dictionary<Char, TuringInstruction>>();
            for (int i = 0; i < states; i++)
            {
                Dictionary<Char, TuringInstruction> tableRow = new Dictionary<char, TuringInstruction>();
                table.Add(tableRow);
            }
        }

        public TuringInstruction GetInstruction(Char symbol, int state)
        {
            return table[state][symbol];
        }

        public void SetInstruction(Char symbol, int state, TuringInstruction instruction)
        {
            table[state][symbol] = instruction;
        }
    }
}
