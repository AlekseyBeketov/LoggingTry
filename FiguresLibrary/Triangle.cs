using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiguresLand
{
    public class Triangle : Figure, IFixStable
    {
        public double Width { get; set; }
        public double Height { get; set; }
        public Triangle(double width, double height, Colors color) : base(color)
        {
            Width = width;
            Height = height;
            base.Area = CalculateArea();
        }

        public override double CalculateArea()
        {
            return Width / 2 * Height;
        }

        public async Task FixStable()
        {
            await Task.Delay(7000);
            Console.WriteLine($"Треугольник, площадью {base.Area}, укрепил нестабильные конструкции на горе.");
        }
    }
}
