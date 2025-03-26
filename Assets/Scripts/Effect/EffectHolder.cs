using System;
using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class EffectHolder
    {
        public Action OnEffectsChanged;

        private PawnController _pawn;
        private List<Effect> _allEffects;

        public List<Effect> AllEffects => _allEffects;

        public EffectHolder(PawnController pawn)
        {
            _pawn = pawn;
            _allEffects = new();
        }

        public void ApplyEffects(List<EffectCreator> effects)
        {
            ApplyEffects(effects, _pawn);
        }

        public void ApplyEffects(List<EffectCreator> effects, PawnController source)
        {
            foreach (EffectCreator effect in effects)
            {
                if (effect.Triggered)
                {
                    AddEffect(effect.Config.CreateEffect(_pawn, source, effect.Value, effect.Duration));
                }
            }
        }

        public void AddEffect(Effect effect)
        {
            _allEffects.Add(effect);
        }

        public void RemoveEffect(Effect effect)
        {
            if (_allEffects.Contains(effect))
            {
                _allEffects.Remove(effect);
            }
        }

        public void HandleEffects(float deltaTime)
        {
            for (int i = _allEffects.Count - 1; i >= 0; i--)
            {
                _allEffects[i].OnTick(deltaTime);
            }
            OnEffectsChanged?.Invoke();
        }
    }
}