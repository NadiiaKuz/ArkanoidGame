using ArkanoidGame.GameObjects.MovingDirection;
using ArkanoidGame.GameObjects.Positioning;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace ArkanoidGame.GameObjects.Instances
{
    /// <summary>
    /// Represents a game ball that can bounce off walls (edges of the shape)
    /// or from a given game object (player's moving platform).
    /// The ball can also perform diagonal movements in the game - due to
    /// the implementation of the IBouncingDiagonalMovingGameObject interface
    /// </summary>
    public class BouncingBall : GameObject, IBouncingDiagonalMovingGameObject
    {
        protected int radius;
        protected Random random;
        protected WallPosition wallPosition;
        protected WallPosition failureWallConstraint;
        protected bool reachedFailureConstraint;
        protected int movingSpeed;
        protected GameObject movingPlatform;
        protected List<GameObject> destroyingStaticBloks;
        protected bool happenedCollisionWithBounceFromObject;
        protected IDiagonalMovingDirection diagonalMovingDirection;

        public int Radius { get { return radius; } }

        public BouncingBall(string title, int radius) : base(title)
        {
            this.radius = radius;
            this.movingSpeed = 1;
            this.reachedFailureConstraint = false;
            this.happenedCollisionWithBounceFromObject = false;
            this.wallPosition = WallPosition.NoWall;
            this.failureWallConstraint = WallPosition.NoWall;
            this.random = new Random();
        }

        public override Rectangle GetObjectRectangle()
        {
            return new Rectangle(position.X, position.Y, 2 * radius, 2 * radius);
        }

        public void MoveUpRight()
        {
            position.X += movingSpeed;
            position.Y -= movingSpeed;
        }

        public void MoveUpLeft()
        {
            position.X -= movingSpeed;
            position.Y -= movingSpeed;
        }

        public void MoveDownLeft()
        {
            position.X -= movingSpeed;
            position.Y += movingSpeed;
        }

        public void MoveDownRight()
        {
            position.X += movingSpeed;
            position.Y += movingSpeed;
        }
    }
}
