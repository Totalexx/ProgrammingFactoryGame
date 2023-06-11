using System;
using UnityEngine;

namespace Programming
{
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

}