using UnityEngine;
using UnityEngine.UI;

namespace Wirune.W04.Test01
{
    public class UIView : MvvmBehaviour
    {
        [SerializeField] private GameObject m_Panel;

        protected virtual void Reset()
        {
            m_Panel = gameObject;
        }

        public virtual void SetActive(bool isActive)
        {
            m_Panel.SetActive(isActive);
        }
    }
}
