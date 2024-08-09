using System;
using Zenject;

namespace Gameplay.Shapes
{
    public class TemperatureOverhaul : IInitializable, IDisposable
    {
        private int _temperature;

        [Inject]
        private ITemperatureController _temperatureController;

        [Inject]
        private ITemperatureConfigs _configs;

        public void Initialize()
        {
            _configs.OnConfigsReady += ChangeTemperature;
        }

        public void Dispose()
        {
            _configs.OnConfigsReady -= ChangeTemperature;
        }

        private void ChangeTemperature()
        {
            _temperature = _temperatureController.GetCurrentTemperature();
        }

        public int GetTemperature()
        {
            return _temperature;
        }

        public bool IsPositiveTemperature()
        {
            //logic
            return _temperature > 0;
        }
        
        //как тестировать методы, зависящие от внутреннего состояния обьекта, если состояние меняется ТОЛЬКо изнутри.
        //1. Поставить Сеттер. Нарушение инкапсуляции.
        //2. Invoke ивента и подложить данные в метод _temperatureController.GetCurrentTemperature()
        //3. Положить сотояние через рефлексию
        
    }

}