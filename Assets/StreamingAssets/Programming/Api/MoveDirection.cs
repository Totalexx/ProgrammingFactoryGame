namespace Programming.Api
{
    public class MoveDirection
    {
        public static readonly MoveDirection LEFT = new ();
        public static readonly MoveDirection RIGHT = new ();
        public static readonly MoveDirection UP = new ();
        public static readonly MoveDirection DOWN = new ();

        private MoveDirection() {}
    }
}