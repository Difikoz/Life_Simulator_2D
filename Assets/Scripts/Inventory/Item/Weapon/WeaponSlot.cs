using UnityEngine;

namespace WinterUniverse
{
    public class WeaponSlot : BasicComponent
    {
        [SerializeField] private Transform _weaponRoot;

        private PawnController _pawn;
        private WeaponItemConfig _config;
        private WeaponController _weapon;

        public WeaponItemConfig Config => _config;
        public WeaponController Weapon => _weapon;

        public override void Initialize()
        {
            base.Initialize();
            _pawn = GetComponentInParent<PawnController>();
        }

        public void ChangeConfig(WeaponItemConfig config)
        {
            _config = config;
            // spawn?
            // change sprite?
        }
    }
}