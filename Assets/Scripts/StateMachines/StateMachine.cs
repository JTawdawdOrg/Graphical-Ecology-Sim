using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class StateMachine : MonoBehaviour
{
    private Tracker tracker;

    [SerializeField] public float hunger = 100;
    [SerializeField] public float thirst = 100;
    [SerializeField] public float reproductiveUrge = 0;

    [SerializeField] public float hungerUsage = 1;
    [SerializeField] public float thirstUsage = 1;
    [SerializeField] public float reproductiveUrgeIncrease = 1;

    [SerializeField] public float hungerThreshold = 50;
    [SerializeField] public float thirstThreshold= 50;
    [SerializeField] public float reproductiveUrgeThreshhold = 99;

    [SerializeField] public MatingCallEvent matingCallEvent;
    
    [SerializeField] public GameObject predator;
	  [SerializeField] public float speed;
    
    public bool isMale;

    [SerializeField] private float maturity = 0;
    public bool isBaby;

    public State _state { get; protected set; }

    public NavMeshAgent navMeshAgent;
    public Detection detection;

    protected virtual void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        detection = GetComponent<Detection>();
        tracker = GameObject.Find("Tracker").GetComponent<Tracker>();
    }

    protected virtual void Update()
    {
        hunger -= hungerUsage * Time.deltaTime;
        thirst -= thirstUsage * Time.deltaTime;

        if (reproductiveUrge < 100 && !isBaby)
            reproductiveUrge += reproductiveUrgeIncrease * Time.deltaTime;

        if (hunger <= 0 || thirst <= 0)
            MyDestroy(this.gameObject);

        if (isBaby && maturity < 100)
            maturity += 1 * Time.deltaTime;

        if (maturity >= 100)
            Mature();
    }

    public void SetState(State state)
    {
        _state = state;
        StartCoroutine(_state.OnStart());
    }

    public void MyDestroy(GameObject gameObject)
    {
        if (!gameObject)
            return;

        StateMachine stateMachine = gameObject.GetComponent<StateMachine>();
        if (stateMachine)
            stateMachine._state.OnExit();
        Destroy(gameObject);
    }
    public virtual void SpawnBaby()
    {
        
    }
    public virtual void Mature()
    {

    }
}