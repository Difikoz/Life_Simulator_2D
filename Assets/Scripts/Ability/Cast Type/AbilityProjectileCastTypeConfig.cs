using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Projectile", menuName = "Winter Universe/Ability/Cast Type/New Projectile")]
    public class AbilityProjectileCastTypeConfig : AbilityCastTypeConfig
    {
        [SerializeField] private GameObject _projectile;
        [SerializeField] private float _range = 10f;
        [SerializeField] private float _force = 10f;
        [SerializeField] private float _spread = 5f;
        [SerializeField] private int _count = 1;
        [SerializeField] private int _pierce = 1;
        [SerializeField] private bool _isHoming;
        [SerializeField] private float _turnSpeed = 180f;

        public GameObject Projectile => _projectile;
        public float Range => _range;
        public float Force => _force;
        public float Spread => _spread;
        public int Count => _count;
        public int Pierce => _pierce;
        public bool IsHoming => _isHoming;
        public float TurnSpeed => _turnSpeed;

        public void SpawnProjectiles()
        {
            for (int i = 0; i < _count; i++)
            {
                _spread = Random.Range(-_spread, _spread);
                //_spread += _shootPoint.transform.eulerAngles.z;
                //_projectile(_shootPoint.transform.position, Quaternion.Euler(0f, 0f, _spread)).Initialize(_pawn, _config);
            }
        }

        public override void OnCast(PawnController caster, PawnController target, Vector3 position, Vector3 direction, AbilityHitTypeConfig hitType, AbilityTargetType targetType)
        {

        }
    }
}