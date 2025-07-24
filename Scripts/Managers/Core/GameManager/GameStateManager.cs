using UnityEngine;
using _Game.Scripts.General;
using _Game.Scripts.Managers.Core;

namespace _Game.Scripts.Managers
{
    public class GameStateManager : MonoBehaviour
    {
        #region Unity Lifecycle Methods
        
        private void Awake() => SubscribeEvents();

        private void OnDisable() => UnsubscribeEvents();

        #endregion

        #region Event Subscription Methods

        private void SubscribeEvents()
        {
            EventManager.LevelEvents.LevelLoaded += OnLevelLoaded;
            EventManager.LevelEvents.LevelStart += OnLevelStart;
            EventManager.LevelEvents.LevelSuccess += OnLevelSuccess;
            EventManager.LevelEvents.LevelFail += OnLevelFail;
        }

        private void UnsubscribeEvents()
        {
            EventManager.LevelEvents.LevelLoaded -= OnLevelLoaded;
            EventManager.LevelEvents.LevelStart -= OnLevelStart;
            EventManager.LevelEvents.LevelSuccess -= OnLevelSuccess;
            EventManager.LevelEvents.LevelFail -= OnLevelFail;
        }

        #endregion

        #region Event Handlers

        // Changes the game state when the level is loaded
        private void OnLevelLoaded(GameObject go)
        {
            GameStateData.ChangeGameState(GameState.LevelLoaded);
        }

        // Changes the game state when the level starts
        private void OnLevelStart()
        {
            GameStateData.ChangeGameState(GameState.LevelStart);
        }

        // Changes the game state when the level ends in success
        private void OnLevelSuccess()
        {
            GameStateData.ChangeGameState(GameState.LevelSuccess);
        }

        // Changes the game state when the level ends in failure
        private void OnLevelFail()
        {
            GameStateData.ChangeGameState(GameState.LevelFail);
        }

        #endregion
    }
}
