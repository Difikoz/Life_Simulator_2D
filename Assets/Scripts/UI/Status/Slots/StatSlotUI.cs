using UnityEngine.EventSystems;

namespace WinterUniverse
{
    public class StatSlotUI : BasicTextSlotUI, ISelectHandler
    {
        private StatConfig _config;

        public void Initialize(StatConfig config)
        {
            _config = config;
            SetText(config.DisplayName.GetLocalizedString());
        }

        public void OnSelect(BaseEventData eventData)
        {
            GameManager.StaticInstance.UIManager.StatusBar.StatsPage.ShowFullInformation(_config);
        }
    }
}