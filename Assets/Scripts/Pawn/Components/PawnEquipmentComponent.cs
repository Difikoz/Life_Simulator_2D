using System;

namespace WinterUniverse
{
    public class PawnEquipmentComponent : PawnComponent
    {
        public Action OnEquipmentChanged;

        private WeaponSlot _weaponSlot;
        private ArmorSlot _armorSlot;

        public WeaponSlot MeleeWeaponSlot => _weaponSlot;
        public ArmorSlot ArmorSlot => _armorSlot;

        public override void Initialize()
        {
            base.Initialize();
            _weaponSlot = GetComponentInChildren<WeaponSlot>();
            _armorSlot = GetComponentInChildren<ArmorSlot>();
            _weaponSlot.Initialize();
            _armorSlot.Initialize();
        }

        public void EquipWeapon(WeaponItemConfig config, bool removeNewFromInventory = true, bool addOldToInventory = true)
        {
            if (_pawn.Status.StateHolder.CompareStateValue("Is Perfoming Action", true))
            {
                return;
            }
            if (_weaponSlot.Weapon != null && _weaponSlot.Weapon.IsPerfomingAction)
            {
                return;
            }
            UnequipWeapon(addOldToInventory);
            if (removeNewFromInventory)
            {
                _pawn.Inventory.RemoveItem(config);
            }
            _weaponSlot.ChangeConfig(config);
            _pawn.Status.StatHolder.AddStatModifiers(config.Modifiers);
            OnEquipmentChanged?.Invoke();
        }

        public void EquipArmor(ArmorItemConfig config, bool removeNewFromInventory = true, bool addOldToInventory = true)
        {
            if (_pawn.Status.StateHolder.CompareStateValue("Is Perfoming Action", true))
            {
                return;
            }
            UnequipArmor(addOldToInventory);
            if (removeNewFromInventory)
            {
                _pawn.Inventory.RemoveItem(config);
            }
            _armorSlot.ChangeConfig(config);
            _pawn.Status.StatHolder.AddStatModifiers(config.Modifiers);
            OnEquipmentChanged?.Invoke();
        }

        public void UnequipWeapon(bool addOldToInventory = true)
        {
            if (_weaponSlot.Config == null || _weaponSlot.Weapon == null)
            {
                return;
            }
            if (_weaponSlot.Weapon.IsPerfomingAction)
            {
                return;
            }
            _pawn.Status.StatHolder.RemoveStatModifiers(_weaponSlot.Config.Modifiers);
            if (addOldToInventory)
            {
                _pawn.Inventory.AddItem(_weaponSlot.Config);
            }
            _weaponSlot.ChangeConfig(null);
            OnEquipmentChanged?.Invoke();
        }

        public void UnequipArmor(bool addOldToInventory = true)
        {
            if (_armorSlot.Config == null)
            {
                return;
            }
            _pawn.Status.StatHolder.RemoveStatModifiers(_armorSlot.Config.Modifiers);
            if (addOldToInventory)
            {
                _pawn.Inventory.AddItem(_armorSlot.Config);
            }
            _armorSlot.ChangeConfig(null);
            OnEquipmentChanged?.Invoke();
        }
    }
}