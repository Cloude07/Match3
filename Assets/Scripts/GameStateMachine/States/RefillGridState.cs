using Animation;
using Audio;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Game.GridSystem;
using Game.MatchTiles;
using Game.Score;
using Game.Tiles;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace GameStateMachine.States
{
    public class RefillGridState : IState, IDisposable
    {
        private CancellationTokenSource _cts;
        private IGrid _grid;
        private IStateSwitcher _stateSwitcher;
        private IAnimation _animation;
        private MatchFinder _matchFinder;
        private TilePool _tilePool;
        private readonly Transform _parent;
        private GameProgress _gameProgress;
        private AudioManager _audioManager;

        private List<Vector2Int> _tilesToRefillPos = new List<Vector2Int>();

        public RefillGridState(IGrid grid, IStateSwitcher stateSwitcher, IAnimation animation,
            MatchFinder matchFinder, TilePool tilePool, Transform parent, GameProgress gameProgress,
            AudioManager audioManager)
        {
            _grid = grid;
            _stateSwitcher = stateSwitcher;
            _animation = animation;
            _matchFinder = matchFinder;
            _tilePool = tilePool;
            _parent = parent;
            _gameProgress = gameProgress;
            _audioManager = audioManager;
        }

        public async void Enter()
        {
            await FallTiles();
            await RefillGrid();
            if (_matchFinder.CheckBoardForMatches(_grid))
            {
                _stateSwitcher.SwitchState<RemoveTilesState>();
                _audioManager.PlayMatch();
            }
            else
            {
                _audioManager.PlayNoMatch();
                CheckEndGame();
            }
        }

        public void Exit()
        {
            _cts?.Cancel();
        }

        public void Dispose()
        {
            _cts?.Dispose();
        }

        private async UniTask FallTiles()
        {
            _cts = new CancellationTokenSource();
            for (int x = 0; x < _grid.Width; x++)
            {
                for (int y = 0; y < _grid.Height; y++)
                {
                    if (_grid.GetValue(x, y))
                        continue;

                    for (int i = y + 1; i < _grid.Height; i++)
                    {
                        if (_grid.GetValue(x, i) == null)
                            continue;

                        if (_grid.GetValue(x, i).IsInteractable == false)
                            continue;

                        var tile = _grid.GetValue(x, i);
                        _grid.SetValue(x, y, tile);
                        _animation.MoveTile(tile, _grid.GridToWorld(x, y), Ease.InBack);
                        _grid.SetValue(x, i, null);
                        _tilesToRefillPos.Add(new Vector2Int(x, i));
                        break;
                    }
                }
            }
            await UniTask.Delay(TimeSpan.FromSeconds(0.3f), _cts.IsCancellationRequested);
            _audioManager.PlayWhoosh();
            _cts.Cancel();
        }

        private async UniTask RefillGrid()
        {
            _cts = new CancellationTokenSource();
            for (int x = 0; x < _grid.Width; x++)
            {
                for (int y = 0; y < _grid.Height; y++)
                {
                    if (_grid.GetValue(x, y) != null)
                        continue;
                    var tile = _tilePool.GetTile(_grid.GridToWorld(x, y), _parent);
                    tile.gameObject.SetActive(true);
                    _grid.SetValue(x, y, tile);
                    await _animation.Reveal(tile.gameObject, 0.2f);


                }
                await UniTask.Delay(TimeSpan.FromSeconds(0.1f), _cts.IsCancellationRequested);
                _audioManager.PlayPop();
            }
            _cts.Cancel();
        }

        private void CheckEndGame()
        {
            if (_gameProgress.CheckGoalScore())
                _stateSwitcher.SwitchState<WinState>();
            else if (_gameProgress.Moves <= 0)
                _stateSwitcher.SwitchState<LoseState>();
            else
                _stateSwitcher.SwitchState<PlayerTurnState>();
        }
    }
}
