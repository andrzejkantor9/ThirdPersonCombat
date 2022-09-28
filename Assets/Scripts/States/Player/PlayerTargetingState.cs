using UnityEngine;

using TPCombat.Debug;

namespace TPCombat.States.Player
{
    public class PlayerTargetingState : PlayerBaseState
    {
        #region Config
        //[Header("CONFIG")]
        #endregion

        #region Cache
        //[Header("CACHE")]
        //[Space(8f)]
        readonly int TARGETING_BLEND_TREE_ANIMID = Animator.StringToHash("TargetingBlendTree");
        readonly int TARGETING_FORWARD_ANIMID = Animator.StringToHash("TargetingForwardSpeed");
        readonly int TARGETING_RIGHT_ANIMID = Animator.StringToHash("TargetingRightSpeed");

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
        public PlayerTargetingState(PlayerStateMachine stateMachine) : base(stateMachine)
        {
        }
        #endregion

        #region PublicMethods
        #endregion

        #region Interfaces & Inheritance
        public override void Enter()
        {
            base.Enter();

            _stateMachine.InputReader.onCancelInput += OnCancel;
            _stateMachine.InputReader.onTargetInput += OnCancel;

            _stateMachine.Animator.CrossFadeInFixedTime(TARGETING_BLEND_TREE_ANIMID, CROSS_FADE_DURATION);
        }
        public override void Tick(float deltaTime)
        {
            base.Tick(deltaTime);

            CustomLogger.Log($"current target: {_stateMachine.Targeter?.CurrentTarget?.name}", this, LogCategory.Camera, LogFrequency.MostFrames, LogDetails.Basic);

            if(_stateMachine.InputReader.IsAttacking)
            {
                _stateMachine.SwitchState(new PlayerAttackingState(_stateMachine, 0));
            }
            else if(!_stateMachine.Targeter.CurrentTarget)
            {
                _stateMachine.SwitchState(new PlayerFreeLookState(_stateMachine));
            }
            else
            {
                Vector3 movement = CalculateMovement();
                Move(movement * _stateMachine.TargetingMovementSpeed, deltaTime);

                UpdateAnimator(deltaTime);
                FaceTarget();
            }
        }
        
        public override void Exit()
        {
            base.Exit();
            _stateMachine.InputReader.onCancelInput -= OnCancel;
            _stateMachine.InputReader.onTargetInput -= OnCancel;
        }
        
        #endregion

        #region Events & Statics
        void OnCancel()
        {
            _stateMachine.Targeter.Cancel();
            _stateMachine.SwitchState(new PlayerFreeLookState(_stateMachine));
        }
        #endregion

        #region PrivateMethods
        Vector3 CalculateMovement()
        {
            Vector3 movement = new Vector3();

            movement += _stateMachine.transform.right * _stateMachine.InputReader.MovementValue.x;
            movement += _stateMachine.transform.forward  * _stateMachine.InputReader.MovementValue.y;

            return movement;
        }

        void UpdateAnimator(float deltaTime)
        {
            if(_stateMachine.InputReader.MovementValue.y == 0)
            {
                _stateMachine.Animator.SetFloat(TARGETING_FORWARD_ANIMID, 0, 0.1f, deltaTime);
            }
            else
            {
                float value = _stateMachine.InputReader.MovementValue.y > 0 ? 1f : -1f;
                _stateMachine.Animator.SetFloat(TARGETING_FORWARD_ANIMID, value, 0.1f, deltaTime);
            }

            if(_stateMachine.InputReader.MovementValue.x == 0)
            {
                _stateMachine.Animator.SetFloat(TARGETING_RIGHT_ANIMID, 0, 0.1f, deltaTime);
            }
            else
            {
                float value = _stateMachine.InputReader.MovementValue.x > 0 ? 1f : -1f;
                _stateMachine.Animator.SetFloat(TARGETING_RIGHT_ANIMID, value, 0.1f, deltaTime);
            }
        }
        #endregion
    }
}
