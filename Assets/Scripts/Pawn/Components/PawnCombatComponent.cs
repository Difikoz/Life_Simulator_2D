using System;
using UnityEngine;

namespace WinterUniverse
{
    public class PawnCombatComponent : PawnComponent
    {
        public Action OnTargetChanged;

        public PawnController Target { get; private set; }

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