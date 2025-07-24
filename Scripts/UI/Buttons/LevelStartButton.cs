using _Game.Scripts.Managers.Core;

namespace _Game.Scripts.UI.Buttons
{
    public class LevelStartButton : ButtonBase
    {
        protected override void OnClicked()
        {
            EventMethod();
        }

        private void EventMethod()
        {
            EventManager.LevelEvents.LevelStart?.Invoke();
        }
    }   
}