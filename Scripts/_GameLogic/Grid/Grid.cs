using System;
using System.Collections.Generic;
using System.Linq;
using _Game.Scripts._GameLogic.Pure;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Game.Scripts._GameLogic.Grid
{
    public class Grid : Slot<GridItem>, IGridActions
    {
        [ReadOnly] [ShowInInspector] private List<Grid> _connectedNeighbors;
        private HashSet<Grid> _highlightNeighbors = new();

        public event Action<Grid> OnGridChanged;
        private void OnDestroy() => UnsubscribeFromNeighbors();

        protected override void OnInitialize()
        {
        }

        private void FindAllConnectedNeighbors()
        {
            UnsubscribeFromNeighbors();

            Grid[,] gridArray = RuntimeGridCache.GetGridCache();

            if (gridArray == null)
            {
                Debug.LogError("Grid array is null in RuntimeGridCache");
                return;
            }

            _connectedNeighbors = RuntimeGridMatrixQueryProvider.GetConnectedNeighborsOfSameType(this, gridArray);

            SubscribeToNeighbors();
        }

        private void SubscribeToNeighbors()
        {
            foreach (var neighbor in _connectedNeighbors.Where(neighbor => neighbor != this))
                neighbor.OnGridChanged += OnNeighborChanged;
        }
        
        private void ClearNeighbors()
        {
            _connectedNeighbors?.Clear();
            _highlightNeighbors.Clear();
        }

        private void UnsubscribeFromNeighbors()
        {
            if (_connectedNeighbors == null) return;

            foreach (var neighbor in _connectedNeighbors.Where(neighbor => neighbor != null && neighbor != this))
                neighbor.OnGridChanged -= OnNeighborChanged;
            
            ClearNeighbors();
        }

        private void OnNeighborChanged(Grid neighbor)
        {
            Debug.Log($"Neighbor at position {neighbor.GetPosition()} has changed.");
        }

        protected override void OnItemSet()
        {
            Debug.Log("Grid Item Set: " + gameObject.name);
            OnGridChanged?.Invoke(this);
        }

        protected override void OnItemRemoved()
        {
            Debug.Log("Grid Item Removed: " + gameObject.name);
            OnGridChanged?.Invoke(this);
        }

        public void OnSelect()
        {
            FindAllConnectedNeighbors();
            var items = _connectedNeighbors.Select(grid => grid.GetItem()).ToList();
            foreach (var item in items) item.Select();
        }

        public void OnDrag(Grid rayCastGrid)
        {
            if (_connectedNeighbors.Contains(rayCastGrid) == false) return;
            if (!_highlightNeighbors.Add(rayCastGrid)) return;

            var highlightGrid = _highlightNeighbors.FirstOrDefault(grid => grid == rayCastGrid);
            if (highlightGrid != null)
                highlightGrid.GetItem().Highlight();
        }

        public void OnDeselect()
        {
            var items = _connectedNeighbors.Select(grid => grid.GetItem()).ToList();
            foreach (var item in items) 
                item.Deselect();
            foreach (var highlightGrid in _highlightNeighbors)
            {
                highlightGrid.GetItem().Unhighlight();
                
                
                //Action based match - IMatchActions
                //if (_highlightNeighbors.Count > 2)
                    //highlightGrid.RemoveItem();
            }

            UnsubscribeFromNeighbors();
        }
    }
}