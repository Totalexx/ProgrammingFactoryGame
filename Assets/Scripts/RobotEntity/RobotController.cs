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

    void Awake()
    {
        SetName();
    }

    private void Update()
    {
        robotAction = robotAction.Run();
        if (Input.GetKey(KeyCode.Q))
            Mine();
    }

    public void MoveTo(MoveDirection moveDirection, Action onAchieved)
    {
        var nextPosition = transform.position + moveDirection.Direction;
        robotAction = new MoveAction(this, nextPosition, onAchieved);
    }

    public void CheckItem()
    {
        
    }
    
    public void Mine()
    {
        var colliders = new List<Collider2D>();
        Physics2D.OverlapCollider(gameObject.GetComponent<Collider2D>(), new ContactFilter2D().NoFilter(), colliders);
        
        var oreCollider = colliders.FirstOrDefault(c => c.GetComponent<Item>() != null);
        if (oreCollider == null)
            return;

        var itemMined = oreCollider.GetComponent<Item>().item;
        
        try
        {
            GetComponent<RobotInventory>().AddItem(new RobotItem(itemMined, 1));
        }
        catch (Exception e)
        {
            // ignored
        }

    }

    public void PutItem()
    {
            
    }
    
    private void SetName()
    {
        RobotName = NameGenerator.Generate();
        transform.Find("Canvas").Find("RobotName").GetComponent<TextMeshProUGUI>().SetText(RobotName);
        transform.name = "Robot-" + RobotName;
    }
}