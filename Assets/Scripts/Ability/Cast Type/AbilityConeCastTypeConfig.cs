using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Cone", menuName = "Winter Universe/Ability/Cast Type/New Cone")]
    public class AbilityConeCastTypeConfig : AbilityCastTypeConfig
    {
        [SerializeField] private float _distance = 2f;
        [SerializeField] private float _angle = 45f;

        public float Distance => _distance;
        public float Angle => _angle;
    }
}