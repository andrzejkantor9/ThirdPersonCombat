using UnityEngine;

using TPCombat.Debug;

namespace TPCombat.Combat
{
    public class Ragdoll : MonoBehaviour
    {
        #region Config
        //[Header("CONFIG")]
        #endregion

        #region Cache
        [Header("CACHE")]
        //[Space(8f)]
        [SerializeField]
        Animator _animator;
        [SerializeField]
        CharacterController _characterController;

        Collider[] _allColliders;
        Rigidbody[] _allRigidbodies;

        private const string RAGDOLL_TAG = "Ragdoll";
        #endregion

        #region States
        #endregion

        #region Events & Statics
        #endregion

        #region Data
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////

        #region EngineMethods & Contructors
        void Awake() 
        {
            CustomLogger.AssertNotNull(_animator, "_animator", this);
            CustomLogger.AssertNotNull(_characterController, "_characterController", this);

            _allColliders = GetComponentsInChildren<Collider>(true);
            _allRigidbodies = GetComponentsInChildren<Rigidbody>(true);

            SetRagdollActive(false);
        }
        #endregion

        #region PublicMethods
        public void SetRagdollActive(bool ragdollActive)
        {
            foreach(Collider collider in _allColliders)
            {
                if(collider.gameObject.CompareTag(RAGDOLL_TAG))
                {
                    collider.enabled = ragdollActive;
                }
            }

            foreach(Rigidbody rigidbody in _allRigidbodies)
            {
                if(rigidbody.gameObject.CompareTag(RAGDOLL_TAG))
                {
                    rigidbody.isKinematic = !ragdollActive;
                    rigidbody.useGravity = ragdollActive;
                }
            }

            _characterController.enabled = !ragdollActive;
            _animator.enabled = !ragdollActive;
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
