using UnityEngine;

namespace WinterUniverse
{
    public class PawnAnimatorComponent : PawnComponent
    {
        private Animator _animator;

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