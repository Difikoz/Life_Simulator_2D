using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Ability", menuName = "Winter Universe/Ability/New Ability")]
    public class AbilityConfig : BasicInfoConfig
    {
        [SerializeField] private AbilityCastTypeConfig _castType;
        [SerializeField] private AbilityHitTypeConfig _hitType;
        [SerializeField] private AbilityTargetType _targetType;

        public AbilityCastTypeConfig CastType => _castType;
        public AbilityHitTypeConfig HitType => _hitType;
        public AbilityTargetType TargetType => _targetType;
    }
}