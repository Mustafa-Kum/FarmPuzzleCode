using System.Collections.Generic;
using _Game.Scripts._GameLogic.Data.Grid;
using _Game.Scripts._GameLogic.Pure;
using _Game.Scripts.Helper.Extensions.System;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace _Game.Scripts._GameLogic.Grid
{
    public class GridItemInitializer : MonoBehaviour
    {
        [Inject] private GridItemDataContainer _gridItemDataContainer;
        [Inject] private DiContainer _diContainer;
        private GridItemGenerateProvider _gridItemGenerateProvider;

        private void Awake()
        {
            _gridItemGenerateProvider = new GridItemGenerateProvider(_gridItemDataContainer, _diContainer);
            
            InitializeGridItems();
        }

        [Button("Generate Grid Items", ButtonSizes.Medium)]
        [GUIColor(0.8f, 0.8f, 1)]
        private void InitializeGridItems()
        {
            var grid = RuntimeGridCache.GetGridCache();
            if (grid == null)
            {
                TDebug.LogError("Grid array is not set in RuntimeGridDataCache.");
                return;
            }

            _gridItemGenerateProvider.GenerateGridItems(transform);
        }
        
        [Button("Generate Single Grid Item", ButtonSizes.Medium)]
        [GUIColor(0.8f, 0.8f, 1)]
        private void InitializeSingleGridItem()
        {
            _gridItemGenerateProvider.GenerateSingleGridItem(transform);
        }
    }
}