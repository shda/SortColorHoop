using UnityEngine;

namespace SortColorHoop.Scripts.Rings
{
    [System.Serializable]
    public class RingSprites
    {
        [SerializeField] private Sprite front;
        [SerializeField] private Sprite back;

        public Sprite Front => front;
        public Sprite Back => back;
    }
}