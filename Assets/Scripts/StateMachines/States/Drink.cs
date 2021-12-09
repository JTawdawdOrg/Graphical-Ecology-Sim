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

            if (_stateMachine.hunger < 50)
                _stateMachine.SetState(new Feed(_stateMachine));
            else if (_stateMachine.reproductiveUrge > 99)
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

        // drink from closest water position
        _stateMachine.navMeshAgent.SetDestination(closestWaterPos.position);
        yield return new WaitForSeconds(5f);
        _stateMachine.thirst += 100;
    }

    public override IEnumerator OnExit()
    {
        return base.OnExit();
    }
}