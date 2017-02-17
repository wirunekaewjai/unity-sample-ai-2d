using UnityEngine;

namespace Wirune.W03.Test01
{
    public class TestController : Controller<TestModel, TestView>
    {
        [Observe("RequestRandomPoint")]
        void OnRequestRandomPoint()
        {
            Model.point = Random.Range(0, 100000);
            View.OnPointChanged(Model.point);
        }

        [Observe("RequestSetPoint")]
        void OnRequestSetPoint(int point)
        {
            Model.point = point;
            View.OnPointChanged(Model.point);
        }
    }
}
