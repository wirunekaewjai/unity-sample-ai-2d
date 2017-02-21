using UnityEngine;
using UnityEngine.Events;

namespace Wirune.W04.Test02
{
    public class EnemyModel : MonoBehaviour
    {
        [SerializeField, Range(0, 10)] 
        private float m_Speed = 2f;

        public float Speed
        {
            get { return m_Speed; }
            set { m_Speed = Mathf.Clamp(value, 0f, 10f); }
        }
    }
}
