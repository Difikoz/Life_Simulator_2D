using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Winter Universe/Item/New Weapon")]
    public class WeaponItemConfig : ItemConfig
    {
        [SerializeField] private AbilityConfig _ability;
        [SerializeField] private List<StatModifierCreator> _modifiers = new();

        public AbilityConfig Ability => _ability;
        public List<StatModifierCreator> Modifiers => _modifiers;

        private void OnValidate()
        {
            _itemType = ItemType.Weapon;
        }
    }
}