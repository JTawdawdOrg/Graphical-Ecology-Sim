using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Flee : State
{
	public Flee(StateMachine stateMachine) : base(stateMachine)
    {

    }

    public override IEnumerator OnStart()
    {
		_stateMachine.StartCoroutine(Execution());
        return base.OnStart();
    }

    public override IEnumerator OnUpdate()
    {
        if (_stateMachine.predator==null || Vector3.Distance(_stateMachine.transform.position, _stateMachine.predator.transform.position)>20f)
        {
            _stateMachine.StartCoroutine(OnExit());
			_stateMachine.SetState(new Idle(_stateMachine));
        }

        return base.OnUpdate();
    }

    public override IEnumerator Execution()
    {
		Vector3 directionAway = _stateMachine.transform.position-_stateMachine.predator.transform.position;
		Vector3 targetDestination = new Vector3(_stateMachine.transform.position.x+(Math.Sign(directionAway.x)*_stateMachine.speed),_stateMachine.transform.position.y+(Math.Sign(directionAway.y)*_stateMachine.speed),0);
		_stateMachine.navMeshAgent.SetDestination(targetDestination);
        yield return new WaitForSeconds(1f);
		
    }

    public override IEnumerator OnExit()
    {
        return base.OnExit();
    }
	
}
