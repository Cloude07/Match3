using Animation;
using Game.Board;
using Game.GridSystem;
using Game.MatchTiles;
using Game.Tiles;
using GameStateMachine;
using UnityEngine;
using VContainer;

namespace EntryPoint
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] GameBoard _gameBoard;
        private StateMachine _stateMachine;
        private IGrid _grid;
        private IAnimation _animation;
        private MatchFinder _matchFinder;
        private TilePool _tilePool;
        private void Start()
        {
            _stateMachine = new StateMachine(_gameBoard, _grid, _animation, _matchFinder, _tilePool);
        }

        [Inject]
        private void Construct(IGrid grid, IAnimation animation, MatchFinder matchFinder, TilePool tilePool)
        {
            _grid = grid;
            _animation = animation;
            _matchFinder = matchFinder;
            _tilePool = tilePool;
        }
    }
}
