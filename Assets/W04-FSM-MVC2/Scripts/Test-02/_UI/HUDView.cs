using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Wirune.W04.Test02
{
    public class HUDView : MonoBehaviour
    {
        [SerializeField] private GameObject m_Panel;

        [SerializeField] private Text m_HealthView;
        [SerializeField] private Text m_ScoreView;

        public void ShowPanel()
        {
            m_Panel.SetActive(true);
        }

        public void HidePanel()
        {
            m_Panel.SetActive(false);
        }

        public void OnHealthChanged(int health)
        {
            m_HealthView.text = "HEALTH: " + health.ToString("D2");
        }

        public void OnScoreChanged(int score)
        {
            m_ScoreView.text = "SCORE: "+ score.ToString("D5");
        }
    }
}
