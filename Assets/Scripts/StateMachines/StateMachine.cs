using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class StateMachine : MonoBehaviour
{
    [SerializeField] public float hunger = 100;
    [SerializeField] public float thirst = 100;
    [SerializeField] public float reproductiveUrge = 0;

    [SerializeField] public float hungerUsage = 1;
    [SerializeField] public float thirstUsage = 1;
    [SerializeField] public float reproductiveUrgeIncrease = 1;

    [SerializeField] public float hungerThreshold = 50;
    [SerializeField] public float thirstThreshold= 50;
    [SerializeField] public float reproductiveUrgeThreshhold = 99;

    protected State _state;

    public NavMeshAgent navMeshAgent;
    public Detection detection;

    protected virtual void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        detection = GetComponent<Detection>();

    }

    protected virtual void Update()
    {
        hunger -= hungerUsage * Time.deltaTime;
        thirst -= thirstUsage * Time.deltaTime;

        if (reproductiveUrge < 100)
            reproductiveUrge += reproductiveUrgeIncrease * Time.deltaTime;

        if (hunger <= 0 || thirst <= 0)
            Destroy(this.gameObject);
    }

    public void SetState(State state)
    {
        _state = state;
        StartCoroutine(_state.OnStart());
    }

    public void MyDestroy(GameObject gameObject)
    {
        Destroy(gameObject);
    }

}
