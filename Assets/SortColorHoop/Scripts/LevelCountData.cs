using System;
using UniRx;
using UnityEngine;

namespace SortColorHoop.Scripts
{
    public static class LevelCountData
    {
        public static IntReactiveProperty LevelNumber = new(1);

        public static void Save()
        {
            PlayerPrefs.SetInt("LevelNumber", LevelNumber.Value );
        }

        public static void Load()
        {
            LevelNumber.Value = PlayerPrefs.GetInt("LevelNumber" , 1);
        }
    }
}