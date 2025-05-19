using System;
using UnityEngine;

namespace SortColorHoop.Scripts.Bonuses
{
    public abstract class BonusBase : MonoBehaviour
    {
        public Action OnComplete;
        public bool IsInProcess { get; set; }

        public abstract void StartBonus();
        public abstract void StopBonus();
        public abstract void BreakBonus();
        public abstract bool IsCanUsingBonus();
    }
}