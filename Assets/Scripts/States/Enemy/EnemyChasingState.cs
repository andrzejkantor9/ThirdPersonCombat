using UnityEngine;

using TPCombat.Debug;
using System;

namespace TPCombat.States.Enemy
{
    public class EnemyChasingState : EnemyBaseState
    {
        #region Config
        //[Header("CONFIG")]
        #endregion

        #region Cache
        //[Header("CACHE")]
        //[Space(8f)]
        
        readonly int LOCOMOTION_ANIMID = Animator.StringToHash("Locomotion");
        readonly int SPEED_ANIMID = Animator.StringToHash("Speed");

        const float ANIMATOR_DAMP_TIME = 0.1f;
        const float CROSS_FADE_DURATION = 0.2f;
        #endregion

        #region States
        #endregion

        #region Events & Statics
        #endregion

        #region Data
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////

        #region EngineMethods & Contructors
        public EnemyChasingState(EnemyStateMachine stateMachine) : base(stateMachine)
        {
        }
        #endregion

        #region PublicMethods
        #endregion

        #region Interfaces & Inheritance
        public override void Enter()
        {
            base.Enter();

            _stateMachine.Animator.CrossFadeInFixedTime(LOCOMOTION_ANIMID, CROSS_FADE_DURATION);
        }
        public override void Tick(float deltaTime)
        {
            base.Tick(deltaTime);

            if(IsInAttackRange())
            {
                _stateMachine.SwitchState(new EnemyAttackingState(_stateMachine));
            }
            else if(IsInChaseRange())
            {
                MoveToPlayer(deltaTime);
                FacePlayer();
                _stateMachine.Animator.SetFloat(SPEED_ANIMID, 1f, ANIMATOR_DAMP_TIME, deltaTime);
            }
            else
            {
                _stateMachine.SwitchState(new EnemyIdleState(_stateMachine));
            }
        }

        public override void Exit()
        {
            base.Exit();

            _stateMachine.Agent.ResetPath();
            _stateMachine.Agent.velocity = Vector3.zero;
        }
        #endregion

        #region Events & Statics
        #endregion

        #region PrivateMethods
        void MoveToPlayer(float deltaTime)
        {
            if(_stateMachine.Agent.isOnNavMesh)
            {    
                _stateMachine.Agent.destination = _stateMachine.PlayerHealth.transform.position;
                Move(_stateMachine.Agent.desiredVelocity.normalized * _stateMachine.MovementSpeed, deltaTime);
            }

            _stateMachine.Agent.velocity = _stateMachine.CharacterController.velocity;
        }

        bool IsInAttackRange()
        {
            if(_stateMachine.PlayerHealth.IsDead)
                return false;

            float playerDistanceSquared = (_stateMachine.PlayerHealth.transform.position - _stateMachine.transform.position).sqrMagnitude;

            return playerDistanceSquared <= _stateMachine.AttackRange * _stateMachine.AttackRange;
        }
        #endregion
    }
}
