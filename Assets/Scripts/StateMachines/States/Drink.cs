using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drink : State
{
    public Drink(StateMachine stateMachine) : base(stateMachine)
    {

    }

    public override IEnumerator OnStart()
    {
        return base.OnStart();
    }

    public override IEnumerator Execution()
    {
        return base.Execution();
    }

    public override IEnumerator OnExit()
    {
        return base.OnExit();
    }
}
