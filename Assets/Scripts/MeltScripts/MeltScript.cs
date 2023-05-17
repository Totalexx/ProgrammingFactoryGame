using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UIElements;

public class MeltScript : MonoBehaviour
{
    public GameObject panel;
    public Transform inventoryPanel;
    public Transform MeltPanel;
    public GameObject craftPanel;
    public bool isOpen;
    public List<InventorySlot> slots = new List<InventorySlot>();

    private Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        for (var i = 0; i < inventoryPanel.childCount; i++)
        {
            if (inventoryPanel.GetChild(i).GetComponent<InventorySlot>() != null)
                slots.Add(inventoryPanel.GetChild(i).GetComponent<InventorySlot>());
        }
        panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && !isOpen)
        {
            Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Collider2D colliderBuilding = Physics2D.OverlapPoint(mousePos);
            var isBuilding = colliderBuilding.gameObject.GetComponent<Item>().item.isBuilding;
            if (isBuilding)
            {
                isOpen = !isOpen;
                panel.SetActive(isOpen);
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isOpen = false;
            panel.SetActive(false);
        }
    }

    //void UpdateInventory()
    //{
    //    for (var i = 0; i < inventoryPanel.childCount; i++)
    //    {
    //        if (inventoryPanel.GetChild(i).GetComponent<InventorySlot>() != null)
    //        {
    //            var child = anotherInventoryPanel.GetChild(i).GetComponent<InventorySlot>();
    //            child = slots[i];
    //        }  
    //    }
    //}
}
