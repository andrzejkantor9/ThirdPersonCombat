using System;

using UnityEngine;

namespace TPCombat.Physics
{
    public class LedgeDetector : MonoBehaviour
    {
        #region Config
        //[Header("CONFIG")]
        #endregion

        #region Cache
        //[Header("CACHE")]
    	//[Space(8f)]
        #endregion

        #region States
        #endregion

        #region Events & Statics
        ///<summary>
        ///<param1> where touched the ledge
        ///<param2> direction where player should face
        ///</summary>
        public event Action<Vector3, Vector3> onLedgeDetected;
        #endregion

        #region Data
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////

        #region EngineMethods & Contructors
        //will only trigger on climbing layer
        private void OnTriggerEnter(Collider other) 
        {
            onLedgeDetected?.Invoke(other.transform.forward, other.ClosestPointOnBounds(transform.position));
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
