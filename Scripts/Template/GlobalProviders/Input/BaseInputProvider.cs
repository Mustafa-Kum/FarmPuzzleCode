using _Game.Scripts.Helper.Services;
using UnityEngine;

namespace _Game.Scripts.Template.GlobalProviders.Input
{
    public abstract class BaseInputProvider : MonoBehaviour
    {
        private CoroutineService _coroutineService;
        private Vector3 _initialClickPosition;
        private bool _isDragging;
        private bool _mainInputEnabled = true;
        private Camera _mainCamera;
        private Coroutine _inputCoroutine;

        private void OnEnable()
        {
            _mainCamera = Camera.main;
        }
        
        protected virtual void Awake()
        {
            _coroutineService = new CoroutineService(this);
            _inputCoroutine = _coroutineService.StartUpdateRoutine(HandleInput, () => _mainInputEnabled);
        }

        protected abstract void OnClick();
        protected abstract void OnDrag();
        protected abstract void OnRelease();
        
        protected Camera GetMainCamera() => _mainCamera;
        
        private void DisableInput()
        {
            _mainInputEnabled = false;
            if (_inputCoroutine != null)
            {
                _coroutineService.Stop(_inputCoroutine);
                _inputCoroutine = null;
            }
            Debug.Log("DisableInput");
        }

        private void EnableInput()
        {
            _mainInputEnabled = true;
            if (_inputCoroutine == null)
            {
                _inputCoroutine = _coroutineService.StartUpdateRoutine(HandleInput, () => _mainInputEnabled);
            }
            Debug.Log("EnableInput");
        }

        private void HandleInput()
        {
            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                OnClick();
            }

            if (UnityEngine.Input.GetMouseButton(0))
            {
                OnDrag();
            }

            if (UnityEngine.Input.GetMouseButtonUp(0))
            {
                OnRelease();
            }
        }
    }
}