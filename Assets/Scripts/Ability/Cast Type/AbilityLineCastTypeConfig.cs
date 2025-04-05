using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Line", menuName = "Winter Universe/Ability/Cast Type/New Line")]
    public class AbilityLineCastTypeConfig : AbilityCastTypeConfig
    {
        [SerializeField] private float _width = 1f;
        [SerializeField] private float _length = 2f;

        public float Width => _width;
        public float Length => _length;

        public override void OnCast(PawnController caster, PawnController target, Vector3 position, Vector3 direction, float angle, AbilityHitTypeConfig hitType, AbilityTargetType targetType)
        {
            Collider2D[] colliders = Physics2D.OverlapBoxAll(position + direction.normalized * _length / 2f, new(_width, _length), angle, GameManager.StaticInstance.LayersManager.DetectableMask);
            foreach (Collider2D collider in colliders)
            {
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