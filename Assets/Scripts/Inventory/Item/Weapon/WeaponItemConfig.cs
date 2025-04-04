using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Winter Universe/Item/New Weapon")]
    public class WeaponItemConfig : ItemConfig
    {
        [SerializeField] private GameObject _weaponPrefab;
        [SerializeField] private List<StatModifierCreator> _modifiers = new();
        [SerializeField] private List<AbilityConfig> _abilities = new();

        public GameObject WeaponPrefab => _weaponPrefab;
        public List<StatModifierCreator> Modifiers => _modifiers;
        public List<AbilityConfig> Abilities => _abilities;

        private void OnValidate()
        {
            _itemType = ItemType.Weapon;
        }
    }
}