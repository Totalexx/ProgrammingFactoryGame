using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Cryptography;
using UnityEngine;

public class CraftItemScript : MonoBehaviour
{
    private Transform canvas;
    private Transform craftPanelActiveSlots;
    private Transform craftPanelPassiveSlots;
    private List<InventorySlot> craftSlots = new List<InventorySlot>();
    private Dictionary<ItemScriptableObject, int> dictResources = new Dictionary<ItemScriptableObject, int>();
    private CraftScriptableObject craftItem;
    private bool isCraftPanelActive;
    private float timeCraft;
    void Start()
    {
        isCraftPanelActive = false;
        canvas = transform.Find("Canvas");
        craftPanelActiveSlots = canvas.Find("CraftPanelActiveSlots");
        craftPanelPassiveSlots = canvas.Find("CraftPanelPassiveSlots");
        for (var i = 0; i < craftPanelActiveSlots.childCount; i++)
        {
            craftSlots.Add(craftPanelActiveSlots.GetChild(i).GetComponent<InventorySlot>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!canvas.GetComponent<CraftPanelManager>().isListCraftPanelActive && !isCraftPanelActive)
        {
            foreach (var slot in craftSlots)
            {
                if (slot.item != null)
                {
                    isCraftPanelActive = true;
                    break;
                }
            }

            if (isCraftPanelActive)
            {
                craftItem = canvas.GetComponent<CraftPanelManager>().lastCraftItem;
                foreach (var resource in craftItem.resources)
                    dictResources.Add(resource.item, resource.craftAmount);
                timeCraft = craftItem.timeCreate;
            }
        }
            

        if (isCraftPanelActive)
        {
            var isCraftable = false;
            foreach (var slot in craftSlots)
            {
                if (slot.item != null)
                {
                    if (slot.amount - dictResources[slot.item] >= 0)
                        isCraftable = true;
                    else
                    {
                        isCraftable = false;
                        break;
                    }
                }
            }
            
            if (isCraftable)
            {
                timeCraft -= Time.deltaTime;
                if (timeCraft < 0)
                {
                    timeCraft = craftItem.timeCreate;
                    AddItem(craftItem.item, craftItem.craftAmount);
                    foreach (var slot in craftSlots)
                        if (slot.item != null)
                            slot.amount -= dictResources[slot.item];
                }
            }
        }

        if (canvas.GetComponent<CraftPanelManager>().isListCraftPanelActive)
        {
            dictResources.Clear();
            isCraftPanelActive = false;
        }
            
    }

    private void AddItem(ItemScriptableObject item, int amount)
    {
        var slot = craftPanelPassiveSlots.GetChild(0).GetComponent<InventorySlot>();
        if (slot.item != null)
            slot.amount += amount;
        else
            slot.SetItem(item, amount);
    }
}
