using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.W01
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
                    if (point is ObservablePoint)
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

