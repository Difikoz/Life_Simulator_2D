using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace WinterUniverse
{
    public class EquipmentSlotUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, ISelectHandler, IDeselectHandler, ISubmitHandler
    {
        [SerializeField] private ItemType _type;
        [SerializeField] private Button _thisButton;
        [SerializeField] private Image _iconImage;
        [SerializeField] private Image _deselectedFrame;
        [SerializeField] private Image _selectedFrame;
        [SerializeField] private Sprite _emptySprite;

        private ItemConfig _config;

        public void Initialize(ItemConfig config)
        {
            _config = config;
            if (_config != null)
            {
                _iconImage.sprite = _config.Icon.LoadAsset();
            }
            else
            {
                _iconImage.sprite = _emptySprite;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _thisButton.Select();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _thisButton.OnDeselect(eventData);
        }

        public void OnSelect(BaseEventData eventData)
        {
            _deselectedFrame.gameObject.SetActive(false);
            _selectedFrame.gameObject.SetActive(true);
            GameManager.StaticInstance.UIManager.StatusBar.InventoryPage.ShowFullInformation(_config);
        }

        public void OnDeselect(BaseEventData eventData)
        {
            _deselectedFrame.gameObject.SetActive(true);
            _selectedFrame.gameObject.SetActive(false);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_config == null)
            {
                return;
            }
            OnSubmit(eventData);
        }

        public void OnSubmit(BaseEventData eventData)
        {
            if (_config == null)
            {
                return;
            }
            //switch (_type)
            //{
            //    case ItemType.MeleeWeapon:
            //        GameManager.StaticInstance.ControllersManager.Player.Equipment.UnequipMeleeWeapon();
            //        break;
            //    case ItemType.RangedWeapon:
            //        GameManager.StaticInstance.ControllersManager.Player.Equipment.UnequipRangedWeapon();
            //        break;
            //    case ItemType.Helmet:
            //        GameManager.StaticInstance.ControllersManager.Player.Equipment.UnequipHelmet();
            //        break;
            //    case ItemType.Chest:
            //        GameManager.StaticInstance.ControllersManager.Player.Equipment.UnequipChest();
            //        break;
            //}
        }
    }
}