using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System;

using UnityEngine;

namespace Wirune.W03.Test01
{
    public class TestController : Controller<TestModel>
    {
        [CommandCallback]
        void RequestRandomPoint()
        {
            Model.DoSetPoint(UnityEngine.Random.Range(0, 100000));
        }

        [CommandCallback]
        void RequestSetPoint(int point)
        {
            Model.DoSetPoint(point);
        }
    }
}
