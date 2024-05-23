using ArkanoidGame.GameObjects.MovingDirection;
using ArkanoidGame.GameObjects.Positioning;

namespace ArkanoidGame.GameObjects
{
    /// <summary>
    /// Describes a game object that can move in any direction specified by the IMovingDirection interface
    /// </summary>
    public interface IMovingGameObject<T> where T : IMovingDirection
    {
        void MoveAtCurrentDirection();
        bool CanMoveAtCurrentDirection(int lowerBoundX, int upperBoundX, int lowerBoundY, int upperBoundY, int upperBoundXDelta, int upperBoundYDelta);
        void InitRandomMovingDirection();
        T GetMovingDirection();
        void SetMovingDirection(T movingDirection);
        void SetWallFailureConstraint(WallPosition failureWallConstraint);
        bool ReachedWallFailureConstraint();
        void ResetReachedWallFailureConstraint();
        void SetMovingSpeed(int speed);
        int GetMovingSpeed();
    }
}
