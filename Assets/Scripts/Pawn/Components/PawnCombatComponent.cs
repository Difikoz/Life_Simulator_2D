using System;
using UnityEngine;

namespace WinterUniverse
{
    public class PawnCombatComponent : PawnComponent
    {
        public Action OnTargetChanged;

        private PawnController _target;

        public PawnController Target => _target;

        public void SetTarget(PawnController target)
        {
            if (target != null)
            {
                _target = target;
                OnTargetChanged?.Invoke();
            }
            else
            {
                ResetTarget();
            }
        }

        public void ResetTarget()
        {
            _target = null;
            OnTargetChanged?.Invoke();
        }
    }
}