using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Line", menuName = "Winter Universe/Ability/Cast Type/New Line")]
    public class AbilityLineCastTypeConfig : AbilityCastTypeConfig
    {
        [SerializeField, Range(0.1f, 999f)] private float _width = 1f;
        [SerializeField, Range(0.1f, 999f)] private float _length = 2f;

        public float Width => _width;
        public float Length => _length;

        public override void OnCast(PawnController caster, PawnController target, Vector3 position, Vector3 direction, float angle, List<AbilityHitTypeConfig> hitTypes, AbilityTargetType targetType)
        {
            Collider2D[] colliders = Physics2D.OverlapBoxAll(position + direction.normalized * _length / 2f, new(_width, _length), angle, GameManager.StaticInstance.LayersManager.DetectableMask);
            foreach (Collider2D collider in colliders)
            {
                foreach (AbilityHitTypeConfig hitType in hitTypes)
                {
                    hitType.OnHit(caster, collider, collider.transform.position, (collider.transform.position - position).normalized, angle, targetType);
                }
            }
        }
    }
}