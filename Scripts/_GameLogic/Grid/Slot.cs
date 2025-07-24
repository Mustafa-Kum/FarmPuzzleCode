using Sirenix.OdinInspector;
using UnityEngine;

namespace _Game.Scripts._GameLogic.Grid
{
    public abstract class Slot<T> : GridItem where T : GridItem
    {
        [ReadOnly][ShowInInspector] protected bool IsOccupied => Item != null;
        [ReadOnly][ShowInInspector] protected T Item { get; private set; }
        [ReadOnly][ShowInInspector] protected Vector2Int Position { get; private set; }
        private GridItemType ItemType { get; set; }
        public bool IsEmpty() => Item == null;
        protected T GetItem() => Item;
        public void SetPosition(Vector2Int position) => Position = position;
        public void SetGridType(GridItemType type) => ItemType = type;
        public GridItemType GetGridType() => ItemType;
        public Vector2Int GetPosition() => Position;
        
        protected virtual void Awake() => OnAwake();
        protected abstract void OnInitialize();
        protected abstract void OnItemSet();
        protected abstract void OnItemRemoved();

        private void OnAwake()
        {
            Item = default;
            OnInitialize(); 
        }

        public void SetItem(T item)
        {
            Item = item;
            OnItemSet(); 
        }

        protected void RemoveItem(bool destroy = true)
        {
            if (destroy)
            {
                Destroy(Item.gameObject);
            }
            Item = default;
            OnItemRemoved(); 
        }
    }
}