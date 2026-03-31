using Game.Board;
using Game.GridSystem;
<<<<<<< HEAD
using Game.Utils;
=======
using Game.Tiles;
using Game.Utils;
using ResourcesLoading;
>>>>>>> ObjectPool
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Grid = Game.GridSystem.Grid;

namespace DI
{
    public class GameScop : LifetimeScope
    {
        [SerializeField]
        private GameBoard _gameBoard;
<<<<<<< HEAD
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_gameBoard);
            builder.Register<IGrid, Grid>(Lifetime.Singleton);
            builder.Register<ISetupCamera, SetupCamera>(Lifetime.Singleton);
=======
        [SerializeField]
        private GameResourcesLoader _resourcesLoader;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_gameBoard);
            builder.RegisterInstance(_resourcesLoader);
            builder.Register<IGrid,Grid>(Lifetime.Singleton);
            builder.Register<SetupCamera>(Lifetime.Singleton);
            builder.Register<TilePool>(Lifetime.Singleton);
>>>>>>> ObjectPool
        }
    }
}
