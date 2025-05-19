using UniRx;
using UnityEngine;

namespace SortColorHoop.Scripts
{
    public static class BonusCountData
    {
        public static IntReactiveProperty BombBonusCount { get; } = new(1);
        public static IntReactiveProperty BrushBonusCount { get; } = new(1);
        public static IntReactiveProperty MagnetBonusCount { get; } = new(1);

        public static void Save()
        {
            PlayerPrefs.SetInt(nameof(BombBonusCount), BombBonusCount.Value);
            PlayerPrefs.SetInt(nameof(BrushBonusCount), BrushBonusCount.Value);
            PlayerPrefs.SetInt(nameof(MagnetBonusCount), MagnetBonusCount.Value);
            
            PlayerPrefs.Save();
        }

        public static void Load()
        {
            BombBonusCount.Value = PlayerPrefs.GetInt(nameof(BombBonusCount), 1);
            BrushBonusCount.Value = PlayerPrefs.GetInt(nameof(BrushBonusCount), 1);
            MagnetBonusCount.Value = PlayerPrefs.GetInt(nameof(MagnetBonusCount), 1);
        }
    }
}