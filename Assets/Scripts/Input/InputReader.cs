using System;

using UnityEngine;
using UnityEngine.InputSystem;

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
        public Vector2 MovementValue {get; private set;}
        #endregion

        #region Events & Statics
        public event Action onJumpEvent;
        public event Action onDodgeEvent;
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
                onJumpEvent?.Invoke();
            }
        }

        public void OnDodge(InputAction.CallbackContext context)
        {
            if(context.performed)
            {
                onDodgeEvent?.Invoke();
            }
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            MovementValue = context.ReadValue<Vector2>();
        }

        public void OnLook(InputAction.CallbackContext context)
        {
        }
        #endregion

        #region Events & Statics
        #endregion

        #region PrivateMethods
        #endregion
    }
}
