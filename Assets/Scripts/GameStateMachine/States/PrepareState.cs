using Game.Board;
using Game.Tiles;
using Levels;
using UnityEngine;

namespace GameStateMachine.States
{
    public class PrepareState : IState
    {
        private readonly IStateSwitcher _stateSwitcher;
        private BlankTilesSetup _blankTilesSetup;
        private BackgroundTilesSetup _backgroundTilesSetup;
        private LevelConfig _levelConfig;
        private GameBoard _gameBoard;

        public PrepareState(IStateSwitcher stateSwitcher, GameBoard gameBoard,
            BackgroundTilesSetup backgroundTilesSetup, BlankTilesSetup blankTilesSetup, LevelConfig levelConfig)
        {
            _gameBoard = gameBoard;
            _stateSwitcher = stateSwitcher;
            _backgroundTilesSetup = backgroundTilesSetup;
            _blankTilesSetup = blankTilesSetup;
            _levelConfig = levelConfig;
        }

        public async void Enter()
        {
            await _backgroundTilesSetup.SetupBackground(_gameBoard.transform, _blankTilesSetup.Blanks,
                _levelConfig.Width, _levelConfig.Height);

            _gameBoard.CreateBoard();
            _stateSwitcher.SwitchState<PlayerTurnState>();
        }

        public void Exit()
        {
            Debug.Log("Game was started!");
        }
    }
}
