using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SortColorHoop.Scripts
{
    public class ClickHandler : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;

        public Action<Stack> OnStackClick;
        
        private void Update()
        { 
            if (Input.GetMouseButtonUp(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                Vector2 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                var hitCollider = Physics2D.OverlapPoint(mouseWorldPos);
                if (hitCollider)
                {
                    var stack = hitCollider.GetComponent<Stack>();
                    OnStackClick?.Invoke(stack);
                }
            }
        }
    }
}