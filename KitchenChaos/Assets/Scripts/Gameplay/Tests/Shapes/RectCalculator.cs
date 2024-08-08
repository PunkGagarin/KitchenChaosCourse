namespace Gameplay.Shapes
{
    public class RectCalculator : IRectCalculator
    {

        // private MyRectangle _first = new MyRectangle(2, 3);
        // private MyRectangle _second = new MyRectangle(4, 6);

        public bool IsFirstGreater(MyRectangle first, MyRectangle second)
        {
            return first.IsGreaterThan(second);
        }
    }
}