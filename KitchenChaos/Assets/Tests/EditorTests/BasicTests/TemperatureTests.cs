using System;
using System.Reflection;
using Gameplay.Shapes;
using NSubstitute;
using NUnit.Framework;
using Zenject;

namespace Tests.EditorTests.BasicTests
{
    public class TemperatureTests
    {
        private DiContainer _container;

        [Inject]
        private TemperatureOverhaul _overhaul;

        [Inject]
        private ITemperatureController _temperatureController;

        [Inject]
        private ITemperatureConfigs _configs;


        [SetUp]
        public void Setup()
        {
            _container = new DiContainer();
            _container.Bind<TemperatureOverhaul>().AsSingle();
            _container.Bind<ITemperatureController>().FromInstance(Substitute.For<ITemperatureController>());
            _container.Bind<ITemperatureConfigs>().FromInstance(Substitute.For<ITemperatureConfigs>());

            _container.Inject(this);

            _overhaul.Initialize();
        }

        private void PrepareInnerState(int state)
        {
            _temperatureController.GetCurrentTemperature().Returns(state);
            _configs.OnConfigsReady += Raise.Event<Action>();
        }

        [Test]
        public void TemperatureIsPositive_Reflection()
        {
            SetPrivateField(_overhaul, "_temperature", 10);

            Assert.IsTrue(_overhaul.IsPositiveTemperature());
        }

        [Test]
        public void TemperatureIsPositive_DoubleMock()
        {
            PrepareInnerState(10);

            Assert.IsTrue(_overhaul.IsPositiveTemperature());
        }

        [TearDown]
        public void TearDown()
        {
            _overhaul.Dispose();
        }

        private void SetPrivateField(object someObject, string fieldName, object value) =>
            someObject.GetType()
                .GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance)?
                .SetValue(someObject, value);
    }
}