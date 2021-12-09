using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reproduce : State
{
    private Vector3 currentTarget;
    private GameObject targetMate;

    private float matingCallMaxCooldown = 5.0f;
    private float matingCallCooldown;
    private float responseMaxCooldown = 5.0f;
    private float responseCooldown;

    public Reproduce(StateMachine stateMachine) : base(stateMachine)
    {

    }

    public override IEnumerator OnStart()
    {
        _stateMachine.detection.detectionMasks = LayerMask.GetMask("Deer");
        _stateMachine.detection.enabled = true;
        _stateMachine.detection.action += SetTargetMate;
        _stateMachine.matingCallEvent.Register(this);
        _stateMachine.StartCoroutine(Execution());
        return base.OnStart();
    }

    void SetTargetMate(Detection detection, GameObject mate)
    {
        targetMate = mate;
        _stateMachine.StartCoroutine(Mate(mate));
    }

    IEnumerator Mate(GameObject mate)
    {
        PreyStateMachine preyStateMachine = mate.GetComponent<PreyStateMachine>();

        if (!preyStateMachine)
            yield break;

        if (preyStateMachine._state.GetType() == typeof(Reproduce))
        {
            Reproduce reproduce = (Reproduce)preyStateMachine._state;
            if (!IsMale())
            {
                _stateMachine.SpawnBaby();
            }
            _stateMachine.StartCoroutine(OnExit());
            yield return new WaitForSeconds(5.0f);
            _stateMachine.reproductiveUrge = 0;
        }
    }

    public override IEnumerator OnUpdate()
    {
        if (!_stateMachine.navMeshAgent.hasPath)
        {
            currentTarget = Vector3.zero;
        }

        if (matingCallCooldown >= 0)
            matingCallCooldown -= 1 * Time.deltaTime;

        if (responseCooldown >= 0)
            responseCooldown -= 1 * Time.deltaTime;
            

        if (matingCallCooldown <= 0)
        {
            _stateMachine.matingCallEvent.MatingCall(_stateMachine.transform.position, IsMale());
            matingCallCooldown = matingCallMaxCooldown;
        }

        if (_stateMachine.reproductiveUrge < 100)
        {
            _stateMachine.StartCoroutine(OnExit());

            if (_stateMachine.hunger < _stateMachine.hungerThreshold)
                _stateMachine.SetState(new Feed(_stateMachine));
            else if (_stateMachine.thirst < _stateMachine.thirstThreshold)
                _stateMachine.SetState(new Drink(_stateMachine));
            else
                _stateMachine.SetState(new Idle(_stateMachine));
        }

        return base.OnUpdate();
    }

    public override IEnumerator Execution()
    {
        return base.Execution();            
    }

    public void Response(Vector3 pos, bool isMale)
    {
        if (isMale == IsMale())
            return;

        if (currentTarget != Vector3.zero)
            if (Vector3.Distance(_stateMachine.transform.position, pos) > Vector3.Distance(_stateMachine.transform.position, currentTarget))
                return;

        if (responseCooldown <= 0)
        {
            responseCooldown = responseMaxCooldown;
            _stateMachine.matingCallEvent.MatingCall(_stateMachine.transform.position, IsMale());
        }

        _stateMachine.navMeshAgent.SetDestination(pos);
        currentTarget = pos;
    }

    bool IsMale()
    {
        if (!_stateMachine)
            return false;

        if (_stateMachine.transform.name == "StagHandler(Clone)" || _stateMachine.transform.name == "StagHandler")
            return true;
        return false;
    }

    public override IEnumerator OnExit()
    {
        _stateMachine.detection.action -= SetTargetMate;
        _stateMachine.detection.enabled = false;
        _stateMachine.matingCallEvent.Deregister(this);
        return base.OnExit();
    }
}
