using System.Collections.Generic;
using System.Linq;
using BlockBlast.Scripts.Common.Extensions;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace SortColorHoop.Scripts
{
    public class Stack : MonoBehaviour
    {
        [SerializeField] private Transform topPointBySelect;
        [SerializeField] private Transform[] ringsPoints;
        public List<Ring> Rings { get; private set; } = new();
        public bool IsSelected { get; private set; }

        private readonly float durationMoving = 0.2f;
        private int _maxRings = 4;

        public void InitByRings(IEnumerable<Ring> rings)
        {
            Rings = rings.ToList();

            for (var i = 0; i < Rings.Count; i++)
            {
                Rings[i].transform.SetParent(ringsPoints[i]);
                Rings[i].transform.localPosition = Vector3.zero;
                Rings[i].SetSortIndex(i);
            }
        }

        public async UniTask MoveRingToUp()
        {
            if (Rings.Count > 0)
            {
                IsSelected = true;

                var ring = Rings.LastOrDefault();
                await ring.transform.DOMove(topPointBySelect.position, durationMoving);
                ring.SetTopOrderByMoving();
            }
        }

        public async UniTask MoveRingToUp(Ring ring)
        {
            await ring.transform.DOMove(topPointBySelect.position, durationMoving);
            ring.SetTopOrderByMoving();
        }

        public async UniTask ReturnRingsToDefaultPosition()
        {
            foreach (var ring in Rings)
            {
                ring.transform.DOLocalMove(Vector3.zero, durationMoving);
                ring.ReturnOrder();
            }
        }

        public bool IsFull()
        {
            return Rings.Count >= _maxRings;
        }

        public bool IsAllRingsOneColor()
        {
            if (!IsFull() || Rings.Count < 1)
                return false;
            
            var firstColor = Rings.First().IndexColor;
            return Rings.All(ring => firstColor == ring.IndexColor);
        }

        public bool IsEmpty()
        {
            return Rings.EmptyOnNull();
        }

        public int GetFreeSpaces()
        {
            return Rings.Count - _maxRings;
        }

        public Ring GetTopRing()
        {
            if (Rings.Any())
            {
                return Rings.Last();
            }

            return null;
        }

        public async UniTask AddAnimatedRing(Ring ring)
        {
            await ring.transform.DOMove(topPointBySelect.transform.position, durationMoving);
            
            var pointTarget = ringsPoints[Rings.Count];
            ring.transform.parent = pointTarget.transform;
            ring.SetSortIndex(Rings.Count);
            
            await ring.transform.DOMove(pointTarget.transform.position, durationMoving);
            Rings.Add(ring);
        }
    }
}