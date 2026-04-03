
using Game.Score;
using Game.UI;

namespace GameStateMachine.States
{
    public class WinState : IState
    {
        private EndGamePanelView _endGamePanelView;

        public WinState(EndGamePanelView endGamePanelView)
        {
            _endGamePanelView = endGamePanelView;
        }

        public void Enter()
        {
           
            _endGamePanelView.ShowEndGamePanel(true);
        }

        public void Exit()
        {
          
        }
    }
}
