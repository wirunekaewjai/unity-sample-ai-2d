using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Wirune.W03.Test02
{
    public class PauseView : MonoBehaviour
    {
        [SerializeField] private GameObject m_Panel;

        public void ShowPanel()
        {
            m_Panel.SetActive(true);
        }

        public void HidePanel()
        {
            m_Panel.SetActive(false);
        }
    }
}
