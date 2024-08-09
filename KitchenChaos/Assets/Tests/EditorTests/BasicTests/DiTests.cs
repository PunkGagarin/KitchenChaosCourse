using System.ComponentModel;
using System.Reflection;
using Gameplay.Shapes;
using NSubstitute;
using NUnit.Framework;
using Zenject;

namespace Tests.EditorTests.BasicTests
{
    public class DiTests
    {
        private DiContainer _container;

        [Inject]
        private ShapeSystem _shapeSystem;

        [Inject]
        private IRectCalculator _rectCalculatorMock;

        [SetUp]
        public void Setup()
        {
            _container = new DiContainer();
            
            _container.Bind<ShapeSystem>().AsSingle();

            var rectCalculator = Substitute.For<IRectCalculator>();
            _container.Bind<IRectCalculator>().FromInstance(rectCalculator);
            
            _container.Inject(this);
        }

        [Test]
        public void IsFirstGreaterFalseTest()
        {
            MyRectangle mr1 = new MyRectangle(1, 2);
            MyRectangle mr2 = new MyRectangle(4, 5);

            _rectCalculatorMock.IsFirstGreater(mr1, mr2).Returns(false);

            bool isFirstGreater = _shapeSystem.IsFirstGreater(mr1, mr2);

            Assert.IsFalse(isFirstGreater);
        }
    }
}