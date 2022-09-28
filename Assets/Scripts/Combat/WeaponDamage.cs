using System.Collections.Generic;

using UnityEngine;

using TPCombat.Debug;

namespace TPCombat.Combat
{
    public class WeaponDamage : MonoBehaviour
    {
        #region Config
        // [Header("CONFIG")]
        #endregion

        #region Cache
        [Header("CACHE")]
    	//[Space(8f)]
        [SerializeField]
        Collider _selfCollider;
        #endregion

        #region States
        List<Collider> _alreadyCollidedWith = new List<Collider>();
        private int _damage;
        #endregion

        #region Events & Statics
        #endregion

        #region Data
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////

        #region EngineMethods & Contructors
        private void Awake() 
        {
            CustomLogger.AssertNotNull(_selfCollider, "_selfCollider", this);
        }

        private void OnEnable() 
        {
            _alreadyCollidedWith.Clear();
        }

        private void OnTriggerEnter(Collider other) 
        {
            if(other != _selfCollider && other.TryGetComponent<Health>(out Health health)
                && !_alreadyCollidedWith.Contains(other))
            {
                health.DealDamage(_damage);
                _alreadyCollidedWith.Add(other);
            }
        }
        #endregion

        #region PublicMethods
        public void SetAttackDamage(int damage)
        {
            _damage = damage;
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
