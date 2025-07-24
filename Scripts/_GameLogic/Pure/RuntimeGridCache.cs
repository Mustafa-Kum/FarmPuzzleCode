using System.Collections.Generic;
using System.Linq;
using Handler.Extensions;
using UnityEngine;

namespace _Game.Scripts._GameLogic.Pure
{
#if UNITY_EDITOR
    using UnityEditor;
    [InitializeOnLoad]
#endif
    public static class RuntimeGridCache
    {
#if UNITY_EDITOR
        static RuntimeGridCache()
        {
            EditorApplication.playModeStateChanged += state =>
            {
                if (state == PlayModeStateChange.ExitingPlayMode)
                {
                    ClearCache();
                }
            };
        }
#endif
        
        private static Grid.Grid[,] _gridCache;
        
        public static void SetGridCache(Grid.Grid[,] gridCache)
        {
            _gridCache = gridCache;
        }
        
        public static Grid.Grid[,] GetGridCache()
        {
            return _gridCache;
        }

        public static int GetGridSize()
        {
            return _gridCache.GetLength(0) * _gridCache.GetLength(1);
        }

        private static void ClearCache()
        {
            _gridCache = null;
        }

        private static List<Grid.Grid> GetAvailableTiles()
        {
            return _gridCache.Cast<Grid.Grid>().Where(grid => grid.IsEmpty()).ToList();
        }

        public static Grid.Grid GetRandomAvailableTile()
        {
            List<Grid.Grid> availableTiles = RuntimeGridCache.GetAvailableTiles();
            if (availableTiles.Count == 0)
            {
                Debug.LogWarning("No available tiles to spawn object.");
                return null;
            }
            
            Grid.Grid tile = availableTiles.GetRandomElement();
            
            return tile;
        }

        public static List<Vector2Int> GetAvailablePositions(Grid.Grid[,] gridArray)
        {
            var positions = new List<Vector2Int>();
            for (var x = 0; x < gridArray.GetLength(0); x++)
            for (var y = 0; y < gridArray.GetLength(1); y++)
                if (gridArray[x, y].IsEmpty())
                    positions.Add(new Vector2Int(x, y));
            return positions;
        }

        public static Grid.Grid GetTileAtPosition(Vector2Int randomPosition)
        {
            return _gridCache[randomPosition.x, randomPosition.y];
        }
    }
}