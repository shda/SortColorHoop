using SortColorHoop.Scripts.Bonuses;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SortColorHoop.Scripts
{
    public class Game : MonoBehaviour
    {
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
        
        private void Start()
        {
            BonusCountData.Load();
            LevelCountData.Load();
            StartLevel(LevelCountData.LevelNumber.Value);
        }

        public void OnRestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void OnCheckEndLevel()
        {
            if (board.IsAllStacksOneColor())
            {
                LevelCountData.LevelNumber.Value++;

                AddRandomBonusInNeed();

                LevelCountData.Save();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        private void AddRandomBonusInNeed()
        {
            if (LevelCountData.LevelNumber.Value % 5 != 0)
                return;

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

            BonusCountData.Save();
        }

        private void StartLevel(int level)
        {
            _levelStartConfigurator = new LevelStartConfigurator();
            _levelStartConfigurator.CreateLevel(board, level);

            levelLabel.SetLevelNumber(level);
        }
    }
}