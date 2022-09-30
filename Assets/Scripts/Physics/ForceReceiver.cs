using UnityEngine;
using UnityEngine.AI;

using TPCombat.Debug;
using System;

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
        [SerializeField]
        NavMeshAgent _navMeshAgent;
        
        const float FINISH_IMPACT_THRESHOLD = 0.2f;
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
            if(_navMeshAgent && _impact.sqrMagnitude < FINISH_IMPACT_THRESHOLD * FINISH_IMPACT_THRESHOLD)
            {
                _impact = Vector3.zero;
                _navMeshAgent.enabled = true;
            }
        }
        #endregion

        #region PublicMethods
        public void AddForce(Vector3 force)
        {
            _impact += force;
            if(_navMeshAgent)
            {
                _navMeshAgent.enabled = false;
            }
        }

        public void Jump(float jumpForce)
        {
            _verticalVelocity += jumpForce;
        }

        public void Reset()
        {
            _verticalVelocity = 0f;
            _impact = Vector3.zero;
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
