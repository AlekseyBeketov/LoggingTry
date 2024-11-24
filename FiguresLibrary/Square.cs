using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiguresLand
{
    public class Square : Figure, IDestroyPit
    {
        public double Width { get; set; }
        public Square(double width,Colors color) : base(color)
        {
            Width = width;
            base.Area = CalculateArea();
        }

        public override double CalculateArea()
        {
            return Width * Width;
        }
        public async Task DestroyPit(Decimal deep)
        {
            await Task.Delay(5000);
            Console.WriteLine($"Квадрат, площадью {base.Area}, укатал ямы глубиной до {deep * 2} м. и создал прочную структуру на равнине.");
        }
    }
}
