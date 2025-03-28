using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Line", menuName = "Winter Universe/Ability/Cast Type/New Line")]
    public class AbilityLineCastTypeConfig : AbilityCastTypeConfig
    {
        [SerializeField] private float _width = 1f;
        [SerializeField] private float _length = 2f;

        public float Width => _width;
        public float Length => _length;

    }
}