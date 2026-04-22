using UnityEngine;
using System.Collections.Generic;
using Chapter.Strategy;

namespace Chapter.Factory
{
    public class DronePool : MonoBehaviour
    {
        public static DronePool Instance { get; private set; }

        private Queue<GameObject> _pool = new Queue<GameObject>();
        public int poolSize = 10;

        void Awake()
        {
            if (Instance == null) Instance = this;

            for (int i = 0; i < poolSize; i++)
            {
                GameObject drone = GameObject.CreatePrimitive(PrimitiveType.Cube);
                drone.AddComponent<Drone>();
                drone.SetActive(false); 
                _pool.Enqueue(drone);
            }
        }

        public GameObject GetDrone(Vector3 position)
        {
            if (_pool.Count > 0)
            {
                GameObject drone = _pool.Dequeue();
                drone.transform.position = position;
                drone.SetActive(true);
                return drone;
            }

            Debug.LogWarning("The pool is out of drones!");
            return null;
        }


        public void ReturnDrone(GameObject drone)
        {
            drone.SetActive(false);
            _pool.Enqueue(drone);
        }
    }
}