using _Game.Scripts._GameLogic.Data.Grid;
using _Game.Scripts._GameLogic.Grid;
using _Game.Scripts.Helper.Extensions.System;
using UnityEngine;
using Zenject;

namespace _Game.Scripts._GameLogic.Pure
{
    public abstract class BaseItemGenerateService
    {
        protected readonly GridItemDataContainer GridItemDataContainer;
        private readonly DiContainer _diContainer;
        
        protected BaseItemGenerateService(GridItemDataContainer container, DiContainer diContainer)
        {
            GridItemDataContainer = container;
            _diContainer = diContainer;
        }

        protected void SpawnSingleObject(GridItem prefab, _GameLogic.Grid.Grid tile, Transform parentTransform, bool verboseLog)
        {
            if (tile == null || !tile.IsEmpty())
            {
                TDebug.LogError("Tile is not empty or null.");
                return;
            }
            
            GameObject item = _diContainer.InstantiatePrefab(prefab, tile.transform.position, prefab.transform.rotation, parentTransform);
            GridItem gridItem = item.GetComponent<GridItem>();
            
            gridItem.SetGrid(tile);
            tile.SetItem(gridItem);
            tile.SetGridType(gridItem.GetItemType());
            LogSpawnedObject(gridItem, tile, verboseLog);
        }

        private void LogSpawnedObject(GridItem item, _GameLogic.Grid.Grid tile, bool verboseLog)
        {
            if (verboseLog)
            {
                TDebug.Log($"Spawned {item.name} at {tile.GetPosition()}");
            }
        }
    }
}