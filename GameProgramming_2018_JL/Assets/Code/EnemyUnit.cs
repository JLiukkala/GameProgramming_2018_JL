using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Text;
using TankGame.AI;
using TankGame.WaypointSystem;

namespace TankGame
{
    public class EnemyUnit : Unit
    {
        [SerializeField]
        private float _detectEnemyDistance;

        [SerializeField]
        private float _shootingDistance;

        [SerializeField]
        private Path _path;

        [SerializeField]
        private float _waypointArriveDistance;

        [SerializeField]
        private Direction _direction;

        private IList<AIStateBase> _states = new List<AIStateBase>();
        public float DetectEnemyDistance { get { return _detectEnemyDistance; } }
        public float ShootingDistance { get { return _shootingDistance; } }
        public PlayerUnit Target { get; set; }

        public Vector3? ToTargetVector
        {
            get
            {
                if (Target != null)
                {
                    return Target.transform.position - transform.position;
                }
                return null;
            }
        }

        public AIStateBase CurrentState { get; private set; }

        public override void Init()
        {
            base.Init();
            InitStates();
        }

        private void InitStates()
        {
            PatrolState patrol = new PatrolState(this, _path, _direction, _waypointArriveDistance);
            _states.Add(patrol);

            FollowTargetState followTarget = new FollowTargetState(this);
            _states.Add(followTarget);

            // Initiating the ShootState and adding it to the state system.
            ShootState shoot = new ShootState(this);
            _states.Add(shoot);

            CurrentState = patrol;
            CurrentState.StateActivated();
        }

        protected override void Update()
        {
            CurrentState.Update();
        }

        public bool PerformTransition(AIStateType targetState)
        {
            if (!CurrentState.CheckTransition(targetState))
            {
                return false;
            }

            bool result = false;

            AIStateBase state = GetStateByType(targetState);
            if (state != null)
            {
                CurrentState.StateDeactivating();
                CurrentState = state;
                CurrentState.StateActivated();
                result = true;
            }

            return result;
        }

        private AIStateBase GetStateByType(AIStateType stateType)
        {
            return _states.FirstOrDefault(state => state.State == stateType);

            //foreach (AIStateBase state in _states)
            //{
            //    if(state.State == stateType)
            //    {
            //        return state;
            //    }
            //}
            //return null;
        }
    }
}
