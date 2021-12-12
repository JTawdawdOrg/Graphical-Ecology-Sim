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
		_stateMachine.detection.detectionMasks = LayerMask.GetMask("Prey");
        _stateMachine.detection.enabled = true;
		_stateMachine.detection.action += SetTargetPrey;
        _stateMachine.StartCoroutine(Execution());
        return base.OnStart();
    }
	
	void SetTargetPrey(Detection detection, GameObject prey){
        Debug.Log("Seeing target prey");
		if (!targetPrey){
            targetPrey = prey;
        }
    }
	
    public override IEnumerator OnUpdate(){
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
            
            if (targetPrey){//If prey has been found, go to it
                _stateMachine.navMeshAgent.SetDestination(targetPrey.transform.position);
                yield return new WaitForSeconds(2f);
                _stateMachine.MyDestroy(targetPrey);
                _stateMachine.hunger += 80;
                targetPrey = null;
            }
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
		_stateMachine.detection.enabled = false;
        _stateMachine.detection.action -= SetTargetPrey;
        return base.OnExit();
    }
}
