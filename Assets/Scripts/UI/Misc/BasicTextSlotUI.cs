using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace WinterUniverse
{
    public class BasicTextSlotUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] protected Button _thisButton;
        [SerializeField] protected TMP_Text _infoText;

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            _thisButton.Select();
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            _thisButton.OnDeselect(eventData);
        }

        public void SetText(string text)
        {
            _infoText.text = text;
        }
    }
}