using System.Collections.Generic;
using _Game.Scripts._GameLogic.Grid;
using UnityEngine;

namespace _Game.Scripts._GameLogic.Pure
{
    public static class RuntimeGridMatrixQueryProvider
    {
        private static readonly int[] Dx = { -1, 0, 1, -1, 1, -1, 0, 1 };
        private static readonly int[] Dy = { 1, 1, 1, 0, 0, -1, -1, -1 };

        public static List<Grid.Grid> GetConnectedNeighborsOfSameType(Grid.Grid grid, Grid.Grid[,] gridArray)
        {
            var connectedNeighbors = new List<Grid.Grid>();
            var queue = new Queue<Grid.Grid>();
            var visited = new HashSet<Grid.Grid>();

            if (gridArray == null)
            {
                Debug.LogError("Grid array is null in RuntimeGridCache");
                return connectedNeighbors;
            }
            
            queue.Enqueue(grid);
            visited.Add(grid);

            GridItemType myType = grid.GetGridType();
            int width = gridArray.GetLength(0);
            int height = gridArray.GetLength(1);

            while (queue.Count > 0)
            {
                Grid.Grid current = queue.Dequeue();
                connectedNeighbors.Add(current);

                foreach (var neighbor in GetNeighbors(current, gridArray, width, height))
                {
                    if (!visited.Contains(neighbor) && neighbor.GetGridType() == myType)
                    {
                        if(neighbor.IsEmpty()) continue;
                        queue.Enqueue(neighbor);
                        visited.Add(neighbor);
                    }
                }
            }

            return connectedNeighbors;
        }

        private static IEnumerable<Grid.Grid> GetNeighbors(Grid.Grid grid, Grid.Grid[,] gridArray, int width, int height)
        {
            Vector2Int pos = grid.GetPosition();
            int x = pos.x;
            int y = pos.y;

            for (int i = 0; i < Dx.Length; i++)
            {
                int newX = x + Dx[i];
                int newY = y + Dy[i];

                if (newX >= 0 && newX < width && newY >= 0 && newY < height)
                {
                    Grid.Grid neighbor = gridArray[newX, newY];
                    if (neighbor != null)
                    {
                        yield return neighbor;
                    }
                }
            }
        }
    }
}