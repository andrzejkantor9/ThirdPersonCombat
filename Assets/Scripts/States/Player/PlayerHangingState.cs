using UnityEngine;

namespace TPCombat.States.Player
{
    public class PlayerHangingState : PlayerBaseState
    {
        #region Config
        //[Header("CONFIG")]
        #endregion

        #region Cache
        //[Header("CACHE")]
        //[Space(8f)]

        readonly int HANGING_ANIMID = Animator.StringToHash("Hanging");

        const float CROSS_FADE_DURATION = 0.2f;
        #endregion

        #region States
        Vector3 _ledgeForward;
        Vector3 _closestPoint;
        #endregion

        #region Events & Statics
        #endregion

        #region Data
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////

        #region EngineMethods & Contructors
        public PlayerHangingState(PlayerStateMachine stateMachine, Vector3 ledgeForward, Vector3 closestPoint) : base(stateMachine)
        {
            _ledgeForward = ledgeForward;
            _closestPoint = closestPoint;
        }
        #endregion

        #region PublicMethods
        #endregion

        #region Interfaces & Inheritance
        public override void Enter()
        {
            base.Enter();

            _stateMachine.InputReader.onDodgeInput += Drop;
            _stateMachine.InputReader.onJumpInput += Climb;

            _stateMachine.transform.rotation = Quaternion.LookRotation(_ledgeForward, Vector3.up);
            _stateMachine.CharacterController.enabled = false;
            _stateMachine.transform.position = _closestPoint - 
                (_stateMachine.LedgeDetector.transform.position - _stateMachine.transform.position);
            _stateMachine.CharacterController.enabled = true;

            _stateMachine.Animator.CrossFadeInFixedTime(HANGING_ANIMID, CROSS_FADE_DURATION);
        }

        public override void Tick(float deltaTime)
        {
            base.Tick(deltaTime);
        }
        
        public override void Exit()
        {
            base.Exit();

            _stateMachine.InputReader.onDodgeInput -= Drop;
            _stateMachine.InputReader.onJumpInput -= Climb;
        }
        #endregion

        #region Events & Statics
        void Drop()
        {
            _stateMachine.ForceReceiver.Reset();
            _stateMachine.CharacterController.Move(Vector3.zero);
            _stateMachine.SwitchState(new PlayerFallingState(_stateMachine));
        }

        void Climb()
        {
            _stateMachine.SwitchState(new PlayerPullUpState(_stateMachine));
        }
        #endregion

        #region PrivateMethods
        #endregion
    }
}
