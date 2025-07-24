using _Game.Scripts._GameLogic.Pure;
using _Game.Scripts.Template.GlobalProviders.Input;
using UnityEngine;

namespace _Game.Scripts._GameLogic.Controller
{
    public class InputGridController : BaseInputProvider
    {
        private Grid.Grid _selectedGrid;
        
        protected override void OnClick()
        {
            var grid = GetGridFromRayCast();
            _selectedGrid = grid;
            if (_selectedGrid == null || _selectedGrid.IsEmpty()) return;
            _selectedGrid.OnSelect();
        }

        protected override void OnDrag()
        {
            var grid = GetGridFromRayCast();
            if (grid == null || grid.IsEmpty()) return;
            if (_selectedGrid == null || _selectedGrid.IsEmpty()) return;
            _selectedGrid.OnDrag(grid);
        }

        protected override void OnRelease()
        {
            if (_selectedGrid == null || _selectedGrid.IsEmpty()) return;
            _selectedGrid.OnDeselect();
            _selectedGrid = null;
        }
        
        private Grid.Grid GetGridFromRayCast()
        {
            var ray = GetMainCamera().ScreenPointToRay(Input.mousePosition);

            if (!Physics.Raycast(ray, out RaycastHit hit))
                return null;

            return hit.collider.TryGetComponent(out Grid.Grid tile) ? tile : null;
        }
    }
}