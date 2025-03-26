using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Stat", menuName = "Winter Universe/Pawn/New Stat")]
    public class StatConfig : BasicInfoConfig
    {
        [SerializeField] private float _baseValue;
        [SerializeField] private float _minValue = -999999f;
        [SerializeField] private float _maxValue = 999999f;
        [SerializeField] private bool _isPercent;

        public float BaseValue => _baseValue;
        public float MinValue => _minValue;
        public float MaxValue => _maxValue;
        public bool IsPercent => _isPercent;
    }
}