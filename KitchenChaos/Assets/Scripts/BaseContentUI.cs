using UnityEngine;

namespace DefaultNamespace
{

    public abstract class BaseContentUI : MonoBehaviour
    {

        [SerializeField]
        protected GameObject _content;

        public virtual void Show()
        {
            _content.SetActive(true);
        }
        public virtual void Hide()
        {
            _content.SetActive(false);
        }
        

    }

}