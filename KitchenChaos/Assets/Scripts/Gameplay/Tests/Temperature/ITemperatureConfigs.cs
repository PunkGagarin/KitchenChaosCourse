using System;

namespace Gameplay.Shapes
{
    public interface ITemperatureConfigs
    {
        event Action<int> OnSomethingGoesSomewhere;
        event Action OnConfigsReady;
    }
}