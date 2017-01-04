using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.L05
{
    public class Player : MonoBehaviour 
    {
        public float speed = 2f;

        void Update ()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector2 direction = new Vector2(horizontal, vertical);
            Vector2 velocity = direction * speed * Time.deltaTime;

            transform.Translate(velocity);
        }
    }

}