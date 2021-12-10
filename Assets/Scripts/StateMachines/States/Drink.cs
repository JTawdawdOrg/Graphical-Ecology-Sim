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
        _stateMachine.StartCoroutine(Execution());
        return base.OnStart();
    }

    public override IEnumerator OnUpdate()
    {
        if (_stateMachine.thirst > 100)
        {
            _stateMachine.StartCoroutine(OnExit());

            if (_stateMachine.hunger < _stateMachine.hungerThreshold)
                _stateMachine.SetState(new Feed(_stateMachine));
            else if (_stateMachine.reproductiveUrge > _stateMachine.reproductiveUrgeThreshhold)
                _stateMachine.SetState(new Reproduce(_stateMachine));
            else
                _stateMachine.SetState(new Idle(_stateMachine));
        }

        return base.OnUpdate();
    }

    public override IEnumerator Execution()
    {
        // find closest water position
        GameObject waterPositions = GameObject.Find("Water Positions");
        Transform[] children = waterPositions.GetComponentsInChildren<Transform>();
        Transform closestWaterPos = children[0];
        float distance = Vector3.Distance(_stateMachine.transform.position, children[0].position);
        foreach (Transform child in children)
        {
            float tempDistance = Vector3.Distance(_stateMachine.transform.position, child.position);
            if (tempDistance < distance)
            {
                closestWaterPos = child;
                distance = tempDistance;
            }
        }

        // get a point on the ground near water position
        float x;
        float z;
        float yOffset = 30;

        Ray ray;
        RaycastHit hit;
        while (true)
        {
            x = closestWaterPos.position.x + Random.Range(-20, 20);
            z = closestWaterPos.position.z + Random.Range(-20, 20);
            ray = new Ray(new Vector3(x, yOffset, z), Vector3.down);
            Physics.Raycast(ray, out hit);
            if (hit.transform && hit.transform.tag == "Ground")
                break;
        }

        // drink from closest water position
        _stateMachine.navMeshAgent.SetDestination(hit.point);
        yield return new WaitForSeconds(5f);
        _stateMachine.thirst += 100;
    }

    public override IEnumerator OnExit()
    {
        return base.OnExit();
    }
}