namespace ArkanoidGame.GameObjects.MovingDirection
{
    public interface IMovingDirection
    {
        bool IsNotMoving();
        void InitRandomDirection();
        void InitRandomSafeDirection();
    }
}
