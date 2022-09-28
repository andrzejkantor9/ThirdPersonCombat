using UnityEngine;

using TPCombat.Debug;

namespace TPCombat.Physics
{
    public class ForceReceiver : MonoBehaviour
    {
        #region Config
        [Header("CONFIG")]
        [SerializeField]
        float _drag = .3f;
        #endregion

        #region Cache
        [Header("CACHE")]
    	[Space(8f)]
        [SerializeField]
        CharacterController _characterController;
        #endregion

        #region States
        public Vector3 Movement => _impact + Vector3.up * _verticalVelocity;

        float _verticalVelocity;
        Vector3 _impact;
        Vector3 _dampingVelocity;
        #endregion

        #region Events & Statics
        #endregion

        #region Data
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////

        #region EngineMethods & Contructors
        private void Awake()
        {
            CustomLogger.AssertNotNull(_characterController, "_characterController", this);
        }

        private void Update() 
        {
            if(_verticalVelocity < 0f && _characterController.isGrounded)
            {
                _verticalVelocity = UnityEngine.Physics.gravity.y * Time.deltaTime;
            }
            else
            {
                _verticalVelocity += UnityEngine.Physics.gravity.y * Time.deltaTime;
            }

            _impact = Vector3.SmoothDamp(_impact, Vector3.zero, ref _dampingVelocity, _drag);
        }
        #endregion

        #region PublicMethods
        public void AddForce(Vector3 force)
        {
            _impact += force;
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
