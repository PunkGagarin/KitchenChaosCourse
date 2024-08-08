using System.Drawing;
using System.Numerics;

namespace Gameplay.Shapes
{
    public class MyRectangle
    {
        private readonly float _length;
        private readonly float _breadth;
        private readonly Vector3 _position;

        public MyRectangle(float length, float breadth)
        {
            _length = length;
            _breadth = breadth;
            _position = Vector3.Zero;
        }

        public float Area()
        {
            return _length * _breadth;
        }

        public float Perimeter()
        {
            return 2 * (_length + _breadth);
        }

        public bool IsInside(float x, float y)
        {
            return InRange(_position.X, _breadth, x) &&
                   InRange(_position.Y, _length, y);
        }

        private static bool InRange(float center, float width, float point)
        {
            return center - width / 2 < point && center + width / 2 > point;
        }
        
        public bool IsGreaterThan(MyRectangle shape)
        {
            return Area() > shape.Area();
        }
    }
}