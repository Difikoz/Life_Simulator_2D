using System;
using System.Collections;
using UnityEngine;

namespace WinterUniverse
{
    public class PawnCombatComponent : PawnComponent
    {
        public Action OnTargetChanged;

        [SerializeField] private Transform _castPoint;

        private PawnController _abilityTarget;
        private Coroutine _coroutine;

        public PawnController Target { get; private set; }
        public AbilityConfig Ability { get; private set; }

        public bool IsPerfomingAction => _coroutine != null;

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            if (Target != null)
            {
                if (Target.Status.StateHolder.CompareStateValue("Is Dead", true))
                {
                    ResetTarget();
                }
                else
                {
                    if (_pawn.Locomotion.ReachedDestination && CanCastAbility())
                    {
                        CastAbility();
                    }
                    else if(!IsPerfomingAction)
                    {
                        _pawn.Locomotion.SetDestination(Target.transform.position, Ability.CastType.Distance);
                    }
                }
            }
            else if (_pawn.Sensor.DetectedTargets.Count > 0)
            {
                SetTarget(_pawn.Sensor.GetClosestTarget());
            }
        }

        public bool CanCastAbility()
        {
            if (IsPerfomingAction)
            {
                return false;
            }
            Ability = _pawn.Equipment.Weapon.Ability;
            switch (Ability.TargetType)
            {
                case AbilityTargetType.Caster:
                    _abilityTarget = _pawn;
                    break;
                case AbilityTargetType.Target:
                    _abilityTarget = _pawn.Combat.Target;
                    break;
                case AbilityTargetType.All:
                    _abilityTarget = _pawn.Combat.Target;
                    break;
            }
            if (Ability.CastType.CanCast(_pawn, _abilityTarget, _castPoint.position, _castPoint.right, _castPoint.eulerAngles.z, Ability.HitTypes, Ability.TargetType))
            {
                return true;
            }
            return false;
        }

        public void CastAbility()
        {
            _pawn.Locomotion.StopMovement();
            _coroutine = StartCoroutine(AbilityCooldown());
        }

        public void SetTarget(PawnController target)
        {
            if (target != null)
            {
                Ability = _pawn.Equipment.Weapon.Ability;
                Target = target;
                OnTargetChanged?.Invoke();
                _pawn.Locomotion.SetDestination(Target.transform.position, Ability.CastType.Distance);
            }
            else
            {
                ResetTarget();
            }
        }

        public void ResetTarget()
        {
            Target = null;
            OnTargetChanged?.Invoke();
            _pawn.Locomotion.StopMovement();
        }

        private IEnumerator AbilityCooldown()
        {
            yield return new WaitForSeconds(Ability.CastTime);
            if (Ability.CastType.CanCast(_pawn, _abilityTarget, _castPoint.position, _castPoint.right, _castPoint.eulerAngles.z, Ability.HitTypes, Ability.TargetType))
            {
                Ability.CastType.OnCast(_pawn, _abilityTarget, _castPoint.position, _castPoint.right, _castPoint.eulerAngles.z, Ability.HitTypes, Ability.TargetType);
            }
            yield return new WaitForSeconds(Ability.CooldownTime);
            _coroutine = null;
        }
    }
}