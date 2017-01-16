using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.W02
{
    public interface IState<T> where T : MonoBehaviour
    {
        void OnEnter(T owner);
        void OnStay(T owner);
        void OnExit(T owner);
    }
}