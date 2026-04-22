using UnityEngine;
using Chapter.Observer;

namespace Chapter.Memento
{
    public class SaveLoadManager : MonoBehaviour
    {
        private BikeController _bikeController;

        private const string SAVE_KEY = "BikeSaveData";

        void Start()
        {
            _bikeController = FindAnyObjectByType<BikeController>();
        }

        void OnGUI()
        {
            GUILayout.BeginArea(new Rect(440, Screen.height - 120, 200, 100));

            if (GUILayout.Button("Save Game State"))
            {
                if (_bikeController != null)
                {
                    BikeMemento state = _bikeController.SaveState();

                    string json = JsonUtility.ToJson(state);

                    PlayerPrefs.SetString(SAVE_KEY, json);
                    PlayerPrefs.Save(); 

                    Debug.Log("Save game to disk successfully! Data: " + json);
                }
            }

            if (GUILayout.Button("Load Game State"))
            {
                if (_bikeController != null)
                {
                    if (PlayerPrefs.HasKey(SAVE_KEY))
                    {
                        string json = PlayerPrefs.GetString(SAVE_KEY);

                        BikeMemento loadedState = JsonUtility.FromJson<BikeMemento>(json);

                        _bikeController.RestoreState(loadedState);
                        Debug.Log("Load data successfully!");
                    }
                    else
                    {
                        Debug.LogWarning("Not found data!");
                    }
                }
            }

            GUILayout.EndArea();
        }
    }
}