using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject Inventory;
    public Transform InventoryPanel;
    public List<InventorySlot> Slots = new List<InventorySlot>();
    public bool IsOpen;
    void Start()
    {
        for(var i = 0; i < InventoryPanel.childCount; i++)
        {
            if(InventoryPanel.GetChild(i).GetComponent<InventorySlot>() != null)
                Slots.Add(InventoryPanel.GetChild(i).GetComponent<InventorySlot>());
        }
        Inventory.SetActive(false);
        IsOpen = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            IsOpen = !IsOpen;
            if (IsOpen)
            {
                Inventory.SetActive(true);
            }
            else
            {
                Inventory.SetActive(false);
            }
        }
    }
}
