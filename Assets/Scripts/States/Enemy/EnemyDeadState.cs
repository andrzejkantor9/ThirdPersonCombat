using UnityEngine;

using TPCombat.Physics;

namespace TPCombat.States.Enemy
{
    public class EnemyDeadState : EnemyBaseState
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
        public EnemyDeadState(EnemyStateMachine stateMachine) : base(stateMachine)
        {
        }
        #endregion

        #region PublicMethods
        public override void Enter()
        {
            base.Enter();
            
            _stateMachine.Ragdoll.SetRagdollActive(true);
            _stateMachine.WeaponDamage.gameObject.SetActive(false);
            GameObject.Destroy(_stateMachine.Target);
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
