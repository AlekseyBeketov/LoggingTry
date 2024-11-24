using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiguresLand
{
    internal interface IBuildBridge
    {
        public Task BuildBridge(Decimal length);
    }
}
