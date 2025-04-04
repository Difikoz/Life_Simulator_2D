using UnityEngine;

namespace WinterUniverse
{
    public class PawnAnimatorComponent : PawnComponent
    {
        [SerializeField] private Transform _headPoint;
        [SerializeField] private Transform _bodyPoint;

        private Animator _animator;

        public Transform HeadPoint => _headPoint;
        public Transform BodyPoint => _bodyPoint;

        public override void Initialize()
        {
            base.Initialize();
            _animator = GetComponent<Animator>();
        }

        public void PlayAction(string name, float fadeTime = 0.1f, bool isPerfomingAction = true)
        {
            _pawn.Status.StateHolder.SetStateValue("Is Perfoming Action", isPerfomingAction);
            _animator.CrossFade(name, fadeTime);
        }

        public void SetFloat(string name, float value)
        {
            _animator.SetFloat(name, value);
        }

        public void SetBool(string name, bool value)
        {
            _animator.SetBool(name, value);
        }
    }
}