using System;
using UnityEngine;

namespace TPCombat.States.Player
{
    public class PlayerFallingState : PlayerBaseState
    {
        #region Config
        //[Header("CONFIG")]
        #endregion

        #region Cache
        //[Header("CACHE")]
        //[Space(8f)]

        readonly int FALL_ANIMID = Animator.StringToHash("Fall");

        const float CROSS_FADE_DURATION = 0.2f;
        #endregion

        #region States
        Vector3 _momentum;
        #endregion

        #region Events & Statics
        #endregion

        #region Data
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////

        #region EngineMethods & Contructors
        public PlayerFallingState(PlayerStateMachine stateMachine) : base(stateMachine)
        {
        }
        #endregion

        #region PublicMethods
        #endregion

        #region Interfaces & Inheritance
        public override void Enter()
        {
            base.Enter();

            _momentum = _stateMachine.CharacterController.velocity;
            _momentum.y = 0f;

            _stateMachine.Animator.CrossFadeInFixedTime(FALL_ANIMID, CROSS_FADE_DURATION);

            _stateMachine.LedgeDetector.onLedgeDetected += LedgeDetected;
        }
        public override void Tick(float deltaTime)
        {
            base.Tick(deltaTime);

            Move(_momentum, deltaTime);
            
            if(_stateMachine.CharacterController.isGrounded)
            {
                ReturnToLocomotion();
            }

            FaceTarget();
        }
        
        public override void Exit()
        {
            base.Exit();

            _stateMachine.LedgeDetector.onLedgeDetected -= LedgeDetected;
        }
        #endregion

        #region Events & Statics
        private void LedgeDetected(Vector3 ledgeForward, Vector3 closestPoint)
        {
            _stateMachine.SwitchState(new PlayerHangingState(_stateMachine, ledgeForward, closestPoint));
        }
        #endregion

        #region PrivateMethods
        #endregion
    }
}
