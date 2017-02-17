using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.W03.Test01
{
    public class TestModel : Model
    {
        [SerializeField]
        int point;

        public void DoSetPoint(int point)
        {
            this.point = point;
            Execute("OnPointChanged", point);
        }
    }
}
