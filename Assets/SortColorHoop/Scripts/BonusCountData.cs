using UniRx;
using UnityEngine;

namespace SortColorHoop.Scripts
{
    public static class BonusCountData
    {
        public static IntReactiveProperty BombBonusCount { get; } = new(1);
        public static IntReactiveProperty BrushBonusCount { get; } = new(1);
        public static IntReactiveProperty MagnetBonusCount { get; } = new(1);
    }
}