namespace FiguresLand
{
    public enum Colors
    {
        Red,
        Green,
        Blue,
        Black,
        White,
        Yellow,
        Purple
    }
    public abstract class Figure
    {
        public Colors Color { get; set; }
        public double Area {  get; set; }
        protected Figure(Colors color) {
            Color = color;
        }
        public abstract double CalculateArea();
    }
}
