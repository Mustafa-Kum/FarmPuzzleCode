using UnityEngine;

namespace _Game.Scripts.Managers.Core
{
    public class GameStartHandler : MonoBehaviour
    {
        #region UNITY METHODS

        private void Awake()
        {
            InitializeGame();
        }
        
        #endregion


        private void InitializeGame()
        {
            EventManager.LevelEvents.GameStarted?.Invoke();
        }
        
    }
}