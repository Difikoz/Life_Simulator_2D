using Lean.Pool;
using UnityEngine;

namespace WinterUniverse
{
    public class EffectsBarUI : BasicComponent
    {
        [SerializeField] private Transform _contentRoot;
        [SerializeField] private GameObject _slotPrefab;

        public override void Enable()
        {
            base.Enable();
            //GameManager.StaticInstance.ControllersManager.Player.Status.EffectHolder.OnEffectsChanged += OnEffectsChanged;
            OnEffectsChanged();
        }

        public override void Disable()
        {
            //GameManager.StaticInstance.ControllersManager.Player.Status.EffectHolder.OnEffectsChanged -= OnEffectsChanged;
            base.Disable();
        }

        private void OnEffectsChanged()
        {
            while (_contentRoot.childCount > 0)
            {
                LeanPool.Despawn(_contentRoot.GetChild(0).gameObject);
            }
            //foreach (Effect effect in GameManager.StaticInstance.ControllersManager.Player.Status.EffectHolder.AllEffects)
            //{
            //    LeanPool.Spawn(_slotPrefab, _contentRoot).GetComponent<EffectSlotUI>().Initialize(effect);
            //}
        }
    }
}