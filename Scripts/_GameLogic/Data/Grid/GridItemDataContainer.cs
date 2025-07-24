using System;
using System.Collections.Generic;
using System.Linq;
using _Game.Scripts._GameLogic.Grid;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Game.Scripts._GameLogic.Data.Grid
{
    [CreateAssetMenu(fileName = nameof(GridItemDataContainer), menuName = "Farm Connect/Data/Grid Item Data Container",
        order = 0)]
    public class GridItemDataContainer : SerializedScriptableObject
    {
        private Dictionary<GridItemType, int> _typeCounts;
        public List<GridItemTypeData> GridItems;
        
        [Serializable]
        public class GridItemTypeData
        {
            public GridItemType Type;
            public GridItem Prefab;
        }
        
        private void OnEnable()
        {
            InitializeTypeCounts();
        }

        private void InitializeTypeCounts()
        {
            _typeCounts = new Dictionary<GridItemType, int>();
            foreach (var typeData in GridItems)
            {
                _typeCounts[typeData.Type] = 0;
            }
        }
        
        public GridItem GetBalancedRandomTypeFirstLevelGridObject()
        {
            var leastUsedTypes = _typeCounts.Where(x => x.Value == _typeCounts.Values.Min()).Select(x => x.Key).ToList();
            var selectedType = leastUsedTypes[Random.Range(0, leastUsedTypes.Count)];
            _typeCounts[selectedType]++;
            var gridObjectTypeData = GridItems.Find(x => x.Type == selectedType);
            return gridObjectTypeData.Prefab;
        }
    }
}