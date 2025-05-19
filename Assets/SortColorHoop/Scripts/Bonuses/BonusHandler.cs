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
            if (_activeBonus)
            {
                if (_activeBonus.IsInProcess)
                {
                    foreach (var toggle in togges)
                    {
                        toggle.SetIsOnWithoutNotify(false);
                    }
                    
                    return;
                }

                _activeBonus.BreakBonus();
            }

            if (!togges.Any(t => t.isOn))
            {
                foreach (var toggle in togges)
                {
                    toggle.SetIsOnWithoutNotify(false);
                }
                _activeBonus?.BreakBonus();
                _activeBonus = null;
                return;
            }
            
            if (!bonusBase.IsCanUsingBonus())
            {
                foreach (var toggle in togges)
                {
                    toggle.SetIsOnWithoutNotify(false);
                }
                return;
            }
            
            _activeBonus = bonusBase;
            _activeBonus.StartBonus();
            
            animationMovingRings.enabled = false;
            _activeBonus.OnComplete = () =>
            {
                animationMovingRings.enabled = true;
                foreach (var toggle in togges)
                {
                    toggle.SetIsOnWithoutNotify(false);
                }
            };
        }

        private void SwitchTogglesActive(Toggle[] toggles, bool isEnable)
        {
            foreach (var toggle in toggles)
            {
                toggle.enabled = isEnable;
            }
        }
    }
}