using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Wirune.W04.Test02
{
    public class GameModel : MonoBehaviour
    {
        public UnityAction<int> ScoreChangedEvent;

        [SerializeField] private PlayerController m_PlayerPrefab;

        [SerializeField] private EnemyController m_EnemyPrefab;
        [SerializeField] private int m_MaxEnemyCount = 5;

        private Pool<EnemyController> m_EnemyPool;

        public PlayerController Player { get; private set; }
        public List<EnemyController> Enemies { get; private set; }

        private int m_Score;
        public int Score
        {
            get { return m_Score; }
            set
            { 
                m_Score = Mathf.Clamp(value, 0, 99999);
                Dispatcher.Invoke(ScoreChangedEvent, m_Score);
            }
        }

        public void Initialize()
        {
            Score = 0;

            m_EnemyPool = new Pool<EnemyController>();
            Enemies = new List<EnemyController>();
        }

        public void SpawnPlayer()
        {
            Player = Instantiate<PlayerController>(m_PlayerPrefab);
        }

        public bool SpawnEnemy()
        {
            if (Enemies.Count >= m_MaxEnemyCount)
            {
                return false;
            }

            var enemy = m_EnemyPool.Spawn(m_EnemyPrefab);
            enemy.View.ResetPosition();

            Enemies.Add(enemy);

            return true;
        }

        public void DespawnEnemy(EnemyController enemy)
        {
            Enemies.Remove(enemy);
            m_EnemyPool.Despawn(enemy);
        }
    }
}

