using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public ItemScriptableObject item;
    public int amount;
    public bool isEmpty = true;
    public bool isCraftSlot = false;
    public GameObject icon;
    public TMP_Text itemAmount;

    private void Awake()
    {
        icon = transform.GetChild(0).gameObject;
        itemAmount = transform.GetChild(1).GetComponent<TMP_Text>();
    }

    private void Start()
    {
        icon = transform.GetChild(0).gameObject;
        itemAmount = transform.GetChild(1).GetComponent<TMP_Text>();
    }

    private void Update()
    {
        if (item != null && !isCraftSlot)
        {
            isEmpty = false;
            icon.GetComponent<Image>().sprite = item.icon;
            icon.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            itemAmount.text = amount.ToString();
        }
        if (amount == 0 && !isCraftSlot)
        {
            isEmpty = true;
            item = null;
            icon.GetComponent<Image>().sprite = null;
            icon.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            itemAmount.text = "";
        }
        if (isCraftSlot)
        {
            isEmpty = true;
            icon.GetComponent<Image>().sprite = item.icon;
            icon.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
            Debug.Log(icon.GetComponent<Image>().color);
        }
    }
    public void SetIcon(Sprite _icon)
    {
        icon.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        icon.GetComponent<Image>().sprite = _icon;
    }
    
    public void SetSlot(InventorySlot inventorySlot)
    {
        item = inventorySlot.item;
        amount = inventorySlot.amount;
        isEmpty = false;
        icon.GetComponent<Image>().sprite = inventorySlot.icon.GetComponent<Image>().sprite;
        itemAmount = inventorySlot.itemAmount;
    }
    
    public void NullifySlotData()
    {
        item = null;
        amount = 0;
        isEmpty = true;
        icon.GetComponent<Image>().sprite = null;
        icon.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        itemAmount.text = null;
    }
}
