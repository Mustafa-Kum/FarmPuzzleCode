using UnityEngine;

namespace _Game.Scripts._GameLogic.Data.Grid
{
    [CreateAssetMenu(fileName = nameof(GridDataContainer), menuName = "Farm Connect/Data/Grid Container", order = 0)]
    public class GridDataContainer : ScriptableObject
    {
        [SerializeField] private GameObject _grid;
        public Vector2Int GridSize;
        public GameObject GetGrid() => _grid;
        public Vector2Int GetGridSize() => GridSize;
    }
}