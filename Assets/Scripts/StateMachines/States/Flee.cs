/*
Program: Flee.cs
Date Created: ‎18/10/‎2021
Description: State prey creatures enter upon seeing a nearby predator
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;

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
		//Exit if the prey has escaped the predator
        if (!_stateMachine.predator || Vector3.Distance(_stateMachine.transform.position, _stateMachine.predator.transform.position)>20f)
        {
            _stateMachine.StartCoroutine(OnExit());
			_stateMachine.SetState(new Idle(_stateMachine));
        }
        else if (_stateMachine.predator)
        {
			//Move in the opposite direction to the predator
            Vector3 currPos = _stateMachine.transform.position;
            Vector3 directionAway = currPos - _stateMachine.predator.transform.position;

            Vector3 goTo = currPos + (directionAway * 1);

			//Make sure the position is on the ground
            Ray ray = new Ray(new Vector3(goTo.x, 30, goTo.z), Vector3.down);
            RaycastHit hit;
            Physics.Raycast(ray, out hit);

            if (hit.transform && hit.transform.tag == "Ground")
                _stateMachine.navMeshAgent.SetDestination(hit.point);
        }
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
