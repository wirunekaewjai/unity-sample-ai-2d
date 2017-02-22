using UnityEngine;

namespace Wirune.W04.Test01
{
    public class GameInitialState : MvvmState<GameViewModel>
    {
        public override void OnEnter()
        {
            base.OnEnter();

            Owner.MenuView.Register(this);
            Owner.Model.Register(Owner.MenuView);

            Owner.MenuView.SetActive(true);
            Owner.Model.LoadBestScore();
        }

        public override void OnExit()
        {
            base.OnExit();

            Owner.MenuView.Unregister(this);
            Owner.Model.Unregister(Owner.MenuView);

            Owner.MenuView.SetActive(false);
        }

        [Bind("Menu.OnStartClick")]
        private void OnStartClick()
        {
            Fsm.ChangeState(GameStateID.Play);
        }
    }
}

