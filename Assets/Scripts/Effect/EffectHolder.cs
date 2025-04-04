using System;
using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class EffectHolder
    {
        public Action OnEffectsChanged;

        private PawnController _pawn;

        public List<Effect> AllEffects { get; private set; }

        public EffectHolder(PawnController pawn)
        {
            _pawn = pawn;
            AllEffects = new();
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
            AllEffects.Add(effect);
        }

        public void RemoveEffect(Effect effect)
        {
            if (AllEffects.Contains(effect))
            {
                AllEffects.Remove(effect);
            }
        }

        public void HandleEffects(float deltaTime)
        {
            for (int i = AllEffects.Count - 1; i >= 0; i--)
            {
                AllEffects[i].OnTick(deltaTime);
            }
            OnEffectsChanged?.Invoke();
        }
    }
}