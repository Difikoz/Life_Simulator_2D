using UnityEngine;

namespace WinterUniverse
{
    public class HUDUI : BasicComponent
    {
        private PlayerVitalityUI _playerVitality;
        private EffectsBarUI _effectsBar;

        public override void Initialize()
        {
            base.Initialize();
            _playerVitality = GetComponentInChildren<PlayerVitalityUI>();
            _effectsBar = GetComponentInChildren<EffectsBarUI>();
            _playerVitality.Initialize();
            _effectsBar.Initialize();
        }

        public override void Enable()
        {
            base.Enable();
            _playerVitality.Enable();
            _effectsBar.Enable();
        }

        public override void Disable()
        {
            _playerVitality.Disable();
            _effectsBar.Disable();
            base.Disable();
        }
    }
}