using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Ability", menuName = "Winter Universe/Ability/New Ability")]
    public class AbilityConfig : BasicInfoConfig
    {
        [SerializeField] private AbilityCastTypeConfig _castType;
        [SerializeField] private AbilityHitTypeConfig _hitType;

        public AbilityCastTypeConfig CastType => _castType;
        public AbilityHitTypeConfig HitType => _hitType;
    }
}