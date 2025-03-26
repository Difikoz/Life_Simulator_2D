using UnityEngine;

namespace WinterUniverse
{
    public class PawnLocomotionComponent : PawnComponent
    {
        private Rigidbody2D _rb;
        [SerializeField] private Vector3 _destination;
        [SerializeField] private Vector2 _moveDirection;
        [SerializeField] private Vector2 _moveVelocity;
        [SerializeField] private bool _reachedDestination;
        [SerializeField] private bool _isFacingRight;

        public bool ReachedDestination => _reachedDestination;
        public bool IsFacingRight => _isFacingRight;

        public override void Initialize()
        {
            base.Initialize();
            _rb = GetComponent<Rigidbody2D>();
            _isFacingRight = true;
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            CalculateMoveVelocity();
            Flip();
            _rb.linearVelocity = _moveVelocity;
        }

        private void CalculateMoveVelocity()
        {
            if (_moveDirection != Vector2.zero && _pawn.Status.StateHolder.CompareStateValue("Is Perfoming Action", false))
            {
                _moveVelocity = Vector2.MoveTowards(_moveVelocity, _moveDirection * _pawn.Status.StatHolder.MoveSpeed, 8f * Time.fixedDeltaTime);
            }
            else
            {
                _moveVelocity = Vector2.MoveTowards(_moveVelocity, Vector2.zero, 16f * Time.fixedDeltaTime);
            }
            _pawn.Animator.SetFloat("Velocity", _moveVelocity.magnitude);
        }

        private void Flip()
        {
            if (_isFacingRight && _moveVelocity.x < 0f)
            {
                FlipLeft();
            }
            else if (!_isFacingRight && _moveVelocity.x > 0f)
            {
                FlipRight();
            }
        }

        private void FlipRight()
        {
            _isFacingRight = true;
            transform.localScale = new(1f, 1f, 1f);
        }

        private void FlipLeft()
        {
            _isFacingRight = false;
            transform.localScale = new(-1f, 1f, 1f);
        }
    }
}