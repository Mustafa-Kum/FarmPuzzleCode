using _Game.Scripts.Helper.Extensions.System;
using _Game.Scripts.Managers.Core;
using DG.Tweening;

namespace _Game.Scripts.Helper.Services
{
    public static class GameEventService
    {
        #region PUBLIC METHODS
        
        public static void TriggerLevelSuccess()
        {
            EventManager.LevelEvents.LevelSuccess?.Invoke();
            EventManager.AudioEvents.AudioPlay?.Invoke(SoundType.LevelSuccess, false, false);
            EventManager.LevelEvents.AfterLevelSuccess?.Invoke();
        }

        public static void TriggerLevelFail()
        {
            EventManager.LevelEvents.LevelFail?.Invoke();
            TDebug.LogRed( " Level LevelFail Event Triggered");
        }
        
        public static void DelayedTriggerLevelFail(float delay)
        {
            DOVirtual.DelayedCall(delay, TriggerLevelFail);
        }
        
        public static void DelayedTriggerLevelSuccess(float delay)
        {
            DOVirtual.DelayedCall(delay, TriggerLevelSuccess);
        }
        
        #endregion
    }
}