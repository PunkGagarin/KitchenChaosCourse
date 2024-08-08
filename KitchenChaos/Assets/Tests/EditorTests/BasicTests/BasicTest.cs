using Gameplay.Shapes;
using NUnit.Framework;

namespace Tests.EditorTests.BasicTests
{
    public class BasicTest
    {
        
    
        private static object[] _cases = {
            new object[]{ new MyRectangle(1, 1), new MyRectangle(1, 1), false},
            new object[]{ new MyRectangle(2, 1), new MyRectangle(3, 1), false},
            new object[]{ new MyRectangle(2, 2), new MyRectangle(1, 3), true},
            new object[]{ new MyRectangle(5, 4), new MyRectangle(6, 3), true}
        };
    
        [TestCaseSource(nameof(_cases))]
        public void CheckWhichRectangleIsBigger(
            MyRectangle first, MyRectangle second, bool isGreater)
        {
            Assert.AreEqual(isGreater, first.IsGreaterThan(second));
        }
    
        private static MyRectangle[] _rectangles =
        {
            new(2, 3),
            new(4, 2),
            new(1, 5),
            new(7, 3)
        };

        [Test, Sequential]
        public void TestPerimeterOfARectangle(
            [ValueSource(nameof(_rectangles))] MyRectangle rectangle, 
            [Values(10, 12, 12, 20)] float perimeter)
        {
            Assert.AreEqual(perimeter, rectangle.Perimeter());
        }
    

        [TestCase(2, 3, 6)]
        [TestCase(7, 5, 35)]
        public void TestAreaOfRectangleWithLength2AndBreadth3(float length, float breadth,
            float expectedArea)
        {
            MyRectangle rectangle = new MyRectangle(length, breadth);
            Assert.AreEqual(expectedArea, rectangle.Area());
        }

        [TestCase(0, 0, true)]
        [TestCase(3, 3, false)]
        public void TestIfPointIsInsideRectangle(float x, float y,
            bool isInside)
        {
            const float length = 3;
            const float breadth = 5;

            MyRectangle rectangle = new MyRectangle(length, breadth);

            Assert.AreEqual(isInside, rectangle.IsInside(x, y));
        }
    
        [TestCase(0, 0, true)]
        [TestCase(3, 3, false)]
        public void TestIfPointIsInsideRectangle2(float x, float y,
            bool isInside)
        {
            const float length = 3;
            const float breadth = 5;

            MyRectangle rectangle = new MyRectangle(length, breadth);

            Assert.AreEqual(isInside, rectangle.IsInside(x, y));
        }

        [Test]
        public void PointsShouldBeInsideRectangle([Random(-2, 2,2)] float x,
            [Range(-1, 1)] float y)
        {
            const float length = 3;
            const float breadth = 5;

            MyRectangle rectangle = new MyRectangle(length, breadth);

            Assert.IsTrue(rectangle.IsInside(x, y));
        }

        [Test]
        public void PointsShouldBeOutsideRectangle([Values(-3, 3, 4)] float x,
            [Range(-1, 1)] float y)
        {
            const float length = 3;
            const float breadth = 5;

            MyRectangle rectangle = new MyRectangle(length, breadth);

            Assert.IsFalse(rectangle.IsInside(x, y));
        }
    }
}