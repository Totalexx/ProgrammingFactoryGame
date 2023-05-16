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
    public List<InventorySlot> slots = new List<InventorySlot>();

    private BuildingScript[,] grid;
    private BuildingScript flyingBuilding;
    private Camera mainCamera;
    private bool avilable;

    private void Awake()
    {
        mainCamera = Camera.main;
        grid = new BuildingScript[gridSize.x, gridSize.y];
        for (var i = 0; i < inventoryPanel.childCount; i++)
            if (inventoryPanel.GetChild(i).GetComponent<InventorySlot>() != null)
                slots.Add(inventoryPanel.GetChild(i).GetComponent<InventorySlot>());
    }
    public void StartPlacingBuidling(BuildingScript buildingPrefab)
    {
        if (flyingBuilding != null)
        {
            Destroy(flyingBuilding.gameObject);
        }
        foreach (var slot in slots)
        {
            if (slot.item.isBuilding)
            {
                RemoveItem(slot.item, 1);
                flyingBuilding = Instantiate(buildingPrefab);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (flyingBuilding != null)
        {
            var pos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            pos.z = -1;

            var x = RoundToCell(pos.x, cellSize);
            var y = RoundToCell(pos.y, cellSize);

            avilable = true;
            

            var collider = flyingBuilding.GetComponent<Collider2D>();
            var aCollider = gridn.GetComponent<Collider2D>();
            //Debug.Log(collider.ToString() + " " + aCollider.ToString());
            if (Physics2D.IsTouching(collider, aCollider))
            {
                Debug.Log("No");
                avilable = false;
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
        avilable = false;
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
