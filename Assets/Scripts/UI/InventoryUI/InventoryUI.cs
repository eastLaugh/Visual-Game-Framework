using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VGF.Inventory;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private SlotUI[] playerSlots;
    private void OnEnable()
    {
        EventHandler.UpdateInventoryUI += OnUpdateInventoryUI;
    }
    private void OnDisable()
    {
        EventHandler.UpdateInventoryUI -= OnUpdateInventoryUI;
    }
    void Start()
    {
        for(int i = 0; i < playerSlots.Length; i++)
        {
            playerSlots[i].slotIndex = i;
        }
    }
    private void OnUpdateInventoryUI(InventoryLocation location, List<InventoryItem> list)
    {
        switch (location)
        {
            case InventoryLocation.player:
                for (int i = 0; i < playerSlots.Length; i++)
                {
                    if (list[i].amount > 0)
                    {
                        var item = InventoryManager.Instance.GetItemDetails(list[i].itemID);
                        playerSlots[i].UpdateSlot(item, list[i].amount);
                    }
                    else
                    {
                        playerSlots[i].UpdateEmptySlot();
                    }
                }
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
