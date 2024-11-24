using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiguresLand
{
    public class Rectangle : Figure, IBuildBridge
    {
        public double Width { get; set; }
        public double Height { get; set; }
        public Rectangle(double width, double height, Colors color) : base(color)
        {
            Width = width;
            Height = height;
            base.Area = CalculateArea();
        }

        public override double CalculateArea()
        {
            return Width * Height;
        }

        public async Task BuildBridge(Decimal length)
        {
            await Task.Delay(3000);
            Console.WriteLine($"Прямоугольник, площадью {base.Area}, построил мост длинной {length} км.");
        }
    }
}
