using System;

using UnityEngine;

namespace TPCombat.States.Player
{
    [Serializable]
    public class Attack
    {
        #region Config
        [field: Header("CONFIG")]
        [field: SerializeField]
        public string AnimationName {get; private set;}
        [field: SerializeField]
        public float TrasitionDuration {get; private set;}

        [field: SerializeField]
        public int ComboStateIndex {get; private set;} = -1;
        [field: SerializeField]
        public float ComboAttackTime {get; private set;}
        [field: SerializeField]
        public int Damage {get; private set;}
        
        [field: SerializeField]
        public float ForceTime {get; private set;}
        [field: SerializeField]
        public float Force {get; private set;}
        #endregion

        #region Cache
        //[Header("CACHE")]
    	//[Space(8f)]
        #endregion

        #region States
        #endregion

        #region Events & Statics
        #endregion

        #region Data
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////

        #region EngineMethods & Contructors
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
