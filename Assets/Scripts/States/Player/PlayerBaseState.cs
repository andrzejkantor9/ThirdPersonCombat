using UnityEngine;

namespace TPCombat.States.Player
{
    public abstract class PlayerBaseState : State
    {
        #region Cache
        protected PlayerStateMachine _stateMachine;
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////

        #region EngineMethods & Contructors
        protected PlayerBaseState(PlayerStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
        #endregion

        #region Interfaces & Inheritance
        protected void Move(float deltaTime)
        {
            Move(Vector3.zero, deltaTime);
        }

        protected void Move(Vector3 motion, float deltaTime)
        {
            _stateMachine.CharacterController.Move((motion + _stateMachine.ForceReceiver.Movement) * deltaTime);
        }

        protected void FaceTarget()
        {
            if(_stateMachine.Targeter.CurrentTarget)
            {
                Vector3 targetPosition = _stateMachine.Targeter.CurrentTarget.transform.position;
                Vector3 lookPosition = targetPosition - _stateMachine.transform.position;
                lookPosition.y = 0f;

                _stateMachine.transform.rotation = Quaternion.LookRotation(lookPosition);
            }
        }
        #endregion
    }
}
