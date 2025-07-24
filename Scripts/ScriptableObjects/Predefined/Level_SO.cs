using System;
using UnityEngine;

namespace _Game.Scripts.ScriptableObjects.Predefined
{
    [CreateAssetMenu(fileName = nameof(Level_SO), menuName = "Handler Project/Core/LevelSO", order = 2)]
    public class Level_SO : ScriptableObject
    {
        #region Public Variables

        [SerializeField] private GameObject _levelPrefab;
        public bool isTutorialLevel;
        
        #endregion
        
        #region Properties

        public GameObject LevelPrefab => _levelPrefab;

        #endregion
    }
}
