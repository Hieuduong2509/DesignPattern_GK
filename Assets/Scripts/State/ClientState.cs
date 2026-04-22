using UnityEngine;
using Chapter.Observer;
using Chapter.Command;
namespace Chapter.State
{
    public class ClientState : MonoBehaviour
    {
        private BikeController _bikeController;


        void Start()
        {
            _bikeController = FindAnyObjectByType<BikeController>();
        }

        void OnGUI()
        {
            GUILayout.BeginArea(new Rect(20, Screen.height - 220, 200, 200));

            if (GUILayout.Button("Start Bike"))
                _bikeController.StartBike();

            if (GUILayout.Button("Turn Left"))
            {
                if (Invoker.Instance != null)
                {
                    Invoker.Instance.ExecuteCommand(
                        new TurnLeft(_bikeController));
                }
            }

            if (GUILayout.Button("Turn Right"))
            {
                if (Invoker.Instance != null)
                {
                    Invoker.Instance.ExecuteCommand(
                        new TurnRight(_bikeController));
                }
            }

            GUI.enabled = _bikeController.IsEngineOn;

            if (GUILayout.Button("Stop Bike"))
                _bikeController.StopBike();

            GUI.enabled = true;
            GUILayout.EndArea();
        }
    }
}
