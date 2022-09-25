using UnityEngine;

using TPCombat.Debug;

namespace TPCombat.States
{
    public abstract class State
    {
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
    }
}
