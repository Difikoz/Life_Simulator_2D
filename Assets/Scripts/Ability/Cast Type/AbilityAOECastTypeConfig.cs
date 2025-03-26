using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "AOE", menuName = "Winter Universe/Ability/Cast Type/New AOE")]
    public class AbilityAOECastTypeConfig : AbilityCastTypeConfig
    {
        [SerializeField] private float _radius = 5f;
        [SerializeField] private int _maxTargets = 10;

        public float Radius => _radius;
        public float MaxTargets => _maxTargets;
    }
}