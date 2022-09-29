using System.Collections.Generic;

using UnityEngine;

using Cinemachine;

using TPCombat.Debug;

namespace TPCombat.Combat
{
    public class Targeter : MonoBehaviour
    {
        #region Config
        //[Header("CONFIG")]
        #endregion

        #region Cache
        [Header("CACHE")]
    	//[Space(8f)]
        [SerializeField]
        CinemachineTargetGroup _cinemachineTargetGroup;

        private Camera _mainCamera;
        #endregion

        #region States
        List<Target> _targets = new List<Target>();
        public Target CurrentTarget {get; private set;}
        #endregion

        #region Events & Statics
        #endregion

        #region Data
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////

        #region EngineMethods & Contructors
        private void Awake() 
        {
            CustomLogger.AssertNotNull(_cinemachineTargetGroup, "_cinemachineTargetGroup", this);
        }

        private void Start() 
        {
            _mainCamera = Camera.main;
        }

        void OnTriggerEnter(Collider other) 
        {
            if(other.TryGetComponent<Target>(out var target))
            {
                _targets.Add(target);
                target.onDestroyEvent += RemoveTarget;
            }
        }

        void OnTriggerExit(Collider other) 
        {
            if(other.TryGetComponent<Target>(out var target))
            {
                RemoveTarget(target);
            }
        }
        #endregion

        #region PublicMethods
        public bool SelectTarget()
        {
            if(_targets.Count != 0)
            {
                Target closestTarget = null;
                float closestTargetDistance = Mathf.Infinity;

                foreach (Target target in _targets)
                {
                    Vector2 viewPosition = _mainCamera.WorldToViewportPoint(target.transform.position);

                    if(target.GetComponentInChildren<Renderer>().isVisible)
                    {
                        Vector2 distanceToCenter = viewPosition - new Vector2(.5f, .5f);
                        if(distanceToCenter.sqrMagnitude < closestTargetDistance)
                        {
                            closestTarget = target;
                            closestTargetDistance = distanceToCenter.sqrMagnitude;
                        }
                    }
                }

                if(closestTarget)
                {    
                    CurrentTarget = closestTarget;
                    _cinemachineTargetGroup.AddMember(CurrentTarget.transform, 1f, 2f);
                    return true;
                }
                else
                    return false;
            }

            return false;
        }

        public void Cancel()
        {
            if(CurrentTarget)
            {
                _cinemachineTargetGroup.RemoveMember(CurrentTarget.transform);
                CurrentTarget = null;
            }
        }
        #endregion

        #region Interfaces & Inheritance
        #endregion

        #region Events & Statics
        void RemoveTarget(Target target)
        {
            if(CurrentTarget == target)
            {
                _cinemachineTargetGroup.RemoveMember(CurrentTarget.transform);
                CurrentTarget = null;
            }

            target.onDestroyEvent -= RemoveTarget;
            _targets.Remove(target);
        }
        #endregion

        #region PrivateMethods
        #endregion
    }
}
