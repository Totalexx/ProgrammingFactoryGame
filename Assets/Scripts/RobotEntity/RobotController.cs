using System;
using System.Collections.Generic;
using System.Linq;
using Programming;
using Programming.RobotAction;
using RobotEntity;
using RobotEntity.RobotAction;
using TMPro;
using UnityEngine;
using Util;

public class RobotController : MonoBehaviour
{
    public float RobotSpeed { get; } = 3f;
    public string RobotName { get; private set; }

    private IRobotAction robotAction = new NoAction();
    private RobotInventory _inventory;

    void Awake()
    {
        SetName();
    }

    private void Update()
    {
        robotAction = robotAction.Run();
        _inventory = GetComponent<RobotInventory>();
    }

    public void MoveTo(MoveDirection moveDirection, Action onAchieved)
    {
        var nextPosition = transform.position + moveDirection.Direction;
        robotAction = new MoveAction(this, nextPosition, onAchieved);
    }

    public void CheckItem()
    {
        
    }
    
    public void Mine(Action afterAction)
    {
        var colliders = new List<Collider2D>();
        Physics2D.OverlapCollider(gameObject.GetComponent<Collider2D>(), new ContactFilter2D().NoFilter(), colliders);
        
        var oreCollider = colliders.FirstOrDefault(c => c.GetComponent<Item>() != null);
        if (oreCollider == null)
            return;

        var itemMined = oreCollider.GetComponent<Item>().item;
        
        try
        {
            _inventory.AddItem(new RobotItem(itemMined, 1));
        }
        catch (Exception e)
        {
            // ignored
        }

        robotAction = new WaitAction(1000, afterAction);
    }

    public void PutItem()
    {
        var colliders = new List<Collider2D>();
        Physics2D.OverlapCollider(gameObject.GetComponent<Collider2D>(), new ContactFilter2D().NoFilter(), colliders);
        
        var buildingCollider = colliders.FirstOrDefault(c => c.GetComponent<InventoryHolder>() != null);
        if (buildingCollider == null)
            return;

        var buildingWithInventory = buildingCollider.GetComponent<InventoryHolder>();
        try
        {
            buildingWithInventory.PutItem(_inventory.Item.Item, 1);
            _inventory.AddItem(new RobotItem(_inventory.Item.Item, -1));
        }
        catch (Exception e)
        {
        }
    }
    
    public void TakeItem()
    {
        var colliders = new List<Collider2D>();
        Physics2D.OverlapCollider(gameObject.GetComponent<Collider2D>(), new ContactFilter2D().NoFilter(), colliders);
        
        var buildingCollider = colliders.FirstOrDefault(c => c.GetComponent<InventoryHolder>() != null);
        if (buildingCollider == null)
            return;

        var buildingWithInventory = buildingCollider.GetComponent<InventoryHolder>();
        
        _inventory.AddItem(buildingWithInventory.TakeItem());
    }
    
    public void WriteLine()
    {
            
    }
    
    private void SetName()
    {
        RobotName = NameGenerator.Generate();
        transform.Find("Canvas").Find("RobotName").GetComponent<TextMeshProUGUI>().SetText(RobotName);
        transform.name = "Robot-" + RobotName;
    }
}