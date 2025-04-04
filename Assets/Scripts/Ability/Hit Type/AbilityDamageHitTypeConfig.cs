using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Damage", menuName = "Winter Universe/Ability/Hit Type/New Damage")]
    public class AbilityDamageHitTypeConfig : AbilityHitTypeConfig
    {
        [SerializeField] private List<DamageType> _damageTypes = new();

        public List<DamageType> DamageTypes => _damageTypes;

        public override void OnHit(PawnController caster, Collider2D collider, Vector3 position, Vector3 direction, AbilityTargetType targetType)
        {
            
        }
    }
}