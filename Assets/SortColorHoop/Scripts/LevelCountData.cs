using System;
using UniRx;
using UnityEngine;

namespace SortColorHoop.Scripts
{
    public class LevelCountData : MonoBehaviour
    {
        public IntReactiveProperty LevelNumber = new(1);
    }
}