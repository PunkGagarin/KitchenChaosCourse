using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Gameplay.Audio;
using Gameplay.Audio.Counters;
using Gameplay.Counter;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Zenject;

namespace Tests.PlayTests.Stove
{
    public class StoveCounterSoundTest
    {

        private DiContainer _container;

        [Inject] private StoveCounterSound _stoveCounterSound;
        [Inject] private ISoundManager _soundManager;
        [Inject] private StoveCounter _stove;


        [SetUp]
        public void SetUp()
        {
            _container = new();

            var go = new GameObject();
            go.SetActive(false);

            _stoveCounterSound = go.AddComponent<StoveCounterSound>();
            _stove = Substitute.For<StoveCounter>();

            _container.Bind<StoveCounterSound>().FromInstance(_stoveCounterSound);
            _container.Bind<StoveCounter>().FromInstance(_stove);
            _container.Bind<ISoundManager>().FromInstance(Substitute.For<ISoundManager>());

            _container.Inject(_stoveCounterSound);
            _container.Inject(this);

            SetPrivateField(_stoveCounterSound, "_stove", _stove);

            _stove.OnStateChanged += _ => { };
        }


        [UnityTest]
        public IEnumerator IfIsPlayingFalseSoundWontPlay()
        {
            yield return null;
        }


        [UnityTest]
        public IEnumerator IfIsPlayingTrueSoundWillPlay()
        {
            _stoveCounterSound.gameObject.SetActive(true);
            _stoveCounterSound.PlayWarningSound();
            
            yield return null;

            _soundManager.Received()
                .PlaySoundByType(Arg.Any<GameAudioType>(), Arg.Any<int>(), Arg.Any<Vector3>());
        }

        private void SetPrivateField(object someObject, string fieldName, object value) =>
            someObject.GetType()
                .GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance)?
                .SetValue(someObject, value);
    }
}