using UnityEngine;

using TPCombat.Debug;

namespace TPCombat.States
{
    public abstract class State
    {
        #region Cache & Constants
        #endregion

        #region Interfaces & Inheritance
        public virtual void Enter()
        {
            CustomLogger.Log($"Enter state: {this.GetType().Name}", this, LogCategory.Input, LogFrequency.Frequent, LogDetails.Basic);
        }
        public virtual void Tick(float deltaTime)
        {
            CustomLogger.Log($"Tick state: {this.GetType().Name}", this, LogCategory.Input, LogFrequency.MostFrames, LogDetails.Medium);
        }
        
        public virtual void Exit()
        {
            CustomLogger.Log($"Exit state {this.GetType().Name}", this, LogCategory.Input, LogFrequency.Frequent, LogDetails.Basic);
        }
        #endregion

        #region Private & Proteceted
        protected float GetNormalizedTime(Animator animator, string tag)
        {
            AnimatorStateInfo currentInfo = animator.GetCurrentAnimatorStateInfo(0);
            AnimatorStateInfo nextInfo = animator.GetNextAnimatorStateInfo(0);

            if(animator.IsInTransition(0) && nextInfo.IsTag(tag))
                return nextInfo.normalizedTime;
            else if(!animator.IsInTransition(0) && currentInfo.IsTag(tag))
                return currentInfo.normalizedTime;
            else
                return 0f;
        }
        #endregion
    }
}
