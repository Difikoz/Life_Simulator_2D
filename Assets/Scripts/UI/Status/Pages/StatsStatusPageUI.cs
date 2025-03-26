using Lean.Pool;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace WinterUniverse
{
    public class StatsStatusPageUI : StatusPageUI
    {
        [Header("Info Bar")]
        [SerializeField] private Image _infoBarIconImage;
        [SerializeField] private TMP_Text _infoBarNameText;
        [SerializeField] private TMP_Text _infoBarDescriptionText;
        [SerializeField] private Sprite _emptySprite;
        [Header("Stats Bar")]
        [SerializeField] private Transform _statBarContentRoot;
        [SerializeField] private GameObject _statBarSlotPrefab;
        [Header("Values Bar")]
        [SerializeField] private BasicTextSlotUI _valueBarBaseValueSlot;
        [SerializeField] private BasicTextSlotUI _valueBarFlatValueSlot;
        [SerializeField] private BasicTextSlotUI _valueBarMultiplierValueSlot;
        [SerializeField] private BasicTextSlotUI _valueBarCurrentValueSlot;
        // modifiers from items
        // modifiers from effects

        private List<StatSlotUI> _slots;

        public override void Initialize()
        {
            base.Initialize();
            _statBarSlotPrefab = new();
            for (int i = 0; i < GameManager.StaticInstance.ConfigsManager.Stats.Count; i++)
            {
                _slots.Add(LeanPool.Spawn(_statBarSlotPrefab, _statBarContentRoot).GetComponent<StatSlotUI>());
            }
            ShowFullInformation(null);
        }

        public override void Enable()
        {
            base.Enable();
            //GameManager.StaticInstance.ControllersManager.Player.Status.StatHolder.OnStatsChanged += OnStatsChanged;
            OnStatsChanged();
        }

        public override void Disable()
        {
            //GameManager.StaticInstance.ControllersManager.Player.Status.StatHolder.OnStatsChanged -= OnStatsChanged;
            base.Disable();
        }

        private void OnStatsChanged()
        {
            ShowFullInformation(null);
            //int currentIndex = 0;
            //foreach (KeyValuePair<string, Stat> kvp in GameManager.StaticInstance.ControllersManager.Player.Status.StatHolder.Stats)
            //{
            //    _slots[currentIndex].Initialize(kvp.Value.Config);
            //    currentIndex++;
            //}
        }

        public void ShowFullInformation(StatConfig config)
        {
            if (config != null)
            {
                _infoBarIconImage.sprite = config.Icon.LoadAsset();
                _infoBarNameText.text = config.DisplayName.GetLocalizedString();
                _infoBarDescriptionText.text = config.Description.GetLocalizedString();
                FillValuesBar(config);
            }
            else
            {
                _infoBarIconImage.sprite = _emptySprite;
                _infoBarNameText.text = string.Empty;
                _infoBarDescriptionText.text = string.Empty;
                ClearValuesBar();
            }
        }

        private void FillValuesBar(StatConfig config)
        {
            ClearValuesBar();
            //Stat stat = GameManager.StaticInstance.ControllersManager.Player.Status.StatHolder.GetStat(config.ID);
            //if (config.IsPercent)
            //{
            //    _valueBarBaseValueSlot.SetText(config.BaseValue.ToString() + "%");
            //    _valueBarMultiplierValueSlot.SetText((stat.FlatValue + stat.MultiplierValue).ToString() + "%");
            //    _valueBarCurrentValueSlot.SetText(stat.CurrentValue.ToString() + "%");
            //}
            //else
            //{
            //    _valueBarBaseValueSlot.SetText(config.BaseValue.ToString());
            //    _valueBarFlatValueSlot.SetText(stat.FlatValue.ToString());
            //    _valueBarMultiplierValueSlot.SetText(stat.MultiplierValue.ToString() + "%");
            //    _valueBarCurrentValueSlot.SetText(stat.CurrentValue.ToString());
            //}
        }

        private void ClearValuesBar()
        {
            _valueBarBaseValueSlot.SetText(string.Empty);
            _valueBarFlatValueSlot.SetText(string.Empty);
            _valueBarMultiplierValueSlot.SetText(string.Empty);
            _valueBarCurrentValueSlot.SetText(string.Empty);
        }
    }
}