using System;
using UnityEngine;

namespace WinterUniverse
{
    public class PawnEquipmentComponent : PawnComponent
    {
        public Action OnEquipmentChanged;

        [SerializeField] private WeaponItemConfig _baseWeapon;
        [SerializeField] private ArmorItemConfig _baseArmor;

        public WeaponItemConfig Weapon { get; private set; }
        public ArmorItemConfig Armor { get; private set; }

        public override void Initialize()
        {
            base.Initialize();
            Weapon = _baseWeapon;
            Armor = _baseArmor;
        }

        public void EquipWeapon(WeaponItemConfig config, bool removeNewFromInventory = true, bool addOldToInventory = true)
        {
            if (config == null || _pawn.Status.StateHolder.CompareStateValue("Is Perfoming Action", true))
            {
                return;
            }
            UnequipWeapon(addOldToInventory);
            if (removeNewFromInventory)
            {
                _pawn.Inventory.RemoveItem(config);
            }
            Weapon = config;
            _pawn.Status.StatHolder.AddStatModifiers(config.Modifiers);
            OnEquipmentChanged?.Invoke();
        }

        public void UnequipWeapon(bool addOldToInventory = true)
        {
            if (Weapon == _baseWeapon)
            {
                return;
            }
            _pawn.Status.StatHolder.RemoveStatModifiers(Weapon.Modifiers);
            if (addOldToInventory)
            {
                _pawn.Inventory.AddItem(Weapon);
            }
            Weapon = _baseWeapon;
            OnEquipmentChanged?.Invoke();
        }

        public void EquipArmor(ArmorItemConfig config, bool removeNewFromInventory = true, bool addOldToInventory = true)
        {
            if (config == null || _pawn.Status.StateHolder.CompareStateValue("Is Perfoming Action", true))
            {
                return;
            }
            UnequipArmor(addOldToInventory);
            if (removeNewFromInventory)
            {
                _pawn.Inventory.RemoveItem(config);
            }
            Armor = config;
            _pawn.Status.StatHolder.AddStatModifiers(Armor.Modifiers);
            OnEquipmentChanged?.Invoke();
        }

        public void UnequipArmor(bool addOldToInventory = true)
        {
            if (Armor == _baseArmor)
            {
                return;
            }
            _pawn.Status.StatHolder.RemoveStatModifiers(Armor.Modifiers);
            if (addOldToInventory)
            {
                _pawn.Inventory.AddItem(Armor);
            }
            Armor = _baseArmor;
            OnEquipmentChanged?.Invoke();
        }
    }
}