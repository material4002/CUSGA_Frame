using Mat.Kits;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Mat
{
    public class Inputs : SingleTon<Inputs>
    {
        public Inputs() : base()
        {
            _playerInput=Entry.Instance.GetComponent<PlayerInput>();
            _data = InputData.Instance;

            _playerInput.onActionTriggered += Trigger;
        }

        PlayerInput _playerInput;
        InputData _data;

        private void Trigger(InputAction.CallbackContext e)
        {
            this.TriggerEvent(e.action.name, new ArgsInput { context = e });
        }
    }
}
public class ArgsInput : EventArgs
{
    public InputAction.CallbackContext context;
}

