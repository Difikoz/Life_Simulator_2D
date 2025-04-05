using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace WinterUniverse
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CircleCollider2D))]
    public class ProjectileController : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private CircleCollider2D _collider;

        private PawnController _owner;
        private PawnController _target;
        private AbilityProjectileCastTypeConfig _config;
        private List<AbilityHitTypeConfig> _hitTypes;
        private AbilityTargetType _targetType;
        private Vector2 _directionToTarget;
        private float _angleToTarget;
        private int _pierceCount;

        public void Initialize(PawnController owner, PawnController target, AbilityProjectileCastTypeConfig config, List<AbilityHitTypeConfig> hitTypes, AbilityTargetType targetType)
        {
            _owner = owner;
            _target = target;
            _config = config;
            _hitTypes = hitTypes;
            _targetType = targetType;
            _spriteRenderer.sprite = _config.ProjectileSprite;
            _collider.radius = _config.ProjectileSize;
            _pierceCount = 0;
            _rb.linearVelocity = transform.right * _config.Force;
            if (_config.IsHoming && _target != null)
            {
                StartCoroutine(HomingCoroutine());
            }
            StartCoroutine(DespawnCoroutine());
        }

        private IEnumerator HomingCoroutine()
        {
            while (_target != null && _target.Status.StateHolder.CompareStateValue("Is Dead", false))
            {
                _directionToTarget = (_target.transform.position - transform.position).normalized;
                _angleToTarget = Mathf.Atan2(_directionToTarget.y, _directionToTarget.x) * Mathf.Rad2Deg;
                _rb.rotation = Mathf.MoveTowardsAngle(_rb.rotation, _angleToTarget, _config.TurnSpeed * Time.deltaTime);
                _rb.linearVelocity = transform.right * _config.Force;
                yield return null;
            }
        }

        private IEnumerator DespawnCoroutine()
        {
            yield return new WaitForSeconds(_config.Range / _config.Force * 1.1f);
            Despawn();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            _pierceCount++;
            foreach (AbilityHitTypeConfig hitType in _hitTypes)
            {
                hitType.OnHit(_owner, collision, transform.position, transform.right, transform.eulerAngles.z, _targetType);
            }
            if (_pierceCount > _config.Pierce)
            {
                Despawn();
            }
        }

        private void Despawn()
        {
            _rb.linearVelocity = Vector2.zero;
            LeanPool.Despawn(gameObject);
        }
    }
}