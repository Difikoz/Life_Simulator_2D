using System;
using UnityEngine;

namespace WinterUniverse
{
    public class PawnLocomotionComponent : PawnComponent
    {
        public Action OnStartMoving;
        public Action OnStopMoving;

        public Vector2 MoveDirection;// { get; private set; }
        public Vector2 MoveVelocity { get; private set; }
        public bool ReachedDestination { get; private set; }
        public bool IsMoving { get; private set; }
        public bool IsFacingRight { get; private set; }

        public override void Initialize()
        {
            base.Initialize();
            IsFacingRight = true;
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            HandleMovement();
            HandleRotation();
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
                    MoveVelocity = Vector2.MoveTowards(MoveVelocity, MoveDirection, Time.fixedDeltaTime);
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
                    MoveVelocity = Vector2.MoveTowards(MoveVelocity, Vector2.zero, Time.fixedDeltaTime);
                }
            }
            _pawn.RB.linearVelocity = MoveVelocity * _pawn.Status.StatHolder.MoveSpeed;
            _pawn.Animator.SetFloat("Velocity", _pawn.RB.linearVelocity.magnitude);
        }

        private void HandleRotation()
        {
            if (IsFacingRight)
            {
                if (MoveVelocity.x < 0f)
                {
                    IsFacingRight = false;
                    transform.localScale = new(-1f, 1f, 1f);
                    _pawn.Animator.ArmsPoint.localScale = new(1f, -1f, 1f);
                }
            }
            else
            {
                if (MoveVelocity.x > 0f)
                {
                    IsFacingRight = true;
                    transform.localScale = new(1f, 1f, 1f);
                    _pawn.Animator.ArmsPoint.localScale = new(1f, 1f, 1f);
                }
            }
        }
    }
}