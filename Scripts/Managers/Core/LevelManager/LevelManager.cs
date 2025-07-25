using System;
using System.Collections;
using _Game.Scripts.Helper.Extensions.System;
using _Game.Scripts.ScriptableObjects.Predefined;
using _Game.Scripts.ScriptableObjects.Saveable;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace _Game.Scripts.Managers.Core
{
    public class LevelManager : MonoBehaviour
    {
        #region Inspector Variables

        [SerializeField] private LevelList_SO _levelListSO;
        [Inject] private PlayerSavableData _playerSavableData;
        [SerializeField] private Transform _levelHolder;
        [ReadOnly] [SerializeField] private GameObject _levelGO;
        [SerializeField] private bool isSpawnFromList;
        [Inject] private DiContainer _container;
        
        #endregion

        #region Unity Methods

        private void OnEnable() => SubscribeToEvents();

        private void OnDisable() => UnsubscribeFromEvents();

        #endregion

        #region Private Methods
        
        private void SubscribeToEvents()
        {
            EventManager.LevelEvents.LevelStart += LoadLevel;
            EventManager.LevelEvents.LevelSuccess += IncreaseLevelIndex;
        }
        
        private void UnsubscribeFromEvents()
        {
            EventManager.LevelEvents.LevelStart -= LoadLevel;
            EventManager.LevelEvents.LevelSuccess -= IncreaseLevelIndex;
        }
        
        private void LoadLevel()
        {
            DestroyExistingLevel();
            LoadSceneAndInstantiateLevel();
        }

        private static IEnumerator ReloadSceneAsync(int sceneIndex, LoadSceneMode loadSceneMode = LoadSceneMode.Single, bool unloadUnusedAssets = false)
        {
            AsyncOperation unloadAo = SceneManager.UnloadSceneAsync(sceneIndex);
            yield return unloadAo;

            if (unloadUnusedAssets)
            {
                AsyncOperation resourceUnloadAo = Resources.UnloadUnusedAssets();
                yield return resourceUnloadAo;
            }

            AsyncOperation loadAo = SceneManager.LoadSceneAsync(sceneIndex, loadSceneMode);
            yield return loadAo; 
        }

        private void LoadSceneAndInstantiateLevel()
        {
            InstantiateNewLevel();
            NotifyLevelLoaded();
        }
        
        private void InstantiateNewLevel()
        {
            var level = FetchLevelFromLevelsList();
            var levelPrefab = level;
            levelPrefab ??= TryGetLevelFromHolder();

            if (levelPrefab != null)
            {
                InstantiatePrefab(levelPrefab);
            }
            
            if (_levelGO == null)
                Debug.LogError("Both TryGetLevelFromSoWithIndexedLevel and TryGetLevelFromHolder returned null.");
        }
    
        private void InstantiatePrefab(GameObject prefab)
        {
            _levelGO = _container.InstantiatePrefab(prefab, _levelHolder.position, prefab.transform.rotation, _levelHolder);
        }
        
        private void NotifyLevelLoaded()
        {
            var e = new Exception("Level is not loaded");
            EventManager.LevelEvents.LevelLoaded?.Invoke(_levelGO ? _levelGO : TryGetLevel());

            TDebug.LogGreen(_levelGO + " is loaded");
        }
        
        [Button]
        public void ReloadScene()
        {
            DOTween.KillAll();
            DOTween.Clear();
            StartCoroutine(ReloadSceneAsync(SceneManager.GetActiveScene().buildIndex));
        }

        private void DestroyExistingLevel()
        {
            if (_levelGO)
                Destroy(_levelGO);
        }

        private void IncreaseLevelIndex()
        {
            _playerSavableData.LevelIndex++;
        }

        private GameObject TryGetLevelFromHolder()
        {
            var level = _levelHolder.childCount > 0 ? _levelHolder.GetChild(0).gameObject : null;

            _levelGO = level;

            return level;
        }

        private GameObject FetchLevelFromLevelsList()
        {
            Level_SO level;

            if (_playerSavableData.LevelIndex >= _levelListSO.AllLevels.Count)
            {
                int randomIndex = UnityEngine.Random.Range(0, _levelListSO.AllLevels.Count);
                level = _levelListSO.GetLevelWithIndex(randomIndex);
                _levelGO = level.LevelPrefab;
            }
            else
            {
                level = _levelListSO.GetLevelWithIndex(_playerSavableData.LevelIndex);
            }

            _levelGO = level.LevelPrefab;

            return _levelGO;
        }

        private GameObject TryGetLevel()
        {
            var level = isSpawnFromList ? FetchLevelFromLevelsList() : TryGetLevelFromHolder();

            _levelGO = level;

            return level;
        }

        #endregion
    }
}
