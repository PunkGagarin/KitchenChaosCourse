using System;
using UnityEngine;
using Zenject;

namespace Gameplay.Shapes
{
    public class ShapeSystem
    {
        [Inject]
        private IRectCalculator _calculator;


        public bool IsFirstGreater(MyRectangle first, MyRectangle second)
        {
            //logic
            return _calculator.IsFirstGreater(first, second);
        }

    }


}