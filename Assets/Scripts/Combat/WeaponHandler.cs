using UnityEngine;

using TPCombat.Debug;

namespace TPCombat.Combat
{
    public class WeaponHandler : MonoBehaviour
    {
        #region Config
        //[Header("CONFIG")]
        #endregion

        #region Cache
        [Header("CACHE")]
    	//[Space(8f)]
        [SerializeField]
        GameObject _weaponLogic;
        #endregion

        #region States
        #endregion

        #region Events & Statics
        #endregion

        #region Data
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////

        #region EngineMethods & Contructors
        private void Awake() 
        {
            CustomLogger.AssertNotNull(_weaponLogic.transform, "_weaponLogic", this);
        }
        #endregion

        #region PublicMethods
        public void EnableWeapon()
        {
            _weaponLogic.SetActive(true);
        }

        public void DisableWeapon()
        {
            _weaponLogic.SetActive(false);
        }
        #endregion

        #region Interfaces & Inheritance
        #endregion

        #region Events & Statics
        #endregion

        #region PrivateMethods
        #endregion
    }
}
