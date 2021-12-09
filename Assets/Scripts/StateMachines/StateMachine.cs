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

    [SerializeField] public MatingCallEvent matingCallEvent;
    [SerializeField] private GameObject babyDeerPrefab;
    [SerializeField] private GameObject maleDeerPrefab;
    [SerializeField] private GameObject femaleDeerPrefab;

    [SerializeField] private float maturity = 0;

    public State _state { get; protected set; }

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

        if (reproductiveUrge < 100 && transform.name != "BabyDeerHandler(Clone)")
            reproductiveUrge += reproductiveUrgeIncrease * Time.deltaTime;

        if (hunger <= 0 || thirst <= 0)
            Destroy(this.gameObject);

        if (transform.name == "BabyDeerHandler(Clone)" && maturity < 100)
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
        Destroy(gameObject);
    }
    public void SpawnBaby()
    {
        Instantiate(babyDeerPrefab, transform.position, Quaternion.identity);
    }
    void Mature()
    {
        int rndm = Random.Range(1,3);
        if (rndm == 1)
            Instantiate(maleDeerPrefab, transform.position, Quaternion.identity);
        else
            Instantiate(femaleDeerPrefab, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
