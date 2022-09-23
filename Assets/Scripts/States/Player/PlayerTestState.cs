using UnityEngine;

using TPCombat.Debug;

namespace TPCombat.States.Player
{
    public class PlayerTestState : PlayerBaseState
    {
        #region Config
        //[Header("CONFIG")]
        float _timer;
        #endregion

        #region Cache
        //[Header("CACHE")]
        //[Space(8f)]
        #endregion

        #region States
        #endregion

        #region Events & Statics
        #endregion

        #region Data
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////

        #region EngineMethods & Contructors
        public PlayerTestState(PlayerStateMachine stateMachine) : base(stateMachine)
        {
        }
        #endregion

        #region PublicMethods
        #endregion

        #region Interfaces & Inheritance
        public override void Enter()
        {
            _stateMachine.InputReader.onJumpEvent += Jump;
            CustomLogger.Log("Enter state", this, LogCategory.Input, LogFrequency.Frequent, LogDetails.Basic);
        }
        
        public override void Tick(float deltaTime)
        {
            _timer += deltaTime;
            CustomLogger.Log($"{_timer}", this, LogCategory.Input, LogFrequency.MostFrames, LogDetails.Medium);
        }

        public override void Exit()
        {
            _stateMachine.InputReader.onJumpEvent -= Jump;
            CustomLogger.Log("Exit state", this, LogCategory.Input, LogFrequency.Frequent, LogDetails.Basic);
        }
        #endregion

        #region Events & Statics
        #endregion

        #region PrivateMethods
        void Jump()
        {
            _stateMachine.SwitchState(new PlayerTestState(_stateMachine));
        }
        #endregion
    }
}
