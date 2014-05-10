using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace TuringMachineCore
{
    public class TuringTableSerializer
    {
        private string MachinePath;

        public TuringTableSerializer()
        {
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            MachinePath = Path.Combine(appDataPath, "TuringMachines");

            if (!Directory.Exists(MachinePath))
            {
                Directory.CreateDirectory(MachinePath);
            }
        }

        public void SaveTable(TuringTable table, string name)
        {
            string fullPath = Path.Combine(MachinePath, name + ".trg");
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(fullPath, FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, table);
            stream.Close();
        }
        
        public TuringTable LoadTable(string name)
        {
            string fullPath = Path.Combine(MachinePath, name + ".trg");
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(fullPath, FileMode.Open, FileAccess.Read, FileShare.Read);
            TuringTable table = (TuringTable)formatter.Deserialize(stream);
            stream.Close();
            return table;
        }
        public IEnumerable<String> ListTables()
        {
            string[] tableFiles = Directory.GetFiles(MachinePath, "*.trg");
            return tableFiles.Select(f => Path.GetFileNameWithoutExtension(f));
        }
    }
}
