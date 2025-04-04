using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Effect", menuName = "Winter Universe/Ability/Hit Type/New Effect")]
    public class AbilityEffectHitTypeConfig : AbilityHitTypeConfig
    {
        [SerializeField] private List<EffectCreator> _effects = new();

        public List<EffectCreator> Effects => _effects;

        public override void OnHit(PawnController caster, Collider2D collider, Vector3 position, Vector3 direction, AbilityTargetType targetType)
        {

        }
    }
}