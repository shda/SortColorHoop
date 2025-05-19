using System;
using SortColorHoop.Scripts.Bonuses;
using TriInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SortColorHoop.Scripts
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private int countSticks;
        
        [SerializeField] private LevelCountData levelCountData;
        [SerializeField] private AnimationMovingRings animationMovingRings;
        [SerializeField] private LevelLabel levelLabel;
        [SerializeField] private Board board;
        [SerializeField] private BonusHandler bonusHandler;
        
        private LevelStartConfigurator _levelStartConfigurator;
        private Stack _selectStack;

        private void Awake()
        {
            animationMovingRings.OnCompleteMoving = OnCheckEndLevel;
        }

        public void OnRestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void OnCheckEndLevel()
        {
            var countFillStacks = board.CountFillStacks();
            
            if (_levelStartConfigurator.CountNeedFillStacksToWin == countFillStacks)
            {
                levelCountData.LevelNumber.Value++;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
        
        private void StartLevel(int level)
        {
            levelCountData.LevelNumber.Value = level;
            
            _levelStartConfigurator = new LevelStartConfigurator();
            _levelStartConfigurator.CreateLevel(board, level);
            
            levelLabel.SetLevelNumber(level);
        }
        
        private void Start()
        {
            StartLevel(levelCountData.LevelNumber.Value);
            // CreateBoard();
        }

        [Button]
        private void CreateBoard()
        {
            board.CreateFullStacksAndFree(countSticks , 2);
        }
    }
}
