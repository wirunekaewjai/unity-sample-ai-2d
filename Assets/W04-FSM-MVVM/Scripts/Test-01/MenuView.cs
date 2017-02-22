using UnityEngine;
using UnityEngine.UI;

namespace Wirune.W04.Test01
{
    public class MenuView : UIView
    {
        [SerializeField]
        private Text m_BestScoreText;

        [SerializeField]
        private Button m_StartButton;

        [Bind("GameModel.OnBestScoreLoaded")]
        private void OnBestScoreLoaded(int bestScore)
        {
            if (bestScore > 0)
            {
                m_BestScoreText.text = "BEST SCORE: " + bestScore.ToString("D5");
            }
            else
            {
                m_BestScoreText.text = "";
            }
        }

        public override void SetActive(bool isActive)
        {
            base.SetActive(isActive);

            if (isActive)
            {
                m_StartButton.onClick.AddListener(OnStartClick);
            }
            else
            {
                m_StartButton.onClick.RemoveListener(OnStartClick);
            }
        }

        private void OnStartClick()
        {
            Execute("Menu.OnStartClick");
        }
    }
}

