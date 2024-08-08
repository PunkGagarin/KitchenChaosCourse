using Zenject;

namespace Gameplay.Shapes
{
    public class DiShape
    {

        [Inject]
        private IRectCalculator _calculator;
        
        public bool IsFirstGreater(MyRectangle first, MyRectangle second)
        {
           return  _calculator.IsFirstGreater(first, second);
        }
    }
}