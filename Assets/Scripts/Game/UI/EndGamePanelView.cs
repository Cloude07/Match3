using Animation;
using Audio;
using Cysharp.Threading.Tasks;
using Game.Score;
using System;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Game.UI
{
    public class EndGamePanelView : MonoBehaviour
    {
        [SerializeField] private GameObject _panel;
        [SerializeField] private RectTransform _panelRectTransform;
        [SerializeField] private Button _closeButton;
        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _score;

        private IAnimation _animation;
        private AudioManager _audioManager;
        private EndGame _endGame;
        private CancellationTokenSource _cts;
        private bool _isWindCondition;

        private readonly string _win = "You have won!!!";
        private readonly string _lose = "you have loose!!!";

        private void OnEnable()
        {
            _closeButton.onClick?.AddListener(ExitGame);
        }

        private void OnDisable()
        {
            _cts?.Dispose();
            _closeButton.onClick?.RemoveListener(ExitGame);
        }

        public async void ShowEndGamePanel(bool isWinCondition)
        {
            _isWindCondition = isWinCondition;
            _title.text = _isWindCondition ? _win : _lose;
        //   _score.text = _isWindCondition ? _win + $"Score: {_endGame.GameData.CurretnScore}" : _lose + $"Score: {_endGame.GameData.CurretnScore}";

            await StartAnimation();
            _closeButton.interactable = true;
        }

        private async UniTask StartAnimation()
        {
            _cts = new CancellationTokenSource();
            _audioManager.PlayWhoosh();
            _panel.SetActive(true);
            _animation.MoveUI(_panelRectTransform, new Vector3(0f, -150f, 0f), 0.5f, DG.Tweening.Ease.InOutBack);
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f), _cts.IsCancellationRequested);
            _audioManager.StopMusic();

            if (_isWindCondition)
                _audioManager.PlayWin();
            else
                _audioManager.PlayLose();
        }

        private void ExitGame() =>
            _endGame.End(_isWindCondition);

        [Inject]
        private void Construct(IAnimation animation, AudioManager audioManager, EndGame endGame)
        {
            _animation = animation;
            _audioManager = audioManager;
            _endGame = endGame;
        }
    }
}
