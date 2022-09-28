using UnityEngine;

using TPCombat.Debug;

namespace TPCombat.Combat
{
    public class Health : MonoBehaviour
    {
        #region Config
        [Header("CONFIG")]
        [SerializeField]
        int _maxHealth = 100;
        #endregion

        #region Cache
        //[Header("CACHE")]
    	//[Space(8f)]
        #endregion

        #region States
        int _health;
        #endregion

        #region Events & Statics
        #endregion

        #region Data
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////

        #region EngineMethods & Contructors
        private void Start() 
        {
            _health = _maxHealth;
        }
        #endregion

        #region PublicMethods
        public void DealDamage(int damage)
        {
            if(_health != 0)
            {
                _health = Mathf.Max(_health - damage, 0);
                CustomLogger.Log($"deal damage {damage} to: {gameObject.name}, health left: {_health}", this, LogCategory.Combat, LogFrequency.Frequent, LogDetails.Medium);
            }
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
