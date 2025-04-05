using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Target", menuName = "Winter Universe/Ability/Cast Type/New Target")]
    public class AbilityTargetCastTypeConfig : AbilityCastTypeConfig
    {
        public override void OnCast(PawnController caster, PawnController target, Vector3 position, Vector3 direction, float angle, List<AbilityHitTypeConfig> hitTypes, AbilityTargetType targetType)
        {
            if (target != null)
            {
                position = target.transform.position;
            }
            foreach (AbilityHitTypeConfig hitType in hitTypes)
            {
                hitType.OnHit(caster, target, position, direction, angle, targetType);
            }
        }
    }
}