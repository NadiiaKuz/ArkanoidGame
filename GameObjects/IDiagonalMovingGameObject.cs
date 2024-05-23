using ArkanoidGame.GameObjects.MovingDirection;

namespace ArkanoidGame.GameObjects
{
    public interface IDiagonalMovingGameObject : IMovingGameObject<IDiagonalMovingDirection>
    {
        void MoveUpRight();
        void MoveUpLeft();
        void MoveDownLeft();
        void MoveDownRight();
        void InitRandomDiagonalMovingDirection();
        void InitRandomSafeDiagonalMovingDirection();
    }
}
