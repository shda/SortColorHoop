using UnityEngine;

namespace SortColorHoop.Scripts.Rings
{
    public class Ring : MonoBehaviour
    {
        [SerializeField] private int sortBack = 100;
        [SerializeField] private int sortFront = 300;
        
        [SerializeField] private SpriteRenderer backRingRenderer;
        [SerializeField] private SpriteRenderer frontRingRenderer;
        
        [SerializeField] private RingSprites[] ringSprites;

        public int IndexColor { get; private set; }

        private int _setSortingBack;
        private int _setSortingFront;
        
        public void SetColorSprites(int indexColor)
        {
            IndexColor = indexColor;
            frontRingRenderer.sprite = ringSprites[indexColor].Front;
            backRingRenderer.sprite = ringSprites[indexColor].Back;
            
            name = indexColor.ToString();
        }

        public void SetSortIndex(int sortIndex)
        {
            backRingRenderer.sortingOrder = sortBack + sortIndex;
            frontRingRenderer.sortingOrder = sortFront + sortIndex;

            _setSortingBack = backRingRenderer.sortingOrder;
            _setSortingFront = frontRingRenderer.sortingOrder;
        }

        public void SetTopSortOrderByMoving()
        {
            backRingRenderer.sortingOrder = 1000;
            frontRingRenderer.sortingOrder = 1000 + 1;
        }

        public void ReturnSaveSortOrder()
        {
            backRingRenderer.sortingOrder = _setSortingBack;
            frontRingRenderer.sortingOrder = _setSortingFront;
        }
    }
}