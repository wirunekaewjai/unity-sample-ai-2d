using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.L04
{
    public class Worker : MonoBehaviour 
    {
        // Constant (static readonly)
        public const byte EAT_STATE     = 1;
        public const byte RELAX_STATE   = 2;
        public const byte SLEEP_STATE   = 3;
        public const byte WORK_STATE    = 4;

        // SerializeField
        public int fullness = 10;
        public int stamina = 10;
        public int happiness = 10;

        [Space]
        public float delay = 1f;

        // Property
        public Fsm<Worker> Fsm { get; private set; }

        void Awake()
        {
            Fsm = new Fsm<Worker>(this);

            Fsm.AddState(EAT_STATE, new EatState());
            Fsm.AddState(RELAX_STATE, new RelaxState());
            Fsm.AddState(SLEEP_STATE, new SleepState());
            Fsm.AddState(WORK_STATE, new WorkState());
        }

        void OnEnable()
        {
            Fsm.ChangeState(WORK_STATE);
        }

        void OnDisable()
        {
            Fsm.ChangeState(0);
        }

        IEnumerator Start()
        {
            while (true)
            {
                Fsm.Update();
                yield return new WaitForSeconds(delay);
            }
        }

//        Update Fsm every frame
//        void Update()
//        {
//            Fsm.Update();
//        }
    }
}