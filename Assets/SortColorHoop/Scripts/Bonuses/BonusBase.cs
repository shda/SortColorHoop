using System;
using UnityEngine;

namespace SortColorHoop.Scripts.Bonuses
{
    public abstract class BonusBase : MonoBehaviour
    {
        [SerializeField] protected BonusCountData bonusCountData;
        
        public Action OnComplete;
        
        public abstract void StartBonus();
        public abstract void StopBonus();
        public abstract bool IsCanUsingBonus();
    }
}