using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreyStateMachine : StateMachine
{
    

    protected override void Start()
    {
        SetState(new Idle(this));
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        _state.OnUpdate();
    }
}
