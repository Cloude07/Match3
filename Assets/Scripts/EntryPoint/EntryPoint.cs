using Animation;
using Game.Board;
using Game.GridSystem;
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
        private void Start()
        {
            _stateMachine = new StateMachine(_gameBoard, _grid, _animation);
        }

        [Inject]
        private void Construct(IGrid grid, IAnimation animation)
        {
            _grid = grid;
            _animation = animation;
        }
    }
}
