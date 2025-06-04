using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_ServerApp
{
    public interface IRequestHandler
    {
        bool CanHandle(string request);
        string Handle(string request);
    }
}
