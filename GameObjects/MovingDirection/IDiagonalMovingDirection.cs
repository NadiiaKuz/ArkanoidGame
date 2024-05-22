namespace ArkanoidGame.GameObjects.MovingDirection
{
    public interface IDiagonalMovingDirection : IMovingDirection
    {
        bool IsMovingUpRight();
        bool IsMovingUpLeft();
        bool IsMovingDownRight();
        bool IsMovingDownLeft();

        void ChangeDirectionToUpLeft();
        void ChangeDirectionToUpRight();
        void ChangeDirectionToDownLeft();
        void ChangeDirectionToDownRight();
    }
}
