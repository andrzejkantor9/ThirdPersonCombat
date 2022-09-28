using UnityEngine;
using UnityEngine.Assertions;

using TPCombat.Input;
using TPCombat.Debug;
using TPCombat.Targeting;
using TPCombat.Physics;
using TPCombat.Combat;

namespace TPCombat.States.Player
{
    public class PlayerStateMachine : StateMachine
    {
        #region Config
        [field: Header("CONFIG")]
        [field: SerializeField]
        public float FreeLookMovementSpeed {get; private set;}
        [field: SerializeField]
        public float TargetingMovementSpeed {get; private set;}
        [field: SerializeField]
        public float RotationDamping {get; private set;}
        #endregion

        #region Cache
        [field: Header("CACHE")]
    	[field: Space(8f)]
        
        [field: SerializeField]
        public InputReader InputReader {get; private set;}
        [field: SerializeField]
        public Animator Animator {get; private set;}

        [field: SerializeField]
        public CharacterController CharacterController {get; private set;}
        [field: SerializeField]
        public  ForceReceiver ForceReceiver {get; private set;}

        [field: SerializeField]
        public Targeter Targeter {get; private set;}
        [field: SerializeField]
        public Attack[] Attacks {get; private set;}
        [field: SerializeField]
        public WeaponDamage WeaponDamage {get; private set;}

        public Transform MainCameraTransform {get; private set;}
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
            CustomLogger.AssertNotNull(InputReader, "InputReader", this);
            CustomLogger.AssertNotNull(CharacterController, "CharacterController", this);
            CustomLogger.AssertNotNull(Animator, "Animator", this);
            CustomLogger.AssertNotNull(Targeter, "Targeter", this);
            CustomLogger.AssertNotNull(ForceReceiver, "ForceReceiver", this);
        }
        
        private void Start() 
        {
            MainCameraTransform = Camera.main.transform;

            SwitchState(new PlayerFreeLookState(this));
        }
        #endregion

        #region PublicMethods
        #endregion

        #region Interfaces & Inheritance
        #endregion

        #region Events & Statics
        #endregion

        #region PrivateMethods
        #endregion
    }
}
