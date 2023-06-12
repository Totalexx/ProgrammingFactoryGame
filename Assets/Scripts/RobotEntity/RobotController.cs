using System;
using Programming;
using Programming.RobotAction;
using RobotEntity.RobotAction;
using TMPro;
using UnityEngine;

public class RobotController : MonoBehaviour
{
    public float RobotSpeed { get; }= 3f;
    public string RobotName { get; private set; }

    private IRobotAction robotAction = new NoAction();
    
    void Start()
    {
        SetName();
    }

    private void Update()
    {
        robotAction = robotAction.Run();
    }

    public void MoveTo(MoveDirection moveDirection, Action onAchieved)
    {
        var nextPosition = transform.position + moveDirection.Direction;
        robotAction = new MoveAction(this, nextPosition, onAchieved);
    }

    private void SetName()
    {
        RobotName = "Robot";//NameGenerator.Generate();
        transform.Find("Canvas").Find("RobotName").GetComponent<TextMeshProUGUI>().SetText(RobotName);
        transform.name = "Robot-" + RobotName;
    }
}