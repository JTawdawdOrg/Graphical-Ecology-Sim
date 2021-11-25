using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunt : State
{
    public Hunt(StateMachine stateMachine) : base(stateMachine)
    {

    }

    public override IEnumerator OnStart()
    {
        Debug.Log("print from Hunt state");
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
    public override string ToString()
    {
        return "Hunt";
    }
}
