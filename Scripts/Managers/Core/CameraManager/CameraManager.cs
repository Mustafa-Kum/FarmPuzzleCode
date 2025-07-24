using System;
using System.Collections.Generic;
using _Game.Scripts.Managers.Core;
using Cinemachine;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Game.Scripts.Managers
{
    public class CameraManager : SerializedMonoBehaviour
    {
        #region Public Variables

        public Dictionary<GameState, CinemachineVirtualCamera> _virtualCameraDictionary = new();
        
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
            EventManager.LevelEvents.LevelStart += HandleOnLevelStart;
            EventManager.LevelEvents.LevelLoaded += HandleOnLevelLoaded;
        }

        private void UnsubscribeFromEvents()
        {
            EventManager.LevelEvents.GameStarted -= HandleOnGameStart;
            EventManager.LevelEvents.LevelStart -= HandleOnLevelStart;
            EventManager.LevelEvents.LevelLoaded -= HandleOnLevelLoaded;
        }

        #endregion

        #region Event Handlers

        private void HandleOnGameStart() => SwitchVirtualCamera(GameState.LevelLoaded);

        private void HandleOnLevelStart() => SwitchVirtualCamera(GameState.LevelStart);

        private void HandleOnLevelLoaded(GameObject arg0) => HandleOnGameStart();
        
        
        #endregion

        #region Camera Management

        private void SwitchVirtualCamera(GameState state)
        {
            DisableAllCameras();

            if (_virtualCameraDictionary.ContainsKey(state)) _virtualCameraDictionary[state].gameObject.SetActive(true);
        }

        private void DisableAllCameras()
        {
            foreach (var cam in _virtualCameraDictionary.Values) cam.gameObject.SetActive(false);
        }

        #endregion
    }
}