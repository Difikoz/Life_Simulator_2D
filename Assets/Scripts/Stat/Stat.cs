using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [System.Serializable]
    public class Stat
    {
        public StatConfig Config { get; private set; }
        public float CurrentValue { get; private set; }
        public float FlatValue { get; private set; }
        public float MultiplierValue { get; private set; }
        public List<float> FlatModifiers { get; private set; }
        public List<float> MultiplierModifiers { get; private set; }

        public Stat(StatConfig config)
        {
            Config = config;
            CurrentValue = Config.BaseValue;
            FlatModifiers = new();
            MultiplierModifiers = new();
        }

        public void AddModifier(StatModifier modifier)
        {
            if (modifier.Type == StatModifierType.Flat)
            {
                FlatModifiers.Add(modifier.Value);
            }
            else if (modifier.Type == StatModifierType.Multiplier)
            {
                MultiplierModifiers.Add(modifier.Value);
            }
            CalculateCurrentValue();
        }

        public void RemoveModifier(StatModifier modifier)
        {
            if (modifier.Type == StatModifierType.Flat && FlatModifiers.Contains(modifier.Value))
            {
                FlatModifiers.Remove(modifier.Value);
            }
            else if (modifier.Type == StatModifierType.Multiplier && MultiplierModifiers.Contains(modifier.Value))
            {
                MultiplierModifiers.Remove(modifier.Value);
            }
            CalculateCurrentValue();
        }

        public void CalculateCurrentValue()
        {
            float value = Config.BaseValue;
            FlatValue = 0f;
            MultiplierValue = 0f;
            foreach (float f in FlatModifiers)
            {
                FlatValue += f;
            }
            foreach (float f in MultiplierModifiers)
            {
                MultiplierValue += f;
            }
            if (MultiplierValue != 0f)
            {
                value += MultiplierValue * value / 100f;
            }
            value = Mathf.Clamp(value, Config.MinValue, Config.MaxValue);
            CurrentValue = value;
        }
    }
}