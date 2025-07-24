using _Game.Scripts._GameLogic.Data.Grid;
using UnityEngine;
using Zenject;

namespace _Game.Scripts._GameLogic.Dependency
{
    public class GameDependencyInstaller : MonoInstaller
    {
        [SerializeField] private GridDataContainer _gridDataContainer;
        [SerializeField] private GridItemDataContainer _gridItemDataContainer;

        public override void InstallBindings()
        {
            Container.Bind<GridDataContainer>().FromInstance(_gridDataContainer).AsSingle();
            Container.Bind<GridItemDataContainer>().FromInstance(_gridItemDataContainer).AsSingle();
        }
    }
}