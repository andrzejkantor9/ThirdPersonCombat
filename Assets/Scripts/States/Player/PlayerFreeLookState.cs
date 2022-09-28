using UnityEngine;

using TPCombat.Debug;

namespace TPCombat.States.Player
{
    public class PlayerFreeLookState : PlayerBaseState
    {
        #region Config
        //[Header("CONFIG")]
        float _timer;
        #endregion

        #region Cache & Constants
        //[Header("CACHE")]
        //[Space(8f)]

        readonly int FREE_LOOK_SPEED_ANIMID = Animator.StringToHash("FreeLookSpeed");
        readonly int FREE_LOOK_BLEND_TREE_ANIMID = Animator.StringToHash("FreeLookBlendTree");

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
        public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine)
        {
        }
        #endregion

        #region PublicMethods
        #endregion

        #region Interfaces & Inheritance
        public override void Enter()
        {
            base.Enter();
            
            _stateMachine.InputReader.onTargetInput += OnTarget;

            _stateMachine.Animator.CrossFadeInFixedTime(FREE_LOOK_BLEND_TREE_ANIMID, CROSS_FADE_DURATION);
        }
        
        public override void Tick(float deltaTime)
        {
            base.Tick(deltaTime);

            if(_stateMachine.InputReader.IsAttacking)
            {
                _stateMachine.SwitchState(new PlayerAttackingState(_stateMachine, 0));
            }
            else if(_stateMachine.InputReader.MovementValue != Vector2.zero)
            {    
                Vector3 movement = Calculatemovement();
                Move(movement * _stateMachine.FreeLookMovementSpeed, deltaTime);

                FaceMovementDirection(movement, deltaTime);
                _stateMachine.Animator.SetFloat(FREE_LOOK_SPEED_ANIMID, 1f, ANIMATOR_DAMP_TIME, deltaTime);
            }
            else
            {
                _stateMachine.Animator.SetFloat(FREE_LOOK_SPEED_ANIMID, 0f, ANIMATOR_DAMP_TIME, deltaTime);
            }

            CustomLogger.Log($"{_stateMachine.InputReader.MovementValue}", this, LogCategory.Input, LogFrequency.EveryFrame, LogDetails.Deep);
        }

        public override void Exit()
        {
            base.Exit();
            _stateMachine.InputReader.onTargetInput -= OnTarget;
        }
        #endregion

        #region Events & Statics
        void OnTarget()
        {
            if(_stateMachine.Targeter.SelectTarget())
            {
                _stateMachine.SwitchState(new PlayerTargetingState(_stateMachine));
            }
        }
        #endregion

        #region PrivateMethods
        Vector3 Calculatemovement()
        {
            Vector3 forward = _stateMachine.MainCameraTransform.forward;
            Vector3 right = _stateMachine.MainCameraTransform.right;
            forward.y = 0f;
            right.y = 0f;

            forward.Normalize();
            right.Normalize();

            return forward * _stateMachine.InputReader.MovementValue.y + 
                right * _stateMachine.InputReader.MovementValue.x;
        }

        void FaceMovementDirection(Vector3 movement, float deltaTime)
        {
            _stateMachine.transform.rotation = Quaternion.Lerp(
                _stateMachine.transform.rotation,
                Quaternion.LookRotation(movement),
                deltaTime * _stateMachine.RotationDamping);
        }
        #endregion
    }
}
