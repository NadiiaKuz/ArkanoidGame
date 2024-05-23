using ArkanoidGame.GameObjects.Positioning;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace ArkanoidGame.GameObjects
{
    /// <summary>
    /// An abstract game object that has some position and name,
    /// and also has access to a game statistics object to manage those statistics data
    /// </summary>
    public abstract class GameObject
    {
        protected GameObjectPosition position;

        public delegate void MultipleGameObjectsInteractionDelegate(object sender, ICollection<GameObject> otherObjects);

        public event MultipleGameObjectsInteractionDelegate CollapsedWithOtherObjects;
        public event EventHandler InitIncrementNumberOfFailures;
        public event EventHandler InitPositiveGameAction;

        protected virtual void OnCollapsedWithOtherObjects(ICollection<GameObject> otherObjects)
        {
            CollapsedWithOtherObjects?.Invoke(this, otherObjects);
        }
        protected virtual void OnInitIncrementNumberOfFailures()
        {
            InitIncrementNumberOfFailures?.Invoke(this, new EventArgs());
        }
        protected virtual void OnInitPositiveGameAction()
        {
            InitPositiveGameAction?.Invoke(this, new EventArgs());
        }

        public string Title { get; set; }

        public GameObjectPosition Position { get { return position; } }

        protected GameObject(string title)
        {
            position = new GameObjectPosition();
            Title = title;
        }

        protected GameObject(string title, int width, int height)
        {
            position = new GameObjectPosition();
            Title = title;
        }

        public void SetPosition(int x, int y)
        {
            position.X = x;
            position.Y = y;
        }

        public abstract Rectangle GetObjectRectangle();
    }
}
