using TMPro;
using UniRx;
using UnityEngine;

namespace SortColorHoop.Scripts
{
    public class BonusToggle : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI countBonuses;
        [SerializeField] private bool isBombBonus;
        [SerializeField] private bool isBrushBonus;
        [SerializeField] private bool isMagnetBonus;

        private void Awake()
        {
            if (isBombBonus)
                BonusCountData.BombBonusCount.Subscribe(OnChangeBonus).AddTo(this);
            
            if (isBrushBonus)
                BonusCountData.BrushBonusCount.Subscribe(OnChangeBonus).AddTo(this);
            
            if (isMagnetBonus)
                BonusCountData.MagnetBonusCount.Subscribe(OnChangeBonus).AddTo(this);
        }

        private void OnChangeBonus(int count)
        {
            countBonuses.text = count.ToString();
        }
    }
}