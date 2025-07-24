using _Game.Scripts._GameLogic.Data.Grid;
using _Game.Scripts.ScriptableObjects.Saveable;
using UnityEngine;
using Zenject;

namespace _Game.Scripts._GameLogic.Dependency
{
    public class TemplateDependencyInstaller : MonoInstaller
    { 
        [SerializeField] private PlayerSavableData _playerSavableData;
        
        public override void InstallBindings()
        {
            Container.Bind<PlayerSavableData>().FromInstance(_playerSavableData).AsSingle();
        }
    }
}