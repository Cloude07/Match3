using Animation;
using Audio;
using Game.Board;
using Game.GridSystem;
using Game.MatchTiles;
using Game.Score;
using Game.Tiles;
using Game.UI;
using Game.Utils;
using GameStateMachine.States;
using Levels;
using System.Collections.Generic;
using System.Linq;

namespace GameStateMachine
{
    public class StateMachine : IStateSwitcher
    {
        private List<IState> _states;
        private IState _currentState;
        private IGrid _grid;
        private IAnimation _animation;
        
        private GameBoard _gameBoard;
        private MatchFinder _matchFinder;
        private TilePool _tilePool;
        private GameProgress _gameProgress;
        private ScoreCalculator _scoreCalculator;
        private AudioManager _audioManager;
        private EndGamePanelView _endGamePanelView;
        private BackgroundTilesSetup _backgroundTilesSetup;
        private BlankTilesSetup _blankTilesSetup;
        private LevelConfig _levelConfig;
        private FXPool _fxPool;

        public StateMachine(GameBoard gameBoard, IGrid grid, IAnimation animation,
            MatchFinder matchFinder, TilePool tilePool, GameProgress gameProgress, 
            ScoreCalculator scoreCalculator, AudioManager audioManager, EndGamePanelView endGamePanelView, 
            BackgroundTilesSetup backgroundTilesSetup, BlankTilesSetup blankTilesSetup, LevelConfig levelConfig, FXPool fXPool)
        {
            _gameBoard = gameBoard;
            _grid = grid;
            _animation = animation;
            _matchFinder = matchFinder;
            _tilePool = tilePool;
            _gameProgress = gameProgress;
            _scoreCalculator = scoreCalculator;
            _audioManager = audioManager;
            _endGamePanelView = endGamePanelView;
            _backgroundTilesSetup = backgroundTilesSetup;
            _blankTilesSetup = blankTilesSetup;
            _levelConfig = levelConfig;
            _fxPool = fXPool;

            _states = new List<IState>()
            {
                new PrepareState(this, _gameBoard, _backgroundTilesSetup, _blankTilesSetup, _levelConfig),
                new PlayerTurnState(_grid, this, _animation, _audioManager),
                new SwapTilesState(_grid, this, _animation, _matchFinder, _gameProgress, _audioManager),
                new RemoveTilesState(_grid, this, _animation, _matchFinder, _scoreCalculator, _audioManager, _fxPool, _gameBoard),
                new RefillGridState(_grid, this, _animation, _matchFinder, _tilePool,
                _gameBoard.transform, _gameProgress, _audioManager),

                new WinState(_endGamePanelView),
                new LoseState(_endGamePanelView)
            };
            _currentState = _states[0];
            _currentState.Enter();
        }

        public void SwitchState<T>() where T : IState
        {
           var state = _states.FirstOrDefault(state => state is T);
            _currentState.Exit();
            _currentState = state;
            _currentState?.Enter();
        }
    }
}
