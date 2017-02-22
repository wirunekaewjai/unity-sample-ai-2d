using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Wirune.W04.Test01
{
    public class GamePlayState : MvvmState<GameViewModel>
    {
        private Coroutine m_Routine;

        public override void OnEnter()
        {
            base.OnEnter();

            Owner.Model.Register(this);
            Owner.Model.Register(Owner.HUDView);

            Owner.Model.Player.Register(this);
            Owner.Model.Player.Register(Owner.HUDView);

            // Notify HUDView
            Owner.Model.Player.Health = Owner.Model.Player.Health;

            Owner.HUDView.SetActive(true);

            Looper.RegisterUpdate(OnUpdate);
            m_Routine = Looper.RegisterCoroutine(OnSpawn());
        }

        public override void OnExit()
        {
            base.OnExit();

            Owner.Model.Unregister(this);
            Owner.Model.Unregister(Owner.HUDView);

            Owner.Model.Player.Unregister(this);
            Owner.Model.Player.Unregister(Owner.HUDView);

            Owner.HUDView.SetActive(false);

            Looper.UnregisterUpdate(OnUpdate);
            Looper.UnregisterCoroutine(m_Routine);
        }

        private void OnUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Fsm.ChangeState(GameStateID.Pause);
            }
        }

        [Bind("GameModel.OnScoreChanged")]
        private void OnScoreChanged(int score)
        {
            if (score % 10 == 0)
            {
                Owner.Model.Player.Health += 1;
            }
        }

        [Bind("Player.OnDied")]
        private void OnPlayerDied()
        {
            Owner.Model.Player.gameObject.SetActive(false);
            Fsm.ChangeState(GameStateID.End);
        }

        [Bind("Enemy.OnClick")]
        private void OnEnemyClick(Enemy enemy)
        {
            Owner.Model.Score += 1;
            Owner.Model.DespawnEnemy(enemy);
        }

        [Bind("Enemy.OnCollision")]
        private void OnEnemyCollision(Enemy enemy)
        {
            Owner.Model.Player.Health -= 1;
            Owner.Model.DespawnEnemy(enemy);
        }

        private IEnumerator OnSpawn()
        {
            while (true)
            {
                if (Owner.Model.SpawnEnemy())
                {
                    var enemies = Owner.Model.ActiveEnemies;
                    var enemy = enemies[enemies.Count - 1];

                    enemy.Register(this);
                }

                var delay = Random.Range(1f, 2f);
                yield return new WaitForSeconds(delay);
            }
        }
    }
}

