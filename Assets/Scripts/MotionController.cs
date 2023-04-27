using UnityEngine;

public class MotionController : MonoBehaviour
{
    public float speed = 1.0f;

    private float deltaX;
    private float deltaY;
    private float moveLimeter = 0.7f;

    private Rigidbody2D body;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        deltaX = Input.GetAxisRaw("Horizontal");
        deltaY = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        if (deltaX != 0 && deltaY != 0)
        {
            deltaX *= moveLimeter;
            deltaY *= moveLimeter;
        }
        body.velocity = new Vector2(deltaX * speed, deltaY * speed);
    }
}
