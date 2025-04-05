using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "AOE", menuName = "Winter Universe/Ability/Cast Type/New AOE")]
    public class AbilityAOECastTypeConfig : AbilityCastTypeConfig
    {
        [SerializeField, Range(0.1f, 999f)] private float _radius = 5f;
        [SerializeField, Range(1, 999)] private int _maxTargets = 10;

        public float Radius => _radius;
        public float MaxTargets => _maxTargets;

        public override void OnCast(PawnController caster, PawnController target, Vector3 position, Vector3 direction, float angle, List<AbilityHitTypeConfig> hitTypes, AbilityTargetType targetType)
        {
            if (target != null)
            {
                position = target.transform.position;
            }
            Collider2D[] colliders = Physics2D.OverlapCircleAll(position, _radius, GameManager.StaticInstance.LayersManager.DetectableMask);
            colliders = colliders.OrderBy(pawn => Vector2.Distance(pawn.transform.position, position)).ToArray();
            int amount = 0;
            foreach (Collider2D collider in colliders)
            {
                if (collider.TryGetComponent(out target))
                {
                    if (amount < _maxTargets)
                    {
                        amount++;
                        foreach(AbilityHitTypeConfig hitType in hitTypes)
                        {
                            hitType.OnHit(caster, target, collider.transform.position, (collider.transform.position - position).normalized, angle, targetType);
                        }
                    }
                }
                else
                {
                    foreach (AbilityHitTypeConfig hitType in hitTypes)
                    {
                        hitType.OnHit(caster, collider, collider.transform.position, (collider.transform.position - position).normalized, angle, targetType);
                    }
                }
            }
        }
    }
}