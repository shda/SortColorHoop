using System;
using UniRx;
using UnityEngine;

namespace SortColorHoop.Scripts
{
    public static class LevelCountData
    {
        public static IntReactiveProperty LevelNumber = new(1);
    }
}