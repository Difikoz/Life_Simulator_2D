using UnityEngine;

namespace WinterUniverse
{
    public class UIManager : BasicComponent
    {
        private PlayerInputActions _inputActions;
        private HUDUI _hud;
        private StatusBarUI _statusBar;

        public HUDUI HUD => _hud;
        public StatusBarUI StatusBar => _statusBar;

        public override void Initialize()
        {
            base.Initialize();
            _inputActions = new();
            _hud = GetComponentInChildren<HUDUI>();
            _statusBar = GetComponentInChildren<StatusBarUI>();
            _hud.Initialize();
            _statusBar.Initialize();
        }

        public override void Enable()
        {
            base.Enable();
            _inputActions.Enable();
            _inputActions.UI.Status.performed += ctx => ToggleStatusBar();
            _inputActions.UI.PreviousTab.performed += ctx => PreviousTab();
            _inputActions.UI.NextTab.performed += ctx => NextTab();
            _hud.Enable();
            _statusBar.Enable();
        }

        public override void Disable()
        {
            _hud.Disable();
            _statusBar.Disable();
            _inputActions.UI.Status.performed -= ctx => ToggleStatusBar();
            _inputActions.UI.PreviousTab.performed -= ctx => PreviousTab();
            _inputActions.UI.NextTab.performed -= ctx => NextTab();
            _inputActions.Disable();
            base.Disable();
        }

        private void ToggleStatusBar()
        {
            if (_statusBar.isActiveAndEnabled)
            {
                _statusBar.gameObject.SetActive(false);
                GameManager.StaticInstance.SetInputMode(InputMode.Game);
            }
            else
            {
                _statusBar.gameObject.SetActive(true);
                GameManager.StaticInstance.SetInputMode(InputMode.UI);
            }
        }

        //private void ToggleHUD()
        //{
        //    if (_hud.isActiveAndEnabled)
        //    {
        //        _hud.gameObject.SetActive(false);
        //    }
        //    else
        //    {
        //        _hud.gameObject.SetActive(true);
        //    }
        //}

        private void PreviousTab()
        {
            if (_statusBar.isActiveAndEnabled)
            {
                _statusBar.PreviousTab();
            }
        }

        private void NextTab()
        {
            if (_statusBar.isActiveAndEnabled)
            {
                _statusBar.NextTab();
            }
        }
    }
}