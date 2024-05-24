using System.Drawing;

namespace ArkanoidGame.GameObjects.Instances
{
    public class StaticBlock : RectangularGameObject
    {
        public Color BorderColor {  get; set; }
        public Color BodyColor { get; set; }
        public int HitsToDestroy { get; set; }
        public int CurrentHits { get; set; } = 0;

        public StaticBlock(string title, int width, int height) : base(title, width, height)
        {
        }
    }
}
