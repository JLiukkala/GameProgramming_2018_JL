using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TankGame.Systems;

namespace TankGame.AI
{
    // ShootState is derived from the AIStateBase class.
    public class ShootState : AIStateBase
    {
        // The distance when the unit shooting at an enemy squared.
        public float SqrShootingDistance
        {
            get { return Owner.ShootingDistance * Owner.ShootingDistance; }
        }

        // The distance when the unit starts following a target squared.
        public float SqrDetectEnemyDistance
        {
            get { return Owner.DetectEnemyDistance * Owner.DetectEnemyDistance; }
        }

        public ShootState(EnemyUnit owner) : base(owner, AIStateType.Shoot)
        {
            // Adding the possible states that can be transitioned to from this state.
            AddTransition(AIStateType.Patrol);
            AddTransition(AIStateType.FollowTarget);
        }

        // Implementing the required method from the AIStateBase class.
        public override void Update()
        {
            // If the state does not need to change, the unit will
            // keep following the target and shooting at it at the same time.
            if (!ChangeState())
            {
                Owner.Mover.Move(Owner.transform.forward);
                Owner.Mover.Turn(Owner.Target.transform.position);
                Owner.Weapon.Shoot();
            }
        }

        private bool ChangeState()
        {
            // Finding out the unit's distance to the player squared.
            Vector3 toPlayerVector = Owner.transform.position - Owner.Target.transform.position;
            float sqrDistanceToPlayer = toPlayerVector.sqrMagnitude;

            // If the distance to the player squared is larger than the 
            // shooting distance squared, the unit will return to the 
            // FollowTarget state.
            if (sqrDistanceToPlayer > SqrShootingDistance)
            {
                return Owner.PerformTransition(AIStateType.FollowTarget);
            }

            // If the distance to the player squared is larger than 
            // the distance to detect an enemy squared, the unit will
            // change the state to the Patrol state.
            if (sqrDistanceToPlayer > SqrDetectEnemyDistance)
            {
                Owner.Target = null;
                return Owner.PerformTransition(AIStateType.Patrol);
            }

            // Otherwise the state will remain unchanged.
            return false;
        }
    }
}
