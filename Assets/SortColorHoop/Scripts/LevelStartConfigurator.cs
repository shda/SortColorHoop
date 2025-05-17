using UnityEngine;

namespace SortColorHoop.Scripts
{
    public class LevelStartConfigurator : MonoBehaviour
    {
        [SerializeField] private Board board;
            
        public void CreateLevel(int level)
        {
            switch (level)
            {
                case <= 10:
                    CreateStacks(6);
                    break;
                case <= 20:
                    CreateStacks(7);
                    break;
                case <= 30:
                    CreateStacks(8);
                    break;
                case <= 40:
                    CreateStacks(9);
                    break;
                default:
                    CreateStacks(10);
                    break;
            }
        }

        private void CreateStacks(int countStacks)
        {
            board.CreateFullStacksAndFree(countStacks , 2);
        }
    }
}