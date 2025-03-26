using UnityEngine;

namespace WinterUniverse
{
    public class PlayerVitalityUI : BasicComponent
    {
        [SerializeField] private VitalityBarUI _healthBar;
        [SerializeField] private VitalityBarUI _staminaBar;

        public override void Initialize()
        {
            base.Initialize();
            _healthBar.Initialize();
            _staminaBar.Initialize();
        }

        public override void Enable()
        {
            base.Enable();
            //+=_healthBar.SetValues();
            //+=_staminaBar.SetValues();
        }

        public override void Disable()
        {
            //-=_healthBar.SetValues();
            //-=_staminaBar.SetValues();
            base.Disable();
        }
    }
}