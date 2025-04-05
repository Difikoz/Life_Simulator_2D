using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class PawnsManager : BasicComponent
    {
        private List<PawnController> _controllersToAdd;
        private List<PawnController> _controllersToRemove;

        public List<PawnController> Controllers { get; private set; }

        public override void Initialize()
        {
            _controllersToAdd = new();
            _controllersToRemove = new();
            Controllers = new();
            PawnController[] controllers = FindObjectsByType<PawnController>(FindObjectsSortMode.None);
            foreach (PawnController controller in controllers)
            {
                controller.Initialize();
                Controllers.Add(controller);
            }
        }

        public override void Enable()
        {
            foreach (PawnController controller in Controllers)
            {
                controller.Enable();
            }
        }

        public override void Disable()
        {
            foreach (PawnController controller in Controllers)
            {
                controller.Disable();
            }
        }

        public override void OnFixedUpdate()
        {
            AddControllers();
            RemoveControllers();
            foreach (PawnController controller in Controllers)
            {
                controller.OnFixedUpdate();
            }
        }

        public void AddController(PawnController controller)
        {
            _controllersToAdd.Add(controller);
        }

        public void RemoveController(PawnController controller)
        {
            _controllersToRemove.Add(controller);
        }

        private void AddControllers()
        {
            if (_controllersToAdd.Count > 0)
            {
                foreach (PawnController controller in _controllersToAdd)
                {
                    controller.Initialize();
                    controller.Enable();
                    Controllers.Add(controller);
                }
                _controllersToAdd.Clear();
            }
        }

        private void RemoveControllers()
        {
            if (_controllersToRemove.Count > 0)
            {
                foreach (PawnController controller in _controllersToRemove)
                {
                    Controllers.Remove(controller);
                    controller.Disable();
                    //controller.Destroy();
                    Destroy(controller.gameObject, 4f);// test
                }
                _controllersToRemove.Clear();
            }
        }
    }
}