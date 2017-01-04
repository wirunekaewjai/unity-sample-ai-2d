using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.L04
{
    public interface IState<T> where T : MonoBehaviour
    {
        void OnEnter(T owner);
        void OnStay(T owner);
        void OnExit(T owner);
    }
}