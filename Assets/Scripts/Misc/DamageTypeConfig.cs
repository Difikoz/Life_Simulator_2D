using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Damage Type", menuName = "Winter Universe/Misc/New Damage Type")]
    public class DamageTypeConfig : BasicInfoConfig
    {
        [SerializeField] private StatConfig _resistanceStat;
        [SerializeField] private GameObject _hitVFX;
        [SerializeField] private AudioClip _hitClip;

        public StatConfig ResistanceStat => _resistanceStat;
        public GameObject HitVFX => _hitVFX;
        public AudioClip HitClip => _hitClip;
    }
}