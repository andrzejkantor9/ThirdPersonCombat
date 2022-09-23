using UnityEngine;

//Program Files (x86)\Unity\<engine version>\Editor\Data\Resources\ScriptTemplates
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
    }
}
