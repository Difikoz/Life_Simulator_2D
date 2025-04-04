using UnityEngine;

namespace WinterUniverse
{
    public abstract class AbilityHitTypeConfig : ScriptableObject
    {
        public abstract void OnHit(PawnController caster, Collider2D collider, Vector3 position, Vector3 direction, AbilityTargetType targetType);
    }
}