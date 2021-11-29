using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Feed : State
{
    private GameObject targetGrass;
    private PreyStateMachine _preyStateMachine;
    private Detection detection;

    public Feed(PreyStateMachine stateMachine) : base(stateMachine)
    {
        _preyStateMachine = stateMachine;
    }

    public override IEnumerator OnStart()
    {
        detection = _stateMachine.GetComponent<Detection>();
        detection.enabled = true;
        detection.action += SetTargetGrass;
        _stateMachine.StartCoroutine(Execution());
        return base.OnStart();
    }

    void SetTargetGrass(Detection detection, GameObject grass)
    {
        if (!targetGrass)
        {
            targetGrass = grass;

        }
        
    }

    public override IEnumerator Execution()
    {
        while(true)
        {
            if (_preyStateMachine.hunger > 100)
            {
                _stateMachine.StartCoroutine(OnExit());
                break;
            }
                

            if (targetGrass)
            {
                // go to target
                _stateMachine.GetComponent<NavMeshAgent>().SetDestination(targetGrass.transform.position);
                yield return new WaitForSeconds(2f);
                _stateMachine.MyDestroy(targetGrass);
                _preyStateMachine.hunger += 40;
                targetGrass = null;
            }
            else
            {
                float x = Random.Range(-10, 10);
                float z = Random.Range(-10, 10);
                Ray ray = new Ray(new Vector3(_stateMachine.transform.position.x + x, 30, _stateMachine.transform.position.z + z), Vector3.down);
                RaycastHit hit;
                Physics.Raycast(ray, out hit);

                if (hit.transform != null && hit.transform.tag == "Ground")
                {
                    _stateMachine.transform.GetComponent<NavMeshAgent>().SetDestination(hit.point);
                    yield return new WaitForSeconds(1.7f);
                }
            } 
        }
    }

    public override IEnumerator OnExit()
    {
        detection = _stateMachine.GetComponent<Detection>();
        detection.enabled = false;
        _stateMachine.SetState(new Idle(_stateMachine));
        return base.OnExit();
    }
}
