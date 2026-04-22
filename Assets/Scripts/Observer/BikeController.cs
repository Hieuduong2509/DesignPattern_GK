using UnityEngine;
using Chapter.State;
using Chapter.Memento;

namespace Chapter.Observer
{
    public class BikeController : Subject
    {
        public bool IsTurboOn
        {
            get; private set;
        }

        public float CurrentHealth
        {
            get { return health; }
        }

        private bool _isEngineOn;
        public bool IsEngineOn
        {
            get { return _isEngineOn; }
        }
        private HUDController _hudController;
        private CameraController _cameraController;
        [SerializeField] private float health = 100.0f;

        private Vector3 _startPosition = Vector3.zero;
        private Quaternion _startRotation = Quaternion.identity;

        void Awake()
        {
            _hudController = gameObject.AddComponent<HUDController>();
            _cameraController = FindAnyObjectByType<CameraController>();
        }

        private void Start()
        {
            StartEngine();
        }

        void OnEnable()
        {
            if (_hudController)
                Attach(_hudController);

            if (_cameraController)
                Attach(_cameraController);
        }

        void OnDisable()
        {
            if (_hudController)
                Detach(_hudController);

            if (_cameraController)
                Detach(_cameraController);
        }

        private void StartEngine()
        {
            _isEngineOn = true;
            NotifyObservers();
        }

        public void ToggleTurbo()
        {
            if (_isEngineOn)
                IsTurboOn = !IsTurboOn;

            NotifyObservers();
        }

        public void TakeDamage(float amount)
        {
            health -= amount;
            IsTurboOn = false;

            NotifyObservers();

            if (health < 0)
                Destroy(gameObject);
        }

        // =========================
        // ADD MISSING FUNCTIONS & POSITION LOGIC
        // =========================
        public BikeMemento SaveState()
        {
            Debug.Log("Bike state saved!");
            return new BikeMemento(health, transform.position, transform.rotation, IsTurboOn);
        }

        public void RestoreState(BikeMemento memento)
        {
            health = memento.Health;
            transform.position = memento.Position;
            transform.rotation = memento.Rotation;
            IsTurboOn = memento.IsTurboOn;

            Debug.Log($"Bike state restored! Health: {health}");

            NotifyObservers();
        }

        public void StartBike()
        {
            Debug.Log("Bike Started");
            _isEngineOn = true;
            NotifyObservers();
        }

        public void StopBike()
        {
            Debug.Log("Bike Stopped");
            _isEngineOn = false;
            NotifyObservers();
        }

        public void Turn(Direction direction)
        {
            Debug.Log("Turning: " + direction);
            transform.Rotate(Vector3.up * (int)direction * 10f);
            NotifyObservers();
        }

        public void SavePosition()
        {
            _startPosition = transform.position;
            _startRotation = transform.rotation;
            Debug.Log("Saved Start Position: " + _startPosition);
        }

        public void ResetPosition()
        {
            Debug.Log("Reset Bike to Saved Position");
            transform.position = _startPosition;
            transform.rotation = _startRotation;
            NotifyObservers();
        }
    }
}
