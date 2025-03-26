using UnityEngine;

namespace WinterUniverse
{
    [System.Serializable]
    public class DamageType
    {
        [SerializeField] private float _damage;
        [SerializeField] private DamageTypeConfig _type;

        public float Damage => _damage;
        public DamageTypeConfig Type => _type;
    }
}