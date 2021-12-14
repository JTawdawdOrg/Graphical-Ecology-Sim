/*
Program: State.cs
Date Created: ‎18/10/‎2021
Description: Basic state for other state classes to inherit from
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State 
{
    protected StateMachine _stateMachine;

    public State(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public virtual IEnumerator OnStart()
    {
        yield break;
    }

    public virtual IEnumerator OnUpdate()
    {
        yield break;
    }

    public virtual IEnumerator Execution()
    {
        yield break;
    }

    public virtual IEnumerator OnExit()
    {
        yield break;
    }
}
