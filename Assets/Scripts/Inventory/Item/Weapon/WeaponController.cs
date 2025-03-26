using UnityEngine;

namespace WinterUniverse
{
    public class WeaponController : BasicComponent
    {
        private PawnController _pawn;

        public bool IsPerfomingAction;

        public override void Initialize()
        {
            base.Initialize();
            _pawn = GetComponentInParent<PawnController>();
        }
    }
}