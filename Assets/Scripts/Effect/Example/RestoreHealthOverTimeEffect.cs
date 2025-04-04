namespace WinterUniverse
{
    public class RestoreHealthOverTimeEffect : Effect
    {
        public RestoreHealthOverTimeEffect(EffectConfig config, PawnController owner, PawnController source, float value, float duration) : base(config, owner, source, value, duration)
        {
        }

        public override void OnApply()
        {
            Owner.Status.RestoreHealthCurrent(Value);
        }

        protected override void ApplyOnTick(float deltaTime)
        {
            Owner.Status.RestoreHealthCurrent(Value * deltaTime);
        }
    }
}