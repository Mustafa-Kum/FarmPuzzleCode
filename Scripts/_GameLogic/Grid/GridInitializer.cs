using _Game.Scripts._GameLogic.Data.Grid;
using _Game.Scripts._GameLogic.Pure;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace _Game.Scripts._GameLogic.Grid
{
    public class GridInitializer : MonoBehaviour
    {
        [Inject] private GridDataContainer _gridDataDataContainer;
        [TableMatrix(HorizontalTitle = "Grid Elements", SquareCells = true, ResizableColumns = false)]
        [ShowInInspector] [ReadOnly] private Grid[,] _gridArray;

        private void Awake() => InitializeGrid();

        private void InitializeGrid()
        {
            var gridSize = _gridDataDataContainer.GetGridSize();
            _gridArray = new Grid[gridSize.x, gridSize.y];
            ProduceGrid();
        }

        private void ProduceGrid()
        {
            var gridSize = _gridDataDataContainer.GetGridSize();
            for (var y = 0; y < gridSize.y; y++)
            for (var x = 0; x < gridSize.x; x++)
            {
                var gridElement = ProduceSingleGridElement();

                if (gridElement == null) continue;
                Grid tile = gridElement.GetComponent<Grid>();
                _gridArray[x, y] = tile;
                InitializeGridElement(gridElement, tile, x, y);
            }

            RuntimeGridCache.SetGridCache(_gridArray);
        }

        private GameObject ProduceSingleGridElement()
        {
            var gridPrefab = _gridDataDataContainer.GetGrid();
#if UNITY_EDITOR
            var grid = PrefabUtility.InstantiatePrefab(gridPrefab, transform) as GameObject;
#endif
#if !UNITY_EDITOR
            var gridElement = Instantiate(gridPrefab, transform);
#endif
            return grid;
        }

        private void InitializeGridElement(GameObject grid, Grid tile, int x, int y)
        {
            grid.transform.position =
                new Vector3(x * grid.transform.localScale.x, 0, y * grid.transform.localScale.z);

            if (tile != null) 
                tile.SetPosition(new Vector2Int(x, y));
        }

        private void OnDrawGizmos()
        {
            if (_gridArray == null) return;
            var gridSize = _gridDataDataContainer.GetGridSize();
            for (var y = 0; y < gridSize.y; y++)
            for (var x = 0; x < gridSize.x; x++)
            {
                var tile = _gridArray[x, y];
                if (tile == null) continue;
                var position = tile.transform.position;
                Gizmos.DrawWireCube(position, tile.transform.localScale);
                Gizmos.color = Color.red;
            }
        }
    }
}