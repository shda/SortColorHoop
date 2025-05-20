using System.Collections.Generic;
using System.Linq;
using BlockBlast.Scripts.Common.Extensions;
using SortColorHoop.Scripts.Rings;
using UnityEngine;

namespace SortColorHoop.Scripts
{
    public class Board : MonoBehaviour
    {
        [SerializeField] private RingPool ringPool;
        [SerializeField] private StackPool stackPool;

        [SerializeField] private Vector3 offsetStack;
        [SerializeField] private float offestY = 1.0f;
        [SerializeField] private int maxXCount = 3;
        
        private readonly List<Stack> _stacks = new();
        
        public void CreateFullStacksAndFree(int countStacks, int freeStacks)
        {
            RemoveOldStacks();
        
            Vector3 startPoint = transform.position;
        
            for (int i = 0; i < countStacks; i++)
            {
                var newStack = stackPool.Get();
                InitializeStackTransform(newStack, startPoint, i, countStacks);
                _stacks.Add(newStack);
            }
        
            CreateRings(countStacks, freeStacks);
        }
        
        private void InitializeStackTransform(Stack stack, Vector3 startPoint, int index, int totalStacks)
        {
            stack.transform.SetParent(transform);
            stack.transform.localRotation = Quaternion.identity;
            stack.transform.localScale = Vector3.one;
        
            var x = offsetStack.x * (index % maxXCount);
            var y = offsetStack.y * (int)((float)index / maxXCount);
            y -= offestY * ((totalStacks - 1) / 3);
        
            stack.transform.localPosition = startPoint + new Vector3(x, y, 0);
        }

        private void CreateRings(int countStacks, int freeStacks)
        {
            var countFullStacks = countStacks - freeStacks;
            
            List<Ring> rings = new();
            for (var i = 0; i < countFullStacks; i++)
            {
                for (var x = 0; x < 4; x++)
                {
                    var ring = ringPool.Get();
                    ring.transform.SetParent(transform);
                    ring.transform.localRotation = Quaternion.identity;
                    ring.transform.localScale = Vector3.one;
                    ring.gameObject.SetActive(true);
                    
                    ring.SetColorSprites(i);
                    rings.Add(ring);
                }
            }
            
            rings.Shuffle();
            
            for (var i = 0; i < countFullStacks; i++)
            {
                var stack = _stacks[i];
                stack.InitByRings(rings.GetRange(i * 4, 4) , i * 100);
            }
        }

        private void RemoveOldStacks()
        {
            foreach (var stack in _stacks)
            {
                foreach (var ring in stack.Rings)
                {
                    ringPool.Release(ring);
                }
                
                stackPool.Release(stack);
            }
            
            _stacks.Clear();
        }

        public bool IsAllStacksOneColor()
        {
            return _stacks.All(stack => stack.IsAllRingsOneColor() || stack.IsEmpty());
        }
    }
}