
using Game.UI;

namespace GameStateMachine.States
{
    public class LoseState : IState
    {
        private EndGamePanelView _endGamePanelView;

        public LoseState(EndGamePanelView endGamePanelView)
        {
            _endGamePanelView = endGamePanelView;
        }

        public void Enter()
        {
            _endGamePanelView.ShowEndGamePanel(false);
       
        }

        public void Exit()
        {
            
        }
    }
}
