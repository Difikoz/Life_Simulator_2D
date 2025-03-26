using UnityEngine.EventSystems;

namespace WinterUniverse
{
    public class InventorySlotUI : BasicFullSlotUI, IPointerClickHandler, ISubmitHandler
    {
        private ItemStack _stack;

        public ItemStack Stack => _stack;

        public void Initialize(ItemStack stack)
        {
            _stack = stack;
            SetIcon(_stack.Item.Icon.LoadAsset());
            SetText(_stack.Amount > 1 ? _stack.Amount.ToString() : "");
        }

        public override void OnSelect(BaseEventData eventData)
        {
            base.OnSelect(eventData);
            GameManager.StaticInstance.UIManager.StatusBar.InventoryPage.ShowFullInformation(_stack.Item);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                //show use, drop and drop all?
            }
            else
            {
                OnSubmit(eventData);
            }
        }

        public void OnSubmit(BaseEventData eventData)
        {
            //show use, drop and drop all?
            //_stack.Item.OnUse(GameManager.StaticInstance.ControllersManager.Player);
        }
    }
}