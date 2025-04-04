using UnityEngine;

namespace WinterUniverse
{
    public class WeaponController : BasicComponent
    {
        [SerializeField] private WeaponItemConfig _config;

        private PawnController _pawn;

        public bool IsPerfomingAction { get; private set; }

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
            IsPerfomingAction = true;
        }
    }
}