using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Cone", menuName = "Winter Universe/Ability/Cast Type/New Cone")]
    public class AbilityConeCastTypeConfig : AbilityCastTypeConfig
    {
        [SerializeField] private float _distance = 2f;
        [SerializeField] private float _angle = 45f;

        public float Distance => _distance;
        public float Angle => _angle;

        public override void OnCast(PawnController caster, PawnController target, Vector3 position, Vector3 direction, float angle, AbilityHitTypeConfig hitType, AbilityTargetType targetType)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(position, _distance, GameManager.StaticInstance.LayersManager.DetectableMask);
            foreach (Collider2D collider in colliders)
            {
                if (Vector2.Angle(direction.normalized, (collider.transform.position - position).normalized) > _angle / 2f)
                {
                    continue;
                }
                if (collider.TryGetComponent(out target))
                {
                    hitType.OnHit(caster, target, collider.transform.position, (collider.transform.position - position).normalized, angle, targetType);
                }
                else
                {
                    hitType.OnHit(caster, collider, collider.transform.position, (collider.transform.position - position).normalized, angle, targetType);
                }
            }
        }
    }
}