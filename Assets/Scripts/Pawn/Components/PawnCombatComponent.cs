using System;
using UnityEngine;

namespace WinterUniverse
{
    public class PawnCombatComponent : PawnComponent
    {
        public Action OnTargetChanged;

        public PawnController Target { get; private set; }
        public Vector2 LookDirection;// { get; private set; }
        public float LookAngle { get; private set; }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            LookAngle = Mathf.Atan2(LookDirection.y, LookDirection.x) * Mathf.Rad2Deg;
            if (_pawn.Locomotion.IsFacingRight)
            {
                _pawn.Animator.ArmsPoint.localRotation = Quaternion.Euler(0f, 0f, LookAngle);
            }
            else
            {
                _pawn.Animator.ArmsPoint.localRotation = Quaternion.Euler(0f, 180f, LookAngle);
            }
        }

        public void SetTarget(PawnController target)
        {
            if (target != null)
            {
                Target = target;
                OnTargetChanged?.Invoke();
            }
            else
            {
                ResetTarget();
            }
        }

        public void ResetTarget()
        {
            Target = null;
            OnTargetChanged?.Invoke();
        }
    }
}