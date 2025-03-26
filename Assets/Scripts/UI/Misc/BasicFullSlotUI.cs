using TMPro;
using UnityEngine;

namespace WinterUniverse
{
    public class BasicFullSlotUI : BasicIconSlotUI
    {
        [SerializeField] protected TMP_Text _infoText;

        public void SetText(string text)
        {
            _infoText.text = text;
        }
    }
}