using System;

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
        bool _isInvulnerable;

        public bool IsDead => _health == 0;
        #endregion

        #region Events & Statics
        public event Action onTakeDamage;
        public event Action onDie;
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
            if(_health != 0 && !_isInvulnerable)
            {
                _health = Mathf.Max(_health - damage, 0);
                onTakeDamage?.Invoke();

                if(_health == 0)
                {
                    onDie?.Invoke();
                }

                CustomLogger.Log($"deal damage {damage} to: {gameObject.name}, health left: {_health}", this, LogCategory.Combat, LogFrequency.Frequent, LogDetails.Medium);
            }
        }

        public void SetInvulnerable(bool isInvulnerable)
        {
            _isInvulnerable = isInvulnerable;
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
