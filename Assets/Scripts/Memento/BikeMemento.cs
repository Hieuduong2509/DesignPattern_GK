using UnityEngine;

namespace Chapter.Memento
{
    [System.Serializable] 
    public class BikeMemento
    {
        public float Health;
        public Vector3 Position;
        public Quaternion Rotation;
        public bool IsTurboOn;

        // Constructor
        public BikeMemento(float health, Vector3 position, Quaternion rotation, bool isTurboOn)
        {
            Health = health;
            Position = position;
            Rotation = rotation;
            IsTurboOn = isTurboOn;
        }
    }
}