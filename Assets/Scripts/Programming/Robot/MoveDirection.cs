using UnityEngine;

namespace Programming
{
    public class MoveDirection
    {
        public static readonly MoveDirection Left = new (new Vector3(-0.5f, 0, 0));
        public static readonly MoveDirection Right = new (new Vector3(0.5f, 0, 0));
        public static readonly MoveDirection Up = new (new Vector3(0, 0.5f, 0));
        public static readonly MoveDirection Down = new (new Vector3(0, -0.5f, 0));

        public Vector3 Direction { get; private set; }
    
        private MoveDirection(Vector3 direction)
        {
            Direction = direction;
        }
    }

}