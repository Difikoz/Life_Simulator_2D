using UnityEngine;

namespace WinterUniverse
{
    public class ArmorSlot : BasicComponent
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private ArmorItemConfig _config;

        public ArmorItemConfig Config => _config;

        public override void Initialize()
        {
            base.Initialize();
            ChangeConfig(null);
        }

        public void ChangeConfig(ArmorItemConfig config)
        {
            _config = config;
            if (_config != null)
            {
                _spriteRenderer.sprite = _config.VisualSprite;
            }
            else
            {
                _spriteRenderer.sprite = null;
            }
        }
    }
}