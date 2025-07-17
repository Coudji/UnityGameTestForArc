using UnityEngine;
using UnityEngine.UIElements;

namespace Arc.UI.Templates
{
    public class VisualSlot : VisualElement
    {
        public Image Icon;
        public Label Quantity;
        public Item Item;
    
        public VisualSlot(int size, int margin)
        {
            Icon = new Image();
            Icon.AddToClassList(UIStyleClasses.SlotIcon);
    
            Add(Icon);
    
            Quantity = new Label();
            Quantity.AddToClassList(UIStyleClasses.SlotQuantity);
    
            Icon.Add(Quantity);
    
            AddToClassList(UIStyleClasses.InventorySlot);
            AddInlineStyle(size, margin);
    
            RegisterCallback<PointerDownEvent>(OnPointerDown);
            RegisterCallback<PointerUpEvent>(OnPointerUp);
            // RegisterCallback<PointerMoveEvent>(OnPointerMove);
    
            RegisterCallback<ClickEvent>(OnClick);
        }
    
        private void AddInlineStyle(int size, int margin)
        {
            style.width = size;
            style.height = size;
            style.marginLeft = margin;
            style.marginTop = margin;
            style.marginRight = margin;
            style.marginBottom = margin;
        }
    
        public void ToEmpty()
        {
            Item = null;
            Icon.image = null;
            Quantity.text = "";
            Quantity.style.display = DisplayStyle.None;
        }
    
        public void SetItem(Item item, int quantity)
        {
            Icon.image = item.Icon.texture;
    
            if (item.MaxStack >= 2)
            {
                Quantity.text = quantity.ToString();
                Quantity.style.display = DisplayStyle.Flex;
            }
            else
            {
                Quantity.text = "";
                Quantity.style.display = DisplayStyle.None;
            }
    
            Item = item;
        }
    
        private bool IsEmpty()
        {
            return Item == null;
        }
    
        private void OnPointerDown(PointerDownEvent evt)
        {
            if (evt.button != 0 || IsEmpty())
                return;
        }
    
        private void OnPointerUp(PointerUpEvent evt)
        {
            // Debug.Log($"Pointer up on slot with ItemId: {Item.Id}");
        }
    
        private void OnClick(ClickEvent evt)
        {
            if (IsEmpty())
                return;
    
            if (evt.clickCount == 2)
            {
                if (Item.Category == ItemCategory.Equipment)
                {
                    ItemEvents.RaiseItemEquipped(Item);
                }
            }
        }
    }
}
