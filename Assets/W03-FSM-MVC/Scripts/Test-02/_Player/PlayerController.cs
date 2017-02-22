using UnityEngine;

namespace Wirune.W03.Test02
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerModel m_Model;
        [SerializeField] private PlayerView m_View;

        public PlayerModel Model { get { return m_Model; } }
        public PlayerView View { get { return m_View; } }

        private void Update()
        {
            var h = Input.GetAxis("Horizontal");
            var v = Input.GetAxis("Vertical");
            var velocity = new Vector2(h, v) * m_Model.Speed;

            var mouseScreenPosition = Input.mousePosition;
            var mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);

            m_View.OnVelocityChanged(velocity);
            m_View.OnTargetChanged(mouseWorldPosition);
        }
    }
}

