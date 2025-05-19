using System;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SortColorHoop.Scripts
{
    public class ClickHandler : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;

        public Subject<Stack> OnStackClick { get; set; } = new();
        
        private void Update()
        { 
            if (Input.GetMouseButtonUp(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                Vector2 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                var hitCollider = Physics2D.OverlapPoint(mouseWorldPos);
                if (hitCollider)
                {
                    var stack = hitCollider.GetComponent<Stack>();
                    OnStackClick?.OnNext(stack);
                }
            }
        }
    }
}