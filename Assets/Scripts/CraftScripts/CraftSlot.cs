using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class CraftSlot : MonoBehaviour
{
    public Transform inventoryPanel;
    public List<InventorySlot> slots = new List<InventorySlot>();

    private void Start()
    {
        for(var i = 0; i < inventoryPanel.childCount; i++)
            if (inventoryPanel.GetChild(i).GetComponent<InventorySlot>() != null)
                slots.Add(inventoryPanel.GetChild(i).GetComponent<InventorySlot>());
    }

    public void CraftItem(CraftScriptableObject craftItem)
    {
        var dictResources = new Dictionary<CraftResource, int>();
        var inventoryItems = new List<InventorySlot>();
        var countResources = 0;
        var isCraftable = true;

        foreach (var slot in slots)
        {
            if (!slot.isEmpty)
                inventoryItems.Add(slot);
            else
                break;
        }

        if (inventoryItems.Count == 0)
            return;

        foreach (var resource in craftItem.resources)
        {
            foreach (var slot in inventoryItems)
            {
                if (resource.item == slot.item)
                {
                    if (resource.craftAmount > slot.amount)
                        isCraftable = false;
                    dictResources.Add(resource, resource.craftAmount);
                    countResources++;
                    break;
                }
            }
            if (!isCraftable)
                break;
        }

        if (countResources != craftItem.resources.Count)
            return;

        if (isCraftable)
        {
            foreach (var resource in craftItem.resources)
                RemoveItem(resource.item, dictResources[resource]);
            AddItem(craftItem.item);
        }
    }

    public void AddItem(ItemScriptableObject item)
    {
        foreach (InventorySlot slot in slots)
        {
            if (slot.item == item)
            {
                slot.amount += 1;
                slot.itemAmount.text = slot.amount.ToString();
                return;
            }
        }
        foreach (InventorySlot slot in slots)
        {
            if (slot.isEmpty)
            {
                slot.isEmpty = false;
                slot.item = item;
                slot.amount = 1;
                slot.SetIcon(item.icon);
                slot.itemAmount.text = "1";
                return;
            }
        }
    }
    public void RemoveItem(ItemScriptableObject item, int amount)
    {
        foreach (InventorySlot slot in slots)
        {
            if (slot.item == item)
            {
                slot.amount -= amount;
                slot.itemAmount.text = slot.amount.ToString();
                if (slot.amount == 0)
                {
                    slot.isEmpty = true;
                    slot.item = null;
                    slot.icon.GetComponent<Image>().sprite = null;
                    slot.icon.GetComponent<Image>().color = new Color(1, 1, 1, 0);
                    slot.itemAmount.text = "";
                }
                return;
            }
        }
    }
}
