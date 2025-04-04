using Lean.Pool;
using UnityEngine;

namespace WinterUniverse
{
    public class WeaponSlot : BasicComponent
    {
        [SerializeField] private Transform _weaponRoot;

        public WeaponItemConfig Config { get; private set; }
        public WeaponController Weapon { get; private set; }

        public override void Initialize()
        {
            base.Initialize();
            ChangeConfig(null);
        }

        public void ChangeConfig(WeaponItemConfig config)
        {
            if(Weapon != null)
            {
                LeanPool.Despawn(Weapon.gameObject);
                Weapon = null;
            }
            Config = config;
            if(Config != null)
            {
                Weapon = LeanPool.Spawn(Config.WeaponPrefab, _weaponRoot).GetComponent<WeaponController>();
            }
        }
    }
}