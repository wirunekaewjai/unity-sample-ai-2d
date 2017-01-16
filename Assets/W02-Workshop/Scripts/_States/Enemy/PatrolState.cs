using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.W02
{
    public class PatrolState : IState<Enemy>
    {
        #region IState implementation
        public void OnEnter(Enemy enemy)
        {
            
        }

        public void OnStay(Enemy enemy)
        {
            if (null != enemy.Player)
            {
                enemy.Fsm.ChangeState(Enemy.CHASE_STATE);
            }
            else
            {
                GraphAgent agent = enemy.agent;
                PatrolPoint point = enemy.GetCurrentPoint();

                agent.Destination = point.Position;

                if (agent.IsReached)
                {
                    if (point is ObservablePoint && ((ObservablePoint)point).Count > 0)
                    {
                        enemy.Fsm.ChangeState(Enemy.OBSERVE_STATE);
                    }
                    else
                    {
                        enemy.NextPoint();
                    }
                }
            }
        }

        public void OnExit(Enemy enemy)
        {
            
        }
        #endregion
    }
}

