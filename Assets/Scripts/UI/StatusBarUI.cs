using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace WinterUniverse
{
    public class StatusBarUI : BasicComponent
    {
        [SerializeField] private TMP_Text _pageNameText;
        [SerializeField] private List<StatusPageUI> _pages = new();

        private int _currentPageIndex;
        private InventoryStatusPageUI _inventoryPage;
        private StatsStatusPageUI _statsPage;
        private JournalStatusPageUI _journalPage;
        private FactionsStatusPageUI _factionsPage;
        private MapStatusPageUI _mapPage;

        public InventoryStatusPageUI InventoryPage => _inventoryPage;
        public StatsStatusPageUI StatsPage => _statsPage;
        public JournalStatusPageUI JournalPage => _journalPage;
        public FactionsStatusPageUI FactionsPage => _factionsPage;
        public MapStatusPageUI MapPage => _mapPage;

        public override void Initialize()
        {
            base.Initialize();
            _inventoryPage = GetComponentInChildren<InventoryStatusPageUI>();
            _statsPage = GetComponentInChildren<StatsStatusPageUI>();
            _journalPage = GetComponentInChildren<JournalStatusPageUI>();
            _factionsPage = GetComponentInChildren<FactionsStatusPageUI>();
            _mapPage = GetComponentInChildren<MapStatusPageUI>();
            foreach (StatusPageUI page in _pages)
            {
                page.Initialize();
            }
        }

        public override void Enable()
        {
            base.Enable();
            foreach (StatusPageUI page in _pages)
            {
                page.Enable();
            }
            ShowTab(0);
            gameObject.SetActive(false);
        }

        public override void Disable()
        {
            foreach (StatusPageUI page in _pages)
            {
                page.Disable();
            }
            base.Disable();
        }

        public void PreviousTab()
        {
            if (_currentPageIndex > 0)
            {
                ShowTab(_currentPageIndex - 1);
            }
            else
            {
                ShowTab(_pages.Count - 1);
            }
        }

        public void NextTab()
        {
            if (_currentPageIndex < _pages.Count - 1)
            {
                ShowTab(_currentPageIndex + 1);
            }
            else
            {
                ShowTab(0);
            }
        }

        public void ShowTab(int index)
        {
            _currentPageIndex = index;
            for (int i = 0; i < _pages.Count; i++)
            {
                _pages[i].gameObject.SetActive(i == _currentPageIndex);
            }
            _pageNameText.text = _pages[_currentPageIndex].PageName.GetLocalizedString();
        }
    }
}