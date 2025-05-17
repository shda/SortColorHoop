using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TriInspector;
using UnityEngine;

namespace SortColorHoop.Scripts
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private Board board;
        [SerializeField] private Vector2Int size;
        [SerializeField] private ClickHandler clickHandler;

        private Stack _selectStack;
        
        private void Awake()
        {
            clickHandler.OnStackClick = OnStackClick;
        }

        private void OnStackClick(Stack stack)
        {
            if (!_selectStack)
            {
                if(stack.IsEmpty())
                    return;
                
                if(stack.IsAllRingsOneColor())
                    return;
                
                _selectStack = stack;
                _selectStack.MoveRingToUp();
            }
            else
            {
                if (_selectStack == stack)
                {
                    _selectStack.ReturnRingsToDefaultPosition();
                    _selectStack = null;
                }
                else
                {
                    if (IsCanMovingRingsToOtherStack(stack))
                    {
                        MovingRingsToOtherStack(_selectStack , stack , false).Forget();
                    }
                }
            }
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

        private void Start()
        {
            CreateBoard();
        }

        [Button]
        private void CreateBoard()
        {
            board.CreateFullStacksAndFree(size.x , 2);
        }
    }
}
