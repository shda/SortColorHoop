using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace SortColorHoop.Scripts
{
    public class AnimationMovingRings : MonoBehaviour
    {
        [SerializeField] private Board board;
        [SerializeField] private ClickHandler clickHandler;

        public Action OnCompleteMoving;
        
        private Stack _selectStack;
        private bool _isPlayingAnimation;
        
        private void Awake()
        {
            clickHandler.OnStackClick.Subscribe(stack => OnStackClick(stack)).AddTo(this);
        }
        
        private async UniTask OnStackClick(Stack stack)
        {
            if(!enabled)
                return;
            
            if(_isPlayingAnimation)
                return;
            
            _isPlayingAnimation = true;
            
            if (!_selectStack)
            {
                if(stack.IsEmpty())
                    return;
                
                if(stack.IsAllRingsOneColor())
                    return;
                
                _selectStack = stack;
                await _selectStack.MoveRingToUp();
            }
            else
            {
                if (_selectStack == stack)
                {
                    await _selectStack.ReturnRingsToDefaultPosition();
                    _selectStack = null;
                }
                else
                {
                    if (IsCanMovingRingsToOtherStack(stack))
                    {
                        await MovingRingsToOtherStack(_selectStack , stack);
                    }
                }
            }

            _isPlayingAnimation = false;
            
            OnCompleteMoving?.Invoke();
        }
        
        private bool IsCanMovingRingsToOtherStack(Stack destinationRing)
        {
            if(destinationRing.IsFull())
                return false;
            
            if(destinationRing.IsAllRingsOneColor())
                return false;

            var isCanMoving = false;
            
            var topDisRing = destinationRing.GetTopRing();
            var topFromRing = _selectStack.GetTopRing();

            if (!topFromRing)
            {
                return false;
            }

            if (!topDisRing)
            {
                isCanMoving = true;
            }
            else if(topFromRing.IndexColor == topDisRing.IndexColor)
            {
                isCanMoving = true;
            }

            return isCanMoving;
        }

        private async UniTask MovingRingsToOtherStack(Stack from , Stack detonation , bool movingUp = false)
        {
            var last = from.Rings.Last();
            if (movingUp)
            {
                await from.MoveRingToUp(last);
            }
            
            from.Rings.Remove(last);
            await detonation.AddAnimatedRing(last);

            if (IsCanMovingRingsToOtherStack(detonation))
            {
                await MovingRingsToOtherStack(from , detonation , true);
            }

            _selectStack = null;
        }
    }
}