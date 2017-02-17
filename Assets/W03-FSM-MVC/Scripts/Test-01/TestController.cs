using UnityEngine;

namespace Wirune.W03.Test01
{
    public class TestController : Controller<TestModel, TestView>
    {
        [Observe("RequestRandomPoint")]
        void OnRequestRandomPoint()
        {
            Model.Point = Random.Range(0, 100000);
        }

        [Observe("RequestSetPoint")]
        void OnRequestSetPoint(int point)
        {
            Model.Point = point;
        }
    }
}
