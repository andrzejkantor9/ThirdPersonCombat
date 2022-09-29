using UnityEngine;

namespace TPCombat.States.Enemy
{
    public abstract class EnemyBaseState : State
    {
        #region Cache
        protected EnemyStateMachine _stateMachine;
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////

        #region EngineMethods & Contructors
        protected EnemyBaseState(EnemyStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
        #endregion

        #region Private & Protected
        protected void Move(float deltaTime)
        {
            Move(Vector3.zero, deltaTime);
        }

        protected void Move(Vector3 motion, float deltaTime)
        {
            _stateMachine.CharacterController.Move((motion + _stateMachine.ForceReceiver.Movement) * deltaTime);
        }

        protected void FacePlayer()
        {
            if(_stateMachine.PlayerHealth)
            {
                Vector3 targetPosition = _stateMachine.PlayerHealth.transform.position;
                Vector3 lookPosition = targetPosition - _stateMachine.transform.position;
                lookPosition.y = 0f;

                _stateMachine.transform.rotation = Quaternion.LookRotation(lookPosition);
            }
        }

        protected bool IsInChaseRange()
        {
            if(_stateMachine.PlayerHealth.IsDead)
                return false;

            float playerDistanceSquared = 
                (_stateMachine.PlayerHealth.transform.position - _stateMachine.transform.position).sqrMagnitude;
            
            return playerDistanceSquared <= _stateMachine.PlayerChasingRange * _stateMachine.PlayerChasingRange;
        }
        #endregion
    }
}
