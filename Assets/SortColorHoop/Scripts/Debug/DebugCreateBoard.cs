using TriInspector;
using UnityEngine;

namespace SortColorHoop.Scripts
{
    public class DebugCreateBoard : MonoBehaviour
    {
        [SerializeField] private int countSticks;
        [SerializeField] private Board board;
        
        [Button]
        private void CreateBoard()
        {
            board.CreateFullStacksAndFree(countSticks , 2);
        }
    }
}