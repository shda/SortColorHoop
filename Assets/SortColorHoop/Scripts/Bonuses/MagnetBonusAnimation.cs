using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using SortColorHoop.Scripts.Rings;
using UnityEngine;

namespace SortColorHoop.Scripts.Bonuses
{
    public class MagnetBonusAnimation : MonoBehaviour
    {
        [SerializeField] private float durationMoving = 0.2f;
        [SerializeField] private Transform[] movingPoint;

        public async UniTask PlayAnimationMovingRings(Stack stack)
        {
            var rings = new List<Ring>();

            var indexRing = 0;
            var ring = await stack.MoveRingToUp();
            while (ring)
            {
                await ring.transform.DOMove(movingPoint[indexRing].position, 0.2f);
                stack.Rings.Remove(ring);
                rings.Add(ring);
                indexRing++;
                
                ring = await stack.MoveRingToUp();
            }
            
            //rings.Reverse();

            foreach (var ringRev in rings)
            { 
                await stack.AddAnimatedRing(ringRev);
            }
        }
    }
}