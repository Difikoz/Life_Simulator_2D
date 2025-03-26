using UnityEngine;
using UnityEngine.Localization;

namespace WinterUniverse
{
    public class StatusPageUI : BasicComponent
    {
        [SerializeField] protected LocalizedString _pageName;

        public LocalizedString PageName => _pageName;
    }
}