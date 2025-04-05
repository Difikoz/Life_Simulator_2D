using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Damage", menuName = "Winter Universe/Ability/Hit Type/New Damage")]
    public class AbilityDamageHitTypeConfig : AbilityHitTypeConfig
    {
        [SerializeField] private List<DamageType> _damageTypes = new();

        public List<DamageType> DamageTypes => _damageTypes;

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
            target.Status.ApplyDamage(_damageTypes, caster);
        }
    }
}