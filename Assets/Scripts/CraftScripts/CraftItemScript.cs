using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftItemScript : MonoBehaviour
{
    private Transform canvas;
    private Transform craftPanelActiveSlots;
    private List<InventorySlot> craftSlots = new List<InventorySlot>();
    void Start()
    {
        canvas = transform.Find("Canvas");
        craftPanelActiveSlots = canvas.Find("CraftPanelActiveSlots");
        for (var i = 0; i < craftPanelActiveSlots.childCount; i++)
        {
            craftSlots.Add(craftPanelActiveSlots.GetChild(i).GetComponent<InventorySlot>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
