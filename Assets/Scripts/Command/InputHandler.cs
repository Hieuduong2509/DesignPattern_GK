using UnityEngine;
using Chapter.Observer;
namespace Chapter.Command
{
    public class InputHandler : MonoBehaviour
    {
        private Invoker _invoker;
        private bool _isReplaying;
        private bool _isRecording;
        private bool _showReplayCompleted;
        private BikeController _bikeController;
        private Command _buttonA, _buttonD, _buttonW;

        void Start()
        {
            _invoker = gameObject.AddComponent<Invoker>();
            _bikeController = FindAnyObjectByType<BikeController>();

            _buttonA = new TurnLeft(_bikeController);
            _buttonD = new TurnRight(_bikeController);
            _buttonW = new ToggleTurbo(_bikeController);
        }

        void Update()
        {
            if (!_isReplaying && _isRecording)
            {
                if (Input.GetKeyUp(KeyCode.A))
                    _invoker.ExecuteCommand(_buttonA);

                if (Input.GetKeyUp(KeyCode.D))
                    _invoker.ExecuteCommand(_buttonD);

                if (Input.GetKeyUp(KeyCode.W))
                    _invoker.ExecuteCommand(_buttonW);
            }
        }

        void OnGUI()
        {
            GUILayout.BeginArea(new Rect(20, 180, 200, 120));

            GUILayout.Label("=== CONTROLS ===");
            GUILayout.Label("A : Turn Left");
            GUILayout.Label("D : Turn Right");
            GUILayout.Label("W : Toggle Turbo");

            GUILayout.EndArea();
            GUILayout.BeginArea(new Rect(Screen.width / 2 - 110, 20, 220, 180));
            if (GUILayout.Button("Start Recording"))
            {
                _bikeController.SavePosition();
                _isReplaying = false;
                _isRecording = true;
                _invoker.Record();
            }

            if (GUILayout.Button("Stop Recording"))
            {
                _bikeController.ResetPosition();
                _isRecording = false;
            }

            if (!_isRecording)
            {
                if (_invoker.HasRecordedCommands)
                {
                    if (GUILayout.Button("Start Replay"))
                    {
                        _bikeController.ResetPosition();
                        _isRecording = false;
                        _isReplaying = true;
                        _showReplayCompleted = false;
                        _invoker.Replay();
                    }

                    // detect replay finished
                    if (_invoker.IsReplayFinished)
                    {
                        _showReplayCompleted = true;
                    }

                    // show UI
                    if (_showReplayCompleted)
                    {
                        GUILayout.Label("Completed replay!");
                    }
                }
            }
            GUILayout.EndArea();
        }

    }
}
