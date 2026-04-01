using Animation;
using Game.Board;
using Game.GridSystem;
using GameStateMachine.States;
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

        public StateMachine(GameBoard gameBoard, IGrid grid, IAnimation animation)
        {
            _gameBoard = gameBoard;
            _grid = grid;
            _animation = animation;
            _states = new List<IState>()
            {
                new PrepareState(this, _gameBoard),
                new PlayerTurnState(_grid, this, _animation),
                new SwapTilesState()
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
