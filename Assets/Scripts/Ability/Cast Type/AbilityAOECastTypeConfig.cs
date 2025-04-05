using System.Linq;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "AOE", menuName = "Winter Universe/Ability/Cast Type/New AOE")]
    public class AbilityAOECastTypeConfig : AbilityCastTypeConfig
    {
        [SerializeField] private float _radius = 5f;
        [SerializeField] private int _maxTargets = 10;

        public float Radius => _radius;
        public float MaxTargets => _maxTargets;

        public override void OnCast(PawnController caster, PawnController target, Vector3 position, Vector3 direction, float angle, AbilityHitTypeConfig hitType, AbilityTargetType targetType)
        {
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
                        hitType.OnHit(caster, target, collider.transform.position, (collider.transform.position - position).normalized, angle, targetType);
                    }
                }
                else
                {
                    hitType.OnHit(caster, collider, collider.transform.position, (collider.transform.position - position).normalized, angle, targetType);
                }
            }
        }
    }
}