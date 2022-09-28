using System;

using UnityEngine;
using UnityEngine.InputSystem;

using TPCombat.Debug;

namespace TPCombat.Input
{
    public class InputReader : MonoBehaviour, Controls.IPlayerActions
    {
        #region Config
        //[Header("CONFIG")]
        #endregion

        #region Cache
        //[Header("CACHE")]
        //[Space(8f)]
        Controls _controls;
        #endregion

        #region States
        public bool IsAttacking {get; private set;}
        public Vector2 MovementValue {get; private set;}
        #endregion

        #region Events & Statics
        public event Action onJumpInput;
        public event Action onDodgeInput;
        public event Action onTargetInput;
        public event Action onCancelInput;
        #endregion

        #region Data
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////

        #region EngineMethods & Contructors
        void Start() 
        {
            _controls = new Controls();
            _controls.Player.SetCallbacks(this);

            _controls.Player.Enable();
        }

        void OnDestroy() 
        {
            _controls.Player.Disable();
        }
        #endregion

        #region PublicMethods
        #endregion

        #region Interfaces & Inheritance
        public void OnJump(InputAction.CallbackContext context)
        {
            if(context.performed)
            {
                onJumpInput?.Invoke();
            }
        }

        public void OnDodge(InputAction.CallbackContext context)
        {
            if(context.performed)
            {
                onDodgeInput?.Invoke();
            }
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            MovementValue = context.ReadValue<Vector2>();
        }

        public void OnLook(InputAction.CallbackContext context)
        {
        }

        public void OnTarget(InputAction.CallbackContext context)
        {
            if(context.performed)
            {
                onTargetInput?.Invoke();
            }
        }

        public void OnCancel(InputAction.CallbackContext context)
        {
            if(context.performed)
            {
                onCancelInput?.Invoke();
            }
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            if(context.performed)
            {
                IsAttacking = true;
                CustomLogger.Log("attack input start", this, LogCategory.Input, LogFrequency.Frequent, LogDetails.Basic);
            }
            else if(context.canceled)
            {
                IsAttacking = false;
                CustomLogger.Log("attack input end", this, LogCategory.Input, LogFrequency.Frequent, LogDetails.Basic);
            }
        }
        #endregion

        #region Events & Statics
        #endregion

        #region PrivateMethods
        #endregion
    }
}
