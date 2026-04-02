using Animation;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using VContainer;

namespace Menu.UI
{
    public class MenuView : MonoBehaviour
    {
        private const float SPEED_MOVE_UI = 0.7f;
        [SerializeField] private RectTransform _leftTower;
        [SerializeField] private RectTransform _rightTower;
        [SerializeField] private RectTransform _wall;
        [SerializeField] private RectTransform _logo;
        [SerializeField] private List<GameObject> _levelButtons = new List<GameObject>();

        private CancellationTokenSource _cts;

        private IAnimation _animation;

        public async UniTask StartAnumation()
        {
            _cts = new CancellationTokenSource();
            //sound
            _animation.MoveUI(_leftTower, new Vector3(
                -27f, -32f, 0), SPEED_MOVE_UI, DG.Tweening.Ease.InOutBack);
            await UniTask.Delay(TimeSpan.FromSeconds(0.2f), _cts.IsCancellationRequested);

            _animation.MoveUI(_rightTower, new Vector3(
                -441f, -70f, 0), SPEED_MOVE_UI + 0.1f, DG.Tweening.Ease.InOutBack);
            await UniTask.Delay(TimeSpan.FromSeconds(0.4f), _cts.IsCancellationRequested);

            _animation.MoveUI(_wall, new Vector3(
                -5f, -8f, 0), SPEED_MOVE_UI + 0.4f, DG.Tweening.Ease.InOutBack);
            await UniTask.Delay(TimeSpan.FromSeconds(0.25f), _cts.IsCancellationRequested);

            _animation.MoveUI(_logo, new Vector3(
                -94f, 20f, 0), SPEED_MOVE_UI + 0.7f, DG.Tweening.Ease.OutBounce);
            await UniTask.Delay(TimeSpan.FromSeconds(0.7f), _cts.IsCancellationRequested);
            foreach (var button in _levelButtons)
            {
                //playSound
                button.SetActive(true);
                await _animation.Reveal(button, 0.1f);
            }
        }


        [Inject]
        private void Construct(IAnimation animation)
        {
            _animation = animation;
        }
    }
}
