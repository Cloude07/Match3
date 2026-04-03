using Animation;
using Audio;
using Data;
using Game.Board;
using Game.GridSystem;
using Game.MatchTiles;
using Game.Score;
using Game.Tiles;
using Game.UI;
using Game.Utils;
using GameStateMachine;
using Levels;
using ResourcesLoading;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.EntryPoint
{
    public class GameEntryPoint : IInitializable
    {
        //bg tile setup
        private LevelConfig _levelConfig;
        private BlankTilesSetup _blankTilesSetup;
        private StateMachine _stateMachine;
        private GameProgress _gameProgress;
        private ScoreCalculator _scoreCalculator;
        private MatchFinder _matchFinder;
        private IGrid _grid;
        private GameBoard _gameBoard;
        private GameDebug _gameDebug;
        private TilePool _tilePool;
        private GameData _gameData;
        private AudioManager _audioManager;
        private IAnimation _animation;
        private GameResourcesLoader _gameResourcesLoader;
        private ISetupCamera _setupCamera;
        //FX pool
        private IAsyncSceneLoading _asyncSceneLoading;
        private EndGamePanelView _endGamePanelView;

        private bool _isDebuging;
        private bool _isVertical;

        public GameEntryPoint(BlankTilesSetup blankTilesSetup, 
            GameProgress gameProgress, ScoreCalculator scoreCalculator, 
            MatchFinder matchFinder, IGrid grid, GameBoard gameBoard, GameDebug gameDebug, TilePool tilePool,
            GameData gameData, AudioManager audioManager, IAnimation animation, GameResourcesLoader gameResourcesLoader, 
            ISetupCamera setupCamera, IAsyncSceneLoading asyncSceneLoading, EndGamePanelView endGamePanelView)
        {
            _blankTilesSetup = blankTilesSetup;
            _gameProgress = gameProgress;
            _scoreCalculator = scoreCalculator;
            _matchFinder = matchFinder;
            _grid = grid;
            _gameBoard = gameBoard;
            _gameDebug = gameDebug;
            _tilePool = tilePool;
            _gameData = gameData;
            _audioManager = audioManager;
            _animation = animation;
            _gameResourcesLoader = gameResourcesLoader;
            _setupCamera = setupCamera;
            _asyncSceneLoading = asyncSceneLoading;
            _endGamePanelView = endGamePanelView;
        }

        public void Initialize()
        {
            _levelConfig = _gameData.CurrentLevel;
            if (_isDebuging)
                _gameDebug.ShowwDebug(_gameBoard.transform);

            _grid.SetupGrid(_levelConfig.Width, _levelConfig.Height);
            _gameProgress.LoadLevelConfig(_levelConfig.GoalScore, _levelConfig.Moves);
            //await resources
            _setupCamera.SetCamera(_grid.Width, _grid.Height, _isVertical);
            _blankTilesSetup.SetupBlanks(_levelConfig);
            _stateMachine = new StateMachine(_gameBoard, _grid, _animation, _matchFinder,
                _tilePool, _gameProgress, _scoreCalculator, _audioManager, _endGamePanelView);
            _asyncSceneLoading.LoadingIsDone(true);
        }
    }
}
