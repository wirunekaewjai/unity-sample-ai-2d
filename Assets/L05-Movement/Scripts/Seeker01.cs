using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.L05
{
    public class Seeker01 : MonoBehaviour 
    {
        public Player player;

        public float moveSpeed = 2f;

        public Vector2 Position
        {
            get
            {
                return transform.position;
            }
            set
            {
                transform.position = value;
            }
        }

        void Update ()
        {
            Vector2 displacement = player.Position - Position;
            Vector2 direction = displacement.normalized;
            Vector2 velocity = direction * moveSpeed * Time.deltaTime;

            Position = Position + velocity;
        }
    }

}