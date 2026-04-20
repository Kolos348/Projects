using System;
using System.Collections.Generic;
using System.Text;

namespace Kompozyt
{
    public interface IFileManager
    {
        string GetInfo(int indent);
        string GetName();
        int GetSize();
    }
}
