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
        protected List<GameObject> destroyingStaticBlocks;
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

        public void InitRandomDiagonalMovingDirection()
        {
            IDiagonalMovingDirection direction = new SimpleDiagonalMovingDirection();
            direction.InitRandomDirection();
            SetMovingDirection(direction);
        }

        public void InitRandomSafeDiagonalMovingDirection()
        {
            IDiagonalMovingDirection safeDirection = new SimpleDiagonalMovingDirection();
            safeDirection.InitRandomSafeDirection();
            SetMovingDirection(safeDirection);
        }

        public void MoveAtCurrentDirection()
        {
            if (diagonalMovingDirection == null || diagonalMovingDirection.IsNotMoving())
            {
                return;
            }

            if (diagonalMovingDirection.IsMovingUpLeft())
            {
                MoveUpLeft();
            }
            else if (diagonalMovingDirection.IsMovingUpRight())
            {
                MoveUpRight();
            }
            else if (diagonalMovingDirection.IsMovingDownLeft())
            {
                MoveDownLeft();
            }
            else if (diagonalMovingDirection.IsMovingDownRight())
            {
                MoveDownRight();
            }
        }

        public bool IsCollisionWithBounceFromObject(int newX, int newY)
        {
            if (movingPlatform.Position.Y > newY + GetObjectRectangle().Height)
            {
                return false;
            }

            if (movingPlatform.Position.X > newX + GetObjectRectangle().Width ||
                movingPlatform.Position.X + movingPlatform.GetObjectRectangle().Width < newX)
            {
                return false;
            }

            happenedCollisionWithBounceFromObject = true;

            OnInitPositiveGameAction();

            return true;
        }

        public bool IsCollisionWithDestroyingObjects(int newX, int newY)
        {
            if (destroyingStaticBlocks == null || destroyingStaticBlocks.Count == 0)
            {
                return false;
            }

            List<StaticBlock> destroyedBlocks = new List<StaticBlock>();
            bool isCollisionWithOneOfTheBlocks = false;

            foreach (GameObject blockGameObject in destroyingStaticBlocks)
            {
                if (blockGameObject.Position.Y + blockGameObject.GetObjectRectangle().Height < newY)
                {
                    continue;
                }

                if (blockGameObject.Position.X > newX + GetObjectRectangle().Width ||
                    blockGameObject.Position.X + blockGameObject.GetObjectRectangle().Width < newX)
                {
                    continue;
                }

                if (blockGameObject is StaticBlock staticBlock)
                {
                    isCollisionWithOneOfTheBlocks = true;
                    if (staticBlock.CurrentHits + 1 >= staticBlock.HitsToDestroy)
                    {
                        destroyedBlocks.Add(staticBlock);
                    }
                    else
                    {
                        staticBlock.CurrentHits++;
                    }
                }
            }

            if (isCollisionWithOneOfTheBlocks)
            {
                List<GameObject> destroyedBlockObjects = new List<GameObject>(destroyedBlocks);
                OnCollapsedWithOtherObjects(destroyedBlockObjects);
                destroyingStaticBlocks.RemoveAll(block => block is StaticBlock staticBlock && destroyedBlocks.Contains(staticBlock));

                wallPosition = WallPosition.WallFromTheTop;
                return true;
            }

            return false;
        }

        public bool CanMoveAtCurrentDirection(int lowerBoundX, int upperBoundX, int lowerBoundY, int upperBoundY, int upperBoundXDelta, int upperBoundYDelta)
        {
            if (diagonalMovingDirection == null || diagonalMovingDirection.IsNotMoving())
            {
                return false;
            }

            int newX = 0;
            int newY = 0;

            if (diagonalMovingDirection.IsMovingUpLeft())
            {
                newX = position.X - movingSpeed;
                newY = position.Y - movingSpeed;

                if (IsCollisionWithDestroyingObjects(newX, newY))
                {
                    return false;
                }

                if (position.X - movingSpeed > lowerBoundX)
                {
                    if (position.Y - movingSpeed > upperBoundY)
                    {
                        return true;
                    }
                    else
                    {
                        wallPosition = WallPosition.WallFromTheTop;
                    }
                }
                else
                {
                    wallPosition = WallPosition.WallFromTheLeft;
                }
            }
            else if (diagonalMovingDirection.IsMovingUpRight())
            {
                newX = position.X + movingSpeed;
                newY = position.Y - movingSpeed;

                if (IsCollisionWithDestroyingObjects(newX, newY))
                {
                    return false;
                }

                if (position.X + movingSpeed + GetObjectRectangle().Width + upperBoundXDelta < upperBoundX)
                {
                    if (position.Y - movingSpeed > lowerBoundY)
                    {
                        return true;
                    }
                    else
                    {
                        wallPosition = WallPosition.WallFromTheTop;
                    }
                }
                else
                {
                    wallPosition = WallPosition.WallFromTheRight;
                }
            }
            else if (diagonalMovingDirection.IsMovingDownLeft())
            {
                newX = position.X - movingSpeed;
                newY = position.Y + movingSpeed + GetObjectRectangle().Height + upperBoundYDelta;

                if (IsCollisionWithBounceFromObject(position.X, position.Y))
                {
                    return false;
                }

                if (newX > lowerBoundX)
                {
                    if (newY < upperBoundY)
                    {
                        return true;
                    }
                    else
                    {
                        wallPosition = WallPosition.WallFromTheBottom;
                        if (wallPosition == failureWallConstraint)
                        {
                            OnInitIncrementNumberOfFailures();
                            reachedFailureConstraint = true;
                        }
                    }
                }
                else
                {
                    wallPosition = WallPosition.WallFromTheLeft;
                }
            }
            else if (diagonalMovingDirection.IsMovingDownRight())
            {
                newX = position.X + movingSpeed + GetObjectRectangle().Width + upperBoundXDelta;
                newY = position.Y + movingSpeed + GetObjectRectangle().Height + upperBoundYDelta;

                if (IsCollisionWithBounceFromObject(position.X, position.Y))
                {
                    return false;
                }

                if (newX < upperBoundX)
                {
                    if (newY < upperBoundY)
                    {
                        return true;
                    }
                    else
                    {
                        wallPosition = WallPosition.WallFromTheBottom;
                        if (wallPosition == failureWallConstraint)
                        {
                            OnInitIncrementNumberOfFailures();
                            reachedFailureConstraint = true;
                        }
                    }
                }
                else
                {
                    wallPosition = WallPosition.WallFromTheRight;
                }
            }

            return false;
        }

        private void BounceWhenMovingUpLeft()
        {
            if (wallPosition == WallPosition.WallFromTheLeft)
            {
                diagonalMovingDirection.ChangeDirectionToUpRight();
            }
            else if (wallPosition == WallPosition.WallFromTheTop)
            {
                diagonalMovingDirection.ChangeDirectionToDownLeft();
            }
        }

        private void BounceWhenMovingUpRight()
        {
            if (wallPosition == WallPosition.WallFromTheRight)
            {
                diagonalMovingDirection.ChangeDirectionToUpLeft();
            }
            else if (wallPosition == WallPosition.WallFromTheTop)
            {
                diagonalMovingDirection.ChangeDirectionToDownRight();
            }
        }

        private void CheckForCollisionWithPlatform(Action actionIfCollisionHappened)
        {
            if (happenedCollisionWithBounceFromObject)
            {
                actionIfCollisionHappened.Invoke();
                happenedCollisionWithBounceFromObject = false;
            }
        }

        private void CheckForCollisionWithPlatformAndBounceWhenMovingDownLeft()
        {
            CheckForCollisionWithPlatform(() => diagonalMovingDirection.ChangeDirectionToUpLeft());

            if (wallPosition == WallPosition.WallFromTheLeft)
            {
                diagonalMovingDirection.ChangeDirectionToDownRight();
            }
            else if (wallPosition == WallPosition.WallFromTheBottom)
            {
                diagonalMovingDirection.ChangeDirectionToUpLeft();
            }
        }

        private void CheckForCollisionWithPlatformAndBounceWhenMovingDownRight()
        {
            CheckForCollisionWithPlatform(() => diagonalMovingDirection.ChangeDirectionToUpRight());

            if (wallPosition == WallPosition.WallFromTheRight)
            {
                diagonalMovingDirection.ChangeDirectionToDownLeft();
            }
            else if (wallPosition == WallPosition.WallFromTheBottom)
            {
                diagonalMovingDirection.ChangeDirectionToUpRight();
            }
        }

        public void Bounce()
        {
            if (diagonalMovingDirection == null || diagonalMovingDirection.IsNotMoving())
            {
                return;
            }

            if (diagonalMovingDirection.IsMovingUpLeft())
            {
                BounceWhenMovingUpLeft();
            }
            else if (diagonalMovingDirection.IsMovingUpRight())
            {
                BounceWhenMovingUpRight();
            }
            else if (diagonalMovingDirection.IsMovingDownLeft())
            {
                CheckForCollisionWithPlatformAndBounceWhenMovingDownLeft();
            }
            else if (diagonalMovingDirection.IsMovingDownRight())
            {
                CheckForCollisionWithPlatformAndBounceWhenMovingDownRight();
            }
        }

        public void SetWallFailureConstraint(WallPosition failureWallConstraint)
        {
            this.failureWallConstraint = failureWallConstraint;
        }

        public bool ReachedWallFailureConstraint()
        {
            return reachedFailureConstraint;
        }

        public void SetMovingSpeed(int speed)
        {
            movingSpeed = speed; 
        }

        public int GetMovingSpeed()
        {
            return movingSpeed;
        }

        public void SetBounceFromObject(GameObject gameObject)
        {
            movingPlatform = gameObject;
        }

        public void ResetReachedWallFailureConstraint()
        {
            reachedFailureConstraint = false;
        }

        public void InitRandomMovingDirection()
        {
            InitRandomDiagonalMovingDirection();
        }

        public IDiagonalMovingDirection GetMovingDirection()
        {
            return diagonalMovingDirection;
        }

        public void SetMovingDirection(IDiagonalMovingDirection movingDirection)
        {
            this.diagonalMovingDirection = movingDirection;
        }

        public void SetBounceFromDestroyingObjects(List<GameObject> destroyingGameObjects)
        {
            this.destroyingStaticBlocks = destroyingGameObjects;
        }
    }
}
