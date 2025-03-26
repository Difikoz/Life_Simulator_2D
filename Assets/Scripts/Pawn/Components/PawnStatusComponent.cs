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

        private EffectHolder _effectHolder;
        private StatHolder _statHolder;
        private StateHolder _stateHolder;
        private FactionConfig _faction;
        private float _healthCurrent;
        private float _regenerationCurrentTickTime;
        private float _regenerationCurrentDelayTime;
        private float _effectsCurrentTickTime;

        public EffectHolder EffectHolder => _effectHolder;
        public StatHolder StatHolder => _statHolder;
        public StateHolder StateHolder => _stateHolder;
        public FactionConfig Faction => _faction;

        public override void Initialize()
        {
            base.Initialize();
            _effectHolder = new(_pawn);
            _statHolder = new();
            _stateHolder = new();
            _statHolder.CreateStats(GameManager.StaticInstance.ConfigsManager.Stats);
            _stateHolder.CreateStates(GameManager.StaticInstance.ConfigsManager.States);
        }

        public override void Enable()
        {
            base.Enable();
            _statHolder.OnStatsChanged += ForceUpdateVitalities;
            ForceUpdateVitalities();
            Revive();
        }

        public override void Disable()
        {
            _statHolder.OnStatsChanged -= ForceUpdateVitalities;
            base.Disable();
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            if (_regenerationCurrentDelayTime >= _regenerationDelayTime)
            {
                if (_regenerationCurrentTickTime >= _regenerationTickTime)
                {
                    RestoreHealthCurrent(_statHolder.HealthRegeneration * _regenerationTickTime);
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
                _effectHolder.HandleEffects(_effectsTickTime);
            }
            else
            {
                _effectsCurrentTickTime += Time.fixedDeltaTime;
            }
        }

        public void ApplyDamage(List<DamageType> damageTypes, PawnController source)
        {
            if (_stateHolder.CompareStateValue("Is Dead", true))
            {
                return;
            }
            if (source != null && UnityEngine.Random.value < _statHolder.Evade / 100f)
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
            if (_stateHolder.CompareStateValue("Is Dead", true))
            {
                return;
            }
            if (_pawn.Equipment.ArmorSlot.Config != null)
            {
                _effectHolder.ApplyEffects(_pawn.Equipment.ArmorSlot.Config.OnDamageEffects);
            }
            float resistance = _statHolder.GetStat(type.ResistanceStat.ID).CurrentValue;
            if (resistance < 100f)
            {
                _regenerationCurrentDelayTime = 0f;
                value -= value * resistance / 100f;
                _healthCurrent = Mathf.Clamp(_healthCurrent - value, 0f, _statHolder.HealthMax);
                if (_healthCurrent > 0f)
                {
                    OnHealthChanged?.Invoke(_healthCurrent, _statHolder.HealthMax);
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
            if (_stateHolder.CompareStateValue("Is Dead", true))
            {
                return;
            }
            _healthCurrent = Mathf.Clamp(_healthCurrent + value, 0f, _statHolder.HealthMax);
            OnHealthChanged?.Invoke(_healthCurrent, _statHolder.HealthMax);
        }

        public void Die(PawnController source)
        {
            if (_stateHolder.CompareStateValue("Is Dead", true))
            {
                return;
            }
            _pawn.Animator.PlayAction("Death");
            _healthCurrent = 0f;
            OnHealthChanged?.Invoke(_healthCurrent, _statHolder.HealthMax);
            _stateHolder.SetStateValue("Is Dead", true);
            OnDied?.Invoke();
        }

        public void Revive()
        {
            _stateHolder.SetStateValue("Is Dead", false);
            RestoreHealthCurrent(_statHolder.HealthMax);
            OnRevived?.Invoke();
        }

        public void ForceUpdateVitalities()
        {
            _healthCurrent = Mathf.Clamp(_healthCurrent, 0f, _statHolder.HealthMax);
            OnHealthChanged?.Invoke(_healthCurrent, _statHolder.HealthMax);
        }

        public void ChangeFaction(FactionConfig config)
        {
            _faction = config;
            OnFactionChanged?.Invoke();
        }
    }
}