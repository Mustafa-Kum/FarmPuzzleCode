using _Game.Scripts.Managers.Core;
using UnityEngine;

namespace _Game.Scripts.ScriptableObjects.RunTime
{
    public class ResetManager : MonoBehaviour
    {
        #region Inspector Variables

        public ResettableData resettableData;

        #endregion

        #region Event Methods

        private void OnEnable()
        {
            EventManager.LevelEvents.LevelStart += SaveInitialData;
            EventManager.LevelEvents.LevelFail += Reset;
            EventManager.LevelEvents.LevelSuccess += Reset;
        }

        private void OnDisable()
        {
            EventManager.LevelEvents.LevelStart -= SaveInitialData;
            EventManager.LevelEvents.LevelFail -= Reset;
            EventManager.LevelEvents.LevelSuccess -= Reset;
        }

        #endregion

        #region Private Methods

        private void SaveInitialData()
        {
            foreach (var resettable in resettableData.resettableData)
            {
                resettable.I.SaveInitialState();
            }
        }
        
        private void Reset()
        {
            resettableData.ResetAllData();
        }
        

        #endregion
    }
}