using UnityEngine;

namespace TPCombat.States.Player
{
    public class PlayerDeadState : PlayerBaseState
    {
        #region Config
        //[Header("CONFIG")]
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
        public PlayerDeadState(PlayerStateMachine stateMachine) : base(stateMachine)
        {
        }
        #endregion

        #region PublicMethods
        public override void Enter()
        {
            base.Enter();
            
            _stateMachine.Ragdoll.SetRagdollActive(true);
            _stateMachine.WeaponDamage.gameObject.SetActive(false);
        }
        public override void Tick(float deltaTime)
        {
            base.Tick(deltaTime);
        }
        
        public override void Exit()
        {
            base.Exit();
        }
        #endregion

        #region Interfaces & Inheritance
        #endregion

        #region Events & Statics
        #endregion

        #region PrivateMethods
        #endregion
    }
}
