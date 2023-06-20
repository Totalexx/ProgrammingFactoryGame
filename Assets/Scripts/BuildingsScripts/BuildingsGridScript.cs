using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingsGridScript : MonoBehaviour
{
    public Vector2Int gridSize = new Vector2Int(10, 10);
    public float cellSize;
    public GameObject gridn;
    public Transform inventoryPanel;
    public InventorySlot handSlot;
    public List<InventorySlot> slots = new List<InventorySlot>();
    public GameObject panel;
    public GameObject canvas;
    public BuildingScript[] buildingPrefab;
    //public InventorySlot inventorySlot;

    private BuildingScript[,] grid;
    private BuildingScript flyingBuilding;
    private Camera mainCamera;
    private bool isOpenCanvas;
    private bool isStartedPlacing;

    private void Awake()
    {
        mainCamera = Camera.main;
        grid = new BuildingScript[gridSize.x, gridSize.y];
        isOpenCanvas = canvas.GetComponent<InventoryManager>().isOpen;
        isStartedPlacing = false;
        for (var i = 0; i < inventoryPanel.childCount; i++)
            if (inventoryPanel.GetChild(i).GetComponent<InventorySlot>() != null)
                slots.Add(inventoryPanel.GetChild(i).GetComponent<InventorySlot>());
    }
    public void StartPlacingBuilding(InventorySlot inventorySlot)
    {
        var isInInventory = false;
        if (flyingBuilding != null)
        {
            Destroy(flyingBuilding.gameObject);
        }
        //if (slot.item.isBuilding)
        //{
        //    RemoveItem(slot.item, 1);
        //    flyingBuilding
        //}
        foreach (var slot in slots)
        {
            for (var i = 0; i < buildingPrefab.Length; i++)
                if (slot.item == buildingPrefab[i].item && slot.item == inventorySlot.item)
                {
                    RemoveItem(slot.item, 1);
                    //canvas.GetComponent<InventoryManager>().isOpen = false;
                    //Debug.Log(buildingPrefab.GetComponent<BuildingScript>().item.ToString() + ";" + slot.item.ToString());
                    //buildingPrefab.GetComponent<BuildingScript>().item = slot.item;
                    flyingBuilding = Instantiate(buildingPrefab[i]);
                    //panel.SetActive(false);
                    isInInventory = true;
                    break;
                }

            if (isInInventory)
            {
                break;
            }
                
        }
    }

    // Update is called once per frameda
    void Update()
    {
        //if (handSlot.item != null)
        //{
        //    if (!isOpenCanvas && handSlot.item.isBuilding && !isStartedPlacing)
        //    {
        //        isStartedPlacing = true;
        //        StartPlacingBuilding(handSlot);
        //    }
        //    if (isOpenCanvas)
        //        isStartedPlacing = false;
        //}
        
        if (flyingBuilding != null)
        {
            var pos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            pos.z = -1;

            var x = RoundToCell(pos.x, cellSize);
            var y = RoundToCell(pos.y, cellSize);

            isOpenCanvas = true;
            

            var collider = flyingBuilding.GetComponent<Collider2D>();
            var aCollider = gridn.GetComponent<Collider2D>();
            //Debug.Log(collider.ToString() + " " + aCollider.ToString());
            if (Physics2D.IsTouching(collider, aCollider))
            {
                Debug.Log("No");
                isOpenCanvas = false;
                flyingBuilding.GetComponent<SpriteRenderer>().color = Color.yellow;
            }

            flyingBuilding.transform.position = new Vector3((float)x, (float)y, -1);
            //Debug.Log(flyingBuilding.transform.position.ToString() + " " + pos);
            if (Input.GetMouseButtonDown(0))
            {
                flyingBuilding = null;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("No");
        isOpenCanvas = false;
        flyingBuilding.GetComponent<SpriteRenderer>().color = Color.yellow;
    }

    private double RoundToCell(float axis, float cellSize)
    {
        var result = Math.Floor(axis / cellSize) * cellSize + cellSize / 2;
        return result;
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
