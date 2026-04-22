using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace Chapter.Command
{
    class Invoker : MonoBehaviour
    {
        public static Invoker Instance { get; private set; }

        void Awake()
        {
            Instance = this;
        }
        private bool _isRecording;
        private bool _isReplaying;
        private float _replayTime;
        private float _recordingTime;

        private int _replayIndex = 0;

        private SortedList<float, Command> _recordedCommands = new SortedList<float, Command>();

        public bool HasRecordedCommands
        {
            get { return _recordedCommands.Count > 0; }
        }
        public bool IsReplayFinished
        {
            get
            {
                return !_isReplaying
                    && _recordedCommands.Count > 0
                    && _replayIndex >= _recordedCommands.Count;
            }
        }

        public void ExecuteCommand(Command command)
        {
            command.Execute();

            if (_isRecording)
            {
                float timeToRecord = _recordingTime;

                while (_recordedCommands.ContainsKey(timeToRecord))
                {
                    timeToRecord += 0.00001f;
                }

                _recordedCommands.Add(timeToRecord, command);

                Debug.Log("Recorded Time: " + timeToRecord);
                Debug.Log("Recorded Command: " + command);
            }
        }

        public void Record()
        {
            _recordingTime = 0.0f;
            _isRecording = true;
            _isReplaying = false;

            _recordedCommands.Clear();
        }

        public void Replay()
        {
            _replayTime = 0.0f;
            _isReplaying = true;
            _isRecording = false;

            _replayIndex = 0;

            if (_recordedCommands.Count <= 0)
                Debug.LogError("No commands to replay!");
        }

        void FixedUpdate()
        {
            if (_isRecording)
                _recordingTime += Time.fixedDeltaTime;

            if (_isReplaying)
            {
                _replayTime += Time.fixedDeltaTime;

                if (_replayIndex < _recordedCommands.Count)
                {
                    if (_replayTime >= _recordedCommands.Keys[_replayIndex])
                    {
                        Debug.Log("Replay Time: " + _replayTime);
                        Debug.Log("Replay Command: " + _recordedCommands.Values[_replayIndex]);

                        _recordedCommands.Values[_replayIndex].Execute();

                        _replayIndex++;
                    }
                }
                else
                {
                    _isReplaying = false;
                }
            }
        }
    }
}
