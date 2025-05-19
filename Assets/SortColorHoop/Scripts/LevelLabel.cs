using TMPro;
using UnityEngine;

namespace SortColorHoop.Scripts
{
    public class LevelLabel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI label;
        [SerializeField] private string prefixText;
        public void SetLevelNumber(int levelNumber)
        {
            label.text = prefixText + levelNumber;   
        }
    }
}