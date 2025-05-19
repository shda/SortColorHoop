using System.Collections.Generic;
using System.Linq;
using BlockBlast.Scripts.Common.Extensions;
using UnityEngine;

namespace SortColorHoop.Scripts
{
    public class Board : MonoBehaviour
    {
        [SerializeField] private RingPool ringPool;
        [SerializeField] private StackPool stackPool;

        [SerializeField] private Vector3 offsetStack;
        [SerializeField] private int maxXCount = 3;
        
        public List<Stack> stacks = new();
        
        public void CreateFullStacksAndFree(int countStacks, int freeStacks)
        {
            RemoveOldStacks();
            
            Vector3 startPoint = transform.position;
            
            for (int i = 0; i < countStacks; i++)
            {
                var newStack = stackPool.Get();
                newStack.transform.SetParent(transform);
                newStack.transform.localRotation = Quaternion.identity;
                newStack.transform.localScale = Vector3.one;
                
                stacks.Add(newStack);
                
                var x = offsetStack.x * (i % maxXCount);
                var y = offsetStack.y * (int) ((float)i / maxXCount);
                
                newStack.transform.localPosition = startPoint + new Vector3(x , y ,0);
            }
            
            CreateRings(countStacks, freeStacks);
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
                    
                    ring.SetColor(i);
                    rings.Add(ring);
                }
            }
            
            rings.Shuffle();
            
            for (var i = 0; i < countFullStacks; i++)
            {
                var stack = stacks[i];
                stack.InitByRings(rings.GetRange(i * 4, 4));
            }
        }

        private void RemoveOldStacks()
        {
            foreach (var stack in stacks)
            {
                stackPool.Release(stack);
            }
            
            stacks.Clear();
        }

        public int CountFillStacks()
        {
            return stacks.Count(stack => stack.IsAllRingsOneColor());
        }
    }
}