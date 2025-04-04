using System;
using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class PawnStatusComponent : PawnComponent
    {
        public Action<float, float> OnHealthChanged;
        public Action OnDied;
        public Action OnRevived;
        public Action OnFactionChanged;

        [SerializeField] private float _regenerationTickTime = 0.5f;
        [SerializeField] private float _regenerationDelayTime = 10f;
        [SerializeField] private float _effectsTickTime = 1f;

        private float _healthCurrent;
        private float _regenerationCurrentTickTime;
        private float _regenerationCurrentDelayTime;
        private float _effectsCurrentTickTime;

        public EffectHolder EffectHolder { get; private set; }
        public StatHolder StatHolder { get; private set; }
        public StateHolder StateHolder { get; private set; }
        public FactionConfig Faction { get; private set; }

        public override void Initialize()
        {
            base.Initialize();
            EffectHolder = new(_pawn);
            StatHolder = new();
            StateHolder = new();
            StatHolder.CreateStats(GameManager.StaticInstance.ConfigsManager.Stats);
            StateHolder.CreateStates(GameManager.StaticInstance.ConfigsManager.States);
        }

        public override void Enable()
        {
            base.Enable();
            StatHolder.OnStatsChanged += ForceUpdateVitalities;
            ForceUpdateVitalities();
            Revive();
        }

        public override void Disable()
        {
            StatHolder.OnStatsChanged -= ForceUpdateVitalities;
            base.Disable();
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            if (_regenerationCurrentDelayTime >= _regenerationDelayTime)
            {
                if (_regenerationCurrentTickTime >= _regenerationTickTime)
                {
                    RestoreHealthCurrent(StatHolder.HealthRegeneration * _regenerationTickTime);
                    _regenerationCurrentTickTime = 0f;
                }
                else
                {
                    _regenerationCurrentTickTime += Time.fixedDeltaTime;
                }
            }
            else
            {
                _regenerationCurrentDelayTime += Time.deltaTime;
            }
            if (_effectsCurrentTickTime >= _effectsTickTime)
            {
                _effectsCurrentTickTime = 0f;
                EffectHolder.HandleEffects(_effectsTickTime);
            }
            else
            {
                _effectsCurrentTickTime += Time.fixedDeltaTime;
            }
        }

        public void ApplyDamage(List<DamageType> damageTypes, PawnController source)
        {
            if (StateHolder.CompareStateValue("Is Dead", true))
            {
                return;
            }
            if (source != null && UnityEngine.Random.value < StatHolder.Evade / 100f)
            {
                return;
            }
            foreach (DamageType dt in damageTypes)
            {
                ReduceHealthCurrent(dt.Damage, dt.Type, source);
            }
        }

        public void ReduceHealthCurrent(float value, DamageTypeConfig type, PawnController source)
        {
            if (StateHolder.CompareStateValue("Is Dead", true))
            {
                return;
            }
            if (_pawn.Equipment.ArmorSlot.Config != null)
            {
                EffectHolder.ApplyEffects(_pawn.Equipment.ArmorSlot.Config.OnDamageEffects);
            }
            float resistance = StatHolder.GetStat(type.ResistanceStat.ID).CurrentValue;
            if (resistance < 100f)
            {
                _regenerationCurrentDelayTime = 0f;
                value -= value * resistance / 100f;
                _healthCurrent = Mathf.Clamp(_healthCurrent - value, 0f, StatHolder.HealthMax);
                if (_healthCurrent > 0f)
                {
                    OnHealthChanged?.Invoke(_healthCurrent, StatHolder.HealthMax);
                }
                else
                {
                    Die(source);
                }
            }
            else if (resistance > 100f)
            {
                RestoreHealthCurrent(value * (resistance - 100f) / 100f);
            }
        }

        public void RestoreHealthCurrent(float value)
        {
            if (StateHolder.CompareStateValue("Is Dead", true))
            {
                return;
            }
            _healthCurrent = Mathf.Clamp(_healthCurrent + value, 0f, StatHolder.HealthMax);
            OnHealthChanged?.Invoke(_healthCurrent, StatHolder.HealthMax);
        }

        public void Die(PawnController source)
        {
            if (StateHolder.CompareStateValue("Is Dead", true))
            {
                return;
            }
            _pawn.Animator.PlayAction("Death");
            _healthCurrent = 0f;
            OnHealthChanged?.Invoke(_healthCurrent, StatHolder.HealthMax);
            StateHolder.SetStateValue("Is Dead", true);
            OnDied?.Invoke();
        }

        public void Revive()
        {
            StateHolder.SetStateValue("Is Dead", false);
            RestoreHealthCurrent(StatHolder.HealthMax);
            OnRevived?.Invoke();
        }

        public void ForceUpdateVitalities()
        {
            _healthCurrent = Mathf.Clamp(_healthCurrent, 0f, StatHolder.HealthMax);
            OnHealthChanged?.Invoke(_healthCurrent, StatHolder.HealthMax);
        }

        public void ChangeFaction(FactionConfig config)
        {
            Faction = config;
            OnFactionChanged?.Invoke();
        }
    }
}