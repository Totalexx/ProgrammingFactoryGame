using Programming;
using TMPro;
using UnityEngine;

public class RobotController : MonoBehaviour
{
    private float _robotSpeed = 0.5f;
    public string RobotName { get; private set; }
    
    void Start()
    {
        SetName();
    }

    public void MoveTo(MoveDirection moveDirection)
    {
        var newPosition = moveDirection.Direction;
        transform.position += newPosition;
    }

    private void SetName()
    {
        RobotName = "Robot";//NameGenerator.Generate();
        transform.Find("Canvas").Find("RobotName").GetComponent<TextMeshProUGUI>().SetText(RobotName);
        transform.name = "Robot-" + RobotName;
    }
}