using UnityEngine.Events;

namespace _Game.Scripts.Managers.Core
{
    public partial class EventManager
    {
        public static class Resource
        {
            #region Currency
            
            public static UnityAction<CurrencyType> CurrencyChanged;

            #endregion
        }
    }
}