using System.Collections.Generic;

public class Snake
{
    public List<Point> Body { get; private set; }
    public Direction CurrentDirection { get; set; }

    public Snake(int initialX, int initialY)
    {
        Body = new List<Point>
        {
            new Point(initialX, initialY)
        };
        CurrentDirection = Direction.Right;
    }

    public void Move()
    {
        var head = Body[0];
        Point newHead;

        switch (CurrentDirection)
        {
            case Direction.Up:
                newHead = new Point(head.X, head.Y - 1);
                break;
            case Direction.Down:
                newHead = new Point(head.X, head.Y + 1);
                break;
            case Direction.Left:
                newHead = new Point(head.X - 1, head.Y);
                break;
            case Direction.Right:
                newHead = new Point(head.X + 1, head.Y);
                break;
            default:
                newHead = head;
                break;
        }

        Body.Insert(0, newHead);
        Body.RemoveAt(Body.Count - 1);
    }

    public void Grow()
    {
        var tail = Body[Body.Count - 1];
        Body.Add(tail);
    }

    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
}
