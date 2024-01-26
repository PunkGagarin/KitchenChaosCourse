using UnityEngine;

namespace Gameplay.Counter
{

    public class StoveCounterVisual : MonoBehaviour
    {

        [SerializeField]
        private GameObject _stoveOnVisualObject;

        [SerializeField]
        private GameObject _particles;

        public void TurnOnVisualEffects()
        {
            _stoveOnVisualObject.gameObject.SetActive(true);
            _particles.gameObject.SetActive(true);
        }

        public void TurnOffVisualEffects()
        {
            _stoveOnVisualObject.gameObject.SetActive(false);
            _particles.gameObject.SetActive(false);
        }


    }

}