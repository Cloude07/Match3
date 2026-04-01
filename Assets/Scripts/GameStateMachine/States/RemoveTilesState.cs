using Animation;
using Cysharp.Threading.Tasks;
using Game.GridSystem;
using Game.MatchTiles;
using Game.Tiles;
using System;
using System.Collections.Generic;
using System.Threading;

namespace GameStateMachine.States
{
    public class RemoveTilesState : IState, IDisposable
    {
        private CancellationTokenSource _cts;
        private IGrid _grid;
        private IStateSwitcher _stateSwitcher;
        private IAnimation _animation;
        private MatchFinder _matchFinder;

        public RemoveTilesState(IGrid grid, IStateSwitcher stateSwitcher,
            IAnimation animation, MatchFinder matchFinder)
        {
            _grid = grid;
            _stateSwitcher = stateSwitcher;
            _animation = animation;
            _matchFinder = matchFinder;
        }


        public async void Enter()
        {
            _cts = new CancellationTokenSource();
            //  score++
            await RemoveTiles(_matchFinder.TilesToRemove, _grid);
             _stateSwitcher.SwitchState<RefillGridState>();
        }


        public void Exit()
        {
            _matchFinder.ClearCurrentMatchResult();
            _cts?.Cancel();
        }
        public void Dispose()
        {
            _cts?.Dispose();
        }

        private async UniTask RemoveTiles(List<Tile> tillesToRemove, IGrid grid)
        {
            foreach (var tile in tillesToRemove)
            {
                //PlaySound
                var pos = grid.WorldToGrid(tile.transform.position);
                grid.SetValue(pos.x, pos.y, null);
                await _animation.HideTile(tile.gameObject);
                //FX
            }
            _cts?.Cancel();
        }

    }
}
