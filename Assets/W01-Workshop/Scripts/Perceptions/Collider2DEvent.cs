using UnityEngine.Events;
using UnityEngine;
using System;

namespace Wirune.W01
{
    [Serializable]
    public class Collider2DEvent : UnityEvent<Perception, Collider2D>
    {

    }
}

