using UnityEngine;

namespace TPCombat.States.Player
{
    public class PlayerBlockingState : PlayerBaseState
    {
        #region Config
        //[Header("CONFIG")]
        #endregion

        #region Cache
        //[Header("CACHE")]
        //[Space(8f)]        
        readonly int BLOCK_ANIMID = Animator.StringToHash("Block");

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
        public PlayerBlockingState(PlayerStateMachine stateMachine) : base(stateMachine)
        {
        }
        #endregion

        #region PublicMethods
        #endregion

        #region Interfaces & Inheritance
        public override void Enter()
        {
            base.Enter();

            _stateMachine.Animator.CrossFadeInFixedTime(BLOCK_ANIMID, CROSS_FADE_DURATION);
            _stateMachine.Health.SetInvulnerable(true);
        }
        public override void Tick(float deltaTime)
        {
            base.Tick(deltaTime);

            Move(deltaTime);

            if(!_stateMachine.InputReader.IsBlocking)
            {
                _stateMachine.SwitchState(new PlayerTargetingState(_stateMachine));
            }
            else if(!_stateMachine.Targeter.CurrentTarget)
            {
                _stateMachine.SwitchState(new PlayerFreeLookState(_stateMachine));
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
