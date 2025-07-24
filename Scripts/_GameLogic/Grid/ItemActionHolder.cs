using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Game.Scripts._GameLogic.Grid
{
    public class ItemActionHolder : SerializedMonoBehaviour
    {
        [SerializeField] private ISlotItemActions[] _slotItemActions;

        private void Awake()
        {
            _slotItemActions ??= GetComponents<ISlotItemActions>();
            
            if (_slotItemActions.Length == 0)
            {
                throw new Exception("No ISlotItemActions found on this object");
            }
            
            foreach (var slotItemAction in _slotItemActions)
            {
                slotItemAction.OnGridItemSet();
            }
        }

        public void OnDestroy()
        {
            foreach (var slotItemAction in _slotItemActions)
            {
                slotItemAction.OnGridItemRemoved();
            }
        }
        
        [Button]
        private void GetItemActions()
        {
            _slotItemActions ??= GetComponents<ISlotItemActions>();
        }
    }
}