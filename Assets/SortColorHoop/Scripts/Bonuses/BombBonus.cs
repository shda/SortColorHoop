using UniRx;
using UnityEngine;

namespace SortColorHoop.Scripts.Bonuses
{
    public class BombBonus : BonusBase
    {
        [SerializeField] private RingPool ringPool;
        [SerializeField] private ClickHandler clickHandler;

        private CompositeDisposable _disposable = new();
        
        public override void StartBonus()
        {
            clickHandler.OnStackClick.Subscribe(OnClickStack).AddTo(_disposable);
        }

        private void OnClickStack(Stack stack)
        {
            if (stack.IsAllRingsOneColor())
            {
                stack.RemoveAllRings(ringPool);
                bonusCountData.BombBonusCount.Value--;
                StopBonus();
            }
            else
            {
                StopBonus();
            }
        }

        public override void StopBonus()
        {
            _disposable.Clear();
            OnComplete?.Invoke();
        }

        public override bool IsCanUsingBonus()
        {
            return bonusCountData.BombBonusCount.Value > 0;
        }
    }
}