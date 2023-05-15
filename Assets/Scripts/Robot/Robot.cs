using UnityEngine;

public class Robot : MonoBehaviour
{

    private float _robotSpeed = 0.5f;
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void MoveTo(MoveDirection moveDirection)
    {
        var newPosition = moveDirection.Direction;
        transform.position += newPosition;
    }
    
}

public class MoveDirection
{
    public static readonly MoveDirection LEFT = new (new Vector3(-1, 0, 0));
    public static readonly MoveDirection RIGHT = new (new Vector3(1, 0, 0));
    public static readonly MoveDirection UP = new (new Vector3(0, 1, 0));
    public static readonly MoveDirection DOWN = new (new Vector3(0, -1, 0));

    public Vector3 Direction { get; private set; }
    
    private MoveDirection(Vector3 direction)
    {
        Direction = direction;
    }
}
