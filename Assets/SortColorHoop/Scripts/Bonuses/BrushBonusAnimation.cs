using System.Collections.Generic;
using BlockBlast.Scripts.Common.Extensions;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace SortColorHoop.Scripts.Bonuses
{
    public class BrushBonusAnimation : MonoBehaviour
    {
        [SerializeField] private float durationMoving = 0.2f;
        [SerializeField] private Transform[] movingPointFirst;
        [SerializeField] private Transform[] movingPointSecond;

        public async UniTask PlayAnimationMovingRings(Stack first, Stack second)
        {
            var listFirstRings =  await PlayMovingRingsToPoints(first , movingPointFirst);
            var listSeconds = await  PlayMovingRingsToPoints(second , movingPointSecond);
            
           // await UniTask.WhenAll(listFirstRings, listSeconds);
            
            var rings = new List<Ring>();
            rings.AddRange(listFirstRings);
            rings.AddRange(listSeconds);
            
            first.Rings.Clear();
            second.Rings.Clear();
            
            rings.Shuffle();

            var isFirst = true;
            foreach (var ring in rings)
            {
                if (isFirst)
                {
                    await first.AddAnimatedRing(ring);
                }
                else
                {
                    await second.AddAnimatedRing(ring);
                }
                
                isFirst = !isFirst;
            }
        }

        private async UniTask<List<Ring>> PlayMovingRingsToPoints(Stack stack , Transform[] movingPoint)
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

            return rings;
        }
    }
}