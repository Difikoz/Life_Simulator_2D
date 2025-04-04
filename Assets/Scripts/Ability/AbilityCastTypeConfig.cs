using UnityEngine;

namespace WinterUniverse
{
    public abstract class AbilityCastTypeConfig : ScriptableObject
    {
        public abstract void OnCast(PawnController caster, PawnController target, Vector3 position, Vector3 direction, AbilityHitTypeConfig hitType, AbilityTargetType targetType);
    }
}