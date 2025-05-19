using UniRx;
using UnityEngine;

namespace SortColorHoop.Scripts
{
    public class BonusCountData : MonoBehaviour
    {
        public IntReactiveProperty BombBonusCount { get; } = new(1);
        public IntReactiveProperty BrushBonusCount { get; } = new(1);
        public IntReactiveProperty MagnetBonusCount { get; } = new(1);
        
        [SerializeField] private int startCount = 1;

        private void Awake()
        {
            BombBonusCount.Value = startCount;
            BrushBonusCount.Value = startCount;
            MagnetBonusCount.Value = startCount;
        }
    }
}