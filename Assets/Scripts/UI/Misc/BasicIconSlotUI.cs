using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace WinterUniverse
{
    public class BasicIconSlotUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
    {
        [SerializeField] protected Button _thisButton;
        [SerializeField] protected Image _iconImage;
        [SerializeField] protected Sprite _emptyIconSprite;
        [SerializeField] protected Image _deselectedFrame;
        [SerializeField] protected Image _selectedFrame;

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            _thisButton.Select();
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            _thisButton.OnDeselect(eventData);
        }

        public virtual void OnSelect(BaseEventData eventData)
        {
            _deselectedFrame.gameObject.SetActive(false);
            _selectedFrame.gameObject.SetActive(true);
        }

        public virtual void OnDeselect(BaseEventData eventData)
        {
            _deselectedFrame.gameObject.SetActive(true);
            _selectedFrame.gameObject.SetActive(false);
        }

        public void SetIcon(Sprite icon)
        {
            if (icon != null)
            {
                _iconImage.sprite = icon;
            }
            else
            {
                _iconImage.sprite = _emptyIconSprite;
            }
        }
    }
}