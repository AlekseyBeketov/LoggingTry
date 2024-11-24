using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiguresLand
{
    internal interface IDestroyPit
    {
        public Task DestroyPit(Decimal deep);
    }
}
