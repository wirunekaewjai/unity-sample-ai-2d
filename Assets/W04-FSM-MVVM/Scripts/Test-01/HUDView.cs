using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Wirune.W04.Test01
{
    public class HUDView : UIView
    {
        [SerializeField] private Text m_HealthView;
        [SerializeField] private Text m_ScoreView;

        [Bind("Player.OnHealthChanged")]
        private void OnHealthChanged(int health)
        {
            m_HealthView.text = "HEALTH: " + health.ToString("D2");
        }

        [Bind("GameModel.OnScoreChanged")]
        private void OnScoreChanged(int score)
        {
            m_ScoreView.text = "SCORE: "+ score.ToString("D5");
        }
    }
}
