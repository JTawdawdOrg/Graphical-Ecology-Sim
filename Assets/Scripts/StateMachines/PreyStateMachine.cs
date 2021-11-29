using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreyStateMachine : StateMachine
{
    [SerializeField] public float hunger = 100;
    [SerializeField] private float hungerUsage = 1;

    void Start()
    {
        SetState(new Idle(this));
    }

    void Update()
    {
        
        hunger -= hungerUsage * Time.deltaTime;
        if (hunger < 50 && _state.GetType() != typeof(Feed))
        {
            SetState(new Feed(this));
        }
        if (hunger <= 0)
        {
            Destroy(this.gameObject);
        }
    }


    // Prey needs to idle
    // SetState(new Idle(this));

    // Prey needs to feed
    // SetState(new Feed(this));

    // Prey needs to drink
    // SetState(new Drink(this));

    // Prey needs to reproduce
    // SetState(new Reproduce(this));
}
