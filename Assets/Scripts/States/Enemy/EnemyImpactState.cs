using UnityEngine;

namespace TPCombat.States.Enemy
{
    public class EnemyImpactState : EnemyBaseState
    {
        #region Config
        // [Header("CONFIG")]
        #endregion

        #region Cache
        //[Header("CACHE")]
        //[Space(8f)]

        readonly int IMPACT_ANIMID = Animator.StringToHash("Impact");

        const float CROSS_FADE_DURATION = 0.2f;
        #endregion

        #region States
        float _duration = 1f;
        #endregion

        #region Events & Statics
        #endregion

        #region Data
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////

        #region EngineMethods & Contructors
        public EnemyImpactState(EnemyStateMachine stateMachine) : base(stateMachine)
        {
        }
        #endregion

        #region PublicMethods
        #endregion

        #region Interfaces & Inheritance
        public override void Enter()
        {
            base.Enter();

            _stateMachine.Animator.CrossFadeInFixedTime(IMPACT_ANIMID, CROSS_FADE_DURATION);
        }
        public override void Tick(float deltaTime)
        {
            base.Tick(deltaTime);

            Move(deltaTime);            
            _duration -= deltaTime;

            if(_duration <= 0f)
            {
                _stateMachine.SwitchState(new EnemyIdleState(_stateMachine));
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
