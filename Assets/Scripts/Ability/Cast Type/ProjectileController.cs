using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class ProjectileController : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rb;

        private PawnController _owner;
        private PawnController _target;
        private AbilityProjectileCastTypeConfig _config;
        private int _pierceCount;
        private List<PawnController> _damagedTargets = new();

        public void Initialize(PawnController owner, PawnController target, AbilityProjectileCastTypeConfig config)
        {
            _owner = owner;
            _target = target;
            _config = config;
            _pierceCount = 0;
            _rb.linearVelocity = transform.right * _config.Force;
            StartCoroutine(DespawnCoroutine());
        }

        private IEnumerator DespawnCoroutine()
        {
            yield return new WaitForSeconds(_config.Range / _config.Force * 1.1f);
            Despawn();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            PawnController target = collision.GetComponentInParent<PawnController>();
            if (_owner != target && !_damagedTargets.Contains(target))
            {
                _damagedTargets.Add(target);
                //target.Status.ApplyDamage(_config.DamageTypes, _owner);
                _pierceCount++;
                if (_pierceCount > _config.Pierce)
                {
                    Despawn();
                }
            }
        }

        private void Despawn()
        {
            _damagedTargets.Clear();
            _rb.linearVelocity = Vector2.zero;
            LeanPool.Despawn(gameObject);
        }
    }
}