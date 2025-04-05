using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Heal", menuName = "Winter Universe/Ability/Hit Type/New Heal")]
    public class AbilityHealHitTypeConfig : AbilityHitTypeConfig
    {
        [SerializeField, Range(1f, 999999f)] private float _value;

        public float Value => _value;

        public override void OnHit(PawnController caster, Collider2D collider, Vector3 position, Vector3 direction, float angle, AbilityTargetType targetType)
        {
            PawnController target = collider.GetComponentInParent<PawnController>();
            if (target != null)
            {
                OnHit(caster, target, position, direction, angle, targetType);
            }
        }

        public override void OnHit(PawnController caster, PawnController target, Vector3 position, Vector3 direction, float angle, AbilityTargetType targetType)
        {
            switch (targetType)
            {
                case AbilityTargetType.Caster:
                    if (caster != target)
                    {
                        return;
                    }
                    break;
                case AbilityTargetType.Target:
                    if (caster == target)
                    {
                        return;
                    }
                    break;
                case AbilityTargetType.All:

                    break;
            }
            target.Status.RestoreHealthCurrent(_value);
        }
    }
}