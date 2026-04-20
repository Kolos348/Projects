using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Threading.Tasks.Dataflow;

namespace Kompozyt
{
    public class Katalog(string name) : IFileManager
    {
        private readonly string name = name;
        private readonly List<IFileManager> entries = [];
        public string GetInfo(int indent)
        {
            var result = new string(' ', indent)
                + $"Kata: {name}, Rozmiar: {GetSize()} bytes\n";

            foreach (var entry in entries)
            {
                result += entry.GetInfo(indent + 2);
            }

            return result;
        }

        public string GetName()
        {
            throw new NotImplementedException();
        }

        public int GetSize()
        {
            int size = 0;
            foreach (IFileManager entry in entries)
            {
                size += entry.GetSize();
            }
            return size;
        }
        public void AddEntry(IFileManager entry)
        {
            if (entry == null || entry == this) return;
            //zapobiec dodaniu siebie do swojego dziecka
            entries.Add(entry);
        }
    }
}
