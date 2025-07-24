using _Game.Scripts.Managers.Core;

namespace _Game.Scripts.UI.Buttons
{
    public class RetryLevelButton : ButtonBase
    {
        #region UNITY METHODS

        private void OnEnable()
        {
            EventManager.LevelEvents.LevelStart += OnLevelStart;
        }

        private void OnDisable()
        {
            EventManager.LevelEvents.LevelStart -= OnLevelStart;
        }

        #endregion
        
        #region INHERITED METHODS

        protected override void OnClicked() {
        }
        
        #endregion

        #region PRIVATE METHODS
        
        private void OnLevelStart()
        {
            targetButton.interactable = true;
        }

        #endregion

    }
}
