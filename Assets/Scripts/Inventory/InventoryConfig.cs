using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Inventory", menuName = "Winter Universe/Pawn/New Inventory")]
    public class InventoryConfig : ScriptableObject
    {
        [SerializeField] private string _id;
        [SerializeField] private List<ItemStack> _stacks = new();

        public string ID => _id;
        public List<ItemStack> Stacks => _stacks;
    }
}