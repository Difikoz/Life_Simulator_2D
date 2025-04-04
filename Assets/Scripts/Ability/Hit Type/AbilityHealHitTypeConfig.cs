using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Heal", menuName = "Winter Universe/Ability/Hit Type/New Heal")]
    public class AbilityHealHitTypeConfig : AbilityHitTypeConfig
    {
        [SerializeField] private float _value;

        public float Value => _value;

        public override void OnHit(PawnController caster, Collider2D collider, Vector3 position, Vector3 direction, AbilityTargetType targetType)
        {

        }
    }
}