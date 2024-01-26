using UnityEngine;

namespace Gameplay.Counter
{

    public class SelectedCounterVisual : MonoBehaviour
    {
        
        [SerializeField]
        private GameObject _selectedVisual;
        
        public void TurnOnSelectedVisual()
        {
            _selectedVisual.SetActive(true);
        }

        public void TurnOffSelectedVisual()
        {
            _selectedVisual.SetActive(false);
        }
    }

}