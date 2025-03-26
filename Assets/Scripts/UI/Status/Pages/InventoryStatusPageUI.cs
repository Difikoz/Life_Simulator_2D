using Lean.Pool;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace WinterUniverse
{
    public class InventoryStatusPageUI : StatusPageUI
    {
        [Header("Info Bar")]
        [SerializeField] private Image _infoBarIconImage;
        [SerializeField] private TMP_Text _infoBarNameText;
        [SerializeField] private TMP_Text _infoBarDescriptionText;
        [SerializeField] private Sprite _emptySprite;
        [Header("Inventory Bar")]
        [SerializeField] private Transform _inventoryContentRoot;
        [SerializeField] private GameObject _inventorySlotPrefab;
        [Header("Equipment Bar")]
        [SerializeField] private EquipmentSlotUI _meleeWeaponSlot;
        [SerializeField] private EquipmentSlotUI _rangedWeaponSlot;
        [SerializeField] private EquipmentSlotUI _helmetArmorSlot;
        [SerializeField] private EquipmentSlotUI _chestArmorSlot;

        public override void Initialize()
        {
            base.Initialize();
            _meleeWeaponSlot.Initialize(null);
            _rangedWeaponSlot.Initialize(null);
            _helmetArmorSlot.Initialize(null);
            _chestArmorSlot.Initialize(null);
        }

        public override void Enable()
        {
            base.Enable();
            //GameManager.StaticInstance.ControllersManager.Player.Inventory.OnInventoryChanged += OnInventoryChanged;
            //GameManager.StaticInstance.ControllersManager.Player.Equipment.OnEquipmentChanged += OnEquipmentChanged;
            OnInventoryChanged();
            OnEquipmentChanged();
        }

        public override void Disable()
        {
            //GameManager.StaticInstance.ControllersManager.Player.Inventory.OnInventoryChanged -= OnInventoryChanged;
            //GameManager.StaticInstance.ControllersManager.Player.Equipment.OnEquipmentChanged -= OnEquipmentChanged;
            base.Disable();
        }

        private void OnInventoryChanged()
        {
            ShowFullInformation(null);
            while (_inventoryContentRoot.childCount > 0)
            {
                LeanPool.Despawn(_inventoryContentRoot.GetChild(0).gameObject);
            }
            //foreach (ItemStack stack in GameManager.StaticInstance.ControllersManager.Player.Inventory.Stacks)
            //{
            //    LeanPool.Spawn(_inventorySlotPrefab, _inventoryContentRoot).GetComponent<InventorySlotUI>().Initialize(stack);
            //}
        }

        private void OnEquipmentChanged()
        {
            ShowFullInformation(null);
            //_meleeWeaponSlot.Initialize(GameManager.StaticInstance.ControllersManager.Player.Equipment.MeleeWeaponSlot.Config);
            //_rangedWeaponSlot.Initialize(GameManager.StaticInstance.ControllersManager.Player.Equipment.RangedWeaponSlot.Config);
            //_helmetArmorSlot.Initialize(GameManager.StaticInstance.ControllersManager.Player.Equipment.HelmetSlot.Config);
            //_chestArmorSlot.Initialize(GameManager.StaticInstance.ControllersManager.Player.Equipment.ChestSlot.Config);
        }

        public void ShowFullInformation(ItemConfig config)
        {
            if (config != null)
            {
                _infoBarIconImage.sprite = config.Icon.LoadAsset();
                _infoBarNameText.text = config.DisplayName.GetLocalizedString();
                _infoBarDescriptionText.text = config.Description.GetLocalizedString();
            }
            else
            {
                _infoBarIconImage.sprite = _emptySprite;
                _infoBarNameText.text = string.Empty;
                _infoBarDescriptionText.text = string.Empty;
            }
        }
    }
}