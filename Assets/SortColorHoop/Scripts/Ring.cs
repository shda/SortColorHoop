using UnityEngine;

namespace SortColorHoop.Scripts
{
    public class Ring : MonoBehaviour
    {
        [SerializeField] private int sortBack = 100;
        [SerializeField] private int sortFront = 300;
        
        [SerializeField] private SpriteRenderer backRenderer;
        [SerializeField] private SpriteRenderer frontRenderer;
        
        [SerializeField] private RingColorType[] colorTypes;

        public int IndexColor { get; private set; }

        private int _setSortBack;
        private int _setSortFront;
        
        public void SetColor(int indexColor)
        {
            IndexColor = indexColor;
            frontRenderer.sprite = colorTypes[indexColor].Front;
            backRenderer.sprite = colorTypes[indexColor].Back;
            
            name = indexColor.ToString();
        }

        public void SetSortIndex(int sortIndex)
        {
            backRenderer.sortingOrder = sortBack + sortIndex;
            frontRenderer.sortingOrder = sortFront + sortIndex;

            _setSortBack = backRenderer.sortingOrder;
            _setSortFront = frontRenderer.sortingOrder;
        }

        public void SetTopOrderByMoving()
        {
            backRenderer.sortingOrder = 500;
            frontRenderer.sortingOrder = 500 + 1;
        }

        public void ReturnOrder()
        {
            backRenderer.sortingOrder = _setSortBack;
            frontRenderer.sortingOrder = _setSortFront;
        }
    }
}