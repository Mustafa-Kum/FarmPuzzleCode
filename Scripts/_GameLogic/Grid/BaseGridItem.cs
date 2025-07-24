using Sirenix.OdinInspector;
using UnityEngine;

namespace _Game.Scripts._GameLogic.Grid
{
    public abstract class BaseGridItem : SerializedMonoBehaviour
    {
        public GridItemType Type;

        public GridItemType GetItemType() => Type;
    }
}