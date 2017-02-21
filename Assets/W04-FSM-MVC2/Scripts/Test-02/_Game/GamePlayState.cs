using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Wirune.W04.Test02
{
    public class GamePlayState : FsmUpdatableState<GameController>
    {
        private Coroutine m_Routine;

        public override void OnEnter()
        {
            base.OnEnter();
            Owner.HUDView.ShowPanel();

            var model = Owner.GameModel;

            model.ScoreChangedEvent += OnScoreChanged;
            model.ScoreChangedEvent += Owner.HUDView.OnScoreChanged;
            model.Player.Model.HealthChangedEvent += Owner.HUDView.OnHealthChanged;
            model.Player.Model.DiedEvent += OnPlayerDied;

            Owner.HUDView.OnScoreChanged(model.Score);
            Owner.HUDView.OnHealthChanged(model.Player.Model.Health);

            m_Routine = Looper.RegisterCoroutine(OnSpawn());
        }

        public override void OnExit()
        {
            base.OnExit();
            Owner.HUDView.HidePanel();

            var model = Owner.GameModel;

            model.ScoreChangedEvent -= OnScoreChanged;
            model.ScoreChangedEvent -= Owner.HUDView.OnScoreChanged;
            model.Player.Model.HealthChangedEvent -= Owner.HUDView.OnHealthChanged;
            model.Player.Model.DiedEvent -= OnPlayerDied;

            Looper.UnregisterCoroutine(m_Routine);
        }

        private void OnUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Fsm.ChangeState(GameStateID.Pause);
            }
        }

        private void OnScoreChanged(int score)
        {
            if (score % 10 == 0)
            {
                Owner.GameModel.Player.Model.Health += 1;
            }
        }

        private void OnPlayerDied()
        {
            var enemies = Owner.GameModel.Enemies;
            while (enemies.Count > 0)
            {
                Owner.GameModel.DespawnEnemy(enemies[0]);
            }

            Owner.GameModel.Player.gameObject.SetActive(false);
            Fsm.ChangeState(GameStateID.End);
        }

        private IEnumerator OnSpawn()
        {
            while (true)
            {
                if (Owner.GameModel.SpawnEnemy())
                {
                    var enemies = Owner.GameModel.Enemies;
                    var enemy = enemies[enemies.Count - 1];

                    enemy.View.ClickEvent = () =>
                    {
                        Owner.GameModel.Score += 1;
                        Owner.GameModel.DespawnEnemy(enemy);
                    };
                    
                    enemy.View.ColisionEvent = () =>
                    {
                        Owner.GameModel.Player.Model.Health -= 1;
                        Owner.GameModel.DespawnEnemy(enemy);
                    };
                }

                var delay = Random.Range(1f, 2f);
                yield return new WaitForSeconds(delay);
            }
        }
    }
}

