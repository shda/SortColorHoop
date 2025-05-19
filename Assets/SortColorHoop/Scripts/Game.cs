using SortColorHoop.Scripts.Bonuses;
using TriInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SortColorHoop.Scripts
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private int countSticks;
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
            if (board.IsCompleteGame())
            {
                AddRandomBonus();
                LevelCountData.LevelNumber.Value++;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        private void AddRandomBonus()
        {
            var bonusIndex = Random.Range(0, 3);
            switch (bonusIndex)
            {
                case 0:
                    BonusCountData.MagnetBonusCount.Value++;
                    break;
                case 1:
                    BonusCountData.BrushBonusCount.Value++;
                    break;
                case 2:
                    BonusCountData.BombBonusCount.Value++;
                    break;
                default:
                    break;
            }
        }
        
        private void StartLevel(int level)
        {
            LevelCountData.LevelNumber.Value = level;
            
            _levelStartConfigurator = new LevelStartConfigurator();
            _levelStartConfigurator.CreateLevel(board, level);
            
            levelLabel.SetLevelNumber(level);
        }
        
        private void Start()
        {
            StartLevel(LevelCountData.LevelNumber.Value);
        }

        [Button]
        private void CreateBoard()
        {
            board.CreateFullStacksAndFree(countSticks , 2);
        }
    }
}
