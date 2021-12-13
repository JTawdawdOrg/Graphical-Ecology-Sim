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

    private bool mated = false;

    public Reproduce(StateMachine stateMachine) : base(stateMachine)
    {

    }

    public override IEnumerator OnStart()
    {
        //_stateMachine.detection.detectionMasks = LayerMask.GetMask("Prey");
        _stateMachine.detection.detectionMasks = LayerMask.GetMask(LayerMask.LayerToName(_stateMachine.gameObject.layer));
        _stateMachine.detection.enabled = true;
        _stateMachine.detection.action += SetTargetMate;
        _stateMachine.detection.detectionAngle = 180.0f;
        _stateMachine.matingCallEvent.Register(this);
        _stateMachine.StartCoroutine(Execution());

        mated = false;

        return base.OnStart();
    }

    void SetTargetMate(Detection detection, GameObject mate)
    {
        StateMachine stateMachine = mate.GetComponent<StateMachine>();
        if (!stateMachine)
            return;
        if (_stateMachine.isMale == stateMachine.isMale)
            return;
        if (mated)
            return;
        targetMate = mate;
        _stateMachine.StartCoroutine(Mate(mate));
    }

    IEnumerator Mate(GameObject mate)
    {
        PreyStateMachine preyStateMachine = mate.GetComponent<PreyStateMachine>();
        PredatorStateMachine predatorStateMachine = mate.GetComponent<PredatorStateMachine>();

        if (!preyStateMachine && !predatorStateMachine)
            yield break;
        else if (!preyStateMachine && predatorStateMachine)
        {
            if (predatorStateMachine._state.GetType() == typeof(Reproduce))
            {
                Reproduce reproduce = (Reproduce)predatorStateMachine._state;
                if (!_stateMachine.isMale)
                {
                    predatorStateMachine.SpawnBaby();
                }
                _stateMachine.StartCoroutine(OnExit());
                yield return new WaitForSeconds(5.0f);
                _stateMachine.reproductiveUrge = 0;
            }
        }
        else if (preyStateMachine && !predatorStateMachine)
        {
            if (preyStateMachine._state.GetType() == typeof(Reproduce))
            {
                Reproduce reproduce = (Reproduce)preyStateMachine._state;
                if (!_stateMachine.isMale)
                {
                    preyStateMachine.SpawnBaby();
                }
                _stateMachine.StartCoroutine(OnExit());
                mated = true;
                yield return new WaitForSeconds(5.0f);
                _stateMachine.reproductiveUrge = 0;
            }
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
            _stateMachine.matingCallEvent.MatingCall(_stateMachine.transform.position, _stateMachine.isMale);
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
        if (!_stateMachine)
            return;

        if (isMale == _stateMachine.isMale)
            return;

        if (currentTarget != Vector3.zero)
            if (Vector3.Distance(_stateMachine.transform.position, pos) > Vector3.Distance(_stateMachine.transform.position, currentTarget))
                return;

        if (responseCooldown <= 0)
        {
            responseCooldown = responseMaxCooldown;
            _stateMachine.matingCallEvent.MatingCall(_stateMachine.transform.position, _stateMachine.isMale);
        }

        _stateMachine.navMeshAgent.SetDestination(pos);
        currentTarget = pos;
    }

    public override IEnumerator OnExit()
    {
        _stateMachine.detection.detectionAngle = 90.0f;
        _stateMachine.detection.action -= SetTargetMate;
        _stateMachine.detection.enabled = false;
        _stateMachine.matingCallEvent.Deregister(this);
        return base.OnExit();
    }
}
