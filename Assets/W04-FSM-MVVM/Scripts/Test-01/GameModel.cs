using System.Collections.Generic;
using UnityEngine;

namespace Wirune.W04.Test01
{
    [System.Serializable]
    public class GameModel : MvvmObject
    {
        [Header("Prefab")]
        [SerializeField] private Player m_PlayerPrefab;
        [SerializeField] private Enemy m_EnemyPrefab;

        private int m_Score = 0;
        public int Score
        {
            get { return m_Score; }
            set
            { 
                m_Score = Mathf.Clamp(value, 0, 99999);
                Execute("GameModel.OnScoreChanged", m_Score);
            }
        }

        private int m_MaxEnemyCount = 5;
        public int MaxEnemyCount
        {
            get { return m_MaxEnemyCount; }
            set { m_MaxEnemyCount = value; }
        }

        private Player m_Player;
        public Player Player
        {
            get
            {
                if (null == m_Player)
                {
                    m_Player = Object.Instantiate<Player>(m_PlayerPrefab);
                }

                return m_Player;
            }
        }

        private readonly Queue<Enemy> m_InactiveEnemies = new Queue<Enemy>();
        private readonly List<Enemy> m_ActiveEnemies = new List<Enemy>();

        public List<Enemy> ActiveEnemies { get { return m_ActiveEnemies; } }

        public void LoadBestScore()
        {
            var bestScore = PlayerPrefs.GetInt("W04-BestScore", 0);
            Execute("GameModel.OnBestScoreLoaded", bestScore);
        }

        public void SaveBestScore()
        {
            var bestScore = PlayerPrefs.GetInt("W04-BestScore", 0);
            PlayerPrefs.SetInt("W04-BestScore", Mathf.Max(bestScore, Score));
        }

        public bool SpawnEnemy()
        {
            if (m_ActiveEnemies.Count >= MaxEnemyCount)
            {
                return false;
            }

            if (m_InactiveEnemies.Count == 0)
            {
                m_InactiveEnemies.Enqueue(Object.Instantiate<Enemy>(m_EnemyPrefab));
            }

            var enemy = m_InactiveEnemies.Dequeue();
            enemy.gameObject.SetActive(true);
            enemy.ResetPosition();

            m_ActiveEnemies.Add(enemy);

            return true;
        }

        public void DespawnEnemy(Enemy enemy)
        {
            m_ActiveEnemies.Remove(enemy);
            m_InactiveEnemies.Enqueue(enemy);

            enemy.gameObject.SetActive(false);
        }
    }
}

