using UnityEngine;

namespace Wirune.W04.Test02
{
    public class GameInitialState : FsmState<GameController>
    {
        public override void OnEnter()
        {
            base.OnEnter();

            Owner.GameModel.Initialize();

            Owner.MenuView.startClickEvent += OnStart;
            Owner.MenuView.ShowPanel();

            var bestScore = PlayerPrefs.GetInt("BestScore", 0);
            Owner.MenuView.SetBestScore(bestScore);
        }

        public override void OnExit()
        {
            base.OnExit();

            Owner.MenuView.startClickEvent -= OnStart;
            Owner.MenuView.HidePanel();
        }

        private void OnStart()
        {
            Owner.GameModel.SpawnPlayer();
            Fsm.ChangeState(GameStateID.Play);
        }
    }
}

