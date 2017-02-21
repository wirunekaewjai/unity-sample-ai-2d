using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Wirune.W04.Test02
{
    public class MenuView : MonoBehaviour
    {
        public UnityAction startClickEvent;

        [SerializeField]
        private GameObject m_Panel;

        [SerializeField]
        private Text m_BestScoreText;

        [SerializeField]
        private Button m_StartButton;

        public void SetBestScore(int bestScore)
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

        public void ShowPanel()
        {
            m_Panel.SetActive(true);
            m_StartButton.onClick.AddListener(OnStartClick);
        }

        public void HidePanel()
        {
            m_Panel.SetActive(false);
            m_StartButton.onClick.RemoveListener(OnStartClick);
        }

        private void OnStartClick()
        {
            Dispatcher.Invoke(startClickEvent);
        }
    }
}

