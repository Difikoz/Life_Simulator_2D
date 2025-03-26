using UnityEngine;

namespace WinterUniverse
{
    public class GameManager : Singleton<GameManager>
    {
        private InputMode _inputMode;
        private AudioManager _audioManager;
        private CameraManager _cameraManager;
        private ConfigsManager _configsManager;
        private TimeManager _timeManager;
        private UIManager _uiManager;

        public InputMode InputMode => _inputMode;
        public AudioManager AudioManager => _audioManager;
        public CameraManager CameraManager => _cameraManager;
        public ConfigsManager ConfigsManager => _configsManager;
        public TimeManager TimeManager => _timeManager;
        public UIManager UIManager => _uiManager;

        protected override void Awake()
        {
            base.Awake();
            GetComponents();
            InitializeComponents();
        }

        private void OnEnable()
        {
            EnableComponents();
        }

        private void OnDisable()
        {
            DisableComponents();
        }

        private void GetComponents()
        {
            _audioManager = GetComponentInChildren<AudioManager>();
            _cameraManager = GetComponentInChildren<CameraManager>();
            _configsManager = GetComponentInChildren<ConfigsManager>();
            _timeManager = GetComponentInChildren<TimeManager>();
            _uiManager = GetComponentInChildren<UIManager>();
        }

        private void InitializeComponents()
        {
            _audioManager.Initialize();
            _cameraManager.Initialize();
            _configsManager.Initialize();
            _timeManager.Initialize();
            _uiManager.Initialize();
            SetInputMode(InputMode.Game);
        }

        private void EnableComponents()
        {
            _audioManager.Enable();
            _cameraManager.Enable();
            _configsManager.Enable();
            _timeManager.Enable();
            _uiManager.Enable();
        }

        private void DisableComponents()
        {
            _audioManager.Disable();
            _cameraManager.Disable();
            _configsManager.Disable();
            _timeManager.Disable();
            _uiManager.Disable();
        }

        private void FixedUpdate()
        {
            _audioManager.OnFixedUpdate();
            _cameraManager.OnFixedUpdate();
            _configsManager.OnFixedUpdate();
            _timeManager.OnFixedUpdate();
            _uiManager.OnFixedUpdate();
        }

        public void SetInputMode(InputMode mode)
        {
            _inputMode = mode;
        }
    }
}