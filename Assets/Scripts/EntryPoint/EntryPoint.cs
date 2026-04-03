using Animation;
using Audio;
using Game.Board;
using Game.GridSystem;
using Game.MatchTiles;
using Game.Score;
using Game.Tiles;
using GameStateMachine;
using Levels;
using UnityEngine;
using VContainer;

namespace EntryPoint
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] GameBoard _gameBoard;
        [SerializeField] private LevelConfig _levelConfig;
        private StateMachine _stateMachine;
        private IGrid _grid;
        private IAnimation _animation;
        private MatchFinder _matchFinder;
        private TilePool _tilePool;
        private GameProgress _gameProgress;
        private ScoreCalculator _scoreCalculator;
        private AudioManager _audioManager;
        private IAsyncSceneLoading _asyncSceneLoading;

        private void Start()
        {
            _stateMachine = new StateMachine(_gameBoard, _grid, _animation,
                _matchFinder, _tilePool, _gameProgress, _scoreCalculator, _audioManager);
            _gameProgress.LoadLevelConfig(_gameBoard.LevelConfig.GoalScore, _gameBoard.LevelConfig.Moves);
            _asyncSceneLoading.LoadingIsDone(true);
        }

        [Inject]
        private void Construct(IGrid grid, IAnimation animation, 
            MatchFinder matchFinder, TilePool tilePool, GameProgress gameProgress, 
            ScoreCalculator scoreCalculator, AudioManager audioManager, IAsyncSceneLoading asyncSceneLoading)
        {
            _grid = grid;
            _animation = animation;
            _matchFinder = matchFinder;
            _tilePool = tilePool;
            _gameProgress = gameProgress;
            _scoreCalculator = scoreCalculator;
            _audioManager = audioManager;
            _asyncSceneLoading = asyncSceneLoading;
        }
    }
}
