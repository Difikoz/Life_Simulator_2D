using System;

namespace WinterUniverse
{
    public class PawnEquipmentComponent : PawnComponent
    {
        public Action OnEquipmentChanged;

        public WeaponSlot WeaponSlot { get; private set; }
        public ArmorSlot ArmorSlot { get; private set; }

        public override void Initialize()
        {
            base.Initialize();
            WeaponSlot = GetComponentInChildren<WeaponSlot>();
            ArmorSlot = GetComponentInChildren<ArmorSlot>();
            WeaponSlot.Initialize();
            ArmorSlot.Initialize();
        }

        public void EquipWeapon(WeaponItemConfig config, bool removeNewFromInventory = true, bool addOldToInventory = true)
        {
            if (_pawn.Status.StateHolder.CompareStateValue("Is Perfoming Action", true))
            {
                return;
            }
            if (WeaponSlot.Weapon != null && WeaponSlot.Weapon.IsPerfomingAction)
            {
                return;
            }
            UnequipWeapon(addOldToInventory);
            if (removeNewFromInventory)
            {
                _pawn.Inventory.RemoveItem(config);
            }
            WeaponSlot.ChangeConfig(config);
            _pawn.Status.StatHolder.AddStatModifiers(config.Modifiers);
            OnEquipmentChanged?.Invoke();
        }

        public void UnequipWeapon(bool addOldToInventory = true)
        {
            if (WeaponSlot.Config == null || WeaponSlot.Weapon == null)
            {
                return;
            }
            if (WeaponSlot.Weapon.IsPerfomingAction)
            {
                return;
            }
            _pawn.Status.StatHolder.RemoveStatModifiers(WeaponSlot.Config.Modifiers);
            if (addOldToInventory)
            {
                _pawn.Inventory.AddItem(WeaponSlot.Config);
            }
            WeaponSlot.ChangeConfig(null);
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
            ArmorSlot.ChangeConfig(config);
            _pawn.Status.StatHolder.AddStatModifiers(config.Modifiers);
            OnEquipmentChanged?.Invoke();
        }

        public void UnequipArmor(bool addOldToInventory = true)
        {
            if (ArmorSlot.Config == null)
            {
                return;
            }
            _pawn.Status.StatHolder.RemoveStatModifiers(ArmorSlot.Config.Modifiers);
            if (addOldToInventory)
            {
                _pawn.Inventory.AddItem(ArmorSlot.Config);
            }
            ArmorSlot.ChangeConfig(null);
            OnEquipmentChanged?.Invoke();
        }
    }
}