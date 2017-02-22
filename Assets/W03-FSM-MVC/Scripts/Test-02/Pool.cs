using System.Collections.Generic;
using UnityEngine;

namespace Wirune.W03.Test02
{
    public class Pool<T> where T : Component
    {
        private Dictionary<T, Queue<T>> m_Pools = new Dictionary<T, Queue<T>>();
        private Dictionary<T, Queue<T>> m_Links = new Dictionary<T, Queue<T>>();

        public T Spawn(T prefab)
        {
            if (!m_Pools.ContainsKey(prefab))
            {
                m_Pools.Add(prefab, new Queue<T>());
            }

            var pool = m_Pools[prefab];
            if (pool.Count == 0)
            {
                var clone = Object.Instantiate<T>(prefab);
                m_Links.Add(clone, pool);

                return clone;
            }

            var respawn = pool.Dequeue();

            respawn.transform.SetParent(null);
            respawn.gameObject.SetActive(true);

            return respawn;
        }

        public void Despawn(T respawn)
        {
            m_Links[respawn].Enqueue(respawn);
            respawn.gameObject.SetActive(false);
        }
    }
}

