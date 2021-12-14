/*
Program: Feed.cs
Date Created: ‎18/10/‎2021
Description: State prey creatures enter whilst hungry to search for food to eat
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Feed : State
{
    private GameObject targetGrass;

    public Feed(StateMachine stateMachine) : base(stateMachine)
    {

    }

    public override IEnumerator OnStart()
    {
		//Looks for a nearby grass object within the creature's vision
        _stateMachine.detection.detectionMasks = LayerMask.GetMask("Grass");
        _stateMachine.detection.enabled = true;
        _stateMachine.detection.action += SetTargetGrass;
        _stateMachine.StartCoroutine(Execution());
        return base.OnStart();
    }
	
	//Sets new grass targets in the OnStart function
    void SetTargetGrass(Detection detection, GameObject grass)
    {
        if (!targetGrass)
        {
            targetGrass = grass;
        }
    }

    public override IEnumerator OnUpdate()
    {
		//If hunger is sated, switch to a different state based off of the creature's needs
        if (_stateMachine.hunger > 100)
        {
            _stateMachine.StartCoroutine(OnExit());

            if (_stateMachine.thirst < _stateMachine.thirstThreshold)
                _stateMachine.SetState(new Drink(_stateMachine));
            else if (_stateMachine.reproductiveUrge > _stateMachine.reproductiveUrgeThreshhold)
                _stateMachine.SetState(new Reproduce(_stateMachine));
            else
                _stateMachine.SetState(new Idle(_stateMachine));
        }

        return base.OnUpdate();
    }

    public override IEnumerator Execution()
    {
        while(true)
        {
            if (_stateMachine.hunger > 100)
            {
                break;
            }
                
            if (targetGrass)
            {
                // go to target
                _stateMachine.navMeshAgent.SetDestination(targetGrass.transform.position);
                yield return new WaitForSeconds(2f);
                _stateMachine.MyDestroy(targetGrass);
                _stateMachine.hunger += 40;
                targetGrass = null;
            }
            else
            {
				//Find a position on the ground
                float x = Random.Range(-10, 10);
                float z = Random.Range(-10, 10);
                Ray ray = new Ray(new Vector3(_stateMachine.transform.position.x + x, 30, _stateMachine.transform.position.z + z), Vector3.down);
                RaycastHit hit;
                Physics.Raycast(ray, out hit);

                if (hit.transform != null && hit.transform.tag == "Ground")
                {
                    if (!_stateMachine.navMeshAgent.isOnNavMesh)
                        break;

                    NavMeshPath navMeshPath = new NavMeshPath();
                    _stateMachine.navMeshAgent.CalculatePath(hit.point, navMeshPath);

                    if (navMeshPath.status == NavMeshPathStatus.PathPartial)
                        continue;

                    _stateMachine.navMeshAgent.SetDestination(hit.point);
                    yield return new WaitForSeconds(1.7f);
                }
            } 
        }
    }

    public override IEnumerator OnExit()
    {
		//Clear target
        _stateMachine.detection.action -= SetTargetGrass;
        _stateMachine.detection.enabled = false;
        return base.OnExit();
    }
}
