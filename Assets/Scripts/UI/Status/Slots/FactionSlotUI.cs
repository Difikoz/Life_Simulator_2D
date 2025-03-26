using UnityEngine.EventSystems;

namespace WinterUniverse
{
    public class FactionSlotUI : BasicTextSlotUI, ISelectHandler
    {
        private FactionConfig _config;

        public void Initialize(FactionConfig config)
        {
            _config = config;
            SetText(_config.DisplayName.GetLocalizedString());
        }

        public void OnSelect(BaseEventData eventData)
        {
            GameManager.StaticInstance.UIManager.StatusBar.FactionsPage.ShowFullInformation(_config);
        }
    }
}