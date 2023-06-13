using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class InventoryManager : MonoBehaviour
{
    public GameObject inventory;
    public GameObject craftPanel;
    public GameObject meltPanel;
    public GameObject textRecipes;
    public GameObject textMelts;
    public Transform inventoryPanel;
    public ItemScriptableObject furnaceItem;
    public List<InventorySlot> slots = new List<InventorySlot>();
    public float distanceBetweenPlayerResources;
    
    private Camera mainCamera;
    public bool isOpen;
    void Start()
    {
        mainCamera = Camera.main;
        for(var i = 0; i < inventoryPanel.childCount; i++)
        {
            if(inventoryPanel.GetChild(i).GetComponent<InventorySlot>() != null)
                slots.Add(inventoryPanel.GetChild(i).GetComponent<InventorySlot>());
        }
        inventory.SetActive(false);
    }

    private void Awake()
    {
        inventory.SetActive(true);
    }
    void Update()
    {
        Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 cameraPos = mainCamera.ScreenToWorldPoint(new Vector2(Screen.width / 2, Screen.height / 2));
        
        //Debug.Log(Vector2.Distance(cameraPos, mousePos));

        if (Input.GetMouseButtonDown(0) && Vector2.Distance(cameraPos, mousePos) < distanceBetweenPlayerResources && !isOpen)
        {
            Collider2D collider2D = Physics2D.OverlapPoint(mousePos);
            var itemResource = collider2D?.gameObject.GetComponent<Item>()?.item;
            if (itemResource != null && !itemResource.isBuilding)
            {
                AddItem(itemResource, 1);
            }
        }
        //if (Input.GetMouseButtonDown(1) &&
        //    !isOpen &&
        //    Physics2D.OverlapPoint(mousePos).gameObject.GetComponent<Item>().item == furnaceItem)
        //{
        //    Collider2D colliderBuilding = Physics2D.OverlapPoint(mousePos);
        //    var isFurnace = colliderBuilding.gameObject.GetComponent<Item>().item == furnaceItem;
        //    var smth = colliderBuilding.gameObject.GetComponent<MeltManager>();
        //    Debug.Log(smth);
        //    if (isFurnace)
        //    {
        //        isOpen = !isOpen;
        //        inventory.SetActive(true);
        //        meltPanel.SetActive(true);
        //        craftPanel.SetActive(false);
        //        textRecipes.SetActive(false);
        //        textMelts.SetActive(true);
        //    }
        //}

        if (Input.GetKeyDown(KeyCode.T))
        {
            isOpen = !isOpen;
            inventory.SetActive(isOpen);
            meltPanel.SetActive(false);
            craftPanel.SetActive(true);
            textRecipes.SetActive(true);
            textMelts.SetActive(false);
        }
    }

    private void AddItem(ItemScriptableObject item, int amount)
    {
        foreach (InventorySlot slot in slots)
        {
            if (slot.item == item)
            {
                slot.amount += amount;
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
                slot.amount = amount;
                slot.SetIcon(item.icon);
                slot.itemAmount.text = amount.ToString();
                return;
            }
        }
    }

    public void ClosePanel()
    {
        isOpen = false;
        inventory.SetActive(false);
    }
}
