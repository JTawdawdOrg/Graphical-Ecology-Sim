using System;
using UnityEngine;
using UnityEngine.AI;

namespace Behaviours
{
    public class Predator : MonoBehaviour
    {
        private String[] DNA;
        private Boolean isMale;
        private float FP = 20;
        private float WP = 20;
        private float Speed = 0.1f;
        private float Threshold = 10.0f;
        private PredatorStateMachine stateMachine;
        private float desireability;
        private float gestationPeriod;
        

        private float hunger, thirst = 0.0f;

        
        public float Fp
        {
            get => FP;
            set => FP = value;
        }

        public float Wp
        {
            get => WP;
            set => WP = value;
        }

        public float Speed1
        {
            get => Speed;
            set => Speed = value;
        }

        public void Start()
        {
            stateMachine = new PredatorStateMachine();
            stateMachine.SetState(new Idle(stateMachine));

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("enviroment"))
            {
                Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), other);
            }
            if (other.gameObject.CompareTag("predator"))
            {
                Debug.Log("collided with predator " , other);
            }
            if (other.gameObject.CompareTag("prey"))
            {
                Debug.Log("collided with prey " , other);
            }
        }
        
        

        private void moveRandomly()
        {
            Vector3 step = new Vector3(UnityEngine.Random.Range(-10, 10), UnityEngine.Random.Range(0, 0), UnityEngine.Random.Range(-10, 10));
            transform.position =  transform.position + step * Speed;
            //agent.SetDestination();
            Debug.Log(stateMachine.GetState());


        }

        private void ExhaustFoodWater()
        {
            //HP = HP - Time.deltaTime;
            //WP = WP - Time.deltaTime;
            
            hunger += Time.deltaTime * 1 / FP;
            thirst += Time.deltaTime * 1 / WP;
        }

        private void CheckState()
        {
            if ( hunger > 1)
            {
                Debug.Log("died from hunger");
                Destroy(gameObject);
            }
            if ( thirst > 1)
            {
                Debug.Log("died from hunger");
                Destroy(gameObject);
            }
        }

        protected virtual void Update()
        {
            moveRandomly();
            // CheckState();
        }
    }
}