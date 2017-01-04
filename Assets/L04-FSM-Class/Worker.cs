using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.L04
{
    public class Worker : MonoBehaviour 
    {
        public const byte EAT_STATE     = 1;
        public const byte RELAX_STATE   = 2;
        public const byte SLEEP_STATE   = 3;
        public const byte WORK_STATE    = 4;

        public int fullness = 10;
        public int stamina = 10;
        public int happiness = 10;

        public Fsm<Worker> fsm;

        void Awake()
        {
            fsm = new Fsm<Worker>(this);

            fsm.AddState(EAT_STATE, new EatState());
            fsm.AddState(RELAX_STATE, new RelaxState());
            fsm.AddState(SLEEP_STATE, new SleepState());
            fsm.AddState(WORK_STATE, new WorkState());
        }

        void OnEnable()
        {
            fsm.ChangeState(WORK_STATE);
        }

        void OnDisable()
        {
            fsm.ChangeState(0);
        }

        void Update()
        {
            fsm.Update();
        }
    }
}