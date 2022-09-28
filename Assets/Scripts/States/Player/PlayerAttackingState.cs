using UnityEngine;

using TPCombat.Debug;
using System;

namespace TPCombat.States.Player
{
    public class PlayerAttackingState : PlayerBaseState
    {
        #region Config
        //[Header("CONFIG")]
        #endregion

        #region Cache & Constants
        //[Header("CACHE")]
        //[Space(8f)]

        private const string ATTACK_TAG = "Attack";
        #endregion

        #region States
        Attack _attack;
        float _previousFrameTime;

        bool _alreadyAppliedForce;
        #endregion

        #region Events & Statics
        #endregion

        #region Data
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////

        #region EngineMethods & Contructors
        public PlayerAttackingState(PlayerStateMachine stateMachine, int attackIndex) : base(stateMachine)
        {
            _attack = _stateMachine.Attacks[attackIndex];
        }
        #endregion

        #region PublicMethods
        #endregion

        #region Interfaces & Inheritance
        public override void Enter()
        {
            base.Enter();

            CustomLogger.Log($"attack name: {_attack.AnimationName}", this, LogCategory.Combat, LogFrequency.Frequent, LogDetails.Basic);

            _stateMachine.WeaponDamage.SetAttackDamage(_attack.Damage);
            _stateMachine.Animator.CrossFadeInFixedTime(_attack.AnimationName, _attack.TrasitionDuration);
        }
        public override void Tick(float deltaTime)
        {
            base.Tick(deltaTime);

            Move(deltaTime);
            FaceTarget();
            float normalizedTime = GetNormalizedTime();

            if(normalizedTime < 1f)
            {
                if(normalizedTime >= _attack.ForceTime)
                {
                    TryApplyForce();
                }

                if(_stateMachine.InputReader.IsAttacking)
                {
                    TryComboAttack(normalizedTime);
                }
            }
            else
            {
                if(_stateMachine.Targeter.CurrentTarget)
                {
                    _stateMachine.SwitchState(new PlayerTargetingState(_stateMachine));
                }
                else
                {
                    _stateMachine.SwitchState(new PlayerFreeLookState(_stateMachine));
                }
            }

            _previousFrameTime = normalizedTime;
        }
        
        public override void Exit()
        {
            base.Exit();
        }
        #endregion

        #region Events & Statics
        #endregion

        #region PrivateMethods
        float GetNormalizedTime()
        {
            AnimatorStateInfo currentInfo = _stateMachine.Animator.GetCurrentAnimatorStateInfo(0);
            AnimatorStateInfo nextInfo = _stateMachine.Animator.GetNextAnimatorStateInfo(0);

            if(_stateMachine.Animator.IsInTransition(0) && nextInfo.IsTag(ATTACK_TAG))
                return nextInfo.normalizedTime;
            else if(!_stateMachine.Animator.IsInTransition(0) && currentInfo.IsTag(ATTACK_TAG))
                return currentInfo.normalizedTime;
            else
                return 0f;
        }

        void TryComboAttack(float normalizedTime)
        {
            if(_attack.ComboStateIndex != -1 && normalizedTime >= _attack.ComboAttackTime)
            {
                _stateMachine.SwitchState
                (
                    new PlayerAttackingState(_stateMachine, _attack.ComboStateIndex)
                );
            }
        }

        void TryApplyForce()
        {
            if(!_alreadyAppliedForce)
            {
                _stateMachine.ForceReceiver.AddForce(_stateMachine.transform.forward * _attack.Force);

                _alreadyAppliedForce = true;
            }
        }
        #endregion
    }
}
