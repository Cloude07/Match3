using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    public class InputReader : IDisposable
    {
        public event Action Click;
        private Inputs _inputs;
        private InputAction _positionAction;
        private InputAction _ClickAction;

        private bool _isClick;

        public InputReader()
        {
            _inputs = new Inputs();
            _inputs.Player.Click.performed += OnClick;
        }
        public void Dispose()
        {
            _inputs.Player.Click.performed -= OnClick;
        }

        public void EnableInputs(bool isValue)
        {
            if (isValue)
                _inputs.Enable();
            else 
                _inputs.Disable();
        }

        public Vector2 Position() => _inputs.Player.Select.ReadValue<Vector2>();

        private void OnClick(InputAction.CallbackContext context)
        {
            Click?.Invoke();
        }


    }
}
