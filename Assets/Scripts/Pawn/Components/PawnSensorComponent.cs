using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WinterUniverse
{
    public class PawnSensorComponent : PawnComponent
    {
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

        public void Detect()
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
                        if (target != _pawn)
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
            //Collider2D[] colliders = Physics2D.OverlapCircleAll(_pawn.transform.position, _pawn.ViewDistance, GameManager.StaticInstance.LayersManager.ObstacleMask);
            //if (colliders.Length > 0)
            //{
            //    foreach (Collider2D collider in colliders)
            //    {
            //        _obstacles.Add(collider);
            //    }
            //}
            //colliders = Physics2D.OverlapCircleAll(_pawn.transform.position, _pawn.ViewDistance, GameManager.StaticInstance.LayersManager.InteractableMask);
            //if (colliders.Length > 0)
            //{
            //    foreach (Collider2D collider in colliders)
            //    {
            //        if (collider.TryGetComponent(out InteractableBase interactable))
            //        {
            //            _interactables.Add(interactable);
            //        }
            //    }
            //}
            //colliders = Physics2D.OverlapCircleAll(_pawn.transform.position, _pawn.ViewDistance, GameManager.StaticInstance.LayersManager.DetectableMask);
            //if (colliders.Length > 0)
            //{
            //    foreach (Collider2D collider in colliders)
            //    {
            //        if (collider.TryGetComponent(out PawnController pawn))
            //        {
            //            if (pawn != _pawn && !pawn.IsDead)
            //            {
            //                if (pawn != _pawn.Target)
            //                {
            //                    _obstacles.Add(collider);// to avoid other pawns
            //                }
            //                if (TargetIsEnemy(pawn) && TargetIsVisible(pawn.transform))
            //                {
            //                    _targets.Add(pawn);
            //                }
            //            }
            //        }
            //    }
            //}
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