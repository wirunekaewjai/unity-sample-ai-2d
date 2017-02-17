using UnityEngine;
using UnityEngine.UI;

namespace Wirune.W03.Test01
{
    public class TestView : View
    {
        [SerializeField]
        private Text m_TextView;

        void Update()
        {
            if(Input.GetKeyDown(KeyCode.A))
            {
                Notify("RequestRandomPoint");
            }
            else if(Input.GetKeyDown(KeyCode.S))
            {
                Notify("RequestSetPoint", 1000);
            }
        }

        [Observe("PointChanged")]
        void OnPointChanged(int point)
        {
            Debug.Log("Point : " + point);
            m_TextView.text = "Point : " + point;
        }
    }
}
