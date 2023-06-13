namespace Programming.Api
{
    public class MoveDirection
    {
        public static readonly MoveDirection Left = new ();
        public static readonly MoveDirection Right = new ();
        public static readonly MoveDirection Up = new ();
        public static readonly MoveDirection Down = new ();

        private MoveDirection() {}
    }
}