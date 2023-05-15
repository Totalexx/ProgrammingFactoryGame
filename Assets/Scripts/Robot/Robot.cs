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

    // void MoveTo(MoveDirection moveDirection)
    // {
    //     var newPosition = moveDirection.Direction;
    //     transform.position = Vector3.Lerp(transform.position,)
    // }
    
    
}

public class MoveDirection
{
    public static MoveDirection LEFT = new (new Vector2(-1, 0));
    public static MoveDirection RIGHT = new (new Vector2(1, 0));
    public static MoveDirection TOP = new (new Vector2(0, -1));
    public static MoveDirection DOWN = new (new Vector2(0, 1));

    public Vector2 Direction { get; private set; }
    
    private MoveDirection(Vector2 direction)
    {
        Direction = direction;
    }
}
