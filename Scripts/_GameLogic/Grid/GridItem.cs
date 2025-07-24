using _Game.Scripts._GameLogic.Pure;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Game.Scripts._GameLogic.Grid
{
    public class GridItem : BaseGridItem, ISlotItemActions
    {
        [SerializeField] private ISelectActions[] _selectActions;
        [SerializeField] private IHighlightActions[] _highlightActions;
        private GridItemSelectAnimation _selection;
        private Grid _grid;

        private void Awake()
        {
            if (_selectActions == null || _selectActions.Length == 0)
                _selectActions = GetComponentsInChildren<ISelectActions>();
         
            if (_highlightActions == null || _highlightActions.Length == 0)
                _highlightActions = GetComponentsInChildren<IHighlightActions>();
        }
        
        [Button]
        public void FillInterfaceActions()
        {
            _selectActions = GetComponentsInChildren<ISelectActions>();
            _highlightActions = GetComponentsInChildren<IHighlightActions>();
        }

        public void OnGridItemSet()
        {
            Debug.Log("Grid Item Set" + gameObject.name);
        }

        public void OnGridItemRemoved()
        {
            Debug.Log("Grid Item Removed" + gameObject.name);
        }
        
        public void SetGrid(Grid grid)
        {
            _grid = grid;
        }

        public void Select()
        {
            foreach (var selectAction in _selectActions)
            {
                selectAction?.OnSelect();
            }
        }
        
        public void Highlight()
        {
            foreach (var highlightAction in _highlightActions)
            {
                highlightAction?.OnHighlight();
                Debug.Log("Highlighting" + gameObject.name);
            }
        }
        
        public void Unhighlight()
        {
            foreach (var highlightAction in _highlightActions)
            {
                highlightAction?.OnUnhighlight();
            }
        }

        public void Deselect()
        {
            foreach (var selectAction in _selectActions)
            {
                selectAction?.OnDeselect();
            }
        }
    }
}