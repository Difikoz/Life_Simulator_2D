using System;
using System.Collections.Generic;

namespace WinterUniverse
{
    public class StatHolder
    {
        public Action OnStatsChanged;

        public Dictionary<string, Stat> Stats { get; private set; }
        public float HealthMax { get; private set; }
        public float HealthRegeneration { get; private set; }
        public float MoveSpeed { get; private set; }
        public float Evade { get; private set; }

        public StatHolder()
        {
            Stats = new();
        }

        public void CreateStats(List<StatConfig> stats)
        {
            foreach (StatConfig stat in stats)
            {
                Stats.Add(stat.ID, new(stat));
            }
        }

        public void RecalculateStats()
        {
            foreach (KeyValuePair<string, Stat> s in Stats)
            {
                s.Value.CalculateCurrentValue();
            }
            UpdateValues();
        }

        public Stat GetStat(string id)
        {
            if (Stats.ContainsKey(id))
            {
                return Stats[id];
            }
            return null;
        }

        public void AddStatModifiers(List<StatModifierCreator> modifiers)
        {
            foreach (StatModifierCreator smc in modifiers)
            {
                AddStatModifier(smc);
            }
            RecalculateStats();
        }

        public void AddStatModifier(StatModifierCreator smc)
        {
            GetStat(smc.Stat.ID).AddModifier(smc.Modifier);
        }

        public void RemoveStatModifiers(List<StatModifierCreator> modifiers)
        {
            foreach (StatModifierCreator smc in modifiers)
            {
                RemoveStatModifier(smc);
            }
            RecalculateStats();
        }

        public void RemoveStatModifier(StatModifierCreator smc)
        {
            GetStat(smc.Stat.ID).RemoveModifier(smc.Modifier);
        }

        public void UpdateValues()
        {
            HealthMax = GetStat("Health Max").CurrentValue;
            HealthRegeneration = GetStat("Health Regeneration").CurrentValue;
            MoveSpeed = GetStat("Move Speed").CurrentValue;
            Evade = GetStat("Evade").CurrentValue;
            OnStatsChanged?.Invoke();
        }
    }
}