using UnityEngine;

namespace Wirune.W03.Test02
{
    public class GamePauseState : FsmUpdatableState<GameController>
    {
        public override void OnEnter()
        {
            base.OnEnter();

            Owner.PauseView.ShowPanel();
            Owner.GameModel.Player.enabled = false;

            foreach (var enemy in Owner.GameModel.Enemies)
            {
                enemy.enabled = false;
            }
        }

        public override void OnExit()
        {
            base.OnExit();

            Owner.PauseView.HidePanel();
            Owner.GameModel.Player.enabled = true;

            foreach (var enemy in Owner.GameModel.Enemies)
            {
                enemy.enabled = true;
            }
        }

        private void OnUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Fsm.GoToPreviousState();
            }
        }
    }
}

