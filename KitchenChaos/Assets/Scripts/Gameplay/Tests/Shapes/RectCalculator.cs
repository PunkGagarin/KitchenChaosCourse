using System;

namespace Gameplay.Shapes
{
    public class RectCalculator : IRectCalculator
    {

        
        public event Action<int> OnTemperatureChange = delegate { };

        public bool IsFirstGreater(MyRectangle first, MyRectangle second)
        {
            return first.IsGreaterThan(second);
        }
    }
}