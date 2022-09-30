using UnityEngine;

namespace TPCombat.States.Player
{
    public class PlayerPullUpState : PlayerBaseState
    {
        #region Config
        //[Header("CONFIG")]
        #endregion

        #region Cache
        //[Header("CACHE")]
        //[Space(8f)]

        readonly int PULL_UP_ANIMID = Animator.StringToHash("PullUp");

        const string CLIMB_TAG = "Climbing";
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
        public PlayerPullUpState(PlayerStateMachine stateMachine) : base(stateMachine)
        {
        }
        #endregion

        #region PublicMethods
        #endregion

        #region Interfaces & Inheritance
        public override void Enter()
        {
            base.Enter();

            _stateMachine.Animator.CrossFadeInFixedTime(PULL_UP_ANIMID, CROSS_FADE_DURATION);
        }

        public override void Tick(float deltaTime)
        {
            base.Tick(deltaTime);

            if(GetNormalizedTime(_stateMachine.Animator, CLIMB_TAG) > .99f)
            {
                _stateMachine.CharacterController.enabled = false;
                _stateMachine.transform.Translate(
                    _stateMachine.WarpPositionAfterClimb.position - _stateMachine.transform.position,
                    // 0f, 2.325f, 0.65f,
                    Space.Self);
                _stateMachine.CharacterController.enabled = true;

                _stateMachine.SwitchState(new PlayerFreeLookState(_stateMachine, false));
            }
        }
        
        public override void Exit()
        {
            base.Exit();

            _stateMachine.ForceReceiver.Reset();
            _stateMachine.CharacterController.Move(Vector3.zero);
        }
        #endregion

        #region Events & Statics
        #endregion

        #region PrivateMethods
        #endregion
    }
}
