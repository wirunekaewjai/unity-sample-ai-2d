using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Wirune.W04.Test02
{
    public class HUDView : MonoBehaviour
    {
        [SerializeField] private Text m_HealthView;
        [SerializeField] private Text m_ScoreView;

        public void UpdateHealth(int health)
        {
            m_HealthView.text = "HEALTH: " + health.ToString("D2");
        }

        public void UpdateScore(int score)
        {
            m_ScoreView.text = "SCORE: "+ score.ToString("D5");
        }
    }
}
