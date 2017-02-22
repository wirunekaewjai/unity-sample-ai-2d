using UnityEngine;
using UnityEngine.SceneManagement;

namespace Wirune.W04.Test01
{
    public class GameEndState : MvvmState<GameViewModel>
    {
        public override void OnEnter()
        {
            base.OnEnter();

            Save();
            Restart();
        }

        private void Save()
        {
            Owner.Model.SaveBestScore();
        }

        private void Restart()
        {
            var scene = SceneManager.GetActiveScene();
            var sceneIndex = scene.buildIndex;

            SceneManager.LoadScene(sceneIndex);
        }
    }
}

