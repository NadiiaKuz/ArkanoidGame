using System.Drawing;

namespace ArkanoidGame.GameObjects.Instances
{
    /// <summary>
    /// Describes a rectangular shaped game object (the player's moving "platform")
    /// </summary>
    public class RectangularGameObject : GameObject
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public Color Color { get; set; }
        
        public RectangularGameObject(string title, int width, int height) : base(title)
        {
            Width = width;
            Height = height;
        }

        public override Rectangle GetObjectRectangle()
        {
            return new Rectangle(position.X, position.Y, Width, Height);
        }

        public int GetHalfWidth()
        {
            return Width / 2;
        }

        public bool CanMoveToPointWhenCentered(Point point, int boundsStartX, int boundsEndX, int rightBoundsDelta)
        {
            if (point == Point.Empty)
            {
                return false;
            }
            return point.X - GetHalfWidth() >= boundsStartX && point.X + GetHalfWidth() + rightBoundsDelta < boundsEndX;
        }

        public void SetPositionCenteredHorizontally(int initialX)
        {
            position.X = initialX - GetHalfWidth();
        }
    }
}
