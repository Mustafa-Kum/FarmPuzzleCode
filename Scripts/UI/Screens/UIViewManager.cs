using System.Collections.Generic;
using _Game.Scripts.Managers.Core;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Game.Scripts.UI.Screens
{
    public class UIViewManager : SerializedMonoBehaviour
    {
        #region Variables

        [SerializeField] private Dictionary<GameState, GameObject> _gameStateDictionary;

        #endregion

        #region Unity Callbacks

        private void OnEnable()
        {
            SubscribeToEvents();
        }

        private void OnDisable()
        {
            UnsubscribeFromEvents();
        }

        #endregion

        #region Event Subscriptions

        private void SubscribeToEvents()
        {
            EventManager.LevelEvents.GameStarted += HandleOnGameStart;
            EventManager.LevelEvents.LevelSuccess += HandleOnLevelSuccess;
            EventManager.LevelEvents.LevelStart += HandleOnLevelStart; 
            EventManager.LevelEvents.LoadLevel += HandleOnGameStart;
            EventManager.LevelEvents.LevelFail += HandleOnLevelFail;
        }

        private void UnsubscribeFromEvents()
        {
            EventManager.LevelEvents.GameStarted -= HandleOnGameStart;
            EventManager.LevelEvents.LevelSuccess -= HandleOnLevelSuccess;
            EventManager.LevelEvents.LevelStart -= HandleOnLevelStart;
            EventManager.LevelEvents.LoadLevel -= HandleOnGameStart;
            EventManager.LevelEvents.LevelFail -= HandleOnLevelFail;
        }

        #endregion

        #region Event Handlers

        private void HandleOnGameStart()
        {
            OpenPanel(GameState.GameStarted);
        }
        
        private void HandleOnLevelStart()
        {
            OpenPanel(GameState.LevelStart);
        }
        
        private void HandleOnLevelSuccess()
        {
            OpenPanel(GameState.LevelSuccess);
        }
        
        private void HandleOnLevelFail()
        {
            OpenPanel(GameState.LevelFail);
        }
        

        #endregion

        #region Panel Management

        private void OpenPanel(GameState state)
        {
            CloseAllPanels();
            if (_gameStateDictionary.ContainsKey(state))
            {
                _gameStateDictionary[state].SetActive(true);
            }
        }

        private void CloseAllPanels()
        {
            foreach (var panel in _gameStateDictionary.Values)
            {
                panel.SetActive(false);
            }
        }

        #endregion
    }
}
