using Repository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IGetDBConfig
    {
        bool TrySave(string path, DBConfig dbConfig, out string error);
        bool TryLoad(string path, out DBConfig dBConfig, out string error);
    }
}
