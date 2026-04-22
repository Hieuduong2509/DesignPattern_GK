using UnityEngine;
using Chapter.Command;
namespace Chapter.Observer
{
    public class ClientObserver : MonoBehaviour
    {
        private BikeController _bikeController;
        private string _warningMessage = "";
        private float _warningTimer = 0f;
        void Start()
        {
            _bikeController = FindAnyObjectByType<BikeController>();
        }

        void OnGUI()
        {
            GUILayout.BeginArea(new Rect(230, Screen.height - 150, 200, 100));
            if (GUILayout.Button("Damage Bike"))
                if (_bikeController)
                    _bikeController.TakeDamage(15.0f);

            if (GUILayout.Button("Toggle Turbo"))
            {
                if (_bikeController)
                {
                    if (!_bikeController.IsEngineOn)
                    {
                        _warningMessage = "You must start bike to toggle turbo!";
                        _warningTimer = 2f;
                    }
                    else if (Invoker.Instance != null)
                    {
                        Invoker.Instance.ExecuteCommand(
                            new ToggleTurbo(_bikeController));
                    }
                }
            }
            if (_warningTimer > 0f)
            {
                GUI.color = Color.red;
                GUILayout.Label(_warningMessage);
                GUI.color = Color.white;

                _warningTimer -= Time.deltaTime;
            }
            GUILayout.EndArea();
        }
    }
}
