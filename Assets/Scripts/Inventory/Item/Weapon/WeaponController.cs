using System.Collections;
using UnityEngine;

namespace WinterUniverse
{
    public class WeaponController : BasicComponent
    {
        [SerializeField] private WeaponItemConfig _config;
        [SerializeField] private Transform _castPoint;

        private PawnController _pawn;
        private Coroutine _abilityCoroutine;

        public bool IsPerfomingAction => _abilityCoroutine != null;

        public override void Initialize()
        {
            base.Initialize();
            _pawn = GetComponentInParent<PawnController>();
        }

        public void PerformAction()
        {
            if (IsPerfomingAction)
            {
                return;
            }
            _abilityCoroutine = StartCoroutine(UseAbility());
        }

        private IEnumerator UseAbility()
        {
            foreach (AbilityConfig ability in _config.Abilities)
            {
                ability.CastType.OnCast(_pawn, _pawn.Combat.Target, _castPoint.position, _castPoint.right, _castPoint.eulerAngles.z, ability.HitType, ability.TargetType);
            }
            yield return new WaitForSeconds(_config.Cooldown);
            _abilityCoroutine = null;
        }
    }
}