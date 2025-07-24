using _Game.Scripts._GameLogic.Data.Grid;
using UnityEngine;
using Zenject;

namespace _Game.Scripts._GameLogic.Pure
{
    public class GridItemGenerateProvider : BaseItemGenerateService
    {
        public GridItemGenerateProvider(GridItemDataContainer container, DiContainer diContainer) : base(container, diContainer) { }

        public void GenerateGridItems(Transform parent)
        {
            int gridSize = RuntimeGridCache.GetGridSize();

            for (var i = 0; i < gridSize; i++)
            {
                var tile = RuntimeGridCache.GetRandomAvailableTile();
                if (tile == null || !tile.IsEmpty()) continue;

                var prefab = GridItemDataContainer.GetBalancedRandomTypeFirstLevelGridObject();
                SpawnSingleObject(prefab, tile, parent, false);
            }
        }
        
        public void GenerateSingleGridItem(Transform parent)
        {
;           var tile = RuntimeGridCache.GetRandomAvailableTile();
            var prefab = GridItemDataContainer.GetBalancedRandomTypeFirstLevelGridObject();
            SpawnSingleObject(prefab, tile, parent, false);
        }
    }
}