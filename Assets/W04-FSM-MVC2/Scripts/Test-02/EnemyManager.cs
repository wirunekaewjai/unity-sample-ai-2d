using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Wirune.W04.Test02
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField]
        private int m_MaxEnemyCount = 5;

        [SerializeField]
        private GameObject m_EnemyPrefab;

        private List<GameObject> m_Enemies = new List<GameObject>();
        private Queue<GameObject> m_Pool = new Queue<GameObject>();

        private IEnumerator Start()
        {
            while (true)
            {
                Spawn();

                float delay = Random.Range(1f, 3f);
                yield return new WaitForSeconds(delay);
            }
        }

        public void Spawn()
        {
            if (m_Enemies.Count >= m_MaxEnemyCount)
                return;

            if (m_Pool.Count == 0)
            {
                var clone = Instantiate<GameObject>(m_EnemyPrefab);
                m_Pool.Enqueue(clone);
            }

            var camera = Camera.main;
            var center = (Vector2) camera.transform.position;
            var unitCircle = (Vector2)Random.onUnitSphere * camera.orthographicSize * 2f;
            var position = center + unitCircle;

            var enemy = m_Pool.Dequeue();

            enemy.transform.position = position;
            enemy.SetActive(true);

            m_Enemies.Add(enemy);
        }

        public void Despawn(GameObject enemy)
        {
            enemy.SetActive(false);

            m_Enemies.Remove(enemy);
            m_Pool.Enqueue(enemy);
        }

        private static EnemyManager s_Instance = null;
        public static EnemyManager Instance
        {
            get
            {
                if (null == s_Instance)
                {
                    s_Instance = FindObjectOfType<EnemyManager>();
                }

                if (null == s_Instance)
                {
                    GameObject g = new GameObject("Game Manager");

                    s_Instance = g.AddComponent<EnemyManager>();
                    s_Instance.transform.SetSiblingIndex(0);
                }

                return s_Instance;
            }
        }
    }
}
