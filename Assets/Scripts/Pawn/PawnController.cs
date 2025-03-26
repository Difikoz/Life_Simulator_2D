using UnityEngine;

namespace WinterUniverse
{
    [RequireComponent(typeof(PawnAnimatorComponent))]
    [RequireComponent(typeof(PawnCombatComponent))]
    [RequireComponent(typeof(PawnEquipmentComponent))]
    [RequireComponent(typeof(PawnInventoryComponent))]
    [RequireComponent(typeof(PawnLocomotionComponent))]
    [RequireComponent(typeof(PawnStatusComponent))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class PawnController : BasicComponent
    {
        private PawnAnimatorComponent _animator;
        private PawnCombatComponent _combat;
        private PawnEquipmentComponent _equipment;
        private PawnInventoryComponent _inventory;
        private PawnLocomotionComponent _locomotion;
        private PawnStatusComponent _status;

        public PawnAnimatorComponent Animator => _animator;
        public PawnCombatComponent Combat => _combat;
        public PawnEquipmentComponent Equipment => _equipment;
        public PawnInventoryComponent Inventory => _inventory;
        public PawnLocomotionComponent Locomotion => _locomotion;
        public PawnStatusComponent Status => _status;

        public override void Initialize()
        {
            base.Initialize();
            _animator = GetComponent<PawnAnimatorComponent>();
            _combat = GetComponent<PawnCombatComponent>();
            _equipment = GetComponent<PawnEquipmentComponent>();
            _inventory = GetComponent<PawnInventoryComponent>();
            _locomotion = GetComponent<PawnLocomotionComponent>();
            _status = GetComponent<PawnStatusComponent>();
            _animator.Initialize();
            _combat.Initialize();
            _equipment.Initialize();
            _inventory.Initialize();
            _locomotion.Initialize();
            _status.Initialize();
        }

        public override void Enable()
        {
            base.Enable();
            _animator.Enable();
            _combat.Enable();
            _equipment.Enable();
            _inventory.Enable();
            _locomotion.Enable();
            _status.Enable();
        }

        public override void Disable()
        {
            _animator.Disable();
            _combat.Disable();
            _equipment.Disable();
            _inventory.Disable();
            _locomotion.Disable();
            _status.Disable();
            base.Disable();
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            _animator.OnFixedUpdate();
            _combat.OnFixedUpdate();
            _equipment.OnFixedUpdate();
            _inventory.OnFixedUpdate();
            _locomotion.OnFixedUpdate();
            _status.OnFixedUpdate();
        }
    }
}