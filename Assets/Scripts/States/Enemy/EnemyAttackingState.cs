using UnityEngine;

namespace TPCombat.States.Enemy
{
    public class EnemyAttackingState : EnemyBaseState
    {
        #region Config
        //[Header("CONFIG")]
        #endregion

        #region Cache
        //[Header("CACHE")]
        //[Space(8f)]

        readonly int ATTACK_ANIMID = Animator.StringToHash("Attack");

        const float TRASITION_DURATION = 0.1f;
        #endregion

        #region States
        #endregion

        #region Events & Statics
        #endregion

        #region Data
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////

        #region EngineMethods & Contructors
        public EnemyAttackingState(EnemyStateMachine stateMachine) : base(stateMachine)
        {
        }
        #endregion

        #region PublicMethods
        #endregion

        #region Interfaces & Inheritance
        public override void Enter()
        {
            base.Enter();
            
            _stateMachine.WeaponDamage.SetAttack(_stateMachine.AttackDamage, _stateMachine.AttackKnockback);
            _stateMachine.Animator.CrossFadeInFixedTime(ATTACK_ANIMID, TRASITION_DURATION);
        }

        public override void Tick(float deltaTime)
        {
            base.Tick(deltaTime);

            FacePlayer();

            if(GetNormalizedTime(_stateMachine.Animator) >= 1f)
            {
                _stateMachine.SwitchState(new EnemyChasingState(_stateMachine));
            }
        }
        
        public override void Exit()
        {
            base.Exit();
        }
        #endregion

        #region Events & Statics
        #endregion

        #region PrivateMethods
        #endregion
    }
}
