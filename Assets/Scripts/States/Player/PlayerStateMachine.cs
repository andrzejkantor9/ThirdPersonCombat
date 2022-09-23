using UnityEngine;

using TPCombat.Input;

//Program Files (x86)\Unity\<engine version>\Editor\Data\Resources\ScriptTemplates
namespace TPCombat.States.Player
{
    public class PlayerStateMachine : StateMachine
    {
        #region Config
        //[Header("CONFIG")]
        #endregion

        #region Cache
        [field: Header("CACHE")]
    	//[Space(8f)]
        [field: SerializeField]
        public InputReader InputReader {get; private set;}
        #endregion

        #region States
        #endregion

        #region Events & Statics
        #endregion

        #region Data
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////

        #region EngineMethods & Contructors
        private void Start() 
        {
            SwitchState(new PlayerTestState(this));
        }
        #endregion

        #region PublicMethods
        #endregion

        #region Interfaces & Inheritance
        #endregion

        #region Events & Statics
        #endregion

        #region PrivateMethods
        #endregion
    }
}
