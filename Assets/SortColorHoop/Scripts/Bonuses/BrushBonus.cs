using UniRx;
using UnityEngine;

namespace SortColorHoop.Scripts.Bonuses
{
    public class BrushBonus : BonusBase
    {
        [SerializeField] private ClickHandler clickHandler;
        [SerializeField] private BrushBonusAnimation brushBonusAnimation;
        
        private readonly CompositeDisposable _disposable = new();
        
        private Stack _firstStack;
        
        public override void StartBonus()
        {
            clickHandler.OnStackClick.Subscribe(OnClickStack).AddTo(_disposable);
        }
        
        private async void OnClickStack(Stack stack)
        {
            if(stack.IsAllRingsOneColor() || stack.IsEmpty())
                return;
            
            if (!_firstStack)
            {
                _firstStack = stack;
                await _firstStack.MoveRingToUp();
                return;
            }

            if (_firstStack == stack)
            {
                _firstStack.ReturnRingsToDefaultPositionAsync();
                _firstStack = null;
                return;
            }

            IsInProcess = true;
            _disposable.Clear();
            
            BonusCountData.BrushBonusCount.Value--;
            await brushBonusAnimation.PlayAnimationMovingRings(_firstStack, stack);
            StopBonus();
        }

        public override void StopBonus()
        {
            _firstStack = null;
            IsInProcess = false;
            OnComplete?.Invoke();
        }

        public override void BreakBonus()
        {
            IsInProcess = false;
            _disposable.Clear();
        }

        public override bool IsCanUsingBonus()
        {
            return BonusCountData.BrushBonusCount.Value > 0;
        }
    }
}