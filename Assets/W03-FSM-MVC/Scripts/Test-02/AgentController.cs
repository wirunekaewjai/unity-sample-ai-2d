using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.W03.Test02
{
    public class AgentController : Controller<AgentModel>
    {
        [CallbackAttribute]
        void IncreaseHealth(int value)
        {
            Model.Health += value;
        }

        [CallbackAttribute]
        void DecreaseHealth(int damage)
        {
            Model.Health -= damage;
        }
    }
}
