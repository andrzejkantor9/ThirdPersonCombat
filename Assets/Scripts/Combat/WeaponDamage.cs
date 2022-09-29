using System.Collections.Generic;

using UnityEngine;

using TPCombat.Debug;
using TPCombat.Physics;

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
        int _damage;
        float _knockback;
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
            if(other != _selfCollider && !_alreadyCollidedWith.Contains(other))
            {
                if(other.TryGetComponent<Health>(out Health health))
                {
                    health.DealDamage(_damage);
                    _alreadyCollidedWith.Add(other);
                }

                if(other.TryGetComponent<ForceReceiver>(out ForceReceiver forceReceiver))
                {
                    Vector3 direction = (other.transform.position - _selfCollider.transform.position).normalized;
                    forceReceiver.AddForce(direction * _knockback);
                }
            }
        }
        #endregion

        #region PublicMethods
        public void SetAttack(int damage, float knockback)
        {
            _damage = damage;
            _knockback = knockback;
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
