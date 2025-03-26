using Lean.Pool;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace WinterUniverse
{
    public class FactionsStatusPageUI : StatusPageUI
    {
        [Header("Info Bar")]
        [SerializeField] private Image _infoBarIconImage;
        [SerializeField] private TMP_Text _infoBarNameText;
        [SerializeField] private TMP_Text _infoBarDescriptionText;
        [SerializeField] private Sprite _emptySprite;
        [Header("Factions Bar")]
        [SerializeField] private Transform _factionBarContentRoot;
        [SerializeField] private GameObject _factionBarSlotPrefab;
        [Header("Relationships Bar")]
        [SerializeField] private Transform _relationshipBarEnemyContentRoot;
        [SerializeField] private Transform _relationshipBarNeutralContentRoot;
        [SerializeField] private Transform _relationshipBarAllyContentRoot;
        [SerializeField] private GameObject _relationshipBarSlotPrefab;

        public override void Initialize()
        {
            base.Initialize();
            foreach (FactionConfig config in GameManager.StaticInstance.ConfigsManager.Factions)
            {
                LeanPool.Spawn(_factionBarSlotPrefab, _factionBarContentRoot).GetComponent<FactionSlotUI>().Initialize(config);
            }
        }

        public void ShowFullInformation(FactionConfig config)
        {
            if (config != null)
            {
                _infoBarIconImage.sprite = config.Icon.LoadAsset();
                _infoBarNameText.text = config.DisplayName.GetLocalizedString();
                _infoBarDescriptionText.text = config.Description.GetLocalizedString();
                FillRelationshipBar(config);
            }
            else
            {
                _infoBarIconImage.sprite = _emptySprite;
                _infoBarNameText.text = string.Empty;
                _infoBarDescriptionText.text = string.Empty;
                ClearRelationshipBar();
            }
        }

        private void FillRelationshipBar(FactionConfig config)
        {
            ClearRelationshipBar();
            foreach (FactionRelationship fr in config.Relationships)
            {
                switch (fr.State)
                {
                    case RelationshipState.Enemy:
                        LeanPool.Spawn(_relationshipBarSlotPrefab, _relationshipBarEnemyContentRoot).GetComponent<BasicIconSlotUI>().SetIcon(fr.Faction.Icon.LoadAsset());
                        break;
                    case RelationshipState.Neutral:
                        LeanPool.Spawn(_relationshipBarSlotPrefab, _relationshipBarNeutralContentRoot).GetComponent<BasicIconSlotUI>().SetIcon(fr.Faction.Icon.LoadAsset());
                        break;
                    case RelationshipState.Ally:
                        LeanPool.Spawn(_relationshipBarSlotPrefab, _relationshipBarAllyContentRoot).GetComponent<BasicIconSlotUI>().SetIcon(fr.Faction.Icon.LoadAsset());
                        break;
                }
            }
        }

        private void ClearRelationshipBar()
        {
            while (_relationshipBarEnemyContentRoot.childCount > 0)
            {
                LeanPool.Despawn(_relationshipBarEnemyContentRoot.GetChild(0).gameObject);
            }
            while (_relationshipBarNeutralContentRoot.childCount > 0)
            {
                LeanPool.Despawn(_relationshipBarNeutralContentRoot.GetChild(0).gameObject);
            }
            while (_relationshipBarAllyContentRoot.childCount > 0)
            {
                LeanPool.Despawn(_relationshipBarAllyContentRoot.GetChild(0).gameObject);
            }
        }
    }
}