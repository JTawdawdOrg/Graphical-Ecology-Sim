using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reproduce : State
{
    public Reproduce(StateMachine stateMachine) : base(stateMachine)
    {

    }

    public override IEnumerator OnStart()
    {
        _stateMachine.StartCoroutine(Execution());
        return base.OnStart();
    }

    public override IEnumerator OnUpdate()
    {
        if (_stateMachine.hunger < _stateMachine.hungerThreshold || _stateMachine.thirst < _stateMachine.thirstThreshold)
            _stateMachine.StartCoroutine(OnExit());

        if (_stateMachine.hunger < _stateMachine.hungerThreshold)
            _stateMachine.SetState(new Feed(_stateMachine));
        else if (_stateMachine.thirst < _stateMachine.thirstThreshold)
            _stateMachine.SetState(new Drink(_stateMachine));

        return base.OnUpdate();
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
