using System;
using System.Collections;
using Gameplay;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Zenject;

namespace Tests.PlayTests
{
    public class KitchenManagerTest
    {
        private DiContainer _container;

        [Inject] private KitchenGameManager _manager;
        [Inject] private IGameInputManager _mockGameInput;

        [SetUp]
        public void Setup()
        {
            _container = new DiContainer();

            _container.Bind<IGameInputManager>().FromInstance(Substitute.For<IGameInputManager>());

            var gameObject = new GameObject();
            gameObject.SetActive(false);
            var manager = gameObject.AddComponent<KitchenGameManager>();

            _container.Bind<KitchenGameManager>().FromInstance(manager).AsSingle();

            //TODO: КАК СДЛЕТАЬ ЭТУ ВЕЩЬ НЕ ОБЯЗАТЕЛЬНОЙ?! (пример в DiTests это необязательно)
            //внутренний Inject обьекта KitchenGameManager  без этой строчки будет равен null
            // [Inject] private IGameInputManager _gameInput;  - вот этот.
            //при том что идентичная логика без этой строчки в DiShape с её (IRectCalculator) работает без Inject по DiShape.
            //Почему?!

            _container.Inject(manager);
            _container.Inject(this);
        }

        [UnityTest]
        public IEnumerator KitchenManagerAwakeWaitingStateTest()
        {
            _manager.gameObject.SetActive(true);

            yield return null;

            var state = _manager.GetCurrentState();

            Assert.AreEqual(KitchenGameManagerState.WaitingToStart, state);
        }
    }
}