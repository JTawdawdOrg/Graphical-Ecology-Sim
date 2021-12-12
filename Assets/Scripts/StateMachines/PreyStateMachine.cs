using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreyStateMachine : StateMachine
{
    [SerializeField] protected GameObject babyDeerPrefab;
    [SerializeField] protected GameObject maleDeerPrefab;
    [SerializeField] protected GameObject femaleDeerPrefab;

    protected override void Start()
    {
        SetState(new Idle(this));
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        _state.OnUpdate();
    }

    public override void SpawnBaby()
    {
        GameObject temp = Instantiate(babyDeerPrefab, transform.position, Quaternion.identity);
        temp.GetComponent<StateMachine>().isBaby = true;
    }

    public override void Mature()
    {
        int rndm = Random.Range(1, 3);
        if (rndm == 1)
            Instantiate(maleDeerPrefab, transform.position, Quaternion.identity);
        else
            Instantiate(femaleDeerPrefab, transform.position, Quaternion.identity);

        MyDestroy(this.gameObject);
    }
}
