using System.Data;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryController : BaseMenuController
{
    [SerializeField]
    private InventoryConfig _config;

    private VisualElement _inventoryGrid;

    protected override void Awake()
    {
        base.Awake();
        InitializeInventory();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        GameUIEvents.OnInventoryChanged += RefreshUI;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        GameUIEvents.OnInventoryChanged -= RefreshUI;
    }

    private void InitializeInventory()
    {
        _inventoryGrid = _uiDocument.rootVisualElement.Q<VisualElement>(
            UIElementNames.InventoryGrid
        );

        if (_inventoryGrid == null)
        {
            Debug.LogError("Inventory grid not found in the UI document.");
            return;
        }

        _inventoryGrid.Clear();
        _inventoryGrid.style.width = (int)(
            (_config.columns + 0.1) * (_config.cellSize + _config.cellMargin * 2)
        );

        for (int i = 0; i < _config.MaxSlots; i++)
        {
            VisualSlot visualSlot = new(_config.cellSize, _config.cellMargin)
            {
                name = $"{UIElementNames.InventorySlot}_{i}",
            };

            _inventoryGrid.Add(visualSlot);
        }
    }

    private void RefreshUI(InventorySystem inventory)
    {
        var slots = inventory.Slots;
        int slotCount = Mathf.Min(_config.MaxSlots, slots.Count, _inventoryGrid.childCount);

        for (int i = 0; i < slotCount; i++)
        {
            if (_inventoryGrid[i] is not VisualSlot visualSlot)
            {
                Debug.LogWarning($"UI Slot {i} is not a VisualSlot.");
                continue;
            }

            InventoryItemSlot slot = slots[i];
            Item item = slot.Item;

            if (slot.IsEmpty || item == null || item.Icon == null)
            {
                visualSlot.ToEmpty();
            }
            else
            {
                visualSlot.SetItem(item, slot.Quantity);
            }
        }
    }
}
