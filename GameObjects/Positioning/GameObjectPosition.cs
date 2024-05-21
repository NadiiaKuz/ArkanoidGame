using System.Drawing;

namespace ArkanoidGame.GameObjects.Positioning
{
    /// <summary>
    /// Position of the game object
    /// </summary>
    public class GameObjectPosition
    {
        private Point position;

        public Point Position { get { return position; } }

        public int X { 
            get 
            { 
                return position == Point.Empty ? 0 : position.X; 
            } 
            set 
            { 
                CreateNewPositionIfEmpty();
                position.X = value;
            } 
        }

        public int Y { 
            get 
            { 
                return position == Point.Empty ? 0 : position.Y;
            } 
            set 
            { 
                CreateNewPositionIfEmpty();
                position.Y = value;
            } 
        }

        private void CreateNewPosition()
        {
            position = new Point();
        }

        private void CreateNewPositionIfEmpty()
        {
            if (position == Point.Empty)
            {
                CreateNewPosition();
            }
        }

        public void SetPosition(Point point)
        {
            CreateNewPositionIfEmpty();
            position.X = point.X;
            position.Y = point.Y;
        }

        public void SetPosition(int x, int y)
        {
            CreateNewPositionIfEmpty();
            position.X = x;
            position.Y = y;
        }
    }
}
