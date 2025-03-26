using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Armor", menuName = "Winter Universe/Item/New Armor")]
    public class ArmorItemConfig : ItemConfig
    {
        [SerializeField] private Sprite _visualSprite;
        [SerializeField] private List<StatModifierCreator> _modifiers = new();
        [SerializeField] private List<EffectCreator> _onDamageEffects = new();

        public Sprite VisualSprite => _visualSprite;
        public List<StatModifierCreator> Modifiers => _modifiers;
        public List<EffectCreator> OnDamageEffects => _onDamageEffects;

        private void OnValidate()
        {
            _itemType = ItemType.Armor;
        }
    }
}