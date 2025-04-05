using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public abstract class AbilityCastTypeConfig : ScriptableObject
    {
        [SerializeField, Range(0.1f, 999f)] protected float _distance = 2f;

        public float Distance => _distance;

        public virtual bool CanCast(PawnController caster, PawnController target, Vector3 position, Vector3 direction, float angle, List<AbilityHitTypeConfig> hitTypes, AbilityTargetType targetType)
        {
            if (target != null && Vector2.Distance(target.transform.position, position) > _distance)
            {
                return false;
            }
            return true;
        }

        public abstract void OnCast(PawnController caster, PawnController target, Vector3 position, Vector3 direction, float angle, List<AbilityHitTypeConfig> hitTypes, AbilityTargetType targetType);
    }
}