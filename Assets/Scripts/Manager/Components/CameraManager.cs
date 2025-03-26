using UnityEngine;

namespace WinterUniverse
{
    public class CameraManager : BasicComponent
    {
        private Camera _camera;

        public Camera Camera => _camera;

        public override void Initialize()
        {
            base.Initialize();
            _camera = GetComponentInChildren<Camera>();
        }
    }
}