using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace SortColorHoop.Scripts.Bonuses
{
    public class MagnetBonus : BonusBase
    {
        [SerializeField] private ClickHandler clickHandler;
        [SerializeField] private MagnetBonusAnimation magnetBonusAnimation;
        
        private readonly CompositeDisposable _disposable = new();
        public override void StartBonus()
        {
            clickHandler.OnStackClick.Subscribe(OnClickStack).AddTo(_disposable);
        }

        private void OnClickStack(Stack stack)
        {
            if (stack.Rings.Count > 0 && !stack.IsAllRingsOneColor())
            {
                _disposable.Clear();
                PlayAnimation(stack).Forget();
            }
            else
            {
                StopBonus();
            }
        }

        private async UniTaskVoid PlayAnimation(Stack stack)
        {
            IsInProcess = true;
            BonusCountData.MagnetBonusCount.Value--;
            await magnetBonusAnimation.PlayAnimationMovingRings(stack);
            StopBonus();
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
            return BonusCountData.MagnetBonusCount.Value > 0;
        }
    }
}