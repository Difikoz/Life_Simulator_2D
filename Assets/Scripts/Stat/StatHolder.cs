using System;
using System.Collections.Generic;

namespace WinterUniverse
{
    public class StatHolder
    {
        public Action OnStatsChanged;

        private Dictionary<string, Stat> _stats;
        private float _healthMax;
        private float _healthRegeneration;
        private float _moveSpeed;
        private float _evade;

        public Dictionary<string, Stat> Stats => _stats;
        public float HealthMax => _healthMax;
        public float HealthRegeneration => _healthRegeneration;
        public float MoveSpeed => _moveSpeed;
        public float Evade => _evade;

        public StatHolder()
        {
            _stats = new();
        }

        public void CreateStats(List<StatConfig> stats)
        {
            foreach (StatConfig stat in stats)
            {
                _stats.Add(stat.ID, new(stat));
            }
        }

        public void RecalculateStats()
        {
            foreach (KeyValuePair<string, Stat> s in _stats)
            {
                s.Value.CalculateCurrentValue();
            }
            UpdateValues();
        }

        public Stat GetStat(string id)
        {
            if (_stats.ContainsKey(id))
            {
                return _stats[id];
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
            _healthMax = GetStat("HP MAX").CurrentValue;
            _healthRegeneration = GetStat("HP REGEN").CurrentValue;
            _moveSpeed = GetStat("MSPD").CurrentValue;
            _evade = GetStat("EVADE").CurrentValue;
            OnStatsChanged?.Invoke();
        }
    }
}