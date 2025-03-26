using UnityEngine;

namespace WinterUniverse
{
    public abstract class EffectConfig : BasicInfoConfig
    {
        public abstract Effect CreateEffect(PawnController target, PawnController source, float value, float duration);
    }
}