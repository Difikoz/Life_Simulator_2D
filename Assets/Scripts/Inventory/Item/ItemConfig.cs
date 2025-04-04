using UnityEngine;

namespace WinterUniverse
{
    public abstract class ItemConfig : BasicInfoConfig
    {
        [SerializeField] protected ItemType _itemType;
        [SerializeField] protected int _stackSize = 1;
        [SerializeField] protected float _weight = 1f;
        [SerializeField] protected int _price = 100;

        public ItemType ItemType => _itemType;
        public int StackSize => _stackSize;
        public float Weight => _weight;
        public int Price => _price;

        public void OnUse(PawnController pawn, bool fromInventory = true)
        {
            switch (_itemType)
            {
                case ItemType.Weapon:
                    WeaponItemConfig weapon = (WeaponItemConfig)this;
                    pawn.Equipment.EquipWeapon(weapon, fromInventory);
                    break;
                case ItemType.Armor:
                    ArmorItemConfig armor = (ArmorItemConfig)this;
                    pawn.Equipment.EquipArmor(armor, fromInventory);
                    break;
                case ItemType.Consumable:
                    ConsumableItemConfig consumable = (ConsumableItemConfig)this;
                    if (!fromInventory || pawn.Inventory.RemoveItem(this))
                    {
                        pawn.Status.EffectHolder.ApplyEffects(consumable.Effects);
                    }
                    break;
                case ItemType.Resource:
                    break;
            }
        }
    }
}