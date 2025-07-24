using UnityEngine;

namespace _Game.Scripts._GameLogic.Grid
{
    public class GridItemOnHighlight : MonoBehaviour, IHighlightActions
    {
        [SerializeField] GameObject _highlightObject;
        
        public void OnHighlight()
        {
            _highlightObject.SetActive(true);
        }

        public void OnUnhighlight()
        {
            _highlightObject.SetActive(false);
        }
    }
}