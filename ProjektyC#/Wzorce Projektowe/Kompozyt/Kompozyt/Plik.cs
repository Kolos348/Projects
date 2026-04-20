using System;
using System.Collections.Generic;
using System.Text;

namespace Kompozyt
{
    public class Plik(string name, int size) : IFileManager
    {
        private readonly string name = name;
        private readonly int size = size;

        public string GetInfo(int indent)
        {
            return 
                new string(' ',indent)+$"Name: {name},Size: {size} bytes\n";
        }

        public string GetName()
        {
            return name;
        }

        public int GetSize()
        {
            return size;
        }

     
    }
}
