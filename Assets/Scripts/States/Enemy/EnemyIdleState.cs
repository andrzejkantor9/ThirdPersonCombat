using UnityEngine;

using TPCombat.Debug;

namespace TPCombat.States.Enemy
{
    public class EnemyIdleState : EnemyBaseState
    {
        #region Config
        //[Header("CONFIG")]
        #endregion

        #region Cache
        //[Header("CACHE")]
        //[Space(8f)]
        
        readonly int LOCOMOTION_ANIMID = Animator.StringToHash("Locomotion");
        readonly int SPEED_ANIMID = Animator.StringToHash("Speed");

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
        public EnemyIdleState(EnemyStateMachine stateMachine) : base(stateMachine)
        {
        }
        #endregion

        #region PublicMethods
        #endregion

        #region Interfaces & Inheritance
        public override void Enter()
        {
            base.Enter();

            _stateMachine.Animator.CrossFadeInFixedTime(LOCOMOTION_ANIMID, CROSS_FADE_DURATION);
        }
        public override void Tick(float deltaTime)
        {
            base.Tick(deltaTime);

            Move(deltaTime);
            FacePlayer();

            if(IsInChaseRange())
            {
                CustomLogger.Log($"{_stateMachine.gameObject.name} is in chase range", this, LogCategory.Enemies, LogFrequency.Regular, LogDetails.Basic);
                _stateMachine.SwitchState(new EnemyChasingState(_stateMachine));
            }
            else
            {
                _stateMachine.Animator.SetFloat(SPEED_ANIMID, 0, ANIMATOR_DAMP_TIME, deltaTime);
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
