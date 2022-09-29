using UnityEngine;
using UnityEngine.Assertions;

using TPCombat.Input;
using TPCombat.Debug;
using TPCombat.Physics;
using TPCombat.Combat;

//add sounds

//target & health as one component - Damagable?
    //or two non-inspector components and one component to add to inspector
    //*array of thresholds to raise events
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
        public  Ragdoll Ragdoll {get; private set;}

        [field: SerializeField]
        public Targeter Targeter {get; private set;}
        [field: SerializeField]
        public Attack[] Attacks {get; private set;}
        [field: SerializeField]
        public WeaponDamage WeaponDamage {get; private set;}
        [field: SerializeField]
        public Health Health {get; private set;}

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
            CustomLogger.AssertNotNull(Animator, "Animator", this);

            CustomLogger.AssertNotNull(CharacterController, "CharacterController", this);
            CustomLogger.AssertNotNull(ForceReceiver, "ForceReceiver", this);
            CustomLogger.AssertNotNull(Ragdoll, "Ragdoll", this);

            CustomLogger.AssertNotNull(Targeter, "Targeter", this);
            CustomLogger.AssertNotNull(WeaponDamage, "WeaponDamage", this);
            CustomLogger.AssertNotNull(Health, "Health", this);
        }
        
        private void Start() 
        {
            MainCameraTransform = Camera.main.transform;

            SwitchState(new PlayerFreeLookState(this));
        }

        void OnEnable()
        {
            Health.onTakeDamage += TakeDamage;
            Health.onDie += Die;
        }

        void OnDisable() 
        {
            Health.onTakeDamage -= TakeDamage;
            Health.onDie -= Die;
        }
        #endregion

        #region PublicMethods
        #endregion

        #region Interfaces & Inheritance
        #endregion

        #region Events & Statics
        
        void TakeDamage()
        {
            SwitchState(new PlayerImpactState(this));
        }
        
        void Die()
        {
            SwitchState(new PlayerDeadState(this));
        }
        #endregion

        #region PrivateMethods
        #endregion
    }
}
