using UnityEngine;
using UnityEngine.SceneManagement;

namespace Wirune.W03.Test02
{
    public class GameEndState : FsmState<GameController>
    {
        public override void OnEnter()
        {
            base.OnEnter();

            Save();
            Restart();
        }

        private void Save()
        {
            var bestScore = PlayerPrefs.GetInt("W03-BestScore", 0);
            var currentScore = Owner.GameModel.Score;

            PlayerPrefs.SetInt("W03-BestScore", Mathf.Max(bestScore, currentScore));
        }

        private void Restart()
        {
            var scene = SceneManager.GetActiveScene();
            var sceneIndex = scene.buildIndex;

            SceneManager.LoadScene(sceneIndex);
        }
    }
}

