using UnityEngine;

namespace WinterUniverse
{
    public class ArmorSlot : BasicComponent
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public ArmorItemConfig Config { get; private set; }

        public override void Initialize()
        {
            base.Initialize();
            ChangeConfig(null);
        }

        public void ChangeConfig(ArmorItemConfig config)
        {
            Config = config;
            if (Config != null)
            {
                _spriteRenderer.sprite = Config.VisualSprite;
            }
            else
            {
                _spriteRenderer.sprite = null;
            }
        }
    }
}