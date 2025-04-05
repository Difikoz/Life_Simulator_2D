using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Cone", menuName = "Winter Universe/Ability/Cast Type/New Cone")]
    public class AbilityConeCastTypeConfig : AbilityCastTypeConfig
    {
        [SerializeField, Range(0f, 360f)] private float _angle = 45f;

        public float Angle => _angle;

        public override void OnCast(PawnController caster, PawnController target, Vector3 position, Vector3 direction, float angle, List<AbilityHitTypeConfig> hitTypes, AbilityTargetType targetType)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(position, _distance, GameManager.StaticInstance.LayersManager.DetectableMask);
            foreach (Collider2D collider in colliders)
            {
                if (Vector2.Angle(direction.normalized, (collider.transform.position - position).normalized) > _angle / 2f)
                {
                    continue;
                }
                foreach (AbilityHitTypeConfig hitType in hitTypes)
                {
                    hitType.OnHit(caster, collider, collider.transform.position, (collider.transform.position - position).normalized, angle, targetType);
                }
            }
        }
    }
}