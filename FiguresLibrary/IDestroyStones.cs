using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiguresLand
{
    internal interface IDestroyStones
    {
        public Task DestroyStones(Decimal speed);
    }
}
