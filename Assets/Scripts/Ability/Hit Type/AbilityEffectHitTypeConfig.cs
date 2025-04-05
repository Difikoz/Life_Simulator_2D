using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Effect", menuName = "Winter Universe/Ability/Hit Type/New Effect")]
    public class AbilityEffectHitTypeConfig : AbilityHitTypeConfig
    {
        [SerializeField] private List<EffectCreator> _effects = new();

        public List<EffectCreator> Effects => _effects;

        public override void OnHit(PawnController caster, Collider2D collider, Vector3 position, Vector3 direction, float angle, AbilityTargetType targetType)
        {

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
            target.Status.EffectHolder.ApplyEffects(_effects, caster);
        }
    }
}