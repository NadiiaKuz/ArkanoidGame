using System.Collections.Generic;

namespace ArkanoidGame.GameObjects
{
    public interface IBouncingDiagonalMovingGameObject : IDiagonalMovingGameObject
    {
        void Bounce();
        void SetBounceFormObject(GameObject gameObject);
        bool IsCollisionWithBounceFromObject(int newX, int newY);
        void SetBounceFromDestroyingObjects(List<GameObject> destroyingGameObjects);
        bool IsCollisionWithDestroyingObjects(int newX, int newY);
    }
}
