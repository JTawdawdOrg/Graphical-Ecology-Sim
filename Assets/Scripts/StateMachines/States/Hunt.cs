/*
Program: Hunt.cs
Date Created: ‎18/10/‎2021
Description: State predator creatures enter whilst hungry to search for food to eat
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Hunt : State
{
	private GameObject targetPrey;
	
    public Hunt(StateMachine stateMachine) : base(stateMachine)
	{
	
	}
	
	public override IEnumerator OnStart(){
		//Looks for a nearby prey creature within the creature's vision
		_stateMachine.detection.detectionMasks = LayerMask.GetMask("Prey");
        _stateMachine.detection.enabled = true;
		_stateMachine.detection.action += SetTargetPrey;
        _stateMachine.StartCoroutine(Execution());
        return base.OnStart();
    }
	
	//Sets new prey targets in the OnStart function
	void SetTargetPrey(Detection detection, GameObject prey){
		if (!targetPrey){
            targetPrey = prey;
        }
    }
	
    public override IEnumerator OnUpdate(){
        //If hunger is sated, switch to a different state based off of the creature's needs
		if (_stateMachine.hunger > 100){
            
			_stateMachine.StartCoroutine(OnExit());

            if (_stateMachine.thirst < 50)
                _stateMachine.SetState(new Drink(_stateMachine));
            else if (_stateMachine.reproductiveUrge > 99)
                _stateMachine.SetState(new Reproduce(_stateMachine));
            else
                _stateMachine.SetState(new Idle(_stateMachine));
        }
        return base.OnUpdate();
    }

    public override IEnumerator Execution(){
        while(true){
            if (_stateMachine.hunger > 100){
                break;
            }
            
			//If prey has been found, go to it
            if (targetPrey){
                _stateMachine.navMeshAgent.SetDestination(targetPrey.transform.position);
                yield return new WaitForSeconds(2f);
                _stateMachine.MyDestroy(targetPrey);
                _stateMachine.hunger += 80;
                targetPrey = null;
            }
			//Otherwise search around
            else{
                float x = Random.Range(-10, 10);
                float z = Random.Range(-10, 10);
                Ray ray = new Ray(new Vector3(_stateMachine.transform.position.x + x, 30, _stateMachine.transform.position.z + z), Vector3.down);
                RaycastHit hit;
                Physics.Raycast(ray, out hit);

                if (hit.transform != null && hit.transform.tag == "Ground"){
                    _stateMachine.navMeshAgent.SetDestination(hit.point);
                    yield return new WaitForSeconds(1.7f);
                }
            } 
        }
    }

    public override IEnumerator OnExit()
    {
		//Clear target
		_stateMachine.detection.enabled = false;
        _stateMachine.detection.action -= SetTargetPrey;
        return base.OnExit();
    }
}
