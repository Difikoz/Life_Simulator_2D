using Lean.Pool;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Projectile", menuName = "Winter Universe/Ability/Cast Type/New Projectile")]
    public class AbilityProjectileCastTypeConfig : AbilityCastTypeConfig
    {
        [SerializeField] private GameObject _projectile;
        [SerializeField] private Sprite _projectileSprite;
        [SerializeField] private float _projectileSize = 0.2f;
        [SerializeField] private float _range = 10f;
        [SerializeField] private float _force = 10f;
        [SerializeField] private float _spread = 5f;
        [SerializeField] private int _count = 1;
        [SerializeField] private int _pierce = 1;
        [SerializeField] private bool _isHoming;
        [SerializeField] private float _turnSpeed = 180f;

        public GameObject Projectile => _projectile;
        public Sprite ProjectileSprite => _projectileSprite;
        public float ProjectileSize => _projectileSize;
        public float Range => _range;
        public float Force => _force;
        public float Spread => _spread;
        public int Count => _count;
        public int Pierce => _pierce;
        public bool IsHoming => _isHoming;
        public float TurnSpeed => _turnSpeed;

        public override void OnCast(PawnController caster, PawnController target, Vector3 position, Vector3 direction, float angle, AbilityHitTypeConfig hitType, AbilityTargetType targetType)
        {
            float spread;
            for (int i = 0; i < _count; i++)
            {
                spread = angle + Random.Range(-_spread, _spread);
                LeanPool.Spawn(_projectile, position, Quaternion.Euler(0f, 0f, spread)).GetComponent<ProjectileController>().Initialize(caster, target, this, hitType, targetType);
            }
        }
    }
}