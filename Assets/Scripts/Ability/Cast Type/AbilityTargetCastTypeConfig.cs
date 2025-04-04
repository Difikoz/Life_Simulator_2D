using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Target", menuName = "Winter Universe/Ability/Cast Type/New Target")]
    public class AbilityTargetCastTypeConfig : AbilityCastTypeConfig
    {
        [SerializeField] private float _distance = 2f;

        public float Distance => _distance;

        public override void OnCast(PawnController caster, PawnController target, Vector3 position, Vector3 direction, AbilityHitTypeConfig hitType, AbilityTargetType targetType)
        {

        }
    }
}