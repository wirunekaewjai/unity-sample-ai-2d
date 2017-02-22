using UnityEngine;

namespace Wirune.W04.Test01
{
    public class GamePauseState : MvvmState<GameViewModel>
    {
        public override void OnEnter()
        {
            base.OnEnter();

            Owner.PauseView.SetActive(true);
            Owner.Model.Player.enabled = false;

            foreach (var enemy in Owner.Model.ActiveEnemies)
            {
                enemy.enabled = false;
            }

            Looper.RegisterUpdate(OnUpdate);
        }

        public override void OnExit()
        {
            base.OnExit();

            Owner.PauseView.SetActive(false);
            Owner.Model.Player.enabled = true;

            foreach (var enemy in Owner.Model.ActiveEnemies)
            {
                enemy.enabled = true;
            }

            Looper.UnregisterUpdate(OnUpdate);
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

