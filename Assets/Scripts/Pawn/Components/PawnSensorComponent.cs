using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WinterUniverse
{
    public class PawnSensorComponent : PawnComponent
    {
        [SerializeField] private float _detecttionCooldown = 0.5f;
        private float _detectionTime;

        public List<PawnController> DetectedTargets { get; private set; }
        public List<IInteractable> DetectedInteractables { get; private set; }
        public List<Collider2D> DetectedObstacles { get; private set; }

        public override void Initialize()
        {
            base.Initialize();
            DetectedTargets = new();
            DetectedInteractables = new();
            DetectedObstacles = new();
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            if (_detectionTime >= _detecttionCooldown)
            {
                _detectionTime = 0f;
                Detect();
            }
            else
            {
                _detectionTime += Time.fixedDeltaTime;
            }
        }

        private void Detect()
        {
            DetectedTargets.Clear();
            DetectedInteractables.Clear();
            DetectedObstacles.Clear();
            Collider2D[] colliders = Physics2D.OverlapCircleAll(_pawn.transform.position, _pawn.Status.StatHolder.ViewDistance, GameManager.StaticInstance.LayersManager.DetectableMask);
            if (colliders.Length > 0)
            {
                foreach (Collider2D collider in colliders)
                {
                    if (collider.TryGetComponent(out PawnController target))
                    {
                        if (target != _pawn && target.Status.StateHolder.CompareStateValue("Is Dead", false))
                        {
                            if (TargetIsEnemy(target) && TargetIsVisible(target))
                            {
                                DetectedTargets.Add(target);
                            }
                        }
                    }
                    else if (collider.TryGetComponent(out IInteractable interactable))
                    {
                        DetectedInteractables.Add(interactable);
                    }
                    else
                    {
                        DetectedObstacles.Add(collider);
                    }
                }

            }
        }

        public bool TargetIsVisible(PawnController target)
        {
            if (Vector2.Angle(_pawn.transform.right, (target.transform.position - _pawn.transform.position).normalized) > _pawn.Status.StatHolder.ViewAngle / 2f)
            {
                return false;
            }
            if (Physics2D.Linecast(_pawn.transform.position, target.transform.position, GameManager.StaticInstance.LayersManager.ObstacleMask))
            {
                return false;
            }
            return true;
        }

        public bool TargetIsVisible()
        {
            if (_pawn.Combat.Target == null)
            {
                return false;
            }
            return TargetIsVisible(_pawn.Combat.Target);
        }

        public bool TargetIsEnemy(PawnController target)
        {
            return _pawn.Status.Faction.GetState(target.Status.Faction) == RelationshipState.Enemy;
        }

        public PawnController GetClosestTarget()
        {
            return DetectedTargets.OrderBy(target => Vector2.Distance(target.transform.position, _pawn.transform.position)).FirstOrDefault();
        }
    }
}