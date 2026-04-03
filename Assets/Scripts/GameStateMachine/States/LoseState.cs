using Audio;
using UnityEngine;

namespace GameStateMachine.States
{
    public class LoseState : IState
    {
        private AudioManager _audioManager;

        public LoseState(AudioManager audioManager)
        {
            _audioManager = audioManager;
        }

        public void Enter()
        {
           _audioManager.PlayLose();
        }

        public void Exit()
        {
            
        }
    }
}
