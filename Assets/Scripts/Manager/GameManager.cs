using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class GameManager : Singleton<GameManager>
    {
        private List<BasicComponent> _components;
        public InputMode InputMode { get; private set; }
        public AudioManager AudioManager { get; private set; }
        public CameraManager CameraManager { get; private set; }
        public ConfigsManager ConfigsManager { get; private set; }
        public LayersManager LayersManager { get; private set; }
        public PawnsManager PawnsManager { get; private set; }
        public TimeManager TimeManager { get; private set; }
        public UIManager UIManager { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            _components = new();
            AudioManager = GetComponentInChildren<AudioManager>();
            CameraManager = GetComponentInChildren<CameraManager>();
            ConfigsManager = GetComponentInChildren<ConfigsManager>();
            LayersManager = GetComponentInChildren<LayersManager>();
            PawnsManager = GetComponentInChildren<PawnsManager>();
            TimeManager = GetComponentInChildren<TimeManager>();
            UIManager = GetComponentInChildren<UIManager>();
            _components.Add(AudioManager);
            _components.Add(CameraManager);
            _components.Add(ConfigsManager);
            _components.Add(LayersManager);
            _components.Add(PawnsManager);
            _components.Add(TimeManager);
            //_components.Add(UIManager);
            foreach (BasicComponent component in _components)
            {
                component.Initialize();
            }
            SetInputMode(InputMode.Game);
        }

        private void OnEnable()
        {
            foreach (BasicComponent component in _components)
            {
                component.Enable();
            }
        }

        private void OnDisable()
        {
            foreach (BasicComponent component in _components)
            {
                component.Disable();
            }
        }

        private void Update()
        {
            foreach (BasicComponent component in _components)
            {
                component.OnUpdate();
            }
        }

        private void FixedUpdate()
        {
            foreach (BasicComponent component in _components)
            {
                component.OnFixedUpdate();
            }
        }

        private void LateUpdate()
        {
            foreach (BasicComponent component in _components)
            {
                component.OnLateUpdate();
            }
        }

        public void SetInputMode(InputMode mode)
        {
            InputMode = mode;
        }
    }
}