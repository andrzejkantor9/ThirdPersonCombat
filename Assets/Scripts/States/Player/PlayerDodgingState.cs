using UnityEngine;

namespace TPCombat.States.Player
{
    public class PlayerDodgingState : PlayerBaseState
    {
        #region Config
        //[Header("CONFIG")]
        #endregion

        #region Cache
        //[Header("CACHE")]
        //[Space(8f)]

        readonly int DODGING_BLEND_TREE_ANIMID = Animator.StringToHash("DodgingBlendTree");
        readonly int DODGING_FORWARD_ANIMID = Animator.StringToHash("DodgingForward");
        readonly int DODGING_RIGHT_ANIMID = Animator.StringToHash("DodgingRight");

        const float CROSS_FADE_DURATION = 0.2f;
        #endregion

        #region States
        Vector2 _dodgingDirectionInput;
        float _remainingDodgeDuration;
        #endregion

        #region Events & Statics
        #endregion

        #region Data
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////

        #region EngineMethods & Contructors
        public PlayerDodgingState(PlayerStateMachine stateMachine, Vector3 dodgingDirectionInput) : base(stateMachine)
        {
            _dodgingDirectionInput = dodgingDirectionInput;
        }
        #endregion

        #region PublicMethods
        #endregion

        #region Interfaces & Inheritance
        public override void Enter()
        {
            base.Enter();

            _remainingDodgeDuration = _stateMachine.DodgeDuration;
            _stateMachine.Health.SetInvulnerable(true);

            _stateMachine.Animator.SetFloat(DODGING_FORWARD_ANIMID, _dodgingDirectionInput.y);
            _stateMachine.Animator.SetFloat(DODGING_RIGHT_ANIMID, _dodgingDirectionInput.x);
            _stateMachine.Animator.CrossFadeInFixedTime(DODGING_BLEND_TREE_ANIMID, CROSS_FADE_DURATION);
        }
        public override void Tick(float deltaTime)
        {
            base.Tick(deltaTime);

            Vector3 movement = new Vector3();

            movement += _stateMachine.transform.right * _dodgingDirectionInput.x 
                * _stateMachine.DodgeDistance / _stateMachine.DodgeDuration;
            movement += _stateMachine.transform.forward * _dodgingDirectionInput.y 
                * _stateMachine.DodgeDistance / _stateMachine.DodgeDuration;
            Move(movement, deltaTime);

            FaceTarget();

            _remainingDodgeDuration -= deltaTime;
            if(_remainingDodgeDuration <= 0f)
            {
                _stateMachine.SwitchState(new PlayerTargetingState(_stateMachine));
            }
        }
        
        public override void Exit()
        {
            base.Exit();

            _stateMachine.Health.SetInvulnerable(false);
        }
        #endregion

        #region Events & Statics
        #endregion

        #region PrivateMethods
        #endregion
    }
}
