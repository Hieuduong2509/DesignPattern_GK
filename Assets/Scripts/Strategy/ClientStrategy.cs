using UnityEngine;
using System.Collections.Generic;
using Chapter.Factory;

namespace Chapter.Strategy
{
    public class ClientStrategy : MonoBehaviour
    {

        [Header("Setting Pool Spawn")]
        public Collider spawnArea;

        private List<IManeuverBehaviour> _components = new List<IManeuverBehaviour>();

        private void SpawnDrone()
        {
            Vector3 spawnPos = new Vector3(0, 0.5f, 15f);

            if (spawnArea != null)
            {
                Bounds bounds = spawnArea.bounds;
                float randomX = Random.Range(bounds.min.x, bounds.max.x);
                float randomZ = Random.Range(bounds.min.z, bounds.max.z);
                spawnPos = new Vector3(randomX, bounds.max.y + 0.5f, randomZ);
            }

            GameObject spawnedDrone = DronePool.Instance.GetDrone(spawnPos);

            if (spawnedDrone != null)
            {
                ApplyRandomStrategies(spawnedDrone);
            }
        }

        private void ApplyRandomStrategies(GameObject drone)
        {
            _components.Clear();

            Drone droneComponent = drone.GetComponent<Drone>();

            _components.Add(drone.AddComponent<WeavingManeuver>());
            _components.Add(drone.AddComponent<BoppingManeuver>());
            _components.Add(drone.AddComponent<FallbackManeuver>());

            int index = Random.Range(0, _components.Count);

            droneComponent.ApplyStrategy(_components[index]);
        }

        void OnGUI()
        {
            GUILayout.BeginArea(new Rect(Screen.width - 220, 20, 200, 150));
            if (GUILayout.Button("Spawn Drone"))
            {
                SpawnDrone();
            }
            GUILayout.EndArea();
        }
    }
}
