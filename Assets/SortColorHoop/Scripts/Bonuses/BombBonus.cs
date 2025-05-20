using SortColorHoop.Scripts.Rings;
using UniRx;
using UnityEngine;

namespace SortColorHoop.Scripts.Bonuses
{
    public class BombBonus : BonusBase
    {
        [SerializeField] private RingPool ringPool;
        [SerializeField] private ClickHandler clickHandler;

        private readonly CompositeDisposable _disposable = new();
        
        public override void StartBonus()
        {
            clickHandler.OnStackClick.Subscribe(OnClickStack).AddTo(_disposable);
        }

        private void OnClickStack(Stack stack)
        {
            IsInProcess = true;
            
            if (stack.IsAllRingsOneColor())
            {
                stack.RemoveAllRings(ringPool);
                BonusCountData.BombBonusCount.Value--;
                BonusCountData.Save();
                StopBonus();
            }
            else
            {
                StopBonus();
            }
        }

        public override void StopBonus()
        {
            IsInProcess = false;
            _disposable.Clear();
            OnComplete?.Invoke();
        }

        public override void BreakBonus()
        {
            IsInProcess = false;
            _disposable.Clear();
        }

        public override bool IsCanUsingBonus()
        {
            return BonusCountData.BombBonusCount.Value > 0;
        }
    }
}