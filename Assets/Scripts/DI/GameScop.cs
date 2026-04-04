using Game.Board;
using Game.EntryPoint;
using Game.GridSystem;
using Game.MatchTiles;
using Game.Score;
using Game.Tiles;
using Game.UI;
using Game.Utils;
using ResourcesLoading;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Grid = Game.GridSystem.Grid;

namespace DI
{
    public class GameScop : LifetimeScope
    {
        [SerializeField] private GameBoard _gameBoard;
        [SerializeField] private GameResourcesLoader _resourcesLoader;
        [SerializeField] private EndGamePanelView _endGamePanelView;
        [SerializeField] private GameProgressView _progressView;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<GameEntryPoint>();

            builder.RegisterInstance(_gameBoard);
            builder.RegisterInstance(_resourcesLoader);
            builder.RegisterInstance(_endGamePanelView);
            builder.RegisterInstance(_progressView);

            builder.Register<IGrid, Grid>(Lifetime.Singleton);
            builder.Register<ISetupCamera, SetupCamera>(Lifetime.Singleton);
            builder.Register<GameDebug>(Lifetime.Singleton);
            builder.Register<TilePool>(Lifetime.Singleton);
            builder.Register<BlankTilesSetup>(Lifetime.Singleton);
            builder.Register<MatchFinder>(Lifetime.Singleton);
            builder.Register<GameProgress>(Lifetime.Singleton);
            builder.Register<ScoreCalculator>(Lifetime.Singleton);
            builder.Register<EndGame>(Lifetime.Singleton);
            builder.Register<BackgroundTilesSetup>(Lifetime.Singleton);
            builder.Register<FXPool>(Lifetime.Singleton);
        }
    }
}
