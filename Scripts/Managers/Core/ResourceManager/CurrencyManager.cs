using _Game.Scripts._GameLogic.Data.Store.Catalog;
using _Game.Scripts.Managers.Core.StoreManager;
using _Game.Scripts.ScriptableObjects.Saveable;
using UnityEngine;
using UnityEngine.Purchasing;

namespace _Game.Scripts.Managers.Core.ResourceManager
{
    public class CurrencyManager : MonoBehaviour
    {
        #region INSPECTOR VARIABLES

        [SerializeField] private CurrencyValuesSO currencyValuesSo;
        [SerializeField] private ProductCatalogSO productCatalog;

        #endregion

        #region PRIVATE VARIABLES

        private IAPCurrencyStore _iapCurrencyStore;

        #endregion

        #region UNITY METHODS

        private void Awake()
        {
            _iapCurrencyStore = new IAPCurrencyStore(currencyValuesSo, productCatalog);
            _iapCurrencyStore.InitializeProducts();
        }

        private void OnEnable()
        {
            EventManager.IAPEvents.PurchaseSuccess += HandlePurchaseSuccessEvent;
        }

        private void OnDisable()
        {
            EventManager.IAPEvents.PurchaseSuccess -= HandlePurchaseSuccessEvent;
        }

        #endregion

        #region PRIVATE METHODS

        private void HandlePurchaseSuccessEvent(PurchaseEventArgs arg0)
        {
            _iapCurrencyStore.ProcessPurchase(arg0.purchasedProduct.definition.id);
        }

        #endregion
    }
}

public enum CurrencyType
{
    Coin,
    Gem,
}