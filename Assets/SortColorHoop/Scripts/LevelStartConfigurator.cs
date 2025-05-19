using UnityEngine;

namespace SortColorHoop.Scripts
{
    public class LevelStartConfigurator
    {
        public int CountNeedFillStacksToWin;
        
        public void CreateLevel(Board board , int level)
        {
            switch (level)
            {
                case <= 10:
                    CreateStacks(board ,6);
                    break;
                case <= 20:
                    CreateStacks(board ,7);
                    break;
                case <= 30:
                    CreateStacks(board ,8);
                    break;
                case <= 40:
                    CreateStacks(board ,9);
                    break;
                default:
                    CreateStacks(board ,10);
                    break;
            }
        }

        private void CreateStacks(Board board, int countAllStacks)
        {
            int freeStacks = 2;
            
            CountNeedFillStacksToWin = countAllStacks - freeStacks;
            board.CreateFullStacksAndFree(countAllStacks , freeStacks);
        }
    }
}