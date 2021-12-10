using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredatorStateMachine : StateMachine
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

    // Predator needs to idle
    // SetState(new Idle(this));

    // Predator needs to hunt
    // SetState(new Hunt(this));

    // Predator needs to drink
    // SetState(new Drink(this));

    // Predator needs to reproduce
    // SetState(new Reproduce(this));
}
