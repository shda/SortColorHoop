using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SortColorHoop.Scripts.Bonuses
{
    public class BonusHandler : MonoBehaviour
    {
        [SerializeField] private AnimationMovingRings animationMovingRings;
        [SerializeField] private Board board;
        
        [SerializeField] private Toggle[] togges;
        
        private BonusBase _activeBonus;
        
       public void OnPressBonus(BonusBase bonusBase)
       {
           void ResetToggles()
           {
               foreach (var toggle in togges)
               {
                   toggle.SetIsOnWithoutNotify(false);
               }
           }
       
           if (_activeBonus)
           {
               if (_activeBonus.IsInProcess)
               {
                   ResetToggles();
                   return;
               }
       
               _activeBonus.BreakBonus();
           }
       
           if (!togges.Any(t => t.isOn))
           {
               ResetToggles();
               _activeBonus?.BreakBonus();
               _activeBonus = null;
               return;
           }
       
           if (!bonusBase.IsCanUsingBonus())
           {
               ResetToggles();
               return;
           }
       
           _activeBonus = bonusBase;
           _activeBonus.StartBonus();
       
           animationMovingRings.enabled = false;
           _activeBonus.OnComplete = () =>
           {
               BonusCountData.Save();
               animationMovingRings.enabled = true;
               ResetToggles();
           };
       }
    }
}