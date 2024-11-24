using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiguresLand
{
    public class Circle : Figure, IDestroyStones
    {
        public double Radius { get; set; }

        public Circle(double radius, Colors color) : base(color)
        {
            Radius = radius;
            base.Area = CalculateArea();
        }
        public override double CalculateArea()
        {
            return Math.PI * Radius * Radius;
        }

        public async Task DestroyStones(Decimal speed)
        {
            await Task.Delay(10000);
            Console.WriteLine($"Круг площадью {base.Area} на скорости {speed} км/ч успешно раскатал все камни.");
        }
    }
}
