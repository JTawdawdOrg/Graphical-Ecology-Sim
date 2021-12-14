/*
Program: Idle.cs
Date Created: ‎18/10/‎2021
Description: State creatures enter whilst not hungry, thirsty, desiring to reproduce, or in danger
To do: Add some degree of movement/searching for predators whilst idle
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : State
{
	//Layers: 0 = default, 7 = prey, 8 = predator
	public int layer;
    public Idle(StateMachine stateMachine) : base(stateMachine)
    {
		layer = stateMachine.gameObject.layer;
    }

    public override IEnumerator OnStart()
    {
        _stateMachine.StartCoroutine(Execution());
        return base.OnStart();
    }

    public override IEnumerator OnUpdate()
    {
		//Check if the creature has any needs to make it no longer idle
        if (_stateMachine.hunger < _stateMachine.hungerThreshold 
            || _stateMachine.thirst < _stateMachine.thirstThreshold 
            || _stateMachine.reproductiveUrge > _stateMachine.reproductiveUrgeThreshhold)
            _stateMachine.StartCoroutine(OnExit());
		if (_stateMachine.hunger < _stateMachine.hungerThreshold){
            if(layer==8){//Predator
				_stateMachine.SetState(new Hunt(_stateMachine));
            }
			else if(layer==7){//Prey
				_stateMachine.SetState(new Feed(_stateMachine));
			}
		}
        else if (_stateMachine.thirst < _stateMachine.thirstThreshold)
            _stateMachine.SetState(new Drink(_stateMachine));
        else if (_stateMachine.reproductiveUrge > _stateMachine.reproductiveUrgeThreshhold)
            _stateMachine.SetState(new Reproduce(_stateMachine));
            
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
