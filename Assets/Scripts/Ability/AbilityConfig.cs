using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Ability", menuName = "Winter Universe/Ability/New Ability")]
    public class AbilityConfig : BasicInfoConfig
    {
        [SerializeField] private AbilityCastTypeConfig _castType;
        [SerializeField] private List<AbilityHitTypeConfig> _hitTypes = new();
        [SerializeField] private AbilityTargetType _targetType;
        [SerializeField, Range(0.1f, 100f)] private float _castTime = 0.25f;
        [SerializeField, Range(0.1f, 999999f)] private float _cooldownTime = 1f;

        public AbilityCastTypeConfig CastType => _castType;
        public List<AbilityHitTypeConfig> HitTypes => _hitTypes;
        public AbilityTargetType TargetType => _targetType;
        public float CastTime => _castTime;
        public float CooldownTime => _cooldownTime;
    }
}