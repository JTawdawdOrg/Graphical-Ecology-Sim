using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreyStateMachine : StateMachine
{
    void Start()
    {
        SetState(new Idle(this));
    }

   
    // Prey needs to idle
    // SetState(new Idle(this));

    // Prey needs to feed
    // SetState(new Feed(this));

    // Prey needs to drink
    // SetState(new Drink(this));

    // Prey needs to reproduce
    // SetState(new Reproduce(this));
}
