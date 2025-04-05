using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [RequireComponent(typeof(PawnAnimatorComponent))]
    [RequireComponent(typeof(PawnCombatComponent))]
    [RequireComponent(typeof(PawnEquipmentComponent))]
    [RequireComponent(typeof(PawnInventoryComponent))]
    [RequireComponent(typeof(PawnLocomotionComponent))]
    [RequireComponent(typeof(PawnSensorComponent))]
    [RequireComponent(typeof(PawnSoundComponent))]
    [RequireComponent(typeof(PawnStatusComponent))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CircleCollider2D))]
    public class PawnController : BasicComponent
    {
        private List<PawnComponent> _components;
        public Rigidbody2D RB { get; private set; }
        public CircleCollider2D Collider { get; private set; }
        public PawnAnimatorComponent Animator { get; private set; }
        public PawnCombatComponent Combat { get; private set; }
        public PawnEquipmentComponent Equipment { get; private set; }
        public PawnInventoryComponent Inventory { get; private set; }
        public PawnLocomotionComponent Locomotion { get; private set; }
        public PawnSensorComponent Sensor { get; private set; }
        public PawnSoundComponent Sound { get; private set; }
        public PawnStatusComponent Status { get; private set; }
        public ContextSolver ContextSolver { get; private set; }

        public override void Initialize()
        {
            base.Initialize();
            _components = new();
            RB = GetComponent<Rigidbody2D>();
            Collider = GetComponent<CircleCollider2D>();
            Animator = GetComponent<PawnAnimatorComponent>();
            Combat = GetComponent<PawnCombatComponent>();
            Equipment = GetComponent<PawnEquipmentComponent>();
            Inventory = GetComponent<PawnInventoryComponent>();
            Locomotion = GetComponent<PawnLocomotionComponent>();
            Sensor = GetComponent<PawnSensorComponent>();
            Sound = GetComponent<PawnSoundComponent>();
            Status = GetComponent<PawnStatusComponent>();
            ContextSolver = new(this);
            _components.Add(Animator);
            _components.Add(Combat);
            _components.Add(Equipment);
            _components.Add(Inventory);
            _components.Add(Locomotion);
            _components.Add(Sensor);
            _components.Add(Sound);
            _components.Add(Status);
            foreach (PawnComponent component in _components)
            {
                component.Initialize();
            }
        }

        public override void Enable()
        {
            base.Enable();
            foreach (PawnComponent component in _components)
            {
                component.Enable();
            }
            Status.Revive();
        }

        public override void Disable()
        {
            foreach (PawnComponent component in _components)
            {
                component.Disable();
            }
            base.Disable();
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            foreach (PawnComponent component in _components)
            {
                component.OnFixedUpdate();
            }
        }
    }
}