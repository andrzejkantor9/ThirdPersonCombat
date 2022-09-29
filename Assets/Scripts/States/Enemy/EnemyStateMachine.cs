using UnityEngine;
using UnityEngine.AI;

using TPCombat.Debug;
using TPCombat.Physics;
using TPCombat.Combat;

namespace TPCombat.States.Enemy
{
    public class EnemyStateMachine : StateMachine
    {
        #region Config
        [field: Header("CONFIG")]
        [field: SerializeField]
        public float PlayerChasingRange {get; private set;}
        [field: SerializeField]
        public float MovementSpeed {get; private set;}
        [field: SerializeField]
        public float AttackRange {get; private set;}
        [field: SerializeField]
        public int AttackDamage {get; private set;}
        [field: SerializeField]
        public float AttackKnockback {get; private set;}
        #endregion

        #region Cache
        [field: Header("CACHE")]
    	[field: Space(8f)]
        [field: SerializeField]
        public Animator Animator {get; private set;}
        [field: SerializeField]
        public NavMeshAgent Agent {get; private set;}

        [field: SerializeField]
        public WeaponDamage WeaponDamage {get; private set;}
        [field: SerializeField]
        public Health Health {get; private set;}
        [field: SerializeField]
        public Target Target {get; private set;}

        [field: SerializeField]
        public CharacterController CharacterController {get; private set;}
        [field: SerializeField]
        public  ForceReceiver ForceReceiver {get; private set;}
        [field: SerializeField]
        public  Ragdoll Ragdoll {get; private set;}

        public Health PlayerHealth {get; private set;}
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
            CustomLogger.AssertNotNull(Animator, "Animator", this);
            CustomLogger.AssertNotNull(Agent, "Agent", this);

            CustomLogger.AssertNotNull(WeaponDamage, "WeaponDamage", this);
            CustomLogger.AssertNotNull(Health, "Health", this);

            CustomLogger.AssertNotNull(CharacterController, "CharacterController", this);
            CustomLogger.AssertNotNull(ForceReceiver, "ForceReceiver", this);
            CustomLogger.AssertNotNull(Ragdoll, "Ragdoll", this);
        }

        void Start() 
        {
            PlayerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
            Agent.updatePosition = false;
            Agent.updateRotation = false;

            SwitchState(new EnemyIdleState(this));
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

        void OnDrawGizmosSelected() 
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, PlayerChasingRange);
        }
        #endregion

        #region PublicMethods
        #endregion

        #region Interfaces & Inheritance
        #endregion

        #region Events & Statics
        void TakeDamage()
        {
            SwitchState(new EnemyImpactState(this));
        }
        
        void Die()
        {
            SwitchState(new EnemyDeadState(this));
        }
        #endregion

        #region PrivateMethods
        #endregion
    }
}
