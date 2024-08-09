using System;

namespace Gameplay.Shapes
{
    public interface IRectCalculator
    {
        bool IsFirstGreater(MyRectangle first, MyRectangle second);

        public event Action<int> OnTemperatureChange;
    }
}