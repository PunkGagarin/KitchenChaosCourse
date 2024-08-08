using System.ComponentModel;
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
        private DiShape _diShape;

        [Inject]
        private IRectCalculator _rectCalculator;

        [SetUp]
        public void Setup()
        {
            _container = new DiContainer();
            _container.Bind<DiShape>().AsSingle();
            _container.Bind<IRectCalculator>().FromInstance(Substitute.For<IRectCalculator>());
            _container.Inject(this);
        }

        [Test]
        public void IsFirstGreaterFalseTest()
        {
            MyRectangle mr1 = new MyRectangle(1, 2);
            MyRectangle mr2 = new MyRectangle(4, 5);

            _rectCalculator.IsFirstGreater(mr1, mr2).Returns(false);

            bool isFirstGreater = _diShape.IsFirstGreater(mr1, mr2);

            Assert.IsFalse(isFirstGreater);
        }
    }
}