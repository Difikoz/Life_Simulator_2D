using System;
using UnityEngine;

namespace WinterUniverse
{
    public class PawnLocomotionComponent : PawnComponent
    {
        public Action OnStartMoving;
        public Action OnStopMoving;

        private float _stopDistance;

        public Vector3 Destination { get; private set; }
        public Vector2 MoveDirection { get; private set; }
        public Vector2 LookDirection { get; private set; }
        public Vector2 MoveVelocity { get; private set; }
        public float RemainingDistance { get; private set; }
        public float LookAngle { get; private set; }
        public bool ReachedDestination { get; private set; }
        public bool IsMoving { get; private set; }

        public override void Enable()
        {
            base.Enable();
            StopMovement();
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            CalculateLookDirection();
            CheckDestination();
            HandleMovement();
            HandleRotation();
        }

        private void CalculateLookDirection()
        {
            if (_pawn.Combat.Target != null)
            {
                LookDirection = (_pawn.Combat.Target.transform.position - transform.position).normalized;
            }
            else
            {
                LookDirection = MoveDirection;
            }
        }

        private void CheckDestination()
        {
            RemainingDistance = Vector2.Distance(transform.position, Destination);
            if (ReachedDestination)
            {
                if (RemainingDistance > _stopDistance)
                {
                    ReachedDestination = false;
                }
            }
            else
            {
                if (RemainingDistance < _stopDistance)
                {
                    StopMovement();
                }
                else
                {

                    MoveDirection = _pawn.ContextSolver.GetDirectionToMove(Destination);
                }
            }
        }

        private void HandleMovement()
        {
            if (IsMoving)
            {
                if (MoveDirection == Vector2.zero || _pawn.Status.StateHolder.CompareStateValue("Is Perfoming Action", true))
                {
                    IsMoving = false;
                    _pawn.Animator.SetBool("Is Moving", IsMoving);
                    OnStopMoving?.Invoke();
                }
                else
                {
                    MoveVelocity = Vector2.MoveTowards(MoveVelocity, MoveDirection, 2f * Time.fixedDeltaTime);
                }
            }
            else
            {
                if (MoveDirection != Vector2.zero && _pawn.Status.StateHolder.CompareStateValue("Is Perfoming Action", false))
                {
                    IsMoving = true;
                    _pawn.Animator.SetBool("Is Moving", IsMoving);
                    OnStartMoving?.Invoke();
                }
                else
                {
                    MoveVelocity = Vector2.MoveTowards(MoveVelocity, Vector2.zero, 4f * Time.fixedDeltaTime);
                }
            }
            _pawn.RB.linearVelocity = MoveVelocity * _pawn.Status.StatHolder.MoveSpeed;
            _pawn.Animator.SetFloat("Velocity", _pawn.RB.linearVelocity.magnitude);
        }

        private void HandleRotation()
        {
            LookAngle = Mathf.Atan2(LookDirection.y, LookDirection.x) * Mathf.Rad2Deg;
            _pawn.RB.rotation = Mathf.MoveTowardsAngle(_pawn.RB.rotation, LookAngle, _pawn.Status.StatHolder.RotateSpeed * Time.fixedDeltaTime);
        }

        public void SetDestination(Vector3 position, float stopDistance = 0f)
        {
            if (stopDistance > 0f)
            {
                _stopDistance = stopDistance;
            }
            else
            {
                _stopDistance = _pawn.Collider.radius;
            }
            Destination = position;
            ReachedDestination = false;
        }

        public void StopMovement()
        {
            Destination = transform.position;
            MoveDirection = Vector2.zero;
            _stopDistance = _pawn.Collider.radius;
            ReachedDestination = true;
        }
    }
}