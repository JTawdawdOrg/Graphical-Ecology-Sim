using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredatorStateMachine : StateMachine
{
    void Start()
    {
        SetState(new Idle(this));
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
