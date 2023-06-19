using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HandSlotScript : MonoBehaviour
{
    // Start is called before the first frame update
    private InventorySlot handSlot;
    void Start()
    {
        handSlot = transform.GetComponent<InventorySlot>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeSlots(InventorySlot inventorySlot)
    {
        Debug.Log(handSlot.amount);
        if (handSlot.item == null && !inventorySlot.isCraftSlot) 
        {
            handSlot.SetSlot(inventorySlot);
            inventorySlot.NullifySlotData();
        }
        else if (handSlot.item == inventorySlot.item)
        {
            inventorySlot.amount += handSlot.amount;
            handSlot.NullifySlotData();
        } 
        else if (handSlot.item != null)
        {
            ItemScriptableObject item = handSlot.item;
            var amount = handSlot.amount;
            handSlot.SetSlot(inventorySlot);
            inventorySlot.item = item;
            inventorySlot.amount = amount;
            inventorySlot.icon.GetComponent<Image>().sprite = item.icon;
            inventorySlot.icon.GetComponent<Image>().color = new Color(255, 255, 255 , 255);
            inventorySlot.itemAmount.text = amount.ToString();
        }
        //handSlot.icon.GetComponent<Image>().color = new Color(250, 250, 250, 255);
        handSlot.icon.GetComponent<Image>().color = Color.white;
    }
}
