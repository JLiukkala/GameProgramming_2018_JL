using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TankGame.Systems;

namespace TankGame.AI
{
    // ShootState is derived from the AIStateBase class.
    public class ShootState : AIStateBase
    {
        public ShootState(EnemyUnit owner) : base(owner, AIStateType.Shoot)
        {
            // Adding the possible states that can be transitioned to from this state.
            AddTransition(AIStateType.Patrol);
            AddTransition(AIStateType.FollowTarget);
        }

        public override void StateActivated()
        {
            base.StateActivated();
            Owner.Target.Health.UnitDied += OnTargetDied;
        }

        private void OnTargetDied(Unit target)
        {
            Owner.PerformTransition(AIStateType.Patrol);
            Owner.Target = null;
        }

        public override void StateDeactivating()
        {
            base.StateDeactivating();
            Owner.Target.Health.UnitDied -= OnTargetDied;
        }

        // Implementing the required method from the AIStateBase class.
        public override void Update()
        {
            // If the state does not need to change, the unit will
            // keep following the target and shooting at it at the same time.
            if (!ChangeState())
            {
                Owner.Mover.Turn(Owner.Target.transform.position);
                Owner.Weapon.Shoot();
            }
        }

        private bool ChangeState()
        {
            bool result = false;

            float distanceToTarget = Vector3.Distance(Owner.Target.transform.position, Owner.transform.position);

            if (distanceToTarget > Owner.ShootingDistance) 
            {
                Owner.PerformTransition(AIStateType.FollowTarget);
                result = true;
            }

            return result;
        }
    }
}
